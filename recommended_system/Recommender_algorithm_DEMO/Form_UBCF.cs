using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Recommendation_Algorithm;

namespace Recommender_algorithm_DEMO
{
    public partial class Form_UBCF : Form
    {
        static private Regex r = new Regex("^[0-9]+$");

        // 最近邻居个数，默认为5
        private int neigh_num = 5;
        
        // 相似度算法的选择，默认为余弦相似度
        private int sim_alg = 1;

        // 测试用户数目，默认为15
        private int testUserNum = 15;

        // 算法评价准则与指标
        private cAssStrategy obj_AssStrategy;

        private string[] sSimAlg = { "余弦相似度", "Pearson相似度", "修正的余弦相似度" };

        public static int count_dgv = 1;
        private int Rec_Items_num;    // Top-N 推荐项目数

        public Form_UBCF()
        {
            InitializeComponent();

            dataGridView1.RowHeadersVisible = false;

            this.comboBox2.Items.Clear();
            obj_AssStrategy = new cAssStrategy();

            for (int i = 15; i < cReadinData.test_usernum[comboBox1.SelectedIndex]; i++)
            {
                this.comboBox2.Items.Add(i);
            }
            this.comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt_1 = DateTime.Now;
            this.Rec_Items_num = int.Parse(this.textBox13.Text);    // Top-N推荐个数

            this.progressBar1.Maximum = (this.comboBox2.SelectedIndex + 15) * 10 + 5;
            this.progressBar1.Value = 0;
            // 读入数据，生成UI矩阵
            this.textBox3.Text = "开始读入数据";
            this.progressBar1.Value++;
          
            Application.DoEvents();

            cReadinData obj_ReadData = new cReadinData(comboBox1.SelectedIndex);

            this.textBox3.Text = "读入数据完成   训练数据：" + obj_ReadData.sTrainFileName[comboBox1.SelectedIndex]
                + "     测试数据：" + obj_ReadData.testfileName[comboBox1.SelectedIndex];
            this.progressBar1.Value += 2;
         
            Application.DoEvents();

            // 读取最近邻居个数
            int number = int.Parse(textBox2.Text);
            this.neigh_num = number;
            
            // 相似度算法的选择
            if(this.radioButton1.Checked)
            {
                sim_alg = 1;
            }
            else if(this.radioButton2.Checked)
            {
                sim_alg = 2;
            }
            else if(this.radioButton3.Checked)
            {
                sim_alg = 3;
            }
   
            // 测试用户数目，最少为15
            testUserNum = this.comboBox2.SelectedIndex + 15;

            cUserBased_CF obj_UserBased_CF = new cUserBased_CF(this.neigh_num);
            cUser[] testUsers = cReadinData.getTestUser();

            this.textBox3.Text = "初始化完成 相似度算法:" + sim_alg.ToString() +
                " 最近邻居个数:" + this.neigh_num.ToString() + " 测试用户数:" + testUserNum.ToString();
            this.progressBar1.Value += 2;
            Application.DoEvents();

            double MAE_1, Precison, Recall, F_Measure;
            double total_MAE = 0, total_Precison = 0, total_Recall = 0, total_F_Measure = 0;
            double average_MAE, average_Precison, average_Recall, average_F_Measure;
            

            for (int i = 1; i <= this.testUserNum; i++)
            {
                this.progressBar1.Value += 5;
                obj_AssStrategy = obj_UserBased_CF.getPredictRating(testUsers[i], this.sim_alg, Rec_Items_num);
               
                // 取得各项算法评价指标
                MAE_1 = obj_AssStrategy.MAE;
                Precison = obj_AssStrategy.Precison;
                Recall = obj_AssStrategy.Recall;
                F_Measure = obj_AssStrategy.calculateF_Measure();

                // 累计各项指标的和
                total_MAE += MAE_1;
                total_Precison += Precison;
                total_Recall += Recall;
                total_F_Measure += F_Measure;

                this.textBox3.Text = "第 " + i.ToString() + " 个用户计算完成.";

                this.progressBar1.Value += 5;
                Application.DoEvents();
            }
            // 计算各个评价准则的平均值
            average_MAE = total_MAE / this.testUserNum;
            average_Precison = total_Precison / this.testUserNum;
            average_Recall = total_Recall / this.testUserNum;
            average_F_Measure = total_F_Measure / this.testUserNum;

            DateTime dt_2 = DateTime.Now;
            TimeSpan ts = dt_2.Subtract(dt_1);

            this.textBox3.Text = "所有用户计算完成   总耗时:" + ts.TotalMilliseconds + " ms";
            Application.DoEvents();

            this.textBox4.Text = average_MAE.ToString();
            this.textBox5.Text = ts.TotalMilliseconds + " ms";
            this.textBox6.Text = this.sSimAlg[this.sim_alg - 1];
            this.textBox7.Text = this.neigh_num.ToString();
            this.textBox9.Text = average_Precison.ToString();
            this.textBox10.Text = average_Recall.ToString();
            this.textBox11.Text = average_F_Measure.ToString();
            this.textBox12.Text = "20";

            this.dataGridView1.Rows.Add(count_dgv++, sSimAlg[sim_alg - 1],  this.neigh_num, this.Rec_Items_num,average_MAE,
                average_Precison, average_Recall, average_F_Measure, ( ts.TotalMilliseconds / this.testUserNum) + " ms" );
 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();

            for (int i = 15; i < cReadinData.test_usernum[comboBox1.SelectedIndex]; i++)
            {
                this.comboBox2.Items.Add(i);
            }
            this.comboBox2.SelectedIndex = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!r.IsMatch(textBox2.Text))
            {
                MessageBox.Show("请输入数字");
                textBox2.Text = this.neigh_num.ToString();
            }

            int number = int.Parse(textBox2.Text);

            // 最近邻居的个数限定在 5 - 200 之间 
            if ( ( number >= 5) && (number <= 200 ) )
            {
                this.neigh_num = number;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int start_neigh = 5; 
            int end_neigh = 60;
            if( (this.textBox1.Text != "") && (this.textBox8.Text != ""))
            {
                start_neigh = int.Parse(this.textBox1.Text);
                end_neigh = int.Parse(this.textBox8.Text);
            }
            for (int i = 5; i <= 60; i += 5)
            {
                this.textBox2.Text = i.ToString();
                this.button1_Click(sender, e);
            }
        }
    }
}
