    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


/* ---------------------------------------
 * 描述：      实现基于项目的协同过滤算法
 * 作者：      康清波
 * 最后修改于：2012.4.17
 ----------------------------------------- */
namespace Recommendation_Algorithm
{
    class cItemBased_CF
    {
        // 最近邻居个数
        public int max_neighnum;

        // 最近邻居项目集合(余弦相似度)
        private int[][] neighItems_Cosine;

        // 最近邻居项目集合(Pearson相似度)
        private int[][] neighItems_Pearson;

        // 最近邻居项目集合(修正的余弦相似度)
        private int[][] neighItems_AdjCosine;


        // 最近邻居相似度(余弦相似度)
        private double[][] dSimilarity_Cosine;

        // 最近邻居相似度(Pearson相似度)
        private double[][] dSimilarity_Pearson;

        // 最近邻居相似度(修正的余弦相似度)
        private double[][] dSimilarity_AdjCosine;

        // 训练用户集合，包含UI矩阵
        public static cUser[] objUsers = new cUser[cReadinData.totalUserNum];

        private cAssStrategy objAssStrategy;

        // 该cUser对象存储计算的预测评分
        public cUser preditUser;

        // 忽略的项目id，这些项目较少用户评分，无法产生最近邻居
        private int[] ignoreItems = { 599, 711, 814, 830, 852,
                                    857, 1236, 1309, 1310, 1320, 
                                    1343, 1348, 1364, 1373,1457,
                                    1458, 1492, 1493, 1498, 1505,
                                    1520, 1533, 1536, 1543, 1557,
                                    1561, 1562, 1563, 1565, 1582,
                                    1586 };

        //////////////////////////////////////////////////////////////////////////

        // 构造方法
        // 描    述：生成所有项目的最近邻居集合(包含三种算法)及相似度值,集合大小为最大为 max_neigh_num
        public cItemBased_CF(int max_neighnum)
        {
            neighItems_Cosine = new int[1682][];
            neighItems_Pearson = new int[1682][];
            neighItems_AdjCosine = new int[1682][];

            dSimilarity_Cosine = new double[1682][];
            dSimilarity_Pearson = new double[1682][];
            dSimilarity_AdjCosine = new double[1682][];

            this.max_neighnum = max_neighnum;

            objUsers = cReadinData.getBaseUser();        
        }

        public void generateItemNN(int itemid)
        {
            neighItems_Cosine[itemid-1] = new int[max_neighnum];
            neighItems_Pearson[itemid - 1] = new int[max_neighnum];
            neighItems_AdjCosine[itemid - 1] = new int[max_neighnum];

            dSimilarity_Cosine[itemid - 1] = new double[max_neighnum];
            dSimilarity_Pearson[itemid - 1] = new double[max_neighnum];
            dSimilarity_AdjCosine[itemid - 1] = new double[max_neighnum];
        }


