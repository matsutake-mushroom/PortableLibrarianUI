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

namespace ローカル司書さん
{
    public partial class Form1 : Form
    {
        private Subprocess process;
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

            //process = new Subprocess(@"\\fileserver18\users\t.matsutake\private\workspace\pyinst_test\dist\hello_flask\hello_flask.exe", $"{col_q} {col_a} {port} {filenames}");
            process = new Subprocess("python", @"C:\Users\tm314\Workspace\debug.py");

            //start process
            var isStarted = false;
            try
            {
                isStarted = process.Start();
            }
            catch(Exception ex)
            {
                listBox2.Items.Add(ex.Message);
            }

            //process started?
            if (isStarted)
            {
                progressBar1.Value = 5;
                listBox2.Items.Add("起動に成功しました。");
            }
            else
            {
                progressBar1.Value = 0;
                listBox2.Items.Add("起動に失敗しました。");
                setButtonsExecuting(false);
                return;
            }
            //Taskでバッファを書き込む
            Task.Run(() =>
            {
                ref MemoryStream ms = ref process.outputBuffer();
                byte[] buffer = new byte[256];
                string result = System.Text.Encoding.UTF8.GetString(buffer);
                using (var reader = new StreamReader(ms)) {
                    while (true)
                    {
                        try
                        {
                            if (!ms.CanRead)
                            {
                                return;
                            }
                            var line = reader.ReadLine();
                            
                            if (line != null)
                            {
                                Console.WriteLine("READTASK: " + line);
                                Invoke(new Action(() => { listBox2.Items.Add(line); }));
                            }
                           
                        }
                        catch(Exception ex)
                        {
                            Invoke(new Action(() => { listBox2.Items.Add(ex.Message); }));
                            return;
                        }
                    }
                }
            });


            //ロード中、無意味にprogressバーを増やす
            Task.Run(() => {
                    var timer = new System.Timers.Timer();
                    timer.Elapsed += new System.Timers.ElapsedEventHandler((obj, args) =>
                    {
                        Invoke(new Action(() => { progressBar1.Value += 1; }));
                        if (progressBar1.Value <= 80)
                        {
                            timer.Stop();
                        }
                    });
                    timer.Interval = 1000;
                    timer.Enabled = true;
                    timer.Start();
            });
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add("サーバーアプリケーションを終了しています…");
            setButtonsExecuting(false);
            process.Close();
            listBox2.Items.Add("終了しました。");
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
    }
}
