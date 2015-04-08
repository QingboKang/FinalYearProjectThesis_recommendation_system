using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    public class RecItemid_Degree : IComparable
    {
        public int recItems_id;   // 项目id
        public double rec_Degree;   // 该项目所对应的推荐度

        // 排序算法
        public int CompareTo(object other)
        {
            RecItemid_Degree otherTemperature = other as RecItemid_Degree;
            if (this.rec_Degree == otherTemperature.rec_Degree)
                return 0;
            if (this.rec_Degree < otherTemperature.rec_Degree)
                return 1;
            return -1;
        } 
    }

    public class cApriori
    {
        public static cUser[] sourceUsers;

        // 存储离散化后的用户喜好矩阵
        bool[][] discretize;

        // 频繁-1项集
        public static ArrayList freq_one_items = new ArrayList();
        
        // 频繁-2项集
        public static ArrayList freq_itemsets = new ArrayList();     
     
        // 关联规则
        public static ArrayList association_Rules = new ArrayList();

        // 推荐 项目id&推荐度 列表 
        public static RecItemid_Degree[] recItems;

        // 推荐列表中用户喜欢的表项id
        public static ArrayList TestUserLoveItems_id;
        // 推荐列表中用户不喜欢的表项id
        public static ArrayList TestUserDratItems_id;
        // 推荐列表中用户为进行评价的表项id
        public static ArrayList TestUserUnRatingItems_id;


        public static cUser[] testUsers;
        /// <summary>
        /// 生成频繁-1项集
        /// </summary>
        public void genFreq_one_itemsets()
        {
            cReadinData obj_readData = new cReadinData(0);             
            sourceUsers = cReadinData.getBaseUser();       // 读入训练数据
            testUsers = cReadinData.getTestUser();         // 读入测试数据
            float itemSupport;                    
            int count = 0; 
            double total_support = 0;

            discretize = new bool[sourceUsers.Length][];
            bool[] testUser_discretize = new bool[testUsers[1].Ratings.Length];

            ArrayList temp_one_items = new ArrayList();
            Freq_Item obj_freq_item;
            Freq_Item[] obj_freq_items;
            
            // 得到训练用户喜好矩阵
            for (int i = 1; i < sourceUsers.Length; i++)
            {
                discretize[i] = new bool[sourceUsers[i].Ratings.Length];
                discretize[i] = sourceUsers[i].discretizeRating();
            }

            // 得到测试用户喜好矩阵
            for (int i = 1; i < testUsers.Length; i++)
            {
                testUser_discretize = testUsers[i].discretizeRating();
            }

            int count_zero = 0;

            for (int itemid = 1; itemid < sourceUsers[1].Ratings.Length; itemid++)
            {
                // for循环扫描数据集，得到用户中喜欢id为itemid的项目的人数
                for (int userid = 1; userid < sourceUsers.Length; userid++)
                {
                    // 如果id为userid的用户喜欢id为itemid的项目
                    if (discretize[userid][itemid] == true)
                        count++;
                }
                // 得到该项目的支持度值
                itemSupport = (float)((float)count / (float)sourceUsers.Length);

                obj_freq_item = new Freq_Item(itemid, count, itemSupport);
                temp_one_items.Add(obj_freq_item);

                // 计算支持度计数为零的项目数量
                if (itemSupport == 0)
                {
                    count_zero++;
                }

                // 支持度计数累加为和
                total_support += itemSupport;
                count = 0;
            }

            // 支持度阈值(平均值)
            float min_support_one = (float)((float)total_support / (float)(temp_one_items.Count));
            Freq_Item.min_support = min_support_one;

//             obj_freq_items = new Freq_Item[temp_one_items.Count];
// 
//             int count_one_item = 0;
//             foreach (Freq_Item obj in temp_one_items)
//             {
//                 obj_freq_items[count_one_item++] = obj;
//             }
            // 按照支持度排序
         //   Array.Sort(obj_freq_items);

            // 生成频繁-1项集
            foreach (Freq_Item obj in temp_one_items)
            {
                if (obj.support > min_support_one)
                {
                    freq_one_items.Add(obj);
                }
            }
            // 最小支持度计数
            Freq_Item last_one = (Freq_Item)freq_one_items[freq_one_items.Count - 1];
            Freq_Item.min_support_count = last_one.support_count;
        }

        /// <summary>
        /// 生成频繁-2项集
        /// </summary>
        public void genFreq_two_itemsets()
        {
            int count = 0;
            ArrayList temp_freq_itemsets = new ArrayList();
            ArrayList count_ArrayList = new ArrayList();

            Frequent_Itemset obj_Frequent_Itemset;
            Frequent_Itemset[] obj_freq_itemsets;

            int count_Support = 0;
            float total_Support = 0;

            int[] freq_one_items_id = new int[freq_one_items.Count];

            for (int n = 0; n < freq_one_items.Count; n++)
            {
                Freq_Item obj = (Freq_Item)freq_one_items[n];
                freq_one_items_id[n] = obj.itemid;
            }

            for (int n = 0; n < freq_one_items_id.Length - 1; n++)
            {
                int itemid_1 = freq_one_items_id[n];
                for (int m = n + 1; m < freq_one_items_id.Length; m++)
                {
                    int itemid_2 = freq_one_items_id[m];
                    // 扫描数据集，得到共同用户数目
                    for (int userid = 1; userid < sourceUsers.Length; userid++)
                    {
                        // 如果该用户喜欢这两个项目
                        if ((discretize[userid][itemid_1] == true) && (discretize[userid][itemid_2] == true))
                            count_Support++;
                    }

                    // 计算这两个项目的支持度
                    float support = (float)((float)count_Support / (float)sourceUsers.Length);

                    obj_Frequent_Itemset = new Frequent_Itemset(itemid_1, itemid_2, count_Support, support);
                    temp_freq_itemsets.Add(obj_Frequent_Itemset);
                    total_Support += support;

                    count_Support = 0;
                }
            }
            obj_freq_itemsets = new Frequent_Itemset[temp_freq_itemsets.Count];
            int _count_zero = 0;
            foreach (Frequent_Itemset item in temp_freq_itemsets)
            {
                if (item.Support == 0)
                {
                    _count_zero++;
                }

                obj_freq_itemsets[count++] = item;
            }

            Array.Sort(obj_freq_itemsets);

            // 计算所有不为零频繁2项集的支持度平均值 设为支持度阈值
            float Min_Support = total_Support / (float)(obj_freq_itemsets.Length - _count_zero);
            Frequent_Itemset.min_support = Min_Support;

            foreach (Frequent_Itemset obj_freq_itemset in obj_freq_itemsets)
            {
                if (obj_freq_itemset.Support > Frequent_Itemset.min_support)   // 支持度大于支持度阈值
                    freq_itemsets.Add(obj_freq_itemset);
            }
            // 最小支持度计数
            Frequent_Itemset last_two = (Frequent_Itemset)freq_itemsets[freq_itemsets.Count - 1];
            Frequent_Itemset.min_support_count = last_two.support_count;
        }

        /// <summary>
        /// 生成关联规则
        /// </summary>
        public void genAssoc_rules()
        {
            AssociationRule obj_tempAR;
            float total_confidence = 0;
            ArrayList temp_AR = new ArrayList();

            for (int count_ar = 0; count_ar < freq_itemsets.Count; count_ar++)
            {
                Frequent_Itemset obj = (Frequent_Itemset)freq_itemsets[count_ar];

                int first_id = obj._itemid_1;   // 第一个项目id
                int second_id = obj._itemid_2;   // 第二个项目id

                float confidence_1 = (float)((float)obj.support_count / (float)getSupport_count(first_id));  // 置信度1
                total_confidence += confidence_1;
                obj_tempAR = new AssociationRule(first_id, second_id, obj.Support, confidence_1);
                temp_AR.Add(obj_tempAR);

                float confidence_2 = (float)((float)obj.support_count / (float)getSupport_count(second_id));  // 置信度2
                total_confidence += confidence_2;
                obj_tempAR = new AssociationRule(second_id, first_id, obj.Support, confidence_2);
                temp_AR.Add(obj_tempAR);
            }
            // 置信度阈值，取平均值
            float min_confidence = (float)total_confidence / (float)temp_AR.Count;
            AssociationRule.min_confidence = min_confidence;

            // 生成关联规则
            foreach (AssociationRule obj in temp_AR)
            {
                if (obj.confidence > min_confidence)
                    association_Rules.Add(obj);
            }
        }

      /*  public static void Main(String[] args)
        {
         //   cItem obj = new cItem();
        /*    movieInfo[] objs = cItem.movies;

          for (int k = 1; k < objs.Length; k++)
          {
              Console.WriteLine(objs[k].movie_id);
              Console.WriteLine(objs[k].name);
              for (int i = 1; i < objs[k].genres.Length; i++)
              {
                  if (objs[k].genres[i] != "")
                  {
                      Console.WriteLine(objs[k].genres[i]);
                  }
              }
          }
                
            cApriori obj = new cApriori();

            Console.WriteLine("开始产生频繁1项集");
            obj.genFreq_one_itemsets();
            Console.WriteLine("完成产生频繁1项集");
            Console.WriteLine("频繁1项集数目:" + freq_one_items.Count);
            Console.WriteLine("最小支持度计数:" + Freq_Item.min_support_count);
            Console.WriteLine("支持度阈值:" + Freq_Item.min_support);
            Console.WriteLine();

            Console.WriteLine("开始产生频繁2项集");
            obj.genFreq_two_itemsets();
            Console.WriteLine("完成产生频繁2项集");
            Console.WriteLine("频繁2项集数目:" + freq_itemsets.Count);
            Console.WriteLine("最小支持度计数:" + Frequent_Itemset.min_support_count);
            Console.WriteLine("支持度阈值:" + Frequent_Itemset.min_support);
            Console.WriteLine();

            Console.WriteLine("开始产生关联规则");
            obj.genAssoc_rules();
            Console.WriteLine("完成产生关联规则");
            Console.WriteLine( "关联规则数: " + association_Rules.Count);
            Console.WriteLine("置信度阈值:" + AssociationRule.min_confidence);
   //         AssociationRule.readMovieName();

            // 得到每个用户所支持的关联规则
            AssociationRule[][] supp_AssRules = new AssociationRule[sourceUsers.Length][];
            // 每个用户所推荐的项目列表
            RecItemid_Degree[][] recItems = new RecItemid_Degree[sourceUsers.Length][];

            int total_length = 0;
            for (int uid = 1; uid <= 20; uid++)
            {
                supp_AssRules[uid] = cApriori.getSupport_AssRules(uid);
                recItems[uid] = getRecItems(supp_AssRules[uid], uid);
                Console.WriteLine(recItems[uid].Length);
                total_length += recItems[uid].Length;
            }
            Console.WriteLine(total_length/10);
        }*/

        /// <summary>
        /// 得到指定项目的支持度计数
        /// </summary>
        /// <param name="itemid">项目id</param>
        /// <returns>支持度计数</returns>
        public static int getSupport_count(int itemid)
        {
            foreach (Freq_Item obj in freq_one_items)
            {
                if (obj.itemid == itemid)
                {
                    return obj.support_count;
                }
            }
            return 0;
        }
    
        /// <summary>
        /// 获得目标用户支持的关联规则
        /// </summary>
        /// <param name="itemid">目标用户的用户id</param>
        /// <returns>按照关联度排序后的关联规则集合</returns>
        public static AssociationRule[] getSupport_AssRules(int userid)
        {
            // 存储该用户支持的关联规则
            ArrayList support_AssocRules = new ArrayList();
            AssociationRule temp_AR;

            int count_id = 0;
            // 用户评价过且喜欢的项目id数组
            int[] love_items_id = sourceUsers[userid].getUserLoveItems_id(); 

            // 循环搜索关联规则库
            for (int i = 0; i < association_Rules.Count; i++)
            {
                temp_AR = (AssociationRule)association_Rules[i];
                int first_itemid = temp_AR._itemid_1;
                int second_itemid = temp_AR._itemid_2;

                // 该用户喜欢的项目出现在该关联规则的左部
                // && 该用户对规则右部项目未评分
                if( (love_items_id.Contains(first_itemid)) && (sourceUsers[userid].Ratings[second_itemid] == 0) )
                {
                    support_AssocRules.Add(temp_AR);
                }
            }

            // 按照关联度对其所支持的关联规则排序
            AssociationRule[] assRules = new AssociationRule[support_AssocRules.Count];
            int count = 0;
            foreach (AssociationRule obj in support_AssocRules)
            {
                assRules[count++] = obj;
            }
            Array.Sort(assRules);

            return assRules;

        }

        /// <summary>
        /// 根据用户支持的关联规则得到依据推荐度排序的推荐项目
        /// </summary>
        /// <param name="Supp_AssRules">用户所支持的关联规则集合</param>
        /// <param name="userid">用户id</param>
        /// <returns>推荐项目id&推荐度</returns>
        public static RecItemid_Degree[] getRecItems(AssociationRule[] Supp_AssRules, int userid)
        {
            // 每个项目的推荐度
            RecItemid_Degree[] temp_recDegree = new RecItemid_Degree[sourceUsers[userid].Ratings.Length];
            int count_non_zero = 1;

            for (int ItemID = 1; ItemID < sourceUsers[userid].Ratings.Length; ItemID++)
            {
                temp_recDegree[ItemID] = new RecItemid_Degree();
                temp_recDegree[ItemID].recItems_id = ItemID;
                temp_recDegree[ItemID].rec_Degree = 0;   // 项目推荐度初始为零

                // 循环扫描关联规则集，积累项目推荐度
                for (int count_AR = 0; count_AR < Supp_AssRules.Length; count_AR++)
                {
                    if (Supp_AssRules[count_AR]._itemid_2 == ItemID)
                    {
                        temp_recDegree[ItemID].rec_Degree += Supp_AssRules[count_AR].Reco_degrees;
                    }
                }
                if(temp_recDegree[ItemID].rec_Degree != 0)
                {
                    count_non_zero++;
                }
            }
            // 依据推荐度排序
            Array.Sort(temp_recDegree);
            recItems = new RecItemid_Degree[count_non_zero];

            // 得到推荐度不为零的项，返回之
            for (int count_rd = 1; count_rd < recItems.Length; count_rd ++ )
            {
                recItems[count_rd] = temp_recDegree[count_rd];
           
            }

            return recItems;
        }


        public static cAssStrategy getAssStrategy(cUser test_User, int Top_N )
        {
            cAssStrategy obj_AssStrategy = new cAssStrategy();

            TestUserLoveItems_id = new ArrayList();
            TestUserDratItems_id = new ArrayList();
            TestUserUnRatingItems_id = new ArrayList();

            bool[] isLoved_Test = test_User.discretizeRating();
         
            // 获得推荐项目中用户喜欢的项目数目
            float count_interest = 0;
            int itemid;
            for (int i = 0; (i < Top_N) && (i < recItems.Length - 1); i++)
            {
                itemid = recItems[i+1].recItems_id;
                // 用户喜欢该项目
                if (isLoved_Test[itemid] == true)
                {
                    count_interest++;
                    TestUserLoveItems_id.Add(i + 1);
                }
                // 用户不喜欢该项目(即对其评分不为零)
                else if (test_User.Ratings[itemid] != 0)
                {
                    TestUserDratItems_id.Add(i + 1);
                }
                // 用户测试集未对该项目进行评分
                else
                {
                    TestUserUnRatingItems_id.Add(i + 1);
                }
            }

            // 如果推荐列表的长度小于Top_N值
            if (recItems.Length < Top_N)
            {
                Top_N = recItems.Length;
            }
            // 计算查准率
            obj_AssStrategy.Precison = (float) (count_interest / (float)Top_N);

            // 计算查全率
            // 测试集中用户喜欢的项目数量
            int count_total = test_User.love_items_num;
            obj_AssStrategy.Recall = (float)(count_interest / (float)count_total);

            return obj_AssStrategy;
        }

//         /// <summary>
//         /// 查询用户是否评价过该部电影
//         /// </summary>
//         /// <param name="itemid">项目id</param>
//         /// <param name="userid">用户id</param>
//         /// <returns>true - 评分过 false - 未评分过</returns>
//         private static bool isIteminUserRatings(int itemid, int userid)
//         {
//             if (sourceUsers[userid].Ratings[itemid] == 0)
//                 return false;
//             else
//                 return true;
//         }
    }
}
