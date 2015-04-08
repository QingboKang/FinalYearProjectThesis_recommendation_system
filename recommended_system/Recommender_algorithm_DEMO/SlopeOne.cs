using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    public class Rating
    {
        public float Value { get; set; }
        public int Freq { get; set; }

        private float averageValue;

        public float AverageValue
        {
            get
            { 
                return Value / Freq;
            }
            set
            {
                averageValue = value;
            }
        }
    }

    public class RatingDifferenceCollection : Dictionary<string, Rating>
    {
        private string GetKey(int Item1Id, int Item2Id)
        {
            return (Item1Id < Item2Id) ? Item1Id + "/" + Item2Id : Item2Id + "/" + Item1Id;
        }

        public bool Contains(int Item1Id, int Item2Id)
        {
            return this.Keys.Contains<string>(GetKey(Item1Id, Item2Id));
        }

        public Rating this[int Item1Id, int Item2Id]
        {
            get
            {
                return this[this.GetKey(Item1Id, Item2Id)];
            }
            set { this[this.GetKey(Item1Id, Item2Id)] = value; }
        }
    }

    public class SlopeOne
    {
        public  cUser[] objUsers = new cUser[cReadinData.totalUserNum];
        public  cUser[] testUsers;

        public RatingDifferenceCollection _DiffMarix = new RatingDifferenceCollection();  // The dictionary to keep the diff matrix
        public HashSet<int> _Items = new HashSet<int>();  // Tracking how many items totally


        public void AddUserRatings(IDictionary<int, float> userRatings)
        {
            foreach (var item1 in userRatings)
            {
                int item1Id = item1.Key;
                float item1Rating = item1.Value;
                _Items.Add(item1.Key);

                foreach (var item2 in userRatings)
                {
                    if (item2.Key <= item1Id) continue; // Eliminate redundancy
                    int item2Id = item2.Key;
                    float item2Rating = item2.Value;

                    Rating ratingDiff;
                    if (_DiffMarix.Contains(item1Id, item2Id))
                    {
                        ratingDiff = _DiffMarix[item1Id, item2Id];
                    }
                    else
                    {
                        ratingDiff = new Rating();
                        _DiffMarix[item1Id, item2Id] = ratingDiff;
                    }

                    ratingDiff.Value += item1Rating - item2Rating;
                    ratingDiff.Freq += 1;
                }
            }
        }

        // Input ratings of all users
        public void AddUerRatings(IList<IDictionary<int, float>> Ratings)
        {
            foreach (var userRatings in Ratings)
            {
                AddUserRatings(userRatings);
            }
        }

        public IDictionary<int, float> Predict(IDictionary<int, float> userRatings)
        {
            Dictionary<int, float> Predictions = new Dictionary<int, float>();
            foreach (var itemId in this._Items)
            {
                if (userRatings.Keys.Contains(itemId)) continue; // User has rated this item, just skip it

                Rating itemRating = new Rating();

                foreach (var userRating in userRatings)
                {
                    if (userRating.Key == itemId) continue;
                    int inputItemId = userRating.Key;
                    if (_DiffMarix.Contains(itemId, inputItemId))
                    {
                        Rating diff = _DiffMarix[itemId, inputItemId];
                        itemRating.Value += diff.Freq * (userRating.Value + diff.AverageValue * ((itemId < inputItemId) ? 1 : -1));
                        itemRating.Freq += diff.Freq;
                    }
                }
//                 if (itemRating.AverageValue > 5)
//                 {
//                     itemRating.AverageValue = 5;
//                 }
                Predictions.Add(itemId, itemRating.AverageValue);
            }
            return Predictions;
        }

        /// <summary>
        /// 计算MAE值
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="userRatings">用户评分数据</param>
        /// <returns>MAE值</returns>
        private double calculateMAE(int userid, IDictionary<int, float> userRatings)
        {
         //   testUser = new cUser[];

            double sum = 0;
            int count = 0;
            for (int i = 1; i < objUsers[1].Ratings.Length; i++)
            {
                
                if (testUsers[userid].Ratings[i] != 0 && userRatings.Keys.Contains(i))
                {
                    // 预测的评分值减去实际的评分值
                    sum += Math.Abs(userRatings[i] - testUsers[userid].Ratings[i]);
                    count++;
                }
            }
            if (count == 0)
                return 0;

            return sum / count;
        }



        public cAssStrategy getAssStrategy(int userid, IDictionary<int, float> userRatings, int Rec_Items_num, cUser testUser)
        {
            cAssStrategy obj_AssStrategy = new cAssStrategy();
            objUsers = cReadinData.getBaseUser();
            this.testUsers = cReadinData.getTestUser();

            cUser sourceUser = objUsers[userid];

                //             for (int count = 0; count < userRatings.Count; count++)
                //             {
                //                 if (userRatings.ElementAt(count).Value > 5)
                //                     userRatings.ElementAt(count).Value = 5;
                //             }
                // 计算MAE值
                obj_AssStrategy.MAE = calculateMAE(userid, userRatings);

            //////////////////////////////////////////////////////////////////////////

            // 计算关于Top-N推荐的分类精确度准则
            int[] itemid_TopN = new int[Rec_Items_num];
            int count_interest = 0;       // 记录用户对推荐的项目有兴趣的个数
            int count_total = 0;          // 记录用户测试集合中
            float inter_rating = (float) ((float)sourceUser.getTotalRating() / (float)sourceUser.RatingNums );     // 用户的平均评分

            // 排序，便于后面产生推荐
            Sort( ref userRatings );

            IDictionary<int, float> userRatings_test = new Dictionary<int, float>();

            int count = 0;

            // 产生Top-N推荐的项目ID，并计算用户对推荐感兴趣的项目数量
            for (int i = userRatings.Count - 1; i >= 0; i-- )
            {
                if (count == Rec_Items_num)
                    break;
                int itemid = userRatings.ElementAt(i).Key;
                if (testUser.Ratings[itemid] != 0)
                {
                    userRatings_test.Add(userRatings.ElementAt(i));
                    if(testUser.Ratings[itemid] >= inter_rating)
                        count_interest++;
                    count++;
                    continue;
                }
            }

            // 计算测试集中该用户喜欢的项目数量
            foreach (int rating in testUser.Ratings)
            {
                if (rating >= inter_rating)
                    count_total++;
            }

            obj_AssStrategy.Precison = (float)count_interest / itemid_TopN.Length;    // 查准率(Precison)
            if (count_total == 0)
                obj_AssStrategy.Recall = 0;
            else
                obj_AssStrategy.Recall = (float)count_interest / count_total;       // 查全率(Recall)

            return obj_AssStrategy;
        }

    /*    private int selectKeyofMaxValue(IDictionary<int, float> userRatings)
        {
            int maxKey = 0;          // 最大Value所对应的Key
            float maxValue = 0;      // 最大的Value
            for (int count = 0; count < userRatings.Count; count++ )
            {
                float value = userRatings.ElementAt(count).Value;
                if (maxValue < value)
                {
                    maxValue = value;
                    maxKey = userRatings.ElementAt(count).Key;
                    userRatings.ElementAt(count).Value = 0;
                }
            }

            return maxKey;
        }*/

        // 对Dictionary按照value值进行排序
        public static void Sort(ref IDictionary<int, float> dic)
        {
            List<int> keys = new List<int>();
            List<float> values = new List<float>();

            int i = 0;
            foreach (int item in dic.Keys)
            {
                keys.Add(item);
                if (!values.Contains(dic[item]))
                    values.Add(dic[item]);
                i++;
            }

            values.Sort();
            
            while (keys.Count != values.Count)
            {
                values.Add(values[values.Count - 1] + 1);

            }

            for (int j = 0; j < i; j++)
            {
                dic[keys[j]] = values[j];
            }
        }



      /*  public static void Main(String[] args)
        {
            cReadinData rtd = new cReadinData(0);
            testUser = new cUser[cReadinData.getTestUser().Length];

            objUsers = cReadinData.getBaseUser();
            testUser = cReadinData.getTestUser();

            SlopeOne test = new SlopeOne();

            Dictionary<int, float> userRating = new Dictionary<int, float>();

            for (int i = 1; i < objUsers.Length; i++)
            {
                userRating = new Dictionary<int, float>();

                for (int j = 1; j < objUsers[i].Ratings.Length; j++)
                {
                    if (objUsers[i].Ratings[j] != 0)
                    {
                        userRating.Add(j, (float)objUsers[i].Ratings[j]);
                    }
                }

                test.AddUserRatings(userRating);
            }

            double totalMAE = 0;
            double MAE;

            for (int i = 1; i < objUsers.Length; i++)
            {
                userRating = new Dictionary<int, float>();

                for (int j = 1; j < objUsers[i].Ratings.Length; j++)
                {
                    if (objUsers[i].Ratings[j] != 0)
                    {
                        userRating.Add(j, (float)objUsers[i].Ratings[j]);
                    }
                }

                IDictionary<int, float> Predictions = test.Predict(userRating);

                MAE = test.calculateMAE(i, Predictions);
                totalMAE += MAE;

                Console.WriteLine("user id:{0}   MAE:{1}    count:{2}", i, test.calculateMAE(i, Predictions), Predictions.Count);
                if (i % 10 == 0)
                {
                    Console.WriteLine("Average MAE:{0}", totalMAE / 10);
                    totalMAE = 0;
                    Console.ReadKey();
                }
            }
        }
    
    
    */
    }
}
         