namespace OwO_Maker
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new System.Windows.Forms.Button();
            listBox1 = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            t_random_min = new System.Windows.Forms.TextBox();
            ProductionCouponKey = new System.Windows.Forms.ComboBox();
            HumanTime = new System.Windows.Forms.CheckBox();
            t_Times = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            t_Level = new System.Windows.Forms.ComboBox();
            label9 = new System.Windows.Forms.Label();
            ProductionCoupon = new System.Windows.Forms.CheckBox();
            button5 = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            Memory = new System.Windows.Forms.RadioButton();
            TypeWriter = new System.Windows.Forms.RadioButton();
            ShootingRange = new System.Windows.Forms.RadioButton();
            StoneQuarry = new System.Windows.Forms.RadioButton();
            SawMill = new System.Windows.Forms.RadioButton();
            FishPond = new System.Windows.Forms.RadioButton();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            button4 = new System.Windows.Forms.Button();
            listView1 = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader7 = new System.Windows.Forms.ColumnHeader();
            columnHeader4 = new System.Windows.Forms.ColumnHeader();
            columnHeader5 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            columnHeader6 = new System.Windows.Forms.ColumnHeader();
            columnHeader8 = new System.Windows.Forms.ColumnHeader();
            button2 = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(258, 194);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(332, 27);
            button1.TabIndex = 0;
            button1.Text = "Refresh Client List";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 25;
            listBox1.Location = new System.Drawing.Point(7, 22);
            listBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(243, 229);
            listBox1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new System.Drawing.Point(7, 7);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(594, 261);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Nostale Clients";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(t_random_min);
            groupBox4.Controls.Add(ProductionCouponKey);
            groupBox4.Controls.Add(HumanTime);
            groupBox4.Controls.Add(t_Times);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(t_Level);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(ProductionCoupon);
            groupBox4.Location = new System.Drawing.Point(389, 11);
            groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Size = new System.Drawing.Size(201, 177);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Play Settings";
            // 
            // t_random_min
            // 
            t_random_min.Location = new System.Drawing.Point(101, 115);
            t_random_min.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            t_random_min.Name = "t_random_min";
            t_random_min.Size = new System.Drawing.Size(32, 23);
            t_random_min.TabIndex = 12;
            t_random_min.Text = "0";
            // 
            // ProductionCouponKey
            // 
            ProductionCouponKey.FormattingEnabled = true;
            ProductionCouponKey.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" });
            ProductionCouponKey.Location = new System.Drawing.Point(159, 84);
            ProductionCouponKey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProductionCouponKey.Name = "ProductionCouponKey";
            ProductionCouponKey.Size = new System.Drawing.Size(34, 23);
            ProductionCouponKey.TabIndex = 13;
            ProductionCouponKey.Text = "5";
            ProductionCouponKey.SelectedIndexChanged += ProductionCouponKey_SelectedIndexChanged;
            // 
            // HumanTime
            // 
            HumanTime.AutoSize = true;
            HumanTime.Location = new System.Drawing.Point(5, 148);
            HumanTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            HumanTime.Name = "HumanTime";
            HumanTime.Size = new System.Drawing.Size(95, 19);
            HumanTime.TabIndex = 10;
            HumanTime.Text = "Human Time";
            HumanTime.UseVisualStyleBackColor = true;
            // 
            // t_Times
            // 
            t_Times.Location = new System.Drawing.Point(59, 50);
            t_Times.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            t_Times.Name = "t_Times";
            t_Times.Size = new System.Drawing.Size(134, 23);
            t_Times.TabIndex = 7;
            t_Times.Text = "20";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label8.Location = new System.Drawing.Point(139, 119);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(19, 16);
            label8.TabIndex = 6;
            label8.Text = "%";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(118, 87);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(33, 16);
            label3.TabIndex = 14;
            label3.Text = "Key:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(5, 50);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(48, 16);
            label2.TabIndex = 6;
            label2.Text = "Times:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(5, 23);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(46, 16);
            label1.TabIndex = 4;
            label1.Text = "Level: ";
            // 
            // t_Level
            // 
            t_Level.FormattingEnabled = true;
            t_Level.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            t_Level.Location = new System.Drawing.Point(59, 21);
            t_Level.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            t_Level.Name = "t_Level";
            t_Level.Size = new System.Drawing.Size(134, 23);
            t_Level.TabIndex = 0;
            t_Level.Text = "5";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label9.Location = new System.Drawing.Point(6, 117);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(84, 16);
            label9.TabIndex = 4;
            label9.Text = "Fail Chance: ";
            // 
            // ProductionCoupon
            // 
            ProductionCoupon.AutoSize = true;
            ProductionCoupon.Location = new System.Drawing.Point(5, 84);
            ProductionCoupon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProductionCoupon.Name = "ProductionCoupon";
            ProductionCoupon.Size = new System.Drawing.Size(105, 19);
            ProductionCoupon.TabIndex = 9;
            ProductionCoupon.Text = "Prod. Coupons";
            ProductionCoupon.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(258, 227);
            button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(332, 27);
            button5.TabIndex = 5;
            button5.Text = "Add Bot";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(Memory);
            groupBox2.Controls.Add(TypeWriter);
            groupBox2.Controls.Add(ShootingRange);
            groupBox2.Controls.Add(StoneQuarry);
            groupBox2.Controls.Add(SawMill);
            groupBox2.Controls.Add(FishPond);
            groupBox2.Location = new System.Drawing.Point(259, 11);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(127, 177);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Minigames";
            // 
            // Memory
            // 
            Memory.AutoSize = true;
            Memory.Enabled = false;
            Memory.Location = new System.Drawing.Point(8, 153);
            Memory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Memory.Name = "Memory";
            Memory.Size = new System.Drawing.Size(110, 19);
            Memory.TabIndex = 5;
            Memory.Text = "Memory (Event)";
            Memory.UseVisualStyleBackColor = true;
            // 
            // TypeWriter
            // 
            TypeWriter.AutoSize = true;
            TypeWriter.Enabled = false;
            TypeWriter.Location = new System.Drawing.Point(8, 128);
            TypeWriter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TypeWriter.Name = "TypeWriter";
            TypeWriter.Size = new System.Drawing.Size(119, 19);
            TypeWriter.TabIndex = 4;
            TypeWriter.Text = "Typewriter (Event)";
            TypeWriter.UseVisualStyleBackColor = true;
            // 
            // ShootingRange
            // 
            ShootingRange.AutoSize = true;
            ShootingRange.Location = new System.Drawing.Point(8, 75);
            ShootingRange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ShootingRange.Name = "ShootingRange";
            ShootingRange.Size = new System.Drawing.Size(109, 19);
            ShootingRange.TabIndex = 3;
            ShootingRange.Text = "Shooting Range";
            ShootingRange.UseVisualStyleBackColor = true;
            // 
            // StoneQuarry
            // 
            StoneQuarry.AutoSize = true;
            StoneQuarry.Location = new System.Drawing.Point(8, 25);
            StoneQuarry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            StoneQuarry.Name = "StoneQuarry";
            StoneQuarry.Size = new System.Drawing.Size(94, 19);
            StoneQuarry.TabIndex = 2;
            StoneQuarry.Text = "Stone Quarry";
            StoneQuarry.UseVisualStyleBackColor = true;
            // 
            // SawMill
            // 
            SawMill.AutoSize = true;
            SawMill.Location = new System.Drawing.Point(8, 50);
            SawMill.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SawMill.Name = "SawMill";
            SawMill.Size = new System.Drawing.Size(69, 19);
            SawMill.TabIndex = 1;
            SawMill.Text = "Saw Mill";
            SawMill.UseVisualStyleBackColor = true;
            // 
            // FishPond
            // 
            FishPond.AutoSize = true;
            FishPond.Checked = true;
            FishPond.Location = new System.Drawing.Point(8, 100);
            FishPond.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            FishPond.Name = "FishPond";
            FishPond.Size = new System.Drawing.Size(77, 19);
            FishPond.TabIndex = 0;
            FishPond.TabStop = true;
            FishPond.Text = "Fish Pond";
            FishPond.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(10, 14);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(617, 301);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(609, 273);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(listView1);
            tabPage2.Controls.Add(button2);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(609, 273);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Running Bots";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(451, 243);
            button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(150, 27);
            button4.TabIndex = 7;
            button4.Text = "Stop / Delete All";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // listView1
            // 
            listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, columnHeader7, columnHeader4, columnHeader5, columnHeader3, columnHeader6, columnHeader8 });
            listView1.ImeMode = System.Windows.Forms.ImeMode.On;
            listView1.Location = new System.Drawing.Point(7, 7);
            listView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(594, 225);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "BotID";
            columnHeader1.Width = 69;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Minigame";
            columnHeader2.Width = 80;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Level";
            columnHeader7.Width = 45;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Points";
            columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Prod Points";
            columnHeader5.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Use Prod. Coupon";
            columnHeader3.Width = 110;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Human Time";
            columnHeader6.Width = 85;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Progress";
            columnHeader8.Width = 75;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(8, 243);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(150, 27);
            button2.TabIndex = 5;
            button2.Text = "Start All";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(631, 316);
            Controls.Add(tabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "OwO Maker";
            FormClosed += Form_Closed;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ShootingRange;
        private System.Windows.Forms.RadioButton StoneQuarry;
        private System.Windows.Forms.RadioButton SawMill;
        private System.Windows.Forms.RadioButton FishPond;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox t_Level;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox t_Times;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox ProductionCoupon;
        private System.Windows.Forms.CheckBox HumanTime;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox t_random_min;
        private System.Windows.Forms.RadioButton Memory;
        private System.Windows.Forms.RadioButton TypeWriter;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ProductionCouponKey;
    }
}

