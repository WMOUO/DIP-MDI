﻿namespace DIP
{
    partial class DIPSample
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBtoGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.馬賽克ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.位元切面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.負片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.調整亮度和對比ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旋轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.向左旋轉90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.向右旋轉90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旋轉180度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.垂直翻轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.水平翻轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方圖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方等化圖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(876, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(128, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.iPToolStripMenuItem,
            this.旋轉ToolStripMenuItem,
            this.分析ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(876, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = "檔案";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // iPToolStripMenuItem
            // 
            this.iPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBtoGrayToolStripMenuItem,
            this.馬賽克ToolStripMenuItem,
            this.位元切面ToolStripMenuItem,
            this.負片ToolStripMenuItem,
            this.調整亮度和對比ToolStripMenuItem});
            this.iPToolStripMenuItem.Name = "iPToolStripMenuItem";
            this.iPToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.iPToolStripMenuItem.Text = "色彩";
            this.iPToolStripMenuItem.Click += new System.EventHandler(this.iPToolStripMenuItem_Click);
            // 
            // rGBtoGrayToolStripMenuItem
            // 
            this.rGBtoGrayToolStripMenuItem.Name = "rGBtoGrayToolStripMenuItem";
            this.rGBtoGrayToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.rGBtoGrayToolStripMenuItem.Text = "灰階";
            this.rGBtoGrayToolStripMenuItem.Click += new System.EventHandler(this.rGBtoGrayToolStripMenuItem_Click);
            // 
            // 馬賽克ToolStripMenuItem
            // 
            this.馬賽克ToolStripMenuItem.Name = "馬賽克ToolStripMenuItem";
            this.馬賽克ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.馬賽克ToolStripMenuItem.Text = "馬賽克";
            this.馬賽克ToolStripMenuItem.Click += new System.EventHandler(this.馬賽克ToolStripMenuItem_Click);
            // 
            // 位元切面ToolStripMenuItem
            // 
            this.位元切面ToolStripMenuItem.Name = "位元切面ToolStripMenuItem";
            this.位元切面ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.位元切面ToolStripMenuItem.Text = "位元切面";
            this.位元切面ToolStripMenuItem.Click += new System.EventHandler(this.位元切面ToolStripMenuItem_Click);
            // 
            // 負片ToolStripMenuItem
            // 
            this.負片ToolStripMenuItem.Name = "負片ToolStripMenuItem";
            this.負片ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.負片ToolStripMenuItem.Text = "負片";
            this.負片ToolStripMenuItem.Click += new System.EventHandler(this.負片ToolStripMenuItem_Click);
            // 
            // 調整亮度和對比ToolStripMenuItem
            // 
            this.調整亮度和對比ToolStripMenuItem.Name = "調整亮度和對比ToolStripMenuItem";
            this.調整亮度和對比ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.調整亮度和對比ToolStripMenuItem.Text = "調整亮度和對比";
            this.調整亮度和對比ToolStripMenuItem.Click += new System.EventHandler(this.調整亮度和對比ToolStripMenuItem_Click);
            // 
            // 旋轉ToolStripMenuItem
            // 
            this.旋轉ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.向左旋轉90度ToolStripMenuItem,
            this.向右旋轉90度ToolStripMenuItem,
            this.旋轉180度ToolStripMenuItem,
            this.垂直翻轉ToolStripMenuItem,
            this.水平翻轉ToolStripMenuItem});
            this.旋轉ToolStripMenuItem.Name = "旋轉ToolStripMenuItem";
            this.旋轉ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.旋轉ToolStripMenuItem.Text = "旋轉";
            // 
            // 向左旋轉90度ToolStripMenuItem
            // 
            this.向左旋轉90度ToolStripMenuItem.Name = "向左旋轉90度ToolStripMenuItem";
            this.向左旋轉90度ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.向左旋轉90度ToolStripMenuItem.Text = "向左旋轉90度";
            this.向左旋轉90度ToolStripMenuItem.Click += new System.EventHandler(this.向左旋轉90度ToolStripMenuItem_Click);
            // 
            // 向右旋轉90度ToolStripMenuItem
            // 
            this.向右旋轉90度ToolStripMenuItem.Name = "向右旋轉90度ToolStripMenuItem";
            this.向右旋轉90度ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.向右旋轉90度ToolStripMenuItem.Text = "向右旋轉90度";
            this.向右旋轉90度ToolStripMenuItem.Click += new System.EventHandler(this.向右旋轉90度ToolStripMenuItem_Click);
            // 
            // 旋轉180度ToolStripMenuItem
            // 
            this.旋轉180度ToolStripMenuItem.Name = "旋轉180度ToolStripMenuItem";
            this.旋轉180度ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.旋轉180度ToolStripMenuItem.Text = "旋轉180度";
            this.旋轉180度ToolStripMenuItem.Click += new System.EventHandler(this.旋轉180度ToolStripMenuItem_Click);
            // 
            // 垂直翻轉ToolStripMenuItem
            // 
            this.垂直翻轉ToolStripMenuItem.Name = "垂直翻轉ToolStripMenuItem";
            this.垂直翻轉ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.垂直翻轉ToolStripMenuItem.Text = "垂直翻轉";
            this.垂直翻轉ToolStripMenuItem.Click += new System.EventHandler(this.垂直翻轉ToolStripMenuItem_Click);
            // 
            // 水平翻轉ToolStripMenuItem
            // 
            this.水平翻轉ToolStripMenuItem.Name = "水平翻轉ToolStripMenuItem";
            this.水平翻轉ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.水平翻轉ToolStripMenuItem.Text = "水平翻轉";
            this.水平翻轉ToolStripMenuItem.Click += new System.EventHandler(this.水平翻轉ToolStripMenuItem_Click);
            // 
            // 分析ToolStripMenuItem
            // 
            this.分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直方圖ToolStripMenuItem,
            this.直方等化圖ToolStripMenuItem});
            this.分析ToolStripMenuItem.Name = "分析ToolStripMenuItem";
            this.分析ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.分析ToolStripMenuItem.Text = "分析";
            // 
            // 直方圖ToolStripMenuItem
            // 
            this.直方圖ToolStripMenuItem.Name = "直方圖ToolStripMenuItem";
            this.直方圖ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.直方圖ToolStripMenuItem.Text = "直方圖";
            this.直方圖ToolStripMenuItem.Click += new System.EventHandler(this.直方圖ToolStripMenuItem_Click);
            // 
            // 直方等化圖ToolStripMenuItem
            // 
            this.直方等化圖ToolStripMenuItem.Name = "直方等化圖ToolStripMenuItem";
            this.直方等化圖ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.直方等化圖ToolStripMenuItem.Text = "直方等化圖";
            this.直方等化圖ToolStripMenuItem.Click += new System.EventHandler(this.直方等化圖ToolStripMenuItem_Click);
            // 
            // oFileDlg
            // 
            this.oFileDlg.FileName = "openFileDialog1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(128, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // DIPSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 434);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DIPSample";
            this.Text = "DIPSample";
            this.Load += new System.EventHandler(this.DIPSample_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog oFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem rGBtoGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 馬賽克ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 位元切面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 負片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旋轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 向左旋轉90度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 向右旋轉90度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旋轉180度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 垂直翻轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 水平翻轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方圖ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方等化圖ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 調整亮度和對比ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}