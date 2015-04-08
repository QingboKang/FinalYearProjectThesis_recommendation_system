using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    /// <summary>
    /// 频繁-1项集
    /// </summary>
    class Freq_Item : IComparable
    {
        public int itemid;   // 项目id

        private int _support_count;    // 项目支持度计数
        public int support_count
        {
            get
            {
                return this._support_count;
            }
        }

        private float _support;  // 项目支持度
        public float support
        {
            get
            {
                return this._support;
            }
        }

        public static int min_support_count;    // 最小支持度计数
        public static float min_support;        // 最小支持度

        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="itemid">项目id</param>
        /// <param name="support_count">支持度计数</param>
        /// <param name="support">支持度</param>
        public Freq_Item(int itemid, int support_count, float support)
        {
            this.itemid = itemid;
            this._support_count = support_count;
            this._support = support;
        }


        public int CompareTo(object other)
        {
            Freq_Item otherTemperature = other as Freq_Item;
            if (this._support_count == otherTemperature._support_count)
                return 0;
            if (this._support_count < otherTemperature._support_count)
                return 1;
            return -1;
        } 
    }
}
