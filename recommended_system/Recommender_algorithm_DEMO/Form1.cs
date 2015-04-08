using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Recommender_algorithm_DEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void jiyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_UBCF objForm_UBCF = new Form_UBCF();
            objForm_UBCF.Show();
        }

        private void slopeOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   Form_SlopeOne objForm_SlopeOne = new Form_SlopeOne();
          //  objForm_SlopeOne.Show();
        }

        private void ItemCFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_IBCF objForm_IBCF = new Form_IBCF();
            objForm_IBCF.Show();
        }

        private void aprioriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Apriori objForm_Apriori = new Form_Apriori();
            objForm_Apriori.Show();
        }

        private void slopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SlopeOne objForm_SlopeOne = new Form_SlopeOne();
            objForm_SlopeOne.Show();
        }
    }
}
