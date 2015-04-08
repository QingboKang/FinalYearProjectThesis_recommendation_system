using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    /// <summary>
    /// 频繁-2项集
    /// </summary>
    public class Frequent_Itemset : IComparable
    {
        public int _itemid_1;         // 项目一的ID
        public int _itemid_2;         // 项目二的ID

        private int _support_count;    // 支持度计数

        public int support_count
        {
            get
            {
                return this._support_count;
            }
            set
            {
                this._support_count = value;
            }
        }

        private static int _min_support_count;    // 最小支持度计数
        public static int min_support_count
        {
            get
            {
                return _min_support_count;
            }
            set
            {
                _min_support_count = value;
            }
        }

        private float _support;        // 支持度
        public float Support
        {
            get
            {
                return this._support;
            }
            set
            {
                _support = value;
            }
        }

        private static float _min_support;    // 支持度阈值
        public static float min_support
        {
            get
            {
                return _min_support;
            }
            set
            {
                _min_support = value;
            }
        }

        //////////////////////////////////////////////////////////////////////////

        public Frequent_Itemset()
        {

        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="itemid_1">项目一ID</param>
        /// <param name="itemid_2">项目二ID</param>
        /// <param name="support_count">支持度计数</param>
        /// <param name="support">支持度</param>
        public Frequent_Itemset(int itemid_1, int itemid_2, int support_count,  float support)
        {
            this._itemid_1 = itemid_1;
            this._itemid_2 = itemid_2;
            this._support_count = support_count;
            this._support = support;
        }


        public int CompareTo(object other)
        {
            Frequent_Itemset otherTemperature = other as Frequent_Itemset;
            if (this.Support == otherTemperature.Support)
                return 0;
            if (this.Support < otherTemperature.Support)
                return 1;
            return -1;
        } 

    }
}
