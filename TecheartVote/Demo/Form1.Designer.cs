namespace Demo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SecretText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clinetCanSeeAnswer = new System.Windows.Forms.CheckBox();
            this.clientCanChangeDate = new System.Windows.Forms.CheckBox();
            this.clientCanErase = new System.Windows.Forms.CheckBox();
            this.clientCanChangeChannel = new System.Windows.Forms.CheckBox();
            this.clientCanWriteABC = new System.Windows.Forms.CheckBox();
            this.clientCanWriteNumber = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.clientCanSeeSolution = new System.Windows.Forms.CheckBox();
            this.eraseClientMemory = new System.Windows.Forms.CheckBox();
            this.clientCanAnswer = new System.Windows.Forms.CheckBox();
            this.clientCanClearSoon = new System.Windows.Forms.CheckBox();
            this.persistenceConfiguration = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TitleEditor = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.listViewMonitor = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.listSorceMonitor = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.channel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.frequency = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "投票器名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.SecretText);
            this.groupBox2.Location = new System.Drawing.Point(14, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 173);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "密码表";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "每一个密码一个换行";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 144);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "设置密码表（第一步）";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SecretText
            // 
            this.SecretText.Location = new System.Drawing.Point(5, 32);
            this.SecretText.Multiline = true;
            this.SecretText.Name = "SecretText";
            this.SecretText.Size = new System.Drawing.Size(186, 106);
            this.SecretText.TabIndex = 0;
            this.SecretText.Text = "1\r\n2\r\n3\r\n4";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clinetCanSeeAnswer);
            this.groupBox3.Controls.Add(this.clientCanChangeDate);
            this.groupBox3.Controls.Add(this.clientCanErase);
            this.groupBox3.Controls.Add(this.clientCanChangeChannel);
            this.groupBox3.Controls.Add(this.clientCanWriteABC);
            this.groupBox3.Controls.Add(this.clientCanWriteNumber);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.clientCanSeeSolution);
            this.groupBox3.Controls.Add(this.eraseClientMemory);
            this.groupBox3.Controls.Add(this.clientCanAnswer);
            this.groupBox3.Controls.Add(this.clientCanClearSoon);
            this.groupBox3.Controls.Add(this.persistenceConfiguration);
            this.groupBox3.Location = new System.Drawing.Point(14, 331);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 192);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "配置修改";
            // 
            // clinetCanSeeAnswer
            // 
            this.clinetCanSeeAnswer.AutoSize = true;
            this.clinetCanSeeAnswer.Checked = true;
            this.clinetCanSeeAnswer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clinetCanSeeAnswer.Location = new System.Drawing.Point(85, 132);
            this.clinetCanSeeAnswer.Name = "clinetCanSeeAnswer";
            this.clinetCanSeeAnswer.Size = new System.Drawing.Size(96, 16);
            this.clinetCanSeeAnswer.TabIndex = 11;
            this.clinetCanSeeAnswer.Text = "允许查看答案";
            this.clinetCanSeeAnswer.UseVisualStyleBackColor = true;
            // 
            // clientCanChangeDate
            // 
            this.clientCanChangeDate.AutoSize = true;
            this.clientCanChangeDate.Checked = true;
            this.clientCanChangeDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanChangeDate.Location = new System.Drawing.Point(85, 109);
            this.clientCanChangeDate.Name = "clientCanChangeDate";
            this.clientCanChangeDate.Size = new System.Drawing.Size(96, 16);
            this.clientCanChangeDate.TabIndex = 10;
            this.clientCanChangeDate.Text = "允许修改时间";
            this.clientCanChangeDate.UseVisualStyleBackColor = true;
            // 
            // clientCanErase
            // 
            this.clientCanErase.AutoSize = true;
            this.clientCanErase.Checked = true;
            this.clientCanErase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanErase.Location = new System.Drawing.Point(85, 87);
            this.clientCanErase.Name = "clientCanErase";
            this.clientCanErase.Size = new System.Drawing.Size(96, 16);
            this.clientCanErase.TabIndex = 9;
            this.clientCanErase.Text = "允许擦除内存";
            this.clientCanErase.UseVisualStyleBackColor = true;
            // 
            // clientCanChangeChannel
            // 
            this.clientCanChangeChannel.AutoSize = true;
            this.clientCanChangeChannel.Checked = true;
            this.clientCanChangeChannel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanChangeChannel.Location = new System.Drawing.Point(85, 65);
            this.clientCanChangeChannel.Name = "clientCanChangeChannel";
            this.clientCanChangeChannel.Size = new System.Drawing.Size(96, 16);
            this.clientCanChangeChannel.TabIndex = 8;
            this.clientCanChangeChannel.Text = "允许修改信道";
            this.clientCanChangeChannel.UseVisualStyleBackColor = true;
            // 
            // clientCanWriteABC
            // 
            this.clientCanWriteABC.AutoSize = true;
            this.clientCanWriteABC.Checked = true;
            this.clientCanWriteABC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanWriteABC.Location = new System.Drawing.Point(85, 43);
            this.clientCanWriteABC.Name = "clientCanWriteABC";
            this.clientCanWriteABC.Size = new System.Drawing.Size(96, 16);
            this.clientCanWriteABC.TabIndex = 7;
            this.clientCanWriteABC.Text = "允许输入字母";
            this.clientCanWriteABC.UseVisualStyleBackColor = true;
            // 
            // clientCanWriteNumber
            // 
            this.clientCanWriteNumber.AutoSize = true;
            this.clientCanWriteNumber.Checked = true;
            this.clientCanWriteNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanWriteNumber.Location = new System.Drawing.Point(85, 22);
            this.clientCanWriteNumber.Name = "clientCanWriteNumber";
            this.clientCanWriteNumber.Size = new System.Drawing.Size(96, 16);
            this.clientCanWriteNumber.TabIndex = 6;
            this.clientCanWriteNumber.Text = "允许输入数字";
            this.clientCanWriteNumber.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(193, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "设置动态配置（第三步）";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // clientCanSeeSolution
            // 
            this.clientCanSeeSolution.AutoSize = true;
            this.clientCanSeeSolution.Checked = true;
            this.clientCanSeeSolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanSeeSolution.Location = new System.Drawing.Point(6, 110);
            this.clientCanSeeSolution.Name = "clientCanSeeSolution";
            this.clientCanSeeSolution.Size = new System.Drawing.Size(72, 16);
            this.clientCanSeeSolution.TabIndex = 4;
            this.clientCanSeeSolution.Text = "查看答案";
            this.clientCanSeeSolution.UseVisualStyleBackColor = true;
            // 
            // eraseClientMemory
            // 
            this.eraseClientMemory.AutoSize = true;
            this.eraseClientMemory.Checked = true;
            this.eraseClientMemory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eraseClientMemory.Location = new System.Drawing.Point(6, 88);
            this.eraseClientMemory.Name = "eraseClientMemory";
            this.eraseClientMemory.Size = new System.Drawing.Size(72, 16);
            this.eraseClientMemory.TabIndex = 3;
            this.eraseClientMemory.Text = "终端擦除";
            this.eraseClientMemory.UseVisualStyleBackColor = true;
            // 
            // clientCanAnswer
            // 
            this.clientCanAnswer.AutoSize = true;
            this.clientCanAnswer.Checked = true;
            this.clientCanAnswer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanAnswer.Location = new System.Drawing.Point(6, 66);
            this.clientCanAnswer.Name = "clientCanAnswer";
            this.clientCanAnswer.Size = new System.Drawing.Size(72, 16);
            this.clientCanAnswer.TabIndex = 2;
            this.clientCanAnswer.Text = "允许答题";
            this.clientCanAnswer.UseVisualStyleBackColor = true;
            // 
            // clientCanClearSoon
            // 
            this.clientCanClearSoon.AutoSize = true;
            this.clientCanClearSoon.Checked = true;
            this.clientCanClearSoon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clientCanClearSoon.Location = new System.Drawing.Point(6, 44);
            this.clientCanClearSoon.Name = "clientCanClearSoon";
            this.clientCanClearSoon.Size = new System.Drawing.Size(72, 16);
            this.clientCanClearSoon.TabIndex = 1;
            this.clientCanClearSoon.Text = "快速清除";
            this.clientCanClearSoon.UseVisualStyleBackColor = true;
            // 
            // persistenceConfiguration
            // 
            this.persistenceConfiguration.AutoSize = true;
            this.persistenceConfiguration.Checked = true;
            this.persistenceConfiguration.CheckState = System.Windows.Forms.CheckState.Checked;
            this.persistenceConfiguration.Location = new System.Drawing.Point(6, 22);
            this.persistenceConfiguration.Name = "persistenceConfiguration";
            this.persistenceConfiguration.Size = new System.Drawing.Size(72, 16);
            this.persistenceConfiguration.TabIndex = 0;
            this.persistenceConfiguration.Text = "固化配置";
            this.persistenceConfiguration.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.TitleEditor);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Location = new System.Drawing.Point(221, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(255, 499);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "答案设置";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "格式 题号:答案:分数  例如 1:3:3";
            // 
            // TitleEditor
            // 
            this.TitleEditor.Location = new System.Drawing.Point(7, 32);
            this.TitleEditor.Multiline = true;
            this.TitleEditor.Name = "TitleEditor";
            this.TitleEditor.Size = new System.Drawing.Size(242, 432);
            this.TitleEditor.TabIndex = 1;
            this.TitleEditor.Text = "1:1:1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(7, 470);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(242, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "设置答案（第四步）";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listViewMonitor);
            this.groupBox5.Location = new System.Drawing.Point(483, 24);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(260, 499);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "监控窗口";
            // 
            // listViewMonitor
            // 
            this.listViewMonitor.FormattingEnabled = true;
            this.listViewMonitor.ItemHeight = 12;
            this.listViewMonitor.Location = new System.Drawing.Point(6, 20);
            this.listViewMonitor.Name = "listViewMonitor";
            this.listViewMonitor.Size = new System.Drawing.Size(248, 472);
            this.listViewMonitor.TabIndex = 7;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button6);
            this.groupBox6.Controls.Add(this.listSorceMonitor);
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Location = new System.Drawing.Point(750, 24);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(261, 499);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "分数计算";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(7, 469);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(248, 23);
            this.button6.TabIndex = 1;
            this.button6.Text = "下发分数（第六步）";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // listSorceMonitor
            // 
            this.listSorceMonitor.FormattingEnabled = true;
            this.listSorceMonitor.ItemHeight = 12;
            this.listSorceMonitor.Location = new System.Drawing.Point(7, 17);
            this.listSorceMonitor.Name = "listSorceMonitor";
            this.listSorceMonitor.Size = new System.Drawing.Size(248, 412);
            this.listSorceMonitor.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 441);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(248, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "计算分数（第五步）";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // channel
            // 
            this.channel.FormattingEnabled = true;
            this.channel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.channel.Location = new System.Drawing.Point(72, 20);
            this.channel.Name = "channel";
            this.channel.Size = new System.Drawing.Size(121, 20);
            this.channel.TabIndex = 0;
            this.channel.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "信道";
            // 
            // frequency
            // 
            this.frequency.FormattingEnabled = true;
            this.frequency.Items.AddRange(new object[] {
            "dBM0",
            "dBM1"});
            this.frequency.Location = new System.Drawing.Point(72, 47);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(121, 20);
            this.frequency.TabIndex = 2;
            this.frequency.Text = "dBM0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "频率";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "设置（第二步）";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.frequency);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.channel);
            this.groupBox1.Location = new System.Drawing.Point(14, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基础设置";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 535);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Techeart WSDE Answer Card Demo";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox SecretText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox persistenceConfiguration;
        private System.Windows.Forms.CheckBox clientCanClearSoon;
        private System.Windows.Forms.CheckBox clientCanAnswer;
        private System.Windows.Forms.CheckBox eraseClientMemory;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox clientCanSeeSolution;
        private System.Windows.Forms.CheckBox clinetCanSeeAnswer;
        private System.Windows.Forms.CheckBox clientCanChangeDate;
        private System.Windows.Forms.CheckBox clientCanErase;
        private System.Windows.Forms.CheckBox clientCanChangeChannel;
        private System.Windows.Forms.CheckBox clientCanWriteABC;
        private System.Windows.Forms.CheckBox clientCanWriteNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TitleEditor;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox listViewMonitor;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox listSorceMonitor;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox channel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox frequency;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

