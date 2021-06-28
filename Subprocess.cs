using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ローカル司書さん
{
    public class Subprocess
    {
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
            process.StartInfo.Arguments = arguments;
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
            standardOutput.Close();
            standardError.Close();

            process.Close();
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
                    
                    
                    if((char)current == '\n')
                    {
                        writer.Flush();
                    }
                    
                }
                isIdle = true;
            }
        }

        public void Close()
        {
            if (!readTask.IsCompleted)
            {
                tokenSource.Cancel();
                while (true){
                    if (readTask.IsCanceled || readTask.IsCompleted)
                    {
                        readTask.Dispose();
                        break;
                    }
                }
            }
            else{
                readTask.Dispose();
            }
            writer.Close();
            reader.Close();     
        }
        public bool IsIdle()
        {
            return isIdle;
        }
    }
}
