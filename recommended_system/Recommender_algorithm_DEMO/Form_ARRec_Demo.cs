using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Recommendation_Algorithm;

namespace Recommender_algorithm_DEMO
{
    public partial class Form_ARRec_Demo : Form
    {
        public cUser[] sourceUsers;
        public cUser[] testUsers;
        public movieInfo[] objs_movieInfo;
        public int userid = 1;

        // 每个用户所支持的关联规则
        public static AssociationRule[][] supp_AssRules;

        // 每个用户所推荐的项目列表
        RecItemid_Degree[][] recItems;

        public Form_ARRec_Demo()
        {
            InitializeComponent();

          //  cReadinData obj_readData = new cReadinData(0);
            sourceUsers = cReadinData.getBaseUser();       // 读入训练数据
            testUsers = cReadinData.getTestUser();         // 读入测试数据

            supp_AssRules = new AssociationRule[sourceUsers.Length][];
            recItems = new RecItemid_Degree[sourceUsers.Length][];

            dataGridView1.RowHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView3.RowHeadersVisible = false;

            cItem obj = new cItem();
            objs_movieInfo = cItem.movies;

            for (int i = 1; i < testUsers.Length; i++)
            {
                int userid = testUsers[i].id;
                this.comboBox1.Items.Add(userid);
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            userid = testUsers[this.comboBox1.SelectedIndex + 1].id;  // 得到当前用户ID

            cUser currentUser = sourceUsers[userid];
            this.textBox1.Text = currentUser.RatingNums.ToString();
            this.textBox2.Text = (currentUser.Ratings.Length - currentUser.RatingNums - 1).ToString();
            bool[] b_isLoved = currentUser.discretizeRating();

            this.dataGridView1.Rows.Clear();
            this.dataGridView2.Rows.Clear();

            string sGenres = "";      // 电影类型
            int count_love = 0, count_dislove = 0;

            // 填充用户观影数据
            for (int itemid = 1; itemid < currentUser.Ratings.Length; itemid++)
            {
                double rating = currentUser.Ratings[itemid];
                if (rating != 0)
                {
                    sGenres = "";
                    for (int count_gen = 0; count_gen < objs_movieInfo[itemid].genres.Length; count_gen++)
                    {
                        // 形成影片类型字符串
                        if (objs_movieInfo[itemid].genres[count_gen] != "")
                        {
                            sGenres += objs_movieInfo[itemid].genres[count_gen] + " /";
                        }
                    }
                    if (sGenres != "")
                    {
                        sGenres = sGenres.Substring(0, sGenres.Length - 1);
                    }

                    // 喜欢这部电影
                    if (b_isLoved[itemid] == true)
                    {
                        // 添加电影信息记录
                        this.dataGridView1.Rows.Add(itemid, rating, objs_movieInfo[itemid].name,
                            objs_movieInfo[itemid].ReleaseDate, sGenres);
                        count_love++;
                    }

                    // 不喜欢这部电影
                    else
                    {
                        this.dataGridView2.Rows.Add(itemid, rating, objs_movieInfo[itemid].name,
                            objs_movieInfo[itemid].ReleaseDate, sGenres);
                        count_dislove++;
                    }
                }
            }
            // 喜欢与不喜欢的数量
            this.label_lovenum.Text = count_love.ToString();
            this.label_dislovenum.Text = count_dislove.ToString();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        // 查看当前用户支持的关联规则
        private void button1_Click(object sender, EventArgs e)
        {
            Form_Supp_AssRules obj_Form_SuppAssRules = new Form_Supp_AssRules();
            obj_Form_SuppAssRules.Show();

            // 得到ID为userid的用户所支持的关联规则集合
            supp_AssRules[userid] = cApriori.getSupport_AssRules(userid);
            
            obj_Form_SuppAssRules.dataGridView1.RowHeadersVisible = false;
            obj_Form_SuppAssRules.dataGridView1.Rows.Clear();

            for (int i = 0; i < supp_AssRules[userid].Length; i++)
            {
                int itemid_1 = supp_AssRules[userid][i]._itemid_1;
                int itemid_2 = supp_AssRules[userid][i]._itemid_2;

                obj_Form_SuppAssRules.dataGridView1.Rows.Add(i + 1, supp_AssRules[userid][i].Reco_degrees,
                   objs_movieInfo[itemid_1].name, objs_movieInfo[itemid_2].name, supp_AssRules[userid][i].Support,
                   supp_AssRules[userid][i].confidence);
            }
            
        }

        // 对当前用户产生Top-N推荐
        private void button2_Click(object sender, EventArgs e)
        {
            // 得到ID为userid的用户所支持的关联规则集合
            supp_AssRules[userid] = cApriori.getSupport_AssRules(userid);
            // 得到推荐电影列表
            recItems[userid] = cApriori.getRecItems(supp_AssRules[userid], userid);
            int n = int.Parse(this.textBox3.Text);       // 得到推荐数目

            // 所要求的推荐数目大于所能推荐的
            if (n >= recItems[userid].Length)
            {
                n = recItems[userid].Length-1;
            }
            this.dataGridView3.Rows.Clear();
            int itemid; 

            // 填充推荐列表
            string sGenres;
            for (int i = 1; i <= n; i++)
            {
                itemid = recItems[userid][i].recItems_id;
                
                
                sGenres = "";
                for (int count_gen = 0; count_gen < objs_movieInfo[itemid].genres.Length; count_gen++)
                {
                    // 形成影片类型字符串
                    if (objs_movieInfo[itemid].genres[count_gen] != "")
                    {
                        sGenres += objs_movieInfo[itemid].genres[count_gen] + " /";
                    }
                }
                if (sGenres != "")
                {
                    sGenres = sGenres.Substring(0, sGenres.Length - 1);
                }

                this.dataGridView3.Rows.Add( i, recItems[userid][i].rec_Degree, objs_movieInfo[itemid].name,
                          objs_movieInfo[itemid].ReleaseDate, sGenres);
            }

            // 结果分析
            // 得到测试用户在测试集中的索引
            int testIndex = this.comboBox1.SelectedIndex + 1;
            cUser currentTestUser = this.testUsers[testIndex];
            this.label_test_total.Text = currentTestUser.RatingNums.ToString();
            this.label_test_love.Text = currentTestUser.love_items_num.ToString();
            this.label_test_dislove.Text = (currentTestUser.RatingNums - currentTestUser.love_items_num).ToString();

            // 评价准则与指标
            cAssStrategy obj_AssStrategy = cApriori.getAssStrategy(currentTestUser, n);
            this.label_Prec.Text = obj_AssStrategy.Precison.ToString();
            this.label_Reca.Text = obj_AssStrategy.Recall.ToString();
            this.label_F_value.Text = obj_AssStrategy.calculateF_Measure().ToString();

            // 推荐项目在测试集中的信息
            ArrayList loveItems = cApriori.TestUserLoveItems_id;
            ArrayList dratItems = cApriori.TestUserDratItems_id;
            ArrayList unRatingItems = cApriori.TestUserUnRatingItems_id;

            string sLoveIDs = "", sDratIDs = "", sUnRatingIDs = "";
           
            // 喜欢的推荐项目ID
            foreach (int value in loveItems)
            {
                sLoveIDs += value + " | ";
            }
            // 不喜欢的推荐项目ID
            foreach (int value in dratItems)
            {
                sDratIDs += value + " | "; 
            }
            // 未评价的推荐项目ID
            foreach (int value in unRatingItems)
            {
                sUnRatingIDs += value + " | ";
            }

            this.label_loveid.Text = sLoveIDs.ToString();
            this.label_disloveid.Text = sDratIDs.ToString();
            this.label_unrating_id.Text = sUnRatingIDs.ToString();
        }
    }
}
