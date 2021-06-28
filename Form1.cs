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

namespace ローカル司書さん
{
    public partial class Form1 : Form
    {
        private Process process;
        private ProcessStartInfo startinfo;

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
                if(port < 0 || port > 65535)
                {
                    throw new Exception("不正なポート番号です。");
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
            //morph button exec -> stop
            button_exec.Text = "実行中";
            button_exec.Enabled = false;
            button_stop.Enabled = true;

            listBox2.Items.Add("サーバーアプリケーションを起動しています…");

            //create thread (stdout redirect and show)
            startinfo = new ProcessStartInfo(@"\\fileserver18\users\t.matsutake\private\workspace\pyinst_test\dist\hello_flask\hello_flask.exe", $"{col_q} {col_a} {port} {filenames}")
            {
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            listBox2.Items.Add($"{startinfo.FileName} {startinfo.Arguments}");


            using (process = new Process())
            using (var ctoken = new CancellationTokenSource())
            {
                process.StartInfo = startinfo;
                process.EnableRaisingEvents = true;
                
                // コールバックの設定
                process.Exited += (sdr, ev) =>
                {
                    this.Invoke(new Action(() => { listBox2.Items.Add("プロセスが終了しました。"); }));
                    // プロセスが終了すると呼ばれる
                    ctoken.Cancel();
                };

                // プロセスの開始
                var result = process.Start();
                if (!result)
                {
                    MessageBox.Show("起動失敗？");
                }

                progressBar1.Maximum = 100;
                progressBar1.Value = 10;

                Task.Run(() =>
                {
                    while (true)
                    {
                        var l = process.StandardOutput.ReadLine();
                        if (l == null)
                        {
                            break;
                        }
                        Invoke(new Action(() => { listBox2.Items.Add(l); }));
                        Invoke(new Action(() => { progressBar1.Value += 1; }));
                        Console.WriteLine($"stdout = {l}");
                    }
                });
                Task.Run(() =>
                {
                    ctoken.Token.WaitHandle.WaitOne();
                    process.WaitForExit();
                });
                Task.Run(() =>
                {
                    Console.WriteLine("Task3 started");
                    var timer = new System.Timers.Timer();
                    timer.Elapsed += new System.Timers.ElapsedEventHandler((obj, args) =>
                    {
                        Console.WriteLine("Tick");
                        Invoke(new Action(() => { progressBar1.Value += 1; }));
                        if(progressBar1.Value == 80)
                        {
                            timer.Stop();
                        }
                    });
                    timer.Interval = 1000;
                    timer.Enabled = true;
                    timer.Start();
                    Console.WriteLine("timer started");

                });

            }




        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            button_exec.Text = "実行";
            button_exec.Enabled = true;
            button_stop.Enabled = false;
            process.Close();
        }
    }
}
