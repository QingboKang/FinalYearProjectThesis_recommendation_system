using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Recommendation_Algorithm;

namespace Recommender_algorithm_DEMO
{
    public partial class Form_AssociationRules : Form
    {
        public ArrayList association_Rules = new ArrayList();

        public movieInfo[] objs_movieInfo;

        public Form_AssociationRules()
        {
            InitializeComponent();
            association_Rules = cApriori.association_Rules;
            AssociationRule obj;

            // 得到电影信息
            cItem obj_item = new cItem();
            objs_movieInfo = cItem.movies;

            dataGridView1.RowHeadersVisible = false;

            // 填充dataGridView1
            for (int count = 0; count < association_Rules.Count; count++)
            {
                // 取得每个关联规则信息
                obj = (AssociationRule)association_Rules[count];
                this.dataGridView1.Rows.Add(count + 1, objs_movieInfo[obj._itemid_1].name,
                    objs_movieInfo[obj._itemid_2].name, obj.Support, obj.confidence);
            }
        }


    }
}
