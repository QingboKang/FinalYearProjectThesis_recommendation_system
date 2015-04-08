using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    // 用户类
    public class cUser
    {
        // 用户id
        public int id;

        // 用户打分的项目数目
        public int RatingNums;

        // 用户评分数组
        public double[] Ratings;

        // 用户喜欢的项目id数组
        private int[] love_items_id;
        // 用户喜欢的项目数量
        public int love_items_num = 0;

        public cUser(int u)
        {
            id = u;
            Ratings = new double[1683];
            RatingNums = 0;
            for (int count = 0; count < Ratings.Length; count++)
            {
                Ratings[count] = 0;
            }
        }

        // 获得用户对项目总的评分
        public int getTotalRating()
        {
            int totalRating = 0;
            for (int count = 1; count < 1683; count++)
            {
                totalRating += (int)this.Ratings[count];
            }
            return totalRating;
        }


        // 获取该用户对项目的喜好(数值属性离散化)
        public bool[] discretizeRating()
        {
            bool[] result = new bool[Ratings.Length];
            float rating_th = (float)((float)(((float)getTotalRating() / (float)RatingNums)));
            love_items_num = 0;

            for (int count = 1; count < result.Length; count++)
            {
                if (Ratings[count] >= rating_th)
                {
                    result[count] = true;
                    love_items_num++;
                }
                else
                    result[count] = false;
            }
            return result;
        }

        public int[] getUserLoveItems_id()
        {
            bool[] bLoved = this.discretizeRating();
            this.love_items_id = new int[love_items_num];
            int count = 0;
            for (int i = 1; i < bLoved.Length; i++)
            {
                // 用户喜欢当前项目
                if (bLoved[i] == true)
                {
                    this.love_items_id[count++] = i;
                }
            }
            return this.love_items_id;
        }

        // 获得用户对项目的平均评分
        public double getAverageRating()
        {
            int totalRating = 0;
            for (int count = 1; count < 1683; count++)
            {
                totalRating += (int)this.Ratings[count];
            }
            return totalRating / 1683;
        }
    }
}
