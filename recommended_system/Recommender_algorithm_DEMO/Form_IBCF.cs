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
    public partial class Form_IBCF : Form
    {

        static private Regex r = new Regex("^[0-9]+$");

        // 最近邻居个数，默认为4
        private int neigh_num = 4;

        // 相似度算法的选择，默认为余弦相似度
        private int sim_alg = 1;

        // 测试用户数目，默认为15
        private int testUserNum = 15;

        // 算法评价准则与指标
        private cAssStrategy obj_AssStrategy;

        private string[] sSimAlg = { "余弦相似度", "Pearson相似度", "修正的余弦相似度" };

        public static int count_dgv = 1;
        private int Rec_Items_num;               // Top-N 推荐项目数

        cItemBased_CF obj_ItemBased_CF;

        public Form_IBCF()
        {
            InitializeComponent();

            this.comboBox1.Enabled = false;
            obj_AssStrategy = new cAssStrategy();

            this.comboBox2.Items.Clear();

            for (int i = 15; i < cReadinData.test_usernum[comboBox1.SelectedIndex]; i++)
            {
                this.comboBox2.Items.Add(i);
            }

            dataGridView1.RowHeadersVisible = false;
            // 初始化，最近邻居数量最大为200
            obj_ItemBased_CF = new cItemBased_CF(200);

            for (int i = 1; i <= 1682; i++)
            {
                obj_ItemBased_CF.generateItemNN(i);
            }
            // 读取最近邻项目及相似值文件，避免重复计算
            obj_ItemBased_CF.readFile();

            this.comboBox2.SelectedIndex = 0;
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

        // 运行一次算法
        private void button1_Click(object sender, EventArgs e)
        {
            // 记录当前时间
            DateTime dt_1 = DateTime.Now;

            this.Rec_Items_num = int.Parse(this.textBox13.Text);    // Top-N推荐个数

            this.progressBar1.Maximum = this.comboBox2.SelectedIndex + 15 + 22;
            this.progressBar1.Value = 0;
            // 读入数据，生成UI矩阵
            this.textBox3.Text = "开始读入数据";
            this.progressBar1.Value++;

            Application.DoEvents();

            cReadinData obj_ReadData = new cReadinData(comboBox1.SelectedIndex);

            this.textBox3.Text = "读入数据完成   训练数据：" + obj_ReadData.sTrainFileName[comboBox1.SelectedIndex]
                + "     测试数据：" + obj_ReadData.testfileName[comboBox1.SelectedIndex];
            this.progressBar1.Value++;
            Application.DoEvents();

            int number = int.Parse(textBox2.Text);
            this.neigh_num = number;

            // 相似度算法的选择
            if (this.radioButton1.Checked)
            {
                sim_alg = 1;
            }
            else if (this.radioButton2.Checked)
            {
                sim_alg = 2;
            }
            else if (this.radioButton3.Checked)
            {
                sim_alg = 3;
            }

            // 测试用户数目，最少为15
            testUserNum = this.comboBox2.SelectedIndex + 15;

            this.textBox3.Text = "初始化最大邻居个数";
            // 初始化，最近邻居数量最大为200
            obj_ItemBased_CF = new cItemBased_CF(200);

            this.progressBar1.Value++;
            Application.DoEvents();

            // 初始化相关数据
            for (int i = 1; i <= 1682; i++)
            {
                this.textBox3.Text = "初始 " + i + " 个项目数据";
                if ( (i % 100) == 0)
                {
                    this.progressBar1.Value++;
                    Application.DoEvents();
                }
                
                Application.DoEvents();
                obj_ItemBased_CF.generateItemNN(i);
            }

            this.progressBar1.Value++;
            Application.DoEvents();

            this.textBox3.Text = "读取最近邻居及其相似值文件";
            obj_ItemBased_CF.readFile();
            
            this.progressBar1.Value++;
            Application.DoEvents();

            // 得到测试用户集合
            cUser[] testUsers = cReadinData.getTestUser();
            this.textBox3.Text = "读取测试用户集合";
          
            this.progressBar1.Value++;
            Application.DoEvents();

            double MAE, Precison, Recall, F_Measure;
            double total_MAE = 0, total_Precison = 0, total_Recall = 0, total_F_Measure = 0;
            double average_MAE, average_Precison, average_Recall, average_F_Measure;

            // for循环为每个测试用户产生预测评分以及Top-N推荐，并取得算法评价指标
            for (int i = 1; i <= testUserNum; i++)
            {
                obj_AssStrategy = obj_ItemBased_CF.getPredictRating(testUsers[i], this.sim_alg, neigh_num, Rec_Items_num);

                MAE = obj_AssStrategy.MAE;
                Precison = obj_AssStrategy.Precison;
                Recall = obj_AssStrategy.Recall;
                F_Measure = obj_AssStrategy.calculateF_Measure();

                // 累计各项指标的和
                total_MAE += MAE;
                total_Precison += Precison;
                total_Recall += Recall;
                total_F_Measure += F_Measure;

                this.textBox3.Text = "第" + i.ToString() + "个用户 MAE:" + MAE.ToString() + " 查准率:" + Precison 
                    + " 查全率:" + Recall + " F值:" + F_Measure;
                this.progressBar1.Value ++;
                Application.DoEvents();
       
            }

            // 计算各项指标的平均值
            average_MAE = total_MAE / this.testUserNum;
            average_Precison = total_Precison / this.testUserNum;
            average_Recall = total_Recall / this.testUserNum;
            average_F_Measure = total_F_Measure / this.testUserNum;
            

            DateTime dt_2 = DateTime.Now;
            TimeSpan ts = dt_2.Subtract(dt_1);

            this.textBox3.Text = "完成 平均MAE:" + average_MAE.ToString() + " 平均查准率:" + average_Precison +
                " 平均查全率:" + average_Recall + " 平均F值:" + average_F_Measure + " 总耗时:" + ts.TotalMilliseconds + "ms";
            Application.DoEvents();

            this.textBox4.Text = average_MAE.ToString();
            this.textBox5.Text = ts.TotalMilliseconds + "ms";
            this.textBox6.Text = this.sSimAlg[this.sim_alg - 1];
            this.textBox7.Text = this.neigh_num.ToString();
            this.textBox9.Text = average_Precison.ToString();
            this.textBox10.Text = average_Recall.ToString();
            this.textBox11.Text = average_F_Measure.ToString();
            this.textBox12.Text = Rec_Items_num.ToString();

            string log = this.sSimAlg[this.sim_alg - 1] + " 邻居数:" + this.neigh_num.ToString() + " 平均MAE:" + 
                average_MAE.ToString() + " 平均查准率:" + average_Precison + " 平均查全率:" + average_Recall + 
                " 平均F值:" + average_F_Measure + " 总耗时:" + ts.TotalMilliseconds + "ms";

            this.dataGridView1.Rows.Add(count_dgv++, sSimAlg[sim_alg - 1], this.neigh_num, this.Rec_Items_num, average_MAE,
               average_Precison, average_Recall, average_F_Measure, (ts.TotalMilliseconds / this.testUserNum) + " ms");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!r.IsMatch(textBox2.Text))
            {
                MessageBox.Show("请输入数字");
                textBox2.Text = this.neigh_num.ToString();
            }

            int number = int.Parse(textBox2.Text);

            // 最近邻居的个数限定在 4 - 60 之间 
            if ((number >= 4) && (number <= 60))
            {
                this.neigh_num = number;
            }
        }

        // 连续运行算法
        private void button2_Click(object sender, EventArgs e)
        {
            int start_neigh = 4;
            int end_neigh = 60;
            if ((this.textBox1.Text != "") && (this.textBox8.Text != ""))
            {
                start_neigh = int.Parse(this.textBox1.Text);
                end_neigh = int.Parse(this.textBox8.Text);
            }
            for (int i = start_neigh; i <= end_neigh; i += 4)
            {
                this.textBox2.Text = i.ToString();
                this.button1_Click(sender, e);
            }
        }

        private void Form_IBCF_Load(object sender, EventArgs e)
        {

        }
    }
}
