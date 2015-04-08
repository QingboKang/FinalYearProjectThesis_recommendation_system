using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommendation_Algorithm
{
    /// <summary>
    /// 定义算法评价准则与指标的类
    /// </summary>
    public class cAssStrategy
    {
        // mean absolute error 平均绝对误差
        public double MAE { get; set; }

        // 查准率
        public float Precison{ get; set; }

        // 查全率
        public float Recall { get; set; }

        // F值
        private float F_Measure;

        public float calculateF_Measure()
        {
            if (this.Recall + this.Precison == 0)
                return 0;
            return (2 * this.Precison * this.Recall) / (this.Recall + this.Precison);
        }           
    }
}
