namespace Recommender_algorithm_DEMO
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.协同过滤算法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jiyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemCFStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slopeOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基于关联规则ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aprioriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.协同过滤算法ToolStripMenuItem,
            this.slopeOneToolStripMenuItem,
            this.基于关联规则ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(916, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 协同过滤算法ToolStripMenuItem
            // 
            this.协同过滤算法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jiyToolStripMenuItem,
            this.ItemCFStripMenuItem});
            this.协同过滤算法ToolStripMenuItem.Name = "协同过滤算法ToolStripMenuItem";
            this.协同过滤算法ToolStripMenuItem.Size = new System.Drawing.Size(182, 24);
            this.协同过滤算法ToolStripMenuItem.Text = "Memory-based协同过滤";
            // 
            // jiyToolStripMenuItem
            // 
            this.jiyToolStripMenuItem.Name = "jiyToolStripMenuItem";
            this.jiyToolStripMenuItem.Size = new System.Drawing.Size(213, 24);
            this.jiyToolStripMenuItem.Text = "基于用户的协同过滤";
            this.jiyToolStripMenuItem.Click += new System.EventHandler(this.jiyToolStripMenuItem_Click);
            // 
            // ItemCFStripMenuItem
            // 
            this.ItemCFStripMenuItem.Name = "ItemCFStripMenuItem";
            this.ItemCFStripMenuItem.Size = new System.Drawing.Size(213, 24);
            this.ItemCFStripMenuItem.Text = "基于项目的协同过滤";
            this.ItemCFStripMenuItem.Click += new System.EventHandler(this.ItemCFToolStripMenuItem_Click);
            // 
            // slopeOneToolStripMenuItem
            // 
            this.slopeOneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slopeToolStripMenuItem});
            this.slopeOneToolStripMenuItem.Name = "slopeOneToolStripMenuItem";
            this.slopeOneToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.slopeOneToolStripMenuItem.Text = "Model-based协同过滤";
            this.slopeOneToolStripMenuItem.Click += new System.EventHandler(this.slopeOneToolStripMenuItem_Click);
            // 
            // 基于关联规则ToolStripMenuItem
            // 
            this.基于关联规则ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aprioriToolStripMenuItem});
            this.基于关联规则ToolStripMenuItem.Name = "基于关联规则ToolStripMenuItem";
            this.基于关联规则ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.基于关联规则ToolStripMenuItem.Text = "基于关联规则";
            // 
            // aprioriToolStripMenuItem
            // 
            this.aprioriToolStripMenuItem.Name = "aprioriToolStripMenuItem";
            this.aprioriToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.aprioriToolStripMenuItem.Text = "Apriori算法";
            this.aprioriToolStripMenuItem.Click += new System.EventHandler(this.aprioriToolStripMenuItem_Click);
            // 
            // slopeToolStripMenuItem
            // 
            this.slopeToolStripMenuItem.Name = "slopeToolStripMenuItem";
            this.slopeToolStripMenuItem.Size = new System.Drawing.Size(282, 24);
            this.slopeToolStripMenuItem.Text = "基于回归模型的Slope One算法";
            this.slopeToolStripMenuItem.Click += new System.EventHandler(this.slopeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 443);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "推荐算法演示程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 协同过滤算法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jiyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItemCFStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slopeOneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基于关联规则ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aprioriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slopeToolStripMenuItem;
    }
}

