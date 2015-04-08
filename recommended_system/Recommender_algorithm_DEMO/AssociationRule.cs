using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Recommendation_Algorithm
{
    /// <summary>
    /// 该类定义了关联规则
    /// 派生自频繁-2项集类
    /// </summary>
    public class AssociationRule : Frequent_Itemset
    {
        private static string[] movieNames = new string[1682];

        private float _confidence;  // 置信度
        public float confidence
        {
            get
            {
                return this._confidence;
            }
            set
            {
                this._confidence = value;
            }
        }

     /*   public string item_1_name   // 项目1的电影名字
        {
            get
            {
                return movieNames[_itemid_1 - 1];
            }
            set
            {
                item_1_name = value;
            }
        }

        public string item_2_name   // 项目2的电影名字
        {
            get
            {
                return movieNames[_itemid_2 - 1];
            }
            set
            {
                item_2_name = value;
            }
        }*/
 
        public static float min_confidence;  // 置信度阈值

        public float Reco_degrees        // 关联度
        {
            get
            {
                return ( this._confidence * this.Support );
            }
        }

        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="itemid_1">项目1id</param>
        /// <param name="itemid_2">项目2id</param>
        /// <param name="support">支持度</param>
        /// <param name="confidence">置信度</param>
        public AssociationRule(int itemid_1, int itemid_2, float support, float confidence)
        {
            this._itemid_1 = itemid_1;
            this._itemid_2 = itemid_2;
            this.Support = support;
            this.confidence = confidence;
        }

     /*   public static void readMovieName()
        {
            StreamReader rs = new StreamReader("u.item", Encoding.Default);
            string sLine = "";
            int count = 0;

            while (sLine != null)
            {
                sLine = rs.ReadLine();
                if (sLine == null)
                    break;

                int start = sLine.IndexOf('|') + 1;
                int end = sLine.IndexOf('(');
                if (end > start)
                {
                    string name = sLine.Substring(start, end - start);
                    if (name.EndsWith("The "))
                    {
                        Console.WriteLine("yes");
                        name = "The " + name.Substring(0, name.Length - 6);
                        
                    }
                    movieNames[count++] = name;
                }
                else
                {
                    movieNames[count++] = "unknown";
                }

            }
        }*/

        public int CompareTo(object other)
        {
            AssociationRule otherTemperature = other as AssociationRule;
            if (this.Reco_degrees == otherTemperature.Reco_degrees)
                return 0;
            if (this.Reco_degrees < otherTemperature.Reco_degrees)
                return 1;
            return -1;
        } 
    }
}
