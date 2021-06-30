using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Management;

namespace ローカル司書さん
{
    public class Subprocess
    {
        public int statusCode = 0;
        private Process process;
        private ProcessStartInfo startinfo;
        private MemoryStream stream;

        private Output standardOutput;
        private Output standardError;

        public Subprocess(string filename, string arguments)
        {
            stream = new MemoryStream();
            //create thread (stdout redirect and show)
            process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;//リダイレクトするため
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;

            process.StartInfo.FileName = filename;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(filename);
            process.StartInfo.Arguments = arguments;

            process.Exited += new EventHandler((obj, trg) =>
            {
                statusCode = -1;
            });
        }
        public int processId()
        {
            return process.Id;
        }

        public bool Start()
        {
            var result = false;
            try
            {
                result = process.Start();
            }
            catch
            {
                throw;
            }

            standardOutput = new Output(process.StandardOutput);
            standardError = new Output(process.StandardError);
            standardOutput.start(stream);
            standardError.start(stream);

            return result;
        }

        public ref MemoryStream outputBuffer()
        {
            return ref stream;
        }

        public void Close()
        {
            try
            {
                if (!process.HasExited)
                {
                    Console.WriteLine("Killing process: " + process.Id);
                    KillProcessIncludingChildren(process.Id);
                    process.WaitForExit(10000);
                }
            }
            catch
            {
                ;//Closeが成功していた場合
            }
            standardOutput.Close();
            standardError.Close();

            //process.Close();
        }

        private static bool KillProcessIncludingChildren(int pid)
        {
            if (pid == 0)
            {
                return false;
            }

            bool ret = true;
            var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            foreach(var obj in searcher.Get())
            {
                ret &= KillProcessIncludingChildren(Convert.ToInt32(obj["ProcessId"]));
            }

            try
            {
                var p = Process.GetProcessById(pid);
                p.Kill();
            }
            catch (ArgumentException)
            {
                ;//already exited
            }
            catch
            {
                return false;
            }

            return ret;
        }
    }

    public class Output
    {
        private StreamReader reader;
        private StreamWriter writer;
        private Task readTask;
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        private bool isIdle;
        public Output(StreamReader r)
        {
            reader = r;
            isIdle = false;
            readTask = new Task(this.readtask);
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

        }

        public void start(MemoryStream ms)
        {
            writer = new StreamWriter(ms);
            readTask.Start();
        }

        private void readtask()
        {
            try
            {
                while (true)
                {
                    int current;

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    writer.AutoFlush = false;
                    while ((current = reader.Read()) >= 0)
                    {
                        isIdle = false;
                        writer.Write((char)current);
                        Console.Write((char)current);


                        if ((char)current == '\n')
                        {
                            writer.Flush();
                        }

                        if (token.IsCancellationRequested || reader == null)
                        {
                            return;
                        }

                    }
                    isIdle = true;
                }
            }catch(Exception ex)
            {
                ;//
            }
        }

        public void Close()
        {
            if (!readTask.IsCompleted)
            {
                var counter = 0;
                tokenSource.Cancel();
                while (true)
                {
                    if (readTask.IsCanceled || readTask.IsCompleted || readTask.IsFaulted　)
                    {
                        readTask.Dispose();
                        break;
                    }
                    if (!tokenSource.IsCancellationRequested)
                    {
                        tokenSource.Cancel();
                    }
                    counter++;
                    if(counter > 1000000)
                    {
                        break;
                    }
                }
            }
            else{
                readTask.Dispose();
            }

            try
            {
                writer.Close();
            }
            catch
            {
                ;//もう閉じている場合
            }
            try
            {
                reader.Close();
            }
            catch
            {
                ;//もう閉じている場合
            }
        }
        public bool IsIdle()
        {
            return isIdle;
        }
    }
}
