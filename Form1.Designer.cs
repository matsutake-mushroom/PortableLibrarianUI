namespace ローカル司書さん
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_stop = new System.Windows.Forms.Button();
            this.textBox_execPath = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_col_answer = new System.Windows.Forms.TextBox();
            this.textBox_col_question = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_exec = new System.Windows.Forms.Button();
            this.button_refer = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_clearall = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.情報ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(667, 76);
            this.listBox1.TabIndex = 0;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_stop);
            this.groupBox1.Controls.Add(this.textBox_execPath);
            this.groupBox1.Controls.Add(this.textBox_port);
            this.groupBox1.Controls.Add(this.textBox_col_answer);
            this.groupBox1.Controls.Add(this.textBox_col_question);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_exec);
            this.groupBox1.Controls.Add(this.button_refer);
            this.groupBox1.Controls.Add(this.button_clear);
            this.groupBox1.Controls.Add(this.button_clearall);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 250);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "データ設定";
            // 
            // button_stop
            // 
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(544, 201);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(102, 40);
            this.button_stop.TabIndex = 4;
            this.button_stop.Text = "停止";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // textBox_execPath
            // 
            this.textBox_execPath.Location = new System.Drawing.Point(159, 212);
            this.textBox_execPath.Name = "textBox_execPath";
            this.textBox_execPath.Size = new System.Drawing.Size(379, 19);
            this.textBox_execPath.TabIndex = 3;
            this.textBox_execPath.Text = ".\\App\\main.exe";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(136, 185);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(63, 19);
            this.textBox_port.TabIndex = 3;
            this.textBox_port.Text = "8080";
            this.textBox_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_col_answer
            // 
            this.textBox_col_answer.Location = new System.Drawing.Point(136, 160);
            this.textBox_col_answer.Name = "textBox_col_answer";
            this.textBox_col_answer.Size = new System.Drawing.Size(63, 19);
            this.textBox_col_answer.TabIndex = 3;
            this.textBox_col_answer.Text = "2";
            this.textBox_col_answer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_col_question
            // 
            this.textBox_col_question.Location = new System.Drawing.Point(136, 134);
            this.textBox_col_question.Name = "textBox_col_question";
            this.textBox_col_question.Size = new System.Drawing.Size(63, 19);
            this.textBox_col_question.TabIndex = 3;
            this.textBox_col_question.Text = "1";
            this.textBox_col_question.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "アプリを公開するポート";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "ウェブアプリ(main.exe)へのパス";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "回答のデータ列";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "番ポート";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "列目";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "列目 (最左端が1列目)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "質問のデータ列";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "使用するファイル（csv）";
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(652, 201);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(102, 40);
            this.button_exec.TabIndex = 1;
            this.button_exec.Text = "実行";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // button_refer
            // 
            this.button_refer.Location = new System.Drawing.Point(679, 96);
            this.button_refer.Name = "button_refer";
            this.button_refer.Size = new System.Drawing.Size(75, 23);
            this.button_refer.TabIndex = 1;
            this.button_refer.Text = "参照";
            this.button_refer.UseVisualStyleBackColor = true;
            this.button_refer.Click += new System.EventHandler(this.button_refer_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(679, 67);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 1;
            this.button_clear.Text = "クリア";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_clearall
            // 
            this.button_clearall.Location = new System.Drawing.Point(679, 43);
            this.button_clearall.Name = "button_clearall";
            this.button_clearall.Size = new System.Drawing.Size(75, 23);
            this.button_clearall.TabIndex = 1;
            this.button_clearall.Text = "全クリア";
            this.button_clearall.UseVisualStyleBackColor = true;
            this.button_clearall.Click += new System.EventHandler(this.button_clearall_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.情報ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了ToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ファイルToolStripMenuItem.Text = "プログラム";
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // 情報ToolStripMenuItem
            // 
            this.情報ToolStripMenuItem.Name = "情報ToolStripMenuItem";
            this.情報ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.情報ToolStripMenuItem.Text = "情報";
            this.情報ToolStripMenuItem.Click += new System.EventHandler(this.情報ToolStripMenuItem_Click);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.Black;
            this.listBox2.ForeColor = System.Drawing.Color.White;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(12, 356);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(760, 196);
            this.listBox2.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 341);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "実行ログ";
            // 
            // progressBar1
            // 
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar1.Location = new System.Drawing.Point(12, 289);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(760, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label8);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ローカル司書（β）";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_col_answer;
        private System.Windows.Forms.TextBox textBox_col_question;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_refer;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_clearall;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 情報ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_exec;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.TextBox textBox_execPath;
        private System.Windows.Forms.Label label9;
    }
}

