﻿namespace ProteinCoev
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
            this.label3 = new System.Windows.Forms.Label();
            this.comboOrganisms = new System.Windows.Forms.ComboBox();
            this.ColorButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonApply = new System.Windows.Forms.Button();
            this.AlignmentTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.alignmentArea = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericIdentity)).BeginInit();
            this.panel1.SuspendLayout();
            this.AlignmentTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Identity";
            // 
            // numericIdentity
            // 
            this.numericIdentity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericIdentity.Location = new System.Drawing.Point(12, 64);
            this.numericIdentity.Name = "numericIdentity";
            this.numericIdentity.Size = new System.Drawing.Size(88, 20);
            this.numericIdentity.TabIndex = 4;
            this.numericIdentity.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.Location = new System.Drawing.Point(2, 4);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(101, 31);
            this.LoadFileButton.TabIndex = 3;
            this.LoadFileButton.Text = "Load file";
            this.LoadFileButton.UseVisualStyleBackColor = true;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButtonClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboOrganisms);
            this.panel1.Controls.Add(this.ColorButton);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.buttonApply);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericIdentity);
            this.panel1.Controls.Add(this.LoadFileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 462);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Organisms";
            // 
            // comboOrganisms
            // 
            this.comboOrganisms.FormattingEnabled = true;
            this.comboOrganisms.Location = new System.Drawing.Point(6, 149);
            this.comboOrganisms.Name = "comboOrganisms";
            this.comboOrganisms.Size = new System.Drawing.Size(97, 21);
            this.comboOrganisms.TabIndex = 11;
            this.comboOrganisms.SelectedIndexChanged += new System.EventHandler(this.ComboOrganismsSelectedIndexChanged);
            // 
            // ColorButton
            // 
            this.ColorButton.Location = new System.Drawing.Point(3, 90);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(101, 31);
            this.ColorButton.TabIndex = 8;
            this.ColorButton.Text = "Color";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(3, 431);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 7;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(3, 394);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(101, 31);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // AlignmentTabs
            // 
            this.AlignmentTabs.Controls.Add(this.tabPage1);
            this.AlignmentTabs.Controls.Add(this.tabPage2);
            this.AlignmentTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlignmentTabs.Location = new System.Drawing.Point(106, 0);
            this.AlignmentTabs.Name = "AlignmentTabs";
            this.AlignmentTabs.SelectedIndex = 0;
            this.AlignmentTabs.Size = new System.Drawing.Size(865, 462);
            this.AlignmentTabs.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.alignmentArea);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(857, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage5";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // alignmentArea
            // 
            this.alignmentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alignmentArea.Location = new System.Drawing.Point(3, 3);
            this.alignmentArea.Name = "alignmentArea";
            this.alignmentArea.Size = new System.Drawing.Size(851, 430);
            this.alignmentArea.TabIndex = 6;
            this.alignmentArea.Text = "";
            this.alignmentArea.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(857, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 462);
            this.Controls.Add(this.AlignmentTabs);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericIdentity)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboOrganisms;
        private System.Windows.Forms.TabControl AlignmentTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox alignmentArea;
        private System.Windows.Forms.TabPage tabPage2;


    }
}
