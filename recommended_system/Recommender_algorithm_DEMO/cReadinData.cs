using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Recommendation_Algorithm
{  
    class cReadinData
    {
        // 训练数据文件名
        public string[] sTrainFileName = { "u1.base","u2.base","u3.base","u4.base","u5.base" };
        
        // 测试数据文件名
        public string[] testfileName = { "u1.test", "u2.test", "u3.test", "u4.test", "u5.test"};

        // 各个测试集中用户的数目
        public static int[] test_usernum = { 459, 654, 869, 923, 927, 943 };

        // 测试用户集合
        public static cUser[] testUser;

        // 训练用户集合
        public static cUser[] baseUser = new cUser[cReadinData.totalUserNum + 1];

        // 训练集中总的用户数
        public static int totalUserNum = 943;

        // 训练集中所有的用户集合，索引从1开始
        public static cUser[] objUser = new cUser[totalUserNum + 1];


        /* --------- 方法 ---------- */

        // 构造方法,读入
        public cReadinData(int iFileNumber)
        {
            readTrainData(iFileNumber);
            readTestData(iFileNumber);
        }

        /// <summary>
        /// 读入训练数据
        /// </summary>
        /// <param name="iTrainFileNumber">数据集的选择</param>
        private void readTrainData(int iTrainFileNumber)
        {
            string sLine = "";
            StreamReader rs = null;
            try
            {
                rs = new StreamReader(sTrainFileName[iTrainFileNumber], Encoding.Default);
            }
            catch (Exception e)
            {
                MessageBox.Show("未找到数据文件","ERROR");
                return;
            }
            int user, item, rating;
            int countUser = 0, prev = 0, countRating = 0;

            while (sLine != null)
            {
                sLine = rs.ReadLine();

                if (sLine == null)
                {
                    objUser[countUser].RatingNums = countRating;
                    break;
                }

                string sUser = sLine.Substring(0, sLine.IndexOf('\t'));
                string temp = sLine.Substring(sUser.Length + 1);
                string sItem = temp.Substring(0, temp.IndexOf('\t'));
                temp = sLine.Substring(sUser.Length + sItem.Length + 2, 1);

                user = int.Parse(sUser);

                // 新用户
                if (prev != user)
                {
                    prev = user;

                    if (countUser != 0)
                    {
                        objUser[countUser].RatingNums = countRating;
                    }
                    countRating = 0;
                    objUser[++countUser] = new cUser(user);
                }

                item = int.Parse(sItem);
                rating = int.Parse(temp);
                countRating++;

                objUser[countUser].Ratings[item] = rating;
            }
            //   Console.WriteLine("Total User num:{0}", this.objUser.Length);
        }

        /// <summary>
        /// 读入测试数据
        /// </summary>
        /// <param name="iFileNumber">数据集的选择</param>
        private void readTestData(int iFileNumber)
        {
            testUser = new cUser[test_usernum[iFileNumber] + 1];
            string sLine = "";

            StreamReader rs = new StreamReader(testfileName[iFileNumber], Encoding.Default);
            int user, item, rating;
            int countUser = 0, prev = 0, countRating = 0;

            while (sLine != null)
            {
                sLine = rs.ReadLine();

                if (sLine == null)
                {
                    testUser[countUser].RatingNums = countRating;
                    break;
                }

                string sUser = sLine.Substring(0, sLine.IndexOf('\t'));
                string temp = sLine.Substring(sUser.Length + 1);
                string sItem = temp.Substring(0, temp.IndexOf('\t'));
                temp = sLine.Substring(sUser.Length + sItem.Length + 2, 1);

                user = int.Parse(sUser);

                // 新用户
                if (prev != user)
                {
                    prev = user;

                    if (countUser != 0)
                    {
                        testUser[countUser].RatingNums = countRating;
                    }
                    countRating = 0;
                    testUser[++countUser] = new cUser(user);
                }

                item = int.Parse(sItem);
                rating = int.Parse(temp);
                countRating++;

                testUser[countUser].Ratings[item] = rating;
            }
        }


        // 得到训练集中的用户集合
        public static cUser[] getBaseUser()
        {

            return objUser;
        }


        // 得到测试集中的用户集合
        public static cUser[] getTestUser()
        {
            return testUser;
        }

        public static int getDataSparseDegrees()
        {
            int totalRatingNums = 0;
            for (int count = 1; count < objUser.Length; count++)
            {
                totalRatingNums += objUser[count].RatingNums;
            }
            return totalRatingNums;
        }
    }
}
