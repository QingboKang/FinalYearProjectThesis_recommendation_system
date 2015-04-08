using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    // 修正的余弦相似度
    static class AdjCosine
    {
        public static double Calculate(cUser user1, cUser user2)
        {
            double numerator = 0;
            double denominator1 = 0, denominator2 = 0, denominator = 0;
            double average1, average2;
            double result;

            average1 = user1.getTotalRating() / user1.RatingNums;
            average2 = user2.getTotalRating() / user2.RatingNums;

            for (int i = 1; i < 1683; i++)
            {
                if (user1.Ratings[i] != 0 && user2.Ratings[i] != 0)
                {
                    numerator += (user1.Ratings[i] - average1) * (user2.Ratings[i] - average2);
                }
                if (user1.Ratings[i] != 0)
                {
                    denominator1 += Math.Pow(user1.Ratings[i] - average1, 2);
                }
                if (user2.Ratings[i] != 0)
                {
                    denominator2 += Math.Pow(user2.Ratings[i] - average2, 2);
                }
            }
            denominator = Math.Sqrt(denominator1) * Math.Sqrt(denominator2);
            if (denominator == 0)
                return 0;
            result = numerator / denominator;
            return result;
        }
    }
}
