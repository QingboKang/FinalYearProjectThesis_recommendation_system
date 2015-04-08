using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    // 相关相似度(Pearson相关度)
    static class Pearson
    {
        public static double getPearson(cUser user1, cUser user2)
        {
            double average1, average2;

            //   int count = 0;       // 用户1，用户2共同评分的项目数量
            double numerator = 0, denominator1 = 0, denominator2 = 0, denominator;

            //  int count = 0;

            //             for (int i = 1; i < 1683; i++)
            //             {
            //                 if ((user1.Ratings[i] != 0) && (user2.Ratings[i] != 0))
            //                 {
            //                     Console.WriteLine("user1 id:{0}  item id:{1}  rating:{2}", user1.id, i, user1.Ratings[i]);
            //                     Console.WriteLine("user2 id:{0}  item id:{1}  rating:{2}", user2.id, i, user2.Ratings[i]);
            // 
            //                     count++;
            //                 }
            //             }
            //             Console.Write("  count:{0}  ", count);
            //             if (count == 0)
            //                 return 0;
            average1 = user1.getTotalRating() / user1.RatingNums;
            average2 = user2.getTotalRating() / user2.RatingNums;

            for (int i = 1; i < 1683; i++)
            {
                if ((user1.Ratings[i] != 0) && (user2.Ratings[i] != 0))
                {
                    numerator += (user1.Ratings[i] - average1) * (user2.Ratings[i] - average2);
                    denominator1 += Math.Pow(user1.Ratings[i] - average1, 2);
                    denominator2 += Math.Pow(user2.Ratings[i] - average2, 2);
                }
            }
            denominator = Math.Sqrt(denominator1) * Math.Sqrt(denominator2);

            if (denominator == 0)
                return 0;
            return numerator / denominator;
        }
    }
}