        // 最近邻居搜索
        private void  NNS(int itemid, int sim_alg)
        {
            double[] temp = new double[1683-1];
            int[] index = new int[max_neighnum];


            // 相似度算法选择
            // 1. 余弦相似度
            // 2. Pearson相似度
            // 3. 修正的余弦相似度
            for (int count = 1; count < temp.Length; count++)
            {
                if (count == itemid)
                {
                    temp[count - 1] = -1;
                    continue;
                }

                if (sim_alg == 1)
                {
                    temp[count - 1] = itemSimAlgorithm.CalculateCosine(itemid, count);
                }
                else if (sim_alg == 2)
                {
                    temp[count - 1] = itemSimAlgorithm.getPearson( itemid, count);
                }
                else if (sim_alg == 3)
                {
                    temp[count - 1] = itemSimAlgorithm.CalculateAdjCosine(itemid, count);
                }        
            }
          
                for (int i = 0; i < max_neighnum; i++)
                {
                    index[i] = SelectMaxIndex(temp);

                    // 保存最近邻居的相似度值
                    if (sim_alg == 1)
                    {
                        dSimilarity_Cosine[itemid - 1][i] = temp[index[i]];
                        temp[index[i]] = -1;

                        // 保存最近邻居的itemid
                        neighItems_Cosine[itemid - 1][i] = index[i] + 1;
                    }

                    else if (sim_alg == 2)
                    {
                        dSimilarity_Pearson[itemid - 1][i] = temp[index[i]];
                        temp[index[i]] = -1;

                        // 保存最近邻居的itemid
                        neighItems_Pearson[itemid - 1][i] = index[i] + 1;
                    }

                    else if (sim_alg == 3)
                    {
                        dSimilarity_AdjCosine[itemid - 1][i] = temp[index[i]];
                        temp[index[i]] = -1;

                        // 保存最近邻居的itemid
                        neighItems_AdjCosine[itemid - 1][i] = index[i] + 1; 
                    }
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

        /// <summary>
        /// 预测目标用户objDest对项目的评分 
        /// </summary>
        /// <param name="objDest">测试用户</param>
        /// <param name="alg">相似度算法选择</param>
        /// <param name="neigh_num">最近邻居个数</param>
        /// <param name="Rec_Items_num">Top-N推荐个数</param>
        /// <returns>算法评价指标</returns>
        public cAssStrategy getPredictRating(cUser objDest, int alg, int neigh_num, int Rec_Items_num)
        {
            objAssStrategy = new cAssStrategy();
            objUsers = cReadinData.getBaseUser();
            preditUser = new cUser(objDest.id);

            int[][] neighItems = null;
            double[][] dSimilarity = null;
            cUser sourceUser = objUsers[objDest.id];

            switch (alg)
            {
                case 1:
                    neighItems = neighItems_Cosine;
                    dSimilarity = dSimilarity_Cosine;
                    break;
                case 2:
                    neighItems = neighItems_Pearson;
                    dSimilarity = dSimilarity_Pearson;
                    break;
                case 3:
                    neighItems = neighItems_AdjCosine;
                    dSimilarity = dSimilarity_AdjCosine;
                    break;
            }

            double numerator = 0, denominator = 0;
            double total_MAE = 0;

            for (int i = 1; i < objDest.Ratings.Length; i++)
            {
//                 if ((alg == 1) && (ignoreItems.Contains(i-1)))
//                 {
//                     break ;
//                 }
                // 对目标用户训练集合里评分为零的项(itemid为i)产生预测评分
                if (sourceUser.Ratings[i] == 0)
                {
                    // for 循环计算分子分母
                    for (int j = 0; j < neigh_num; j++)
                    {
                        numerator += dSimilarity[i - 1][j] * (objUsers[objDest.id].Ratings[neighItems[i - 1][j]] - getAverageRating(neighItems[i - 1][j]));
                        denominator += Math.Abs(dSimilarity[i-1][j]);
                    }
                    // 确保分母不为零
                    if(denominator == 0)
                        break;
                    preditUser.Ratings[i] = objDest.Ratings[i] * 0.05 + Math.Abs( getAverageRating(i) + numerator / denominator ) ;
                    if (preditUser.Ratings[i] > 5)
                        preditUser.Ratings[i] = 5;

                    numerator = 0;
                    denominator = 0;

                    // 和测试集中的数据相减，计算总的MAE
                    if (objDest.Ratings[i] != 0)
                    {
                        total_MAE += Math.Abs(preditUser.Ratings[i] - objDest.Ratings[i]);
                    }
                }
            }
            objAssStrategy.MAE = total_MAE / (objDest.RatingNums + neigh_num );

            //////////////////////////////////////////////////////////////////////////
            // 计算关于Top-N推荐的分类精确度准则

            // Top-N推荐的项目id, 推荐个数固定为20,便于算法比对
            int[] itemid_TopN = new int[Rec_Items_num];
            int count_interest = 0;       // 记录用户对推荐的项目有兴趣的个数
            int count_total = 0;          // 记录用户测试集合中

            float inter_rating = (float) ((float)sourceUser.getTotalRating() / (float)sourceUser.RatingNums);
           
            // 计算N项推荐项目id 和 查准率
            for (int i = 0; i < itemid_TopN.Length; i++)
            {
                itemid_TopN[i] = SelectMaxIndex(preditUser.Ratings);
                preditUser.Ratings[itemid_TopN[i]] = -1;
                if (objDest.Ratings[itemid_TopN[i]] >= inter_rating )
                    count_interest++;
            }

            // 计算测试集中用户喜欢的项目
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

        // 得到项目id为itemid项的平均评分
        private double getAverageRating(int itemid)
        {
            
            objUsers = cReadinData.getBaseUser();
            
            int totalRating = 0, count = 0;

            for (int i = 1; i < objUsers.Length; i++)
            {
                if (objUsers[i].Ratings[itemid] != 0)
                {
                    totalRating += (int)objUsers[i].Ratings[itemid];
                    count++;
                }
            }
            if (count == 0)
                return 0;
            return totalRating / count;
        }


        // 写最近邻居项目及其对应相似度文件
        //
    public void writeFile()
    {
        FileStream fs_neighItems_Cosine = new FileStream("neighItems_Cosine", FileMode.Create);
        StreamWriter sw_neighItems_Cosine = new StreamWriter(fs_neighItems_Cosine, Encoding.Default);

        for (int i = 0; i < 1682; i++)
        {
            string item = "";
            for(int j = 0; j < max_neighnum; j++)
            {
                item += neighItems_Cosine[i][j] + " ";
            }

            sw_neighItems_Cosine.WriteLine(item);
        }

        sw_neighItems_Cosine.Close();
        fs_neighItems_Cosine.Close();

        //////////////////////////////////////////////////////////////////////////

        FileStream fs_neighItems_Pearson = new FileStream("neighItems_Pearson", FileMode.Create);
        StreamWriter sw_neighItems_Pearson = new StreamWriter(fs_neighItems_Pearson, Encoding.Default);

        for (int i = 0; i < 1682; i++)
        {
            string item = "";
            for (int j = 0; j < max_neighnum; j++)
            {
                item += neighItems_Pearson[i][j] + " ";
            }

            sw_neighItems_Pearson.WriteLine(item);
        }
        sw_neighItems_Pearson.Close();
        fs_neighItems_Pearson.Close();

        //////////////////////////////////////////////////////////////////////////

        FileStream fs_neighItems_AdjCosine = new FileStream("neighItems_AdjCosine", FileMode.Create);
        StreamWriter sw_neighItems_AdjCosine = new StreamWriter(fs_neighItems_AdjCosine, Encoding.Default);

        for (int i = 0; i < 1682; i++)
        {
            string item = "";
            for (int j = 0; j < max_neighnum; j++)
            {
                item += neighItems_AdjCosine[i][j] + " ";
            }

            sw_neighItems_AdjCosine.WriteLine(item);
        }
        sw_neighItems_AdjCosine.Close();
        fs_neighItems_AdjCosine.Close();

        //////////////////////////////////////////////////////////////////////////

        FileStream fs_dSimilarity_Cosine = new FileStream("dSimilarity_Cosine", FileMode.Create);
        StreamWriter sw_dSimilarity_Cosine = new StreamWriter(fs_dSimilarity_Cosine, Encoding.Default);

        for (int i = 0; i < 1682; i++)
        {
            string item = "";
            for (int j = 0; j < max_neighnum; j++)
            {
                item += dSimilarity_Cosine[i][j] + " ";
            }

            sw_dSimilarity_Cosine.WriteLine(item);
        }
        sw_dSimilarity_Cosine.Close();
        fs_dSimilarity_Cosine.Close();

        //////////////////////////////////////////////////////////////////////////

        FileStream fs_dSimilarity_Pearson = new FileStream("dSimilarity_Pearson", FileMode.Create);
        StreamWriter sw_dSimilarity_Pearson = new StreamWriter(fs_dSimilarity_Pearson, Encoding.Default);

        for (int i = 0; i < 1682; i++)
        {
            string item = "";
            for (int j = 0; j < max_neighnum; j++)
            {
                item += dSimilarity_Pearson[i][j] + " ";
            }

            sw_dSimilarity_Pearson.WriteLine(item);
        }
        sw_dSimilarity_Pearson.Close();
        fs_dSimilarity_Pearson.Close();

        //////////////////////////////////////////////////////////////////////////

        FileStream fs_dSimilarity_AdjCosine = new FileStream("dSimilarity_AdjCosine", FileMode.Create);
        StreamWriter sw_dSimilarity_AdjCosine = new StreamWriter(fs_dSimilarity_AdjCosine, Encoding.Default);

        for (int i = 0; i < 1682; i++)
        {
            string item = "";
            for (int j = 0; j < max_neighnum; j++)
            {
                item += dSimilarity_AdjCosine[i][j] + " ";
            }

            sw_dSimilarity_AdjCosine.WriteLine(item);
        }
        sw_dSimilarity_AdjCosine.Close();
        fs_dSimilarity_AdjCosine.Close();
    }

        // 从文件读取最近邻居项目矩阵及相似度矩阵
    public void readFile()
    {
        StreamReader rs_neighItems_Cosine = new StreamReader( "neighItems_Cosine" , Encoding.Default);
        string sLine = "";

        int count = 0;
        while (sLine != null)
        {
            sLine = rs_neighItems_Cosine.ReadLine();
            if(sLine == null)
                break;
            for (int i = 0; i < this.max_neighnum; i++)
            {
                int itemid = int.Parse ( sLine.Substring(0, sLine.IndexOf(' ')) );
                this.neighItems_Cosine[count][i] = itemid;
                sLine = sLine.Substring(sLine.IndexOf(' ') + 1);
            }
        
            count++;
        }

        //////////////////////////////////////////////////////////////////////////

        StreamReader rs_neighItems_Pearson = new StreamReader("neighItems_Pearson", Encoding.Default);
        sLine = "";

        count = 0;
        while (sLine != null)
        {
            sLine = rs_neighItems_Pearson.ReadLine();
            if (sLine == null)
                break;
            for (int i = 0; i < this.max_neighnum; i++)
            {
                int itemid = int.Parse(sLine.Substring(0, sLine.IndexOf(' ')));
                this.neighItems_Pearson[count][i] = itemid;
                sLine = sLine.Substring(sLine.IndexOf(' ') + 1);
            }
            count++;
        }

        //////////////////////////////////////////////////////////////////////////

        StreamReader rs_neighItems_AdjCosine = new StreamReader("neighItems_AdjCosine", Encoding.Default);
        sLine = "";

        count = 0;
        while (sLine != null)
        {
            sLine = rs_neighItems_AdjCosine.ReadLine();
            if (sLine == null)
                break;
            for (int i = 0; i < this.max_neighnum; i++)
            {
                int itemid = int.Parse(sLine.Substring(0, sLine.IndexOf(' ')));
                this.neighItems_AdjCosine[count][i] = itemid;
                sLine = sLine.Substring(sLine.IndexOf(' ') + 1);
            }
            count++;
        }

        //////////////////////////////////////////////////////////////////////////

        StreamReader rs_dSimilarity_Cosine = new StreamReader("dSimilarity_Cosine", Encoding.Default);
        sLine = "";

        count = 0;
        while (sLine != null)
        {
            sLine = rs_dSimilarity_Cosine.ReadLine();
            if (sLine == null)
                break;
            for (int i = 0; i < this.max_neighnum; i++)
            {
                double sim = System.Convert.ToDouble(sLine.Substring(0, sLine.IndexOf(' ')));
                this.dSimilarity_Cosine[count][i] = sim;
                sLine = sLine.Substring(sLine.IndexOf(' ') + 1);
            }
            count++;
        }

        //////////////////////////////////////////////////////////////////////////

        StreamReader rs_dSimilarity_Pearson = new StreamReader("dSimilarity_Pearson", Encoding.Default);
        sLine = "";

        count = 0;
        while (sLine != null)
        {
            sLine = rs_dSimilarity_Pearson.ReadLine();
            if (sLine == null)
                break;
            for (int i = 0; i < this.max_neighnum; i++)
            {
                double sim = System.Convert.ToDouble(sLine.Substring(0, sLine.IndexOf(' ')));
                this.dSimilarity_Pearson[count][i] = sim;
                sLine = sLine.Substring(sLine.IndexOf(' ') + 1);
            }
            count++;
        }

        //////////////////////////////////////////////////////////////////////////

        StreamReader rs_dSimilarity_AdjCosine = new StreamReader("dSimilarity_AdjCosine", Encoding.Default);
        sLine = "";

        count = 0;
        while (sLine != null)
        {
            sLine = rs_dSimilarity_AdjCosine.ReadLine();
            if (sLine == null)
                break;
            for (int i = 0; i < this.max_neighnum; i++)
            {
                double sim = System.Convert.ToDouble(sLine.Substring(0, sLine.IndexOf(' ')));
                this.dSimilarity_AdjCosine[count][i] = sim;
                sLine = sLine.Substring(sLine.IndexOf(' ') + 1);
            }
            count++;
        }
    }


    /*    static void Main(string[] args)
        {
            cReadinData rd = new cReadinData(0);
            cItemBased_CF obj = new cItemBased_CF(200);

            for (int i = 1; i <= 1682; i++)
            {
                obj.generateItemNN(i);
            }

                obj.readFile();

                cUser[] testUsers = cReadinData.getTestUser();

            double total_MAE = 0;
                for (int i = 1; i <= 15; i++)
                {
                    double MAE = obj.getPredictRating(testUsers[i], 1, 30);
                    total_MAE += MAE;
                    Console.WriteLine("Cosine MAE:{0}", MAE);
                }
                Console.WriteLine("Cosine average MAE:{0}", total_MAE / 15);
                Console.ReadKey();

                total_MAE = 0;

                for (int i = 1; i <= 15; i++)
                {
                    double MAE = obj.getPredictRating(testUsers[i], 2 , 30);
                    total_MAE += MAE;
                    Console.WriteLine("Pearson MAE:{0}", MAE);
                }
                Console.WriteLine("Pearson average MAE:{0}", total_MAE / 15);
                Console.ReadKey();

                total_MAE = 0;

                for (int i = 1; i <= 15; i++)
                {
                    double MAE = obj.getPredictRating(testUsers[i], 3, 30);
                    total_MAE += MAE;
                    Console.WriteLine("AdjCosine MAE:{0}", MAE);
                }
                Console.WriteLine("AdjCosine average MAE:{0}", total_MAE / 15);
        }*/
        
    }

    static class itemSimAlgorithm
    {
        private static int[] iItem1_Rating;    // 项目一的评分数据
        private static int[] iItem2_Rating;    // 项目二的评分数据 


  
        // 计算两个项目的Pearson相似度
        public static double getPearson( int item1_id, int item2_id)
        {
            cUser[] totalUsers = cReadinData.getBaseUser();
            double totalRating_1 = 0, totalRating_2 = 0;       // 所有用户对项目1,项目2的总评分
            int count_1 = 0, count_2 = 0;                     // 对项目,项目2评分的用户数目

            for (int i = 1; i < totalUsers.Length; i++)
            {
                if (totalUsers[i].Ratings[item1_id] != 0)
                {
                    count_1++;
                    totalRating_1 += totalUsers[i].Ratings[item1_id];
                }
                if (totalUsers[i].Ratings[item2_id] != 0)
                {
                    count_2++;
                    totalRating_2 += totalUsers[i].Ratings[item2_id];
                }
            }
            double average_1 = totalRating_1 / count_1;     // 项目1的平均评分
            double average_2 = totalRating_2 / count_2;     // 项目2的平均评分

            double numerator = 0, denominator1 = 0, denominator2 = 0;

            for (int count = 1; count < totalUsers.Length; count++)
            {
                // 项目1, 2的共同评分用户
                if ((totalUsers[count].Ratings[item1_id] != 0) && (totalUsers[count].Ratings[item2_id] != 0))
                {
                    numerator += (totalUsers[count].Ratings[item1_id] - average_1) * (totalUsers[count].Ratings[item2_id] - average_2);
                    denominator1 += Math.Pow(totalUsers[count].Ratings[item1_id] - average_1, 2);
                    denominator2 += Math.Pow(totalUsers[count].Ratings[item2_id] - average_2, 2);
                }
            }

            double denominator = Math.Sqrt(denominator1) * Math.Sqrt(denominator2);
            if (denominator == 0)
                return 0;
            double sim = numerator / denominator;

            return sim;
        }

        // 计算两个项目的修正余弦相似度
        public static double CalculateAdjCosine(int item1_id, int item2_id)
        {
            cUser[] totalUsers = cReadinData.getBaseUser();
            double totalRating_1 = 0, totalRating_2 = 0;       // 所有用户对项目1,项目2的总评分
            int count_1 = 0, count_2 = 0;                     // 对项目,项目2评分的用户数目

            for (int i = 1; i < totalUsers.Length; i++)
            {
                if (totalUsers[i].Ratings[item1_id] != 0)
                {
                    count_1++;
                    totalRating_1 += totalUsers[i].Ratings[item1_id];
                }
                if (totalUsers[i].Ratings[item2_id] != 0)
                {
                    count_2++;
                    totalRating_2 += totalUsers[i].Ratings[item2_id];
                }
            }
            double average_1 = totalRating_1 / count_1;     // 项目1的平均评分
            double average_2 = totalRating_2 / count_2;     // 项目2的平均评分

            double numerator = 0, denominator1 = 0, denominator2 = 0;

            for (int count = 1; count < totalUsers.Length; count++)
            {
                // 项目1, 2的共同评分用户
                if ((totalUsers[count].Ratings[item1_id] != 0) && (totalUsers[count].Ratings[item2_id] != 0))
                {
                    numerator += (totalUsers[count].Ratings[item1_id] - average_1) * (totalUsers[count].Ratings[item2_id] - average_2);
                }

                // 对项目1评分的用户集合
                if (totalUsers[count].Ratings[item1_id] != 0)
                {
                    denominator1 += Math.Pow( totalUsers[count].Ratings[item1_id] - average_1, 2);
                }

                // 对项目2评分的用户集合
                if (totalUsers[count].Ratings[item2_id] != 0)
                {
                    denominator2 += Math.Pow( totalUsers[count].Ratings[item2_id] - average_2, 2);
                }
            }
            double denominator = Math.Sqrt(denominator1) * Math.Sqrt(denominator2);

            if (denominator == 0)
                return 0;
            double sim = numerator / denominator;

            return sim;
        }

        // 计算两个项目的余弦相似度
        public static double CalculateCosine(int item1_id, int item2_id)
        {
            
            iItem1_Rating = new int[cReadinData.totalUserNum + 1];
            iItem2_Rating = new int[cReadinData.totalUserNum + 1];
            
            // 获得训练用户数据
            cUser[] objUsers = cReadinData.getBaseUser();

            for( int count = 1; count < iItem1_Rating.Length; count++ )
            {
                iItem1_Rating[count] = (int)objUsers[count].Ratings[item1_id];
                iItem2_Rating[count] = (int)objUsers[count].Ratings[item2_id];
            }
            double dotProduct = CalcDotProduct();
            double length1 = CalcLength(1);
            double length2 = CalcLength(2);

            if (length1 == 0 || length2 == 0)
            {
                return 0;
            }
            double cosine = dotProduct / (length1 * length2);

            return cosine;
        }

        // 计算向量长度(vector length)
        private static double CalcLength(int id)
        { 
            double length = 0;
            if (id == 1)
            {
                for (int i = 1; i < iItem1_Rating.Length; i++)
                {
                    length += Math.Pow(iItem1_Rating[i], 2);
                }
            }
            else if (id == 2)
            {
                for (int i = 1; i < iItem2_Rating.Length; i++)
                {
                    length += Math.Pow(iItem2_Rating[i], 2);
                }
            }

            return Math.Sqrt(length);
        }

        // 计算向量点积(dot product)/内积(inner product)
        private static double CalcDotProduct()
        {
            double dotProduct = 0;

            for (int i = 1; i < iItem1_Rating.Length; i++)
            {
                if ((iItem1_Rating[i] != 0) && (iItem2_Rating[i] != 0))
                    dotProduct += iItem1_Rating[i] * iItem2_Rating[i];
            }

            return dotProduct;
        }
    }

}
