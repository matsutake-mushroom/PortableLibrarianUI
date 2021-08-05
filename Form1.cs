using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;

namespace ローカル司書さん
{
    public partial class Form1 : Form
    {
        private Process process;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            listBox1.Items.AddRange(fileName);
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void button_clearall_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            while(listBox1.SelectedItems.Count > 0)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void button_refer_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = "csv files(*.csv)|*.csv|excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";

                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    listBox1.Items.Add(ofd.FileName);
                }
            }
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo finialize web app
            this.Close();
        }

        private void 情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ローカル司書（β）　version: 0.0.1\n\n(c) HIOKI E.E. Corporation\nt.matsutake@hioki.co.jp", "アプリケーション情報", MessageBoxButtons.OK);
        }

        private void button_exec_Click(object sender, EventArgs e)
        {
            int col_q = -1;
            int col_a = -1;
            int port = -1;
            string filenames = "";
            //value validation
            try
            {
                col_q = Convert.ToInt32(textBox_col_question.Text);
                col_a = Convert.ToInt32(textBox_col_answer.Text);
                port = Convert.ToInt32(textBox_port.Text);
                filenames = string.Join(" ", listBox1.Items.OfType<string>().ToArray());

                if (col_q == col_a)
                {
                    throw new Exception("同じ列を参照しています。");
                }
                if (port < 0 || port > 65535)
                {
                    throw new Exception("不正なポート番号です。");
                }
                foreach(var endpoint in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners())
                {
                    if(endpoint.Port == port)
                    {
                        //使用中
                        throw new Exception("ポートは既に使用されています。違う番号を指定してください。");
                    }
                }

                if(listBox1.Items.Count <= 0)
                {
                    throw new Exception("CSVファイルを登録してください。");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
            //morph button exec -> stop
            progressBar1.Maximum = 100;
            progressBar1.Value = 5;
            setButtonsExecuting(true);

            listBox2.Items.Add("サーバーアプリケーションを起動しています…");

            process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;//リダイレクトするため
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;

            var filename = textBox_execPath.Text;
            var arguments = $"{col_q} {col_a} {port} {filenames}";

            process.StartInfo.FileName = filename;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(filename);
            process.StartInfo.Arguments = arguments;

            process.OutputDataReceived += (s, args) => Display(args.Data);
            process.ErrorDataReceived += (s, args) => Display(args.Data);
            try
            {
                var isStarted = process.Start();

                if (isStarted)
                {
                    progressBar1.Value = 5;
                    listBox2.Items.Add("起動に成功しました。");
                    progressBar1.Value = 50;
                    listBox2.Items.Add("データをメモリにロードしています。１分程度お待ちください。");

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                }
                else
                {
                    progressBar1.Value = 0;
                    listBox2.Items.Add("起動に失敗しました。");
                    setButtonsExecuting(false);
                    return;
                }
            }
            catch(Exception ee)
            {
                listBox2.Items.Add("エラーが発生しました。");
                setButtonsExecuting(false);
                MessageBox.Show(ee.Message);
            }
        }

        private void Display(string output)
        {
            try
            {
                Invoke((MethodInvoker)(() => { listBox2.Items.Add(output); }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\nDuring displaying \"" + output + "\"");
            }

        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            if (true)//processの関連付けの判定をしたい
            {
                listBox2.Items.Add("サーバーアプリケーションを終了しています…(1分程度かかる場合があります)");
                Task.Delay(100);
                setButtonsExecuting(false);

                if (!process.HasExited)
                {
                    Console.WriteLine("Killing process: " + process.Id);
                    KillProcessIncludingChildren(process.Id);
                    process.WaitForExit(10000);
                }
                process.Close();
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                listBox2.Items.Add("終了しました。");
            }
        }
        private static bool KillProcessIncludingChildren(int pid)
        {
            if (pid == 0)
            {
                return false;
            }

            bool ret = true;
            var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            foreach (var obj in searcher.Get())
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

        private void setButtonsExecuting(bool on)
        {
            if (on)
            {
                button_exec.Text = "実行中";
                button_exec.Enabled = false;
                button_stop.Enabled = true;
            }
            else
            {
                button_exec.Text = "実行";
                button_exec.Enabled = true;
                button_stop.Enabled = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!process.HasExited)
                {
                    Console.WriteLine("Killing process: " + process.Id);
                    KillProcessIncludingChildren(process.Id);
                    process.WaitForExit(10000);
                }
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);//ない場合は何もしない
            }
        }
    }
}
