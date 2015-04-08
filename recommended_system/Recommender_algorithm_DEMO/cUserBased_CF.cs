using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/* ---------------------------------------
 * 描述：      实现基于用户的协同过滤算法
 * 作者：      康清波
 * 最后修改于：2012.4.10
 ----------------------------------------- */
namespace Recommendation_Algorithm
{
    public class cUserBased_CF
    {
        // 最近邻居个数
        public int neigh_num;

        // 最近邻居用户集合
        public cUser[] neighUser;

        // 所有相似值
        private double[] dSimilarity;

        // 用户算法评价准则的对象
        private cAssStrategy objAssStrategy;

        // 该cUser对象存储计算的预测评分
        public cUser preditUser;

        //////////////////////////////////////////////////////////////////////////
        // 构造方法
        // 参    数:int — 最近邻居的个数
        public cUserBased_CF(int num)
        {
            this.neigh_num = num;
        }

        // 最近邻居搜索
        private void NNS(cUser objDest, int sim_alg)
        {
            cUser[] objUser = new cUser[cReadinData.totalUserNum + 1];
            neighUser = new cUser[neigh_num];
            dSimilarity = new double[neigh_num];
            double[] temp = new double[cReadinData.totalUserNum];

            objUser = cReadinData.getBaseUser();
            int[] index = new int[neigh_num];


            // 相似度算法选择
            // 1. 余弦相似度
            // 2. Pearson相似度
            // 3. 修正的余弦相似度
            for (int count = 1; count <= cReadinData.totalUserNum; count++)
            {
                if (count == objDest.id)
                {
                    temp[count - 1] = -1;
                    continue;
                }

                if (sim_alg == 1)
                {
                    temp[count - 1] = Cosine.Calculate(objDest, objUser[count]);
                }
                else if (sim_alg == 2)
                {
                    temp[count - 1] = Pearson.getPearson(objDest, objUser[count]);
                }
                else if (sim_alg == 3)
                {
                    temp[count - 1] = AdjCosine.Calculate(objDest, objUser[count]);
                }
            }
            for (int i = 0; i < neigh_num; i++)
            {
                index[i] = SelectMaxIndex(temp);

                dSimilarity[i] = temp[index[i]];
                temp[index[i]] = -1;
                neighUser[i] = objUser[index[i] + 1];
                //   Console.WriteLine("id: {0}   sim:{1}   Rating:{2}",neighUser[i].id, dSimilarity[i], neighUser[i].RatingNums);
            }
        }

        private static int SelectMaxIndex(double[] d)
        {
            int maxIndex = 0;
            double maxValue = d[maxIndex];
            for (int i = 0; i < d.Length; i++)
            {
                if (maxValue < d[i])
                {
                    maxIndex = i;
                    maxValue = d[i];
                }
            }
            return maxIndex;
        }


        // 方法描述：预测目标用户objDest对项目的评分 
        // 方法参数：objDest(cUser) — 目标用户   alg — 相似度算法的选择
        // 返    回：MAE(double) — 该目标用户的统计精度度量
        public cAssStrategy getPredictRating(cUser objDest, int alg, int item_nums)
        {
            objAssStrategy = new cAssStrategy();
            cUser[] objUser = new cUser[cReadinData.totalUserNum + 1];
            
            objUser = cReadinData.getBaseUser();

            int userid = objDest.id;
            cUser destUser = objUser[userid];

            // 最近邻居搜索
            NNS(destUser, alg);

            preditUser = new cUser(destUser.id);
            preditUser.RatingNums = objDest.RatingNums;
            double numerator = 0, denominator = 0;
            double sum = 0;

            // 计算分母
            for (int i = 0; i < neigh_num; i++)
            {
                denominator += Math.Abs(dSimilarity[i]);
            }
            
            int count = 0;
            
            // 对用户训练集中未评分的每一项产生预测评分
            for (int i = 1; i < objDest.Ratings.Length; i++)
            {
                if (destUser.Ratings[i] == 0)
                {
                    for (int j = 0; j < neigh_num; j++)
                    {
                        numerator += dSimilarity[j] * (neighUser[j].Ratings[i] - neighUser[j].getTotalRating() / neighUser[j].RatingNums); 
                    }
                    preditUser.Ratings[i] = Math.Abs(numerator / denominator + destUser.getTotalRating() / destUser.RatingNums) ;

                    if (preditUser.Ratings[i] > 5)
                    {
                        preditUser.Ratings[i] = 5;
                    }
                    // 预测的评分值减去实际的评分值
                    if (objDest.Ratings[i] != 0)
                    {
                        sum += Math.Abs(preditUser.Ratings[i] - objDest.Ratings[i]);

                 //       preditUser.Ratings[i] += 2;
                        count++;
                    }
                    numerator  = 0;  
                }
            }

            // 计算MAE值
            objAssStrategy.MAE = sum / (count);
//             if (alg != 1)
//             {
//                 objAssStrategy.MAE -= 1.8;
//             }
            //////////////////////////////////////////////////////////////////////////

            // 计算关于Top-N推荐的分类精确度准则
            // Top-N推荐的项目id
            int[] itemid_TopN = new int[item_nums];
            int count_interest = 0;                 // 记录用户对推荐的项目有兴趣(评分大于该用户的平均评分)的个数
            int count_total = 0;                    // 记录用户测试集合中
            float inter_rating = (float)((float)destUser.getTotalRating() / (float)destUser.RatingNums);     // 用户的平均评分

            // 计算N项推荐项目id和推荐的项目中用户喜欢的个数
            for (int i = 0; i < itemid_TopN.Length; i++)
            {
                itemid_TopN[i] = SelectMaxIndex(preditUser.Ratings);
                preditUser.Ratings[itemid_TopN[i]] = -1;
                if (objDest.Ratings[itemid_TopN[i]] >= inter_rating)
                    count_interest++;
            }
            // 计算测试集中该用户喜欢的项目数量
            foreach (int rating in objDest.Ratings)
            {
                if (rating >= inter_rating)
                    count_total++;
            }
            objAssStrategy.Precison = (float)count_interest / itemid_TopN.Length;    // 查准率(Precison)
            if (count_total == 0)
                objAssStrategy.Recall = 0;
            else
                objAssStrategy.Recall = (float)count_interest / count_total;       // 查全率(Recall)

            return objAssStrategy;
        }

    }

}
