﻿namespace Recommender_algorithm_DEMO
{
    partial class Form_AssociationRules
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Item_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Item_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Support = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Confidence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_ID,
            this.Column_Item_1,
            this.Column_Item_2,
            this.Column_Support,
            this.Column_Confidence});
            this.dataGridView1.Location = new System.Drawing.Point(1, -2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(765, 441);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column_ID
            // 
            this.Column_ID.HeaderText = "规则编号(ID)";
            this.Column_ID.Name = "Column_ID";
            this.Column_ID.ReadOnly = true;
            this.Column_ID.Width = 80;
            // 
            // Column_Item_1
            // 
            this.Column_Item_1.HeaderText = "规则先导(Antecedent)";
            this.Column_Item_1.Name = "Column_Item_1";
            this.Column_Item_1.ReadOnly = true;
            this.Column_Item_1.Width = 200;
            // 
            // Column_Item_2
            // 
            this.Column_Item_2.HeaderText = "规则后继(Consequent)";
            this.Column_Item_2.Name = "Column_Item_2";
            this.Column_Item_2.ReadOnly = true;
            this.Column_Item_2.Width = 200;
            // 
            // Column_Support
            // 
            this.Column_Support.HeaderText = "支持度(Support)";
            this.Column_Support.Name = "Column_Support";
            this.Column_Support.ReadOnly = true;
            this.Column_Support.Width = 120;
            // 
            // Column_Confidence
            // 
            this.Column_Confidence.HeaderText = "置信度(Confidence)";
            this.Column_Confidence.Name = "Column_Confidence";
            this.Column_Confidence.ReadOnly = true;
            this.Column_Confidence.Width = 120;
            // 
            // Form_AssociationRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 439);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form_AssociationRules";
            this.Text = "关联规则库";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Item_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Item_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Support;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Confidence;
    }
}