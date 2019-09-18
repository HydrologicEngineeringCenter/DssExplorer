using DSSIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatalogExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if( dlg.ShowDialog() == DialogResult.OK)
            {

                using ( DSSReader r = new DSSReader(dlg.FileName))
                {

                    DSSPathCollection paths = r.GetCondensedPathNames(true);

                    dataGridView1.DataSource = paths.ToDataTable();

                }

            }
        }
    }
}
