using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Recommendation_Algorithm;

namespace Recommender_algorithm_DEMO
{
    public partial class Form_SlopeOne : Form
    {
        static private Regex r = new Regex("^[0-9]+$");

        // 测试用户集合
        private cUser[] testUsers;
        // 训练用户集合
        private cUser[] trainUsers;

        // 读取数据的对象
        private cReadinData obj_ReadData; 

        // SlopeOne 算法对象
        private SlopeOne obj_SlopeOne;

        private Dictionary<int, float> userRating;

        private cAssStrategy obj_AssStrategy;

        private double total_MAE = 0, total_Precison = 0, total_Recall = 0, total_F_Measure = 0, total_Time = 0;

        public static int count_dgv = 1;
        private int Rec_Items_num;    // Top-N 推荐项目数

        private static int[] count_Num = new int[14];
        private static double[] stat_Info = new double[14];
        private int userid = 1;


        public Form_SlopeOne()
        {
            InitializeComponent();
            
            // 默认选择第一个数据集
            this.comboBox1.SelectedIndex = 0;

            // 读取数据，得到训练用户集合以及测试用户集合
            obj_ReadData = new cReadinData(this.comboBox1.SelectedIndex);
            obj_AssStrategy = new cAssStrategy();
            testUsers = cReadinData.getTestUser();
            trainUsers = cReadinData.getBaseUser();

            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersVisible = false;

            for (int i = 0; i < count_Num.Length; i++)
            {
                count_Num[i] = 0;
                stat_Info[i] = 0;
            }

                // 根据选择的数据集，填充用户ID的下拉列表
                for (int i = 0; i < testUsers.Length - 1; i++)
                {
                    this.comboBox2.Items.Add(testUsers[i + 1].id);
                }
            // 用户ID默认选择第一个
            this.comboBox2.SelectedIndex = 0;

            obj_SlopeOne = new SlopeOne();
           
            for (int i = 1; i < trainUsers.Length; i++)
            {
                userRating = new Dictionary<int, float>();

                //count_Num[trainUsers[i].RatingNums/20]++;

                for (int j = 1; j < trainUsers[i].Ratings.Length; j++)
                {
                    if (trainUsers[i].Ratings[j] != 0)
                    {
                        userRating.Add(j, (float)trainUsers[i].Ratings[j]);
                    }
                }

                obj_SlopeOne.AddUserRatings(userRating);
            }

            this.comboBox3.Items.Clear();
        
            // 填充 连续运行用户数目 下拉列表
            for (int i = 15; i < testUsers.Length; i++)
            {
                comboBox3.Items.Add(i);
            }
            this.comboBox3.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 清除用户ID下拉列表
            this.comboBox2.Items.Clear();

            // 读取数据，得到训练用户集合以及测试用户集合
            obj_ReadData = new cReadinData(this.comboBox1.SelectedIndex);
            testUsers = cReadinData.getTestUser();
            trainUsers = cReadinData.getBaseUser();

            // 根据选择的数据集，填充用户ID的下拉列表
            for (int i = 0; i < testUsers.Length-1; i++)
            {
                this.comboBox2.Items.Add( testUsers[i+1].id );
            }
           
            // 用户ID默认选择第一个
            this.comboBox2.SelectedIndex = 0;

            this.comboBox3.Items.Clear();
           
            // 填充 连续运行用户数目 下拉列表
            for (int i = 15; i < testUsers.Length; i++)
            {
                comboBox3.Items.Add(i);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 设置当前用户测试集合中已评分的项目数量
            textBox1.Text = trainUsers[comboBox2.SelectedIndex + 1].RatingNums.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // 记录当前时间
            int count_predit = 0;
            DateTime dt_1 = DateTime.Now;
            Rec_Items_num = int.Parse( this.textBox9.Text );
            userRating = new Dictionary<int, float>();

            // 得到用户
            cUser testUser = testUsers[comboBox2.SelectedIndex + 1];

            userid = testUser.id;

            userRating = new Dictionary<int, float>();

            for (int j = 1; j < trainUsers[userid].Ratings.Length; j++)
            {
                if (trainUsers[userid].Ratings[j] != 0)
                {
                    userRating.Add(j, (float)trainUsers[userid].Ratings[j]);
                }
            }
        //    count_predit = trainUsers[userID].Ratings.Length - trainUsers[userID].RatingNums;
            
            // 得到该用户的预测评分
            IDictionary<int, float> Predictions = obj_SlopeOne.Predict(userRating);
            obj_AssStrategy = obj_SlopeOne.getAssStrategy(userid, Predictions, Rec_Items_num, testUser);

            DateTime dt_2 = DateTime.Now;
            TimeSpan ts = dt_2.Subtract(dt_1);

            this.textBox4.Text = obj_AssStrategy.MAE.ToString();        // MAE
            this.textBox5.Text = ts.TotalMilliseconds + "ms";           // 时间
            this.textBox6.Text = obj_AssStrategy.Precison.ToString();   // 查准率
            this.textBox8.Text = obj_AssStrategy.Recall.ToString();     // 查全率
            float F = obj_AssStrategy.calculateF_Measure();             // F1指标
            this.textBox7.Text = F.ToString(); 
            this.textBox2.Text = this.Rec_Items_num.ToString();         // Top-N 推荐数

            this.textBox3.Text = "MAE:" + obj_AssStrategy.MAE + " 查准率:" + obj_AssStrategy.Precison + " 查全率:" + 
                obj_AssStrategy.Recall + " F值:" + F + " 总耗时:" + ts.TotalMilliseconds + "ms";
       //     this.dataGridView1.Rows.Add(count_dgv++, trainUsers[userIndex].id, this.Rec_Items_num, trainUsers[userIndex].RatingNums, obj_AssStrategy.MAE, obj_AssStrategy.Precison,
         //       obj_AssStrategy.Recall, F, ts.TotalMilliseconds + " ms");
            Application.DoEvents();

            stat_Info[trainUsers[userid].RatingNums / 50] += obj_AssStrategy.MAE;
            count_Num[trainUsers[userid].RatingNums / 50]++;
            // 累加相关数据
            total_MAE += obj_AssStrategy.MAE;
            total_Precison += obj_AssStrategy.Precison;
            total_Recall += obj_AssStrategy.Recall;
            total_F_Measure += F;
            total_Time += ts.TotalMilliseconds;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt_1 = DateTime.Now;
            total_MAE = total_Precison = total_Recall = total_F_Measure = 0;

            Rec_Items_num = int.Parse(this.textBox9.Text);        // Top-N 推荐数量

            int usernum = (int)this.comboBox3.SelectedIndex + 1;       // 连续运行用户数

            this.progressBar1.Maximum = usernum;
            this.progressBar1.Value = 0;
            for (int i = 0; i < usernum; i++)
            {
                this.comboBox2.SelectedIndex = i;
                this.button1_Click(sender, e);
                this.progressBar1.Value++;
            }
            double average_MAE = total_MAE / usernum;
            double average_Precison = total_Precison / usernum;
            double average_Recall = total_Recall / usernum;
            double average_F_Measure = total_F_Measure / usernum;

         //   this.dataGridView2.RowHeadersVisible = false;

            string s;
            double[] MAE_result = new double[count_Num.Length];
            int start_num = 0, total_num = 0;
            double tot_MAE = 0;
            for (int i = 0; i < MAE_result.Length; i++, start_num += 50)
            {
                total_num += count_Num[i];
                tot_MAE += stat_Info[i];
                s = start_num.ToString() + @" - " + (start_num + 49).ToString(); 
                MAE_result[i] = stat_Info[i] / count_Num[i];
                this.dataGridView2.Rows.Add(i+1, s, count_Num[i], stat_Info[i], MAE_result[i]);
            }
            this.dataGridView2.Rows.Add(0, "", total_num, tot_MAE, tot_MAE / (double)total_num);
            DateTime dt_2 = DateTime.Now;
            TimeSpan ts = dt_2.Subtract(dt_1);

            this.textBox4.Text = average_MAE.ToString();            // MAE
            this.textBox5.Text = ts.TotalMilliseconds + "ms";       // 时间
            this.textBox6.Text = average_Precison.ToString();       // 查准率
            this.textBox8.Text = average_Recall.ToString();         // 查全率
            this.textBox7.Text = average_F_Measure.ToString();      // F1指标
            this.textBox2.Text = this.Rec_Items_num.ToString();     // Top-N 推荐数
           
            this.textBox3.Text = "平均MAE:" + average_MAE.ToString() + " 平均查准率:" + average_Precison +
                " 平均查全率:" + average_Recall + " 平均F值:" + average_F_Measure + " 总耗时:" + ts.TotalMilliseconds + "ms";

            this.dataGridView1.Rows.Add(count_dgv++, usernum, this.Rec_Items_num,"", average_MAE, average_Precison, average_Recall,
                average_F_Measure, ts.TotalMilliseconds / usernum + " ms");
            Application.DoEvents();
        }
    }
}
