using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recommendation_Algorithm;

namespace Recommender_algorithm_DEMO
{
    public partial class Form_Apriori : Form
    {
        public static ArrayList association_Rules = new ArrayList();

        public static cApriori obj_Apriori = new cApriori();

        // 每个用户所支持的关联规则
        public static AssociationRule[][] supp_AssRules;

        // 每个用户所推荐的项目列表
        public static RecItemid_Degree[][] recItems;

        // 测试用户集
        public static cUser[] testUsers;
        // 训练用户集
        public static cUser[] sourceUsers;

        public Form_Apriori()
        {
            InitializeComponent();

            // 读入用户数据
            cReadinData obj_readData = new cReadinData(0);
            testUsers = cReadinData.getTestUser();
            sourceUsers = cReadinData.getBaseUser();

            // 初始化支持的关联规则集合
            supp_AssRules = new AssociationRule[sourceUsers.Length][];
            // 初始化推荐项目集合
            recItems = new RecItemid_Degree[sourceUsers.Length][];

            // 填充comboBox1
            for (int i = 15; i < testUsers.Length; i++)
            {
                this.comboBox1.Items.Add(i);
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void Form_Apriori_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_AssociationRules obj_Form_AssRules = new Form_AssociationRules();
            obj_Form_AssRules.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 推荐演示
            Form_ARRec_Demo obj_Form_ARRec_Demo = new Form_ARRec_Demo();
            obj_Form_ARRec_Demo.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // 产生频繁1-项集
        private void button2_Click(object sender, EventArgs e)
        {
            obj_Apriori.genFreq_one_itemsets();
            
            // 数量
            this.textBox1.Text = cApriori.freq_one_items.Count.ToString();
            // 支持度计数
            this.textBox3.Text = Freq_Item.min_support_count.ToString();
            // 支持度阈值
            this.textBox2.Text = Freq_Item.min_support.ToString();

            this.textBox7.Text = "频繁1-项集产生完成.";
            this.button6.Enabled = true;
        }

        // 产生频繁2-项集
        private void button6_Click(object sender, EventArgs e)
        {
            obj_Apriori.genFreq_two_itemsets();

            // 数量
            this.textBox6.Text = cApriori.freq_itemsets.Count.ToString();
            // 支持度计数
            this.textBox4.Text = Frequent_Itemset.min_support_count.ToString();
            // 支持度阈值
            this.textBox5.Text = Frequent_Itemset.min_support.ToString();

            this.textBox7.Text = "频繁2-项集产生完成.";
            this.button4.Enabled = true;
        }

        // 产生关联规则
        private void button4_Click(object sender, EventArgs e)
        {
            obj_Apriori.genAssoc_rules();

            // 数量
            this.textBox9.Text = cApriori.association_Rules.Count.ToString();
            // 支持度阈值
            this.textBox8.Text = Frequent_Itemset.min_support.ToString();
            // 置信度阈值
            this.textBox10.Text = AssociationRule.min_confidence.ToString();

            this.textBox7.Text = "关联规则产生完成.";

            this.button3.Enabled = true;    // 可进行推荐演示
            this.button1.Enabled = true;    // 可查看关联规则库
            this.button5.Enabled = true;    // 可连续运行算法
        }

        // 开始运行
        private void button5_Click(object sender, EventArgs e)
        {
            int test_num = this.comboBox1.SelectedIndex + 15;    // 测试用户数量
            int N = int.Parse(this.textBox11.Text);              // 推荐数目
            cUser curTestUser;                                   // 当前测试用户
            cAssStrategy obj_AssStrategy = new cAssStrategy();   // 评价准则与指标

            this.textBox7.Text = "测试用户数:" + test_num.ToString()
                + "  Top-N 推荐数:" + N;
            this.progressBar1.Maximum = (test_num-1) * 3; ;          // 进度条最大值
            this.progressBar1.Value = 0;

            //////////////////////////////////////////////////////////////////////////
            double total_N = 0, total_Precison = 0, total_Recall = 0, total_F = 0, total_Time = 0;
            double Precison, Recall, F_Measure, Time;
            DateTime dt_1, dt_2;
            TimeSpan ts;
            int real_RecNum = N;
            
            // 对测试集中的用户开始产生推荐
            int userid;
            for (int i = 0; i < test_num-1; i++)
            {
                curTestUser = testUsers[i + 1];
                userid = curTestUser.id;

                dt_1 = DateTime.Now;                    // 获取当前时间
                // 得到ID为userid的用户所支持的关联规则集合
                supp_AssRules[userid] = cApriori.getSupport_AssRules(userid);
                this.textBox7.Text = "第 " + (i + 1) + " 个用户所支持的关联规则生成.";
                this.progressBar1.Value++;
                Application.DoEvents();
                
                // 得到推荐电影列表
                recItems[userid] = cApriori.getRecItems(supp_AssRules[userid], userid);


                real_RecNum = (recItems[userid].Length > N ? N : recItems[userid].Length);
                this.textBox7.Text = "第 " + (i + 1) + " 个用户的推荐列表生成.";
                this.progressBar1.Value++;
                Application.DoEvents();

                // 评价准则与指标的计算
                obj_AssStrategy = cApriori.getAssStrategy(curTestUser, N);

                dt_2 = DateTime.Now;
                ts = dt_2.Subtract(dt_1);                              // 时间间隔
                Time = ts.TotalMilliseconds;

                Precison = obj_AssStrategy.Precison;
                Recall = obj_AssStrategy.Recall;
                F_Measure = obj_AssStrategy.calculateF_Measure();

           //     this.label22.Text = userid.ToString();                 // 用户ID

            //    this.label35.Text = real_RecNum.ToString();        // 实际推荐数目    
                total_N += real_RecNum;

                this.label20.Text = Precison.ToString();     // 查准率
                total_Precison += Precison;

                this.label23.Text = Recall.ToString();       // 查全率
                total_Recall += Recall;

                this.label24.Text = F_Measure.ToString();  // F值
                total_F += F_Measure;

                this.label37.Text = Time + " ms";           // 算法运行时间
                total_Time += Time;
                
                this.textBox7.Text = "完成第 " + (i + 1) + " 个用户的结果分析.";
                this.progressBar1.Value++;
                Application.DoEvents();
            }
            // 计算平均值
            int num = test_num - 1;
            double average_N = ((double)total_N / (double)num);
            double average_Precison = (double) ( (double)total_Precison / num );
            double average_Recall = (double) ( (double)total_Recall / num );
            double average_F = (double) ( (double)total_F / num );
            double average_Time = (double)total_Time / num;

            this.label29.Text = average_N.ToString();
            this.label30.Text = average_Precison.ToString();
            this.label31.Text = average_Recall.ToString();
            this.label32.Text = average_F.ToString();
            this.label33.Text = average_Time.ToString() + " ms";
            Application.DoEvents();
        }


    }
}
