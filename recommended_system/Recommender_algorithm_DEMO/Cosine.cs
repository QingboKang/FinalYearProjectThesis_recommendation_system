using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    // 余弦相似度
    static class Cosine
    {
        // 计算向量余弦值
        public static double Calculate(cUser objUser1, cUser objUser2)
        {
            double dotProduct = CalcDotProduct(objUser1, objUser2);
            double length1 = CalcLength(objUser1);
            double length2 = CalcLength(objUser2);
            double cosine = dotProduct / (length1 * length2);

            return cosine;
        }

        // 计算向量长度(vector length)
        private static double CalcLength(cUser objUser)
        {
            double length = 0;
            for (int i = 1; i < 1683; i++)
            {
                length += Math.Pow(objUser.Ratings[i], 2);
            }

            return Math.Sqrt(length);
        }

        // 计算向量点积(dot product)/内积(inner product)
        private static double CalcDotProduct(cUser objUser1, cUser objUser2)
        {
            double dotProduct = 0;

            for (int i = 1; i < 1683; i++)
            {
                if ((objUser1.Ratings[i] != 0) && (objUser2.Ratings[i] != 0))
                    dotProduct += objUser1.Ratings[i] * objUser2.Ratings[i];
            }

            return dotProduct;
        }
    }
}
