using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModels;
using CoreLogic;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {

        CoreDBAnalyzerLogic logic = new CoreDBAnalyzerLogic();
        DatabaseComparison dc = new DatabaseComparison(); 

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logic.SetInitialDBRefrencePoint();
            this.Text = "REF SET TIME:" + DateTime.Now.ToShortTimeString();
            MessageBox.Show("operation complete");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dc = logic.CompareToLastRefrencePoint();
            dataGridView1.DataSource = dc.Comparison.Select(n => new { TableName = n.OriginalTalbe.TableName, Difference = n.EntryDifferences }).ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                string tableName =dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                //dataGridView2.DataSource = (logic.getDifferenceData(dc, ).Tables[0]);

                dataGridView2.DataSource = dc.Comparison.Where(n => n.OriginalTalbe.TableName == tableName).FirstOrDefault().DifferenceTable;

            }

        }



#region private methods
        
        void colorGrid()
        {
            foreach(DataGridViewRow dr in dataGridView1.Rows)
            {
                if ( Convert.ToInt32(dr.Cells["Difference"].Value) < 0)
                {
                    foreach (DataGridViewCell dc in dr.Cells)
                        dc.Style.BackColor = Color.Pink;
                }
                else
                {
                    foreach (DataGridViewCell dc in dr.Cells)
                        dc.Style.BackColor = Color.Green;

                }
                        

            }

        }

#endregion



    }
}
