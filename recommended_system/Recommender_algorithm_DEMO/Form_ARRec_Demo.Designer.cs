namespace Recommender_algorithm_DEMO
{
    partial class Form_ARRec_Demo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_dislovenum = new System.Windows.Forms.Label();
            this.label_lovenum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Rating_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_ReleaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Genres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.Column_rec_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Rec_Degree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_rec_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_rec_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_gen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label_unrating_id = new System.Windows.Forms.Label();
            this.label_disloveid = new System.Windows.Forms.Label();
            this.label_loveid = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label_Reca = new System.Windows.Forms.Label();
            this.label_F_value = new System.Windows.Forms.Label();
            this.label_Prec = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label_Recall = new System.Windows.Forms.Label();
            this.label_Precision = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_test_dislove = new System.Windows.Forms.Label();
            this.label_test_love = new System.Windows.Forms.Label();
            this.label_test_total = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_dislovenum);
            this.groupBox1.Controls.Add(this.label_lovenum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(17, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 643);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户观影记录";
            // 
            // label_dislovenum
            // 
            this.label_dislovenum.AutoSize = true;
            this.label_dislovenum.Location = new System.Drawing.Point(540, 338);
            this.label_dislovenum.Name = "label_dislovenum";
            this.label_dislovenum.Size = new System.Drawing.Size(15, 15);
            this.label_dislovenum.TabIndex = 7;
            this.label_dislovenum.Text = "0";
            this.label_dislovenum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_lovenum
            // 
            this.label_lovenum.AutoSize = true;
            this.label_lovenum.Location = new System.Drawing.Point(540, 24);
            this.label_lovenum.Name = "label_lovenum";
            this.label_lovenum.Size = new System.Drawing.Size(15, 15);
            this.label_lovenum.TabIndex = 6;
            this.label_lovenum.Text = "0";
            this.label_lovenum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(492, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "总数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(492, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "总数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "不喜欢的电影";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column_Rating_,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView2.Location = new System.Drawing.Point(6, 356);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 27;
            this.dataGridView2.Size = new System.Drawing.Size(559, 280);
            this.dataGridView2.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // Column_Rating_
            // 
            this.Column_Rating_.HeaderText = "评分";
            this.Column_Rating_.Name = "Column_Rating_";
            this.Column_Rating_.ReadOnly = true;
            this.Column_Rating_.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "影片名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 180;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "上映日期";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "影片类型";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 220;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "喜欢的电影";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_ID,
            this.Column_Rating,
            this.Column_Name,
            this.Column_ReleaseDate,
            this.Column_Genres});
            this.dataGridView1.Location = new System.Drawing.Point(6, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(559, 280);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column_ID
            // 
            this.Column_ID.HeaderText = "编号";
            this.Column_ID.Name = "Column_ID";
            this.Column_ID.ReadOnly = true;
            this.Column_ID.Width = 60;
            // 
            // Column_Rating
            // 
            this.Column_Rating.HeaderText = "评分";
            this.Column_Rating.Name = "Column_Rating";
            this.Column_Rating.ReadOnly = true;
            this.Column_Rating.Width = 60;
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "影片名";
            this.Column_Name.Name = "Column_Name";
            this.Column_Name.ReadOnly = true;
            this.Column_Name.Width = 180;
            // 
            // Column_ReleaseDate
            // 
            this.Column_ReleaseDate.HeaderText = "上映日期";
            this.Column_ReleaseDate.Name = "Column_ReleaseDate";
            this.Column_ReleaseDate.ReadOnly = true;
            // 
            // Column_Genres
            // 
            this.Column_Genres.HeaderText = "影片类型";
            this.Column_Genres.Name = "Column_Genres";
            this.Column_Genres.ReadOnly = true;
            this.Column_Genres.Width = 220;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择用户";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(17, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 23);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "用户支持的关联规则";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "已观影数";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(198, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "未观影数";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(119, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(64, 25);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(201, 27);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(64, 25);
            this.textBox2.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(542, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 28);
            this.button2.TabIndex = 8;
            this.button2.Text = "产生推荐";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(469, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "推荐数量";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(470, 26);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(66, 25);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "20";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_rec_ID,
            this.Column_Rec_Degree,
            this.Column_rec_Name,
            this.Column_rec_date,
            this.Column_gen});
            this.dataGridView3.Location = new System.Drawing.Point(10, 24);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 27;
            this.dataGridView3.Size = new System.Drawing.Size(630, 396);
            this.dataGridView3.TabIndex = 11;
            // 
            // Column_rec_ID
            // 
            this.Column_rec_ID.HeaderText = "ID";
            this.Column_rec_ID.Name = "Column_rec_ID";
            this.Column_rec_ID.ReadOnly = true;
            this.Column_rec_ID.Width = 30;
            // 
            // Column_Rec_Degree
            // 
            this.Column_Rec_Degree.HeaderText = "推荐度";
            this.Column_Rec_Degree.Name = "Column_Rec_Degree";
            this.Column_Rec_Degree.ReadOnly = true;
            this.Column_Rec_Degree.Width = 150;
            // 
            // Column_rec_Name
            // 
            this.Column_rec_Name.HeaderText = "影片名";
            this.Column_rec_Name.Name = "Column_rec_Name";
            this.Column_rec_Name.ReadOnly = true;
            this.Column_rec_Name.Width = 200;
            // 
            // Column_rec_date
            // 
            this.Column_rec_date.HeaderText = "上映日期";
            this.Column_rec_date.Name = "Column_rec_date";
            this.Column_rec_date.ReadOnly = true;
            // 
            // Column_gen
            // 
            this.Column_gen.HeaderText = "影片类型";
            this.Column_gen.Name = "Column_gen";
            this.Column_gen.ReadOnly = true;
            this.Column_gen.Width = 240;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Location = new System.Drawing.Point(642, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(646, 426);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "推荐列表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(636, 456);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(646, 250);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "推荐结果分析";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label_unrating_id);
            this.groupBox6.Controls.Add(this.label_disloveid);
            this.groupBox6.Controls.Add(this.label_loveid);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Location = new System.Drawing.Point(6, 112);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(634, 128);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "推荐项目在测试集中的信息";
            // 
            // label_unrating_id
            // 
            this.label_unrating_id.AutoSize = true;
            this.label_unrating_id.Location = new System.Drawing.Point(73, 97);
            this.label_unrating_id.Name = "label_unrating_id";
            this.label_unrating_id.Size = new System.Drawing.Size(15, 15);
            this.label_unrating_id.TabIndex = 5;
            this.label_unrating_id.Text = "0";
            // 
            // label_disloveid
            // 
            this.label_disloveid.AutoSize = true;
            this.label_disloveid.Location = new System.Drawing.Point(72, 66);
            this.label_disloveid.Name = "label_disloveid";
            this.label_disloveid.Size = new System.Drawing.Size(15, 15);
            this.label_disloveid.TabIndex = 4;
            this.label_disloveid.Text = "0";
            // 
            // label_loveid
            // 
            this.label_loveid.AutoSize = true;
            this.label_loveid.Location = new System.Drawing.Point(73, 30);
            this.label_loveid.Name = "label_loveid";
            this.label_loveid.Size = new System.Drawing.Size(15, 15);
            this.label_loveid.TabIndex = 3;
            this.label_loveid.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 97);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "未评价:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 15);
            this.label15.TabIndex = 1;
            this.label15.Text = "不喜欢:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 0;
            this.label14.Text = "喜欢:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label_Reca);
            this.groupBox5.Controls.Add(this.label_F_value);
            this.groupBox5.Controls.Add(this.label_Prec);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label_Recall);
            this.groupBox5.Controls.Add(this.label_Precision);
            this.groupBox5.Location = new System.Drawing.Point(323, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(317, 91);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "评价准则与指标";
            // 
            // label_Reca
            // 
            this.label_Reca.AutoSize = true;
            this.label_Reca.Location = new System.Drawing.Point(72, 65);
            this.label_Reca.Name = "label_Reca";
            this.label_Reca.Size = new System.Drawing.Size(15, 15);
            this.label_Reca.TabIndex = 5;
            this.label_Reca.Text = "0";
            this.label_Reca.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_F_value
            // 
            this.label_F_value.AutoSize = true;
            this.label_F_value.Location = new System.Drawing.Point(238, 42);
            this.label_F_value.Name = "label_F_value";
            this.label_F_value.Size = new System.Drawing.Size(15, 15);
            this.label_F_value.TabIndex = 6;
            this.label_F_value.Text = "0";
            this.label_F_value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Prec
            // 
            this.label_Prec.AutoSize = true;
            this.label_Prec.Location = new System.Drawing.Point(72, 30);
            this.label_Prec.Name = "label_Prec";
            this.label_Prec.Size = new System.Drawing.Size(15, 15);
            this.label_Prec.TabIndex = 4;
            this.label_Prec.Text = "0";
            this.label_Prec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(73, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 15);
            this.label13.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(171, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "F1指标:";
            // 
            // label_Recall
            // 
            this.label_Recall.AutoSize = true;
            this.label_Recall.Location = new System.Drawing.Point(6, 65);
            this.label_Recall.Name = "label_Recall";
            this.label_Recall.Size = new System.Drawing.Size(60, 15);
            this.label_Recall.TabIndex = 1;
            this.label_Recall.Text = "查全率:";
            // 
            // label_Precision
            // 
            this.label_Precision.AutoSize = true;
            this.label_Precision.Location = new System.Drawing.Point(6, 30);
            this.label_Precision.Name = "label_Precision";
            this.label_Precision.Size = new System.Drawing.Size(60, 15);
            this.label_Precision.TabIndex = 0;
            this.label_Precision.Text = "查准率:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label_test_dislove);
            this.groupBox4.Controls.Add(this.label_test_love);
            this.groupBox4.Controls.Add(this.label_test_total);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(6, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(283, 82);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "用户测试集信息";
            // 
            // label_test_dislove
            // 
            this.label_test_dislove.AutoSize = true;
            this.label_test_dislove.Location = new System.Drawing.Point(221, 57);
            this.label_test_dislove.Name = "label_test_dislove";
            this.label_test_dislove.Size = new System.Drawing.Size(15, 15);
            this.label_test_dislove.TabIndex = 4;
            this.label_test_dislove.Text = "0";
            this.label_test_dislove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_test_love
            // 
            this.label_test_love.AutoSize = true;
            this.label_test_love.Location = new System.Drawing.Point(221, 21);
            this.label_test_love.Name = "label_test_love";
            this.label_test_love.Size = new System.Drawing.Size(15, 15);
            this.label_test_love.TabIndex = 4;
            this.label_test_love.Text = "0";
            this.label_test_love.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_test_total
            // 
            this.label_test_total.AutoSize = true;
            this.label_test_total.Location = new System.Drawing.Point(87, 42);
            this.label_test_total.Name = "label_test_total";
            this.label_test_total.Size = new System.Drawing.Size(15, 15);
            this.label_test_total.TabIndex = 3;
            this.label_test_total.Text = "0";
            this.label_test_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(140, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "不喜欢的:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "喜欢的:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "电影总数:";
            // 
            // Form_ARRec_Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 720);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form_ARRec_Demo";
            this.Text = "基于关联规则的推荐演示";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Rating_;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ReleaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Genres;
        private System.Windows.Forms.Label label_dislovenum;
        private System.Windows.Forms.Label label_lovenum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_rec_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Rec_Degree;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_rec_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_rec_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_gen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_test_love;
        private System.Windows.Forms.Label label_test_total;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_test_dislove;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label_Recall;
        private System.Windows.Forms.Label label_Precision;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_Prec;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_Reca;
        private System.Windows.Forms.Label label_F_value;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_unrating_id;
        private System.Windows.Forms.Label label_disloveid;
        private System.Windows.Forms.Label label_loveid;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}