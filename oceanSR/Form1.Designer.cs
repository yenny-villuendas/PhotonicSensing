namespace oceanSR
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSelectedVals = new System.Windows.Forms.Button();
            this.rbMultiple = new System.Windows.Forms.RadioButton();
            this.nudSingleWave = new System.Windows.Forms.NumericUpDown();
            this.rbValue = new System.Windows.Forms.RadioButton();
            this.rbRange = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMinWave = new System.Windows.Forms.NumericUpDown();
            this.nudMaxWave = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSingleWave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinWave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWave)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRestart);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(198, 33);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox3.Size = new System.Drawing.Size(230, 262);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Monitoring Config";
            // 
            // btnRestart
            // 
            this.btnRestart.Enabled = false;
            this.btnRestart.Location = new System.Drawing.Point(128, 220);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(96, 24);
            this.btnRestart.TabIndex = 10;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(12, 220);
            this.button2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 24);
            this.button2.TabIndex = 9;
            this.button2.Text = "Monitoring";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSelectedVals);
            this.groupBox4.Controls.Add(this.rbMultiple);
            this.groupBox4.Controls.Add(this.nudSingleWave);
            this.groupBox4.Controls.Add(this.rbValue);
            this.groupBox4.Controls.Add(this.rbRange);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.nudMinWave);
            this.groupBox4.Controls.Add(this.nudMaxWave);
            this.groupBox4.Location = new System.Drawing.Point(12, 95);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox4.Size = new System.Drawing.Size(212, 110);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Wavelenghts";
            // 
            // btnSelectedVals
            // 
            this.btnSelectedVals.Enabled = false;
            this.btnSelectedVals.Location = new System.Drawing.Point(142, 78);
            this.btnSelectedVals.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnSelectedVals.Name = "btnSelectedVals";
            this.btnSelectedVals.Size = new System.Drawing.Size(63, 23);
            this.btnSelectedVals.TabIndex = 8;
            this.btnSelectedVals.Text = "Accept";
            this.btnSelectedVals.UseVisualStyleBackColor = true;
            this.btnSelectedVals.Click += new System.EventHandler(this.btnSelectedVals_Click);
            // 
            // rbMultiple
            // 
            this.rbMultiple.AutoSize = true;
            this.rbMultiple.Location = new System.Drawing.Point(104, 55);
            this.rbMultiple.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.rbMultiple.Name = "rbMultiple";
            this.rbMultiple.Size = new System.Drawing.Size(101, 17);
            this.rbMultiple.TabIndex = 7;
            this.rbMultiple.Text = "Selected values";
            this.rbMultiple.UseVisualStyleBackColor = true;
            this.rbMultiple.CheckedChanged += new System.EventHandler(this.rbMultiple_CheckedChanged);
            // 
            // nudSingleWave
            // 
            this.nudSingleWave.Location = new System.Drawing.Point(151, 25);
            this.nudSingleWave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.nudSingleWave.Maximum = new decimal(new int[] {
            1100,
            0,
            0,
            0});
            this.nudSingleWave.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudSingleWave.Name = "nudSingleWave";
            this.nudSingleWave.Size = new System.Drawing.Size(54, 20);
            this.nudSingleWave.TabIndex = 6;
            this.nudSingleWave.Value = new decimal(new int[] {
            550,
            0,
            0,
            0});
            // 
            // rbValue
            // 
            this.rbValue.AutoSize = true;
            this.rbValue.Location = new System.Drawing.Point(104, 25);
            this.rbValue.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.rbValue.Name = "rbValue";
            this.rbValue.Size = new System.Drawing.Size(52, 17);
            this.rbValue.TabIndex = 5;
            this.rbValue.Text = "Value";
            this.rbValue.UseVisualStyleBackColor = true;
            this.rbValue.CheckedChanged += new System.EventHandler(this.rbValue_CheckedChanged);
            // 
            // rbRange
            // 
            this.rbRange.AutoSize = true;
            this.rbRange.Checked = true;
            this.rbRange.Location = new System.Drawing.Point(9, 25);
            this.rbRange.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.rbRange.Name = "rbRange";
            this.rbRange.Size = new System.Drawing.Size(57, 17);
            this.rbRange.TabIndex = 4;
            this.rbRange.TabStop = true;
            this.rbRange.Text = "Range";
            this.rbRange.UseVisualStyleBackColor = true;
            this.rbRange.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Min";
            // 
            // nudMinWave
            // 
            this.nudMinWave.Location = new System.Drawing.Point(35, 48);
            this.nudMinWave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.nudMinWave.Maximum = new decimal(new int[] {
            1100,
            0,
            0,
            0});
            this.nudMinWave.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudMinWave.Name = "nudMinWave";
            this.nudMinWave.Size = new System.Drawing.Size(54, 20);
            this.nudMinWave.TabIndex = 1;
            this.nudMinWave.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            // 
            // nudMaxWave
            // 
            this.nudMaxWave.Location = new System.Drawing.Point(35, 72);
            this.nudMaxWave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.nudMaxWave.Maximum = new decimal(new int[] {
            1100,
            0,
            0,
            0});
            this.nudMaxWave.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudMaxWave.Name = "nudMaxWave";
            this.nudMaxWave.Size = new System.Drawing.Size(54, 20);
            this.nudMaxWave.TabIndex = 0;
            this.nudMaxWave.Value = new decimal(new int[] {
            750,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Location = new System.Drawing.Point(12, 31);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox2.Size = new System.Drawing.Size(184, 53);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monitoring time (seg)";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(7, 20);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(129, 20);
            this.numericUpDown2.TabIndex = 0;
            this.numericUpDown2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Location = new System.Drawing.Point(12, 33);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox5.Size = new System.Drawing.Size(175, 86);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Device";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(11, 53);
            this.button3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 24);
            this.button3.TabIndex = 7;
            this.button3.Text = "Close device";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 20);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox1.Size = new System.Drawing.Size(175, 53);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Integration time (micro seg)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(7, 20);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            600000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(129, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.tbxFileName);
            this.groupBox6.Controls.Add(this.btnFolder);
            this.groupBox6.Location = new System.Drawing.Point(12, 192);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox6.Size = new System.Drawing.Size(175, 103);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Saving options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Name";
            // 
            // tbxFileName
            // 
            this.tbxFileName.Location = new System.Drawing.Point(41, 30);
            this.tbxFileName.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(122, 20);
            this.tbxFileName.TabIndex = 11;
            this.tbxFileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxFileName_KeyPress);
            // 
            // btnFolder
            // 
            this.btnFolder.Enabled = false;
            this.btnFolder.Location = new System.Drawing.Point(7, 61);
            this.btnFolder.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(153, 24);
            this.btnFolder.TabIndex = 10;
            this.btnFolder.Text = "Select folder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(445, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 310);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "Form1";
            this.Text = "Photonic sensing";
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSingleWave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinWave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWave)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nudMaxWave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMinWave;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbRange;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown nudSingleWave;
        private System.Windows.Forms.RadioButton rbValue;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.RadioButton rbMultiple;
        private System.Windows.Forms.Button btnSelectedVals;
        private System.Windows.Forms.Button btnRestart;
    }
}

