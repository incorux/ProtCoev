namespace ProteinCoev
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numericIdentity = new System.Windows.Forms.NumericUpDown();
            this.LoadFileButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.CreateTreeButton = new System.Windows.Forms.Button();
            this.CoevBtn = new System.Windows.Forms.Button();
            this.RunAllBtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelPosition = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.SelectionRangeStartControl = new System.Windows.Forms.NumericUpDown();
            this.SelectionRangeEndControl = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchForStringButton = new System.Windows.Forms.Button();
            this.SearchSequenceTextBox = new System.Windows.Forms.TextBox();
            this.checkTailing = new System.Windows.Forms.CheckBox();
            this.checkBase = new System.Windows.Forms.CheckBox();
            this.numericStartingCredit = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericCreditLoss = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericCreditGain = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.ColorButton = new System.Windows.Forms.Button();
            this.AlignmentTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.alignmentArea = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericIdentity)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionRangeStartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionRangeEndControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStartingCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCreditLoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCreditGain)).BeginInit();
            this.AlignmentTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Identity treshold";
            // 
            // numericIdentity
            // 
            this.numericIdentity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericIdentity.Location = new System.Drawing.Point(6, 39);
            this.numericIdentity.Name = "numericIdentity";
            this.numericIdentity.Size = new System.Drawing.Size(83, 20);
            this.numericIdentity.TabIndex = 4;
            this.numericIdentity.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.Location = new System.Drawing.Point(3, 4);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(101, 31);
            this.LoadFileButton.TabIndex = 3;
            this.LoadFileButton.Text = "Load file";
            this.LoadFileButton.UseVisualStyleBackColor = true;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButtonClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.CreateTreeButton);
            this.panel1.Controls.Add(this.CoevBtn);
            this.panel1.Controls.Add(this.RunAllBtn);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.labelPosition);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.ColorButton);
            this.panel1.Controls.Add(this.LoadFileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 889);
            this.panel1.TabIndex = 6;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(3, 811);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(101, 23);
            this.progressBar.TabIndex = 23;
            // 
            // CreateTreeButton
            // 
            this.CreateTreeButton.Location = new System.Drawing.Point(5, 777);
            this.CreateTreeButton.Name = "CreateTreeButton";
            this.CreateTreeButton.Size = new System.Drawing.Size(98, 27);
            this.CreateTreeButton.TabIndex = 22;
            this.CreateTreeButton.Text = "Draw tree";
            this.CreateTreeButton.UseVisualStyleBackColor = true;
            this.CreateTreeButton.Click += new System.EventHandler(this.CreateTreeButtonClick);
            // 
            // CoevBtn
            // 
            this.CoevBtn.Location = new System.Drawing.Point(3, 41);
            this.CoevBtn.Name = "CoevBtn";
            this.CoevBtn.Size = new System.Drawing.Size(101, 52);
            this.CoevBtn.TabIndex = 21;
            this.CoevBtn.Text = "Check coevolution";
            this.CoevBtn.UseVisualStyleBackColor = true;
            this.CoevBtn.Click += new System.EventHandler(this.CoevBtnClick);
            // 
            // RunAllBtn
            // 
            this.RunAllBtn.Location = new System.Drawing.Point(5, 744);
            this.RunAllBtn.Name = "RunAllBtn";
            this.RunAllBtn.Size = new System.Drawing.Size(98, 27);
            this.RunAllBtn.TabIndex = 7;
            this.RunAllBtn.Text = "Run all";
            this.RunAllBtn.UseVisualStyleBackColor = true;
            this.RunAllBtn.Click += new System.EventHandler(this.RunAllClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(5, 711);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(98, 27);
            this.button5.TabIndex = 20;
            this.button5.Text = "Psicov";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.PsicovClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(5, 678);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 27);
            this.button3.TabIndex = 18;
            this.button3.Text = "DI";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DIClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 645);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 27);
            this.button2.TabIndex = 17;
            this.button2.Text = "MIp";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.MIpClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 612);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 27);
            this.button1.TabIndex = 16;
            this.button1.Text = "MI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.MIClick);
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelPosition.Location = new System.Drawing.Point(0, 876);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(13, 5, 13, 5);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(44, 13);
            this.labelPosition.TabIndex = 15;
            this.labelPosition.Text = "Position";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.SelectionRangeStartControl);
            this.groupBox1.Controls.Add(this.SelectionRangeEndControl);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SearchForStringButton);
            this.groupBox1.Controls.Add(this.SearchSequenceTextBox);
            this.groupBox1.Controls.Add(this.checkTailing);
            this.groupBox1.Controls.Add(this.checkBase);
            this.groupBox1.Controls.Add(this.numericStartingCredit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericCreditLoss);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericCreditGain);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericIdentity);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonApply);
            this.groupBox1.Location = new System.Drawing.Point(5, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(95, 507);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(0, 373);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(91, 25);
            this.button6.TabIndex = 28;
            this.button6.Text = "Select";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6Click);
            // 
            // SelectionRangeStartControl
            // 
            this.SelectionRangeStartControl.Location = new System.Drawing.Point(6, 321);
            this.SelectionRangeStartControl.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.SelectionRangeStartControl.Name = "SelectionRangeStartControl";
            this.SelectionRangeStartControl.Size = new System.Drawing.Size(83, 20);
            this.SelectionRangeStartControl.TabIndex = 27;
            // 
            // SelectionRangeEndControl
            // 
            this.SelectionRangeEndControl.Location = new System.Drawing.Point(6, 347);
            this.SelectionRangeEndControl.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.SelectionRangeEndControl.Name = "SelectionRangeEndControl";
            this.SelectionRangeEndControl.Size = new System.Drawing.Size(83, 20);
            this.SelectionRangeEndControl.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Choose ranges";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "- OR -";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1, 404);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 25);
            this.button4.TabIndex = 18;
            this.button4.Text = "Reset selection";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.ResetSelectionClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 432);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Find  sequence";
            // 
            // SearchForStringButton
            // 
            this.SearchForStringButton.Location = new System.Drawing.Point(4, 477);
            this.SearchForStringButton.Name = "SearchForStringButton";
            this.SearchForStringButton.Size = new System.Drawing.Size(87, 26);
            this.SearchForStringButton.TabIndex = 16;
            this.SearchForStringButton.Text = "Search";
            this.SearchForStringButton.UseVisualStyleBackColor = true;
            this.SearchForStringButton.Click += new System.EventHandler(this.SearchForStringButtonClick);
            // 
            // SearchSequenceTextBox
            // 
            this.SearchSequenceTextBox.Location = new System.Drawing.Point(4, 451);
            this.SearchSequenceTextBox.Name = "SearchSequenceTextBox";
            this.SearchSequenceTextBox.Size = new System.Drawing.Size(87, 20);
            this.SearchSequenceTextBox.TabIndex = 15;
            // 
            // checkTailing
            // 
            this.checkTailing.AutoSize = true;
            this.checkTailing.Checked = true;
            this.checkTailing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTailing.Location = new System.Drawing.Point(7, 89);
            this.checkTailing.Name = "checkTailing";
            this.checkTailing.Size = new System.Drawing.Size(75, 17);
            this.checkTailing.TabIndex = 14;
            this.checkTailing.Text = "Use tailing";
            this.checkTailing.UseVisualStyleBackColor = true;
            // 
            // checkBase
            // 
            this.checkBase.AutoSize = true;
            this.checkBase.Checked = true;
            this.checkBase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBase.Location = new System.Drawing.Point(8, 66);
            this.checkBase.Name = "checkBase";
            this.checkBase.Size = new System.Drawing.Size(72, 17);
            this.checkBase.TabIndex = 13;
            this.checkBase.Text = "Find base";
            this.checkBase.UseVisualStyleBackColor = true;
            // 
            // numericStartingCredit
            // 
            this.numericStartingCredit.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericStartingCredit.Location = new System.Drawing.Point(4, 221);
            this.numericStartingCredit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericStartingCredit.Name = "numericStartingCredit";
            this.numericStartingCredit.Size = new System.Drawing.Size(85, 20);
            this.numericStartingCredit.TabIndex = 11;
            this.numericStartingCredit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Starting credit";
            // 
            // numericCreditLoss
            // 
            this.numericCreditLoss.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericCreditLoss.Location = new System.Drawing.Point(5, 176);
            this.numericCreditLoss.Name = "numericCreditLoss";
            this.numericCreditLoss.Size = new System.Drawing.Size(84, 20);
            this.numericCreditLoss.TabIndex = 9;
            this.numericCreditLoss.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Credit loss";
            // 
            // numericCreditGain
            // 
            this.numericCreditGain.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericCreditGain.Location = new System.Drawing.Point(5, 130);
            this.numericCreditGain.Name = "numericCreditGain";
            this.numericCreditGain.Size = new System.Drawing.Size(84, 20);
            this.numericCreditGain.TabIndex = 7;
            this.numericCreditGain.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Credit gain";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(4, 247);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(87, 29);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "Find";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // ColorButton
            // 
            this.ColorButton.Location = new System.Drawing.Point(2, 840);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(101, 27);
            this.ColorButton.TabIndex = 8;
            this.ColorButton.Text = "Color";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // AlignmentTabs
            // 
            this.AlignmentTabs.Controls.Add(this.tabPage1);
            this.AlignmentTabs.Controls.Add(this.tabPage2);
            this.AlignmentTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlignmentTabs.Location = new System.Drawing.Point(106, 0);
            this.AlignmentTabs.Name = "AlignmentTabs";
            this.AlignmentTabs.SelectedIndex = 0;
            this.AlignmentTabs.Size = new System.Drawing.Size(1090, 889);
            this.AlignmentTabs.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.alignmentArea);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1082, 863);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage5";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // alignmentArea
            // 
            this.alignmentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alignmentArea.Location = new System.Drawing.Point(3, 3);
            this.alignmentArea.Name = "alignmentArea";
            this.alignmentArea.Size = new System.Drawing.Size(1076, 857);
            this.alignmentArea.TabIndex = 6;
            this.alignmentArea.Text = "";
            this.alignmentArea.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1082, 863);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 889);
            this.Controls.Add(this.AlignmentTabs);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericIdentity)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionRangeStartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionRangeEndControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStartingCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCreditLoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCreditGain)).EndInit();
            this.AlignmentTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericIdentity;
        private System.Windows.Forms.Button LoadFileButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.TabControl AlignmentTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox alignmentArea;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericCreditGain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericCreditLoss;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericStartingCredit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.CheckBox checkBase;
        private System.Windows.Forms.CheckBox checkTailing;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button RunAllBtn;
        private System.Windows.Forms.Button CoevBtn;
        private System.Windows.Forms.Button SearchForStringButton;
        private System.Windows.Forms.TextBox SearchSequenceTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CreateTreeButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown SelectionRangeStartControl;
        private System.Windows.Forms.NumericUpDown SelectionRangeEndControl;
        private System.Windows.Forms.Button button6;


    }
}

