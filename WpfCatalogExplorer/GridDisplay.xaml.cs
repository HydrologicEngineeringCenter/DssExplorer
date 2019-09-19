using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCatalogExplorer
{
    /// <summary>
    /// Interaction logic for GridDisplay.xaml
    /// </summary>
    public partial class GridDisplay : UserControl
    {
        public GridDisplay()
        {
            InitializeComponent();
        }

        public DataTable DataTable {

            set {
                grid1.ItemsSource = value.DefaultView;
             //  grid1.AutoGenerateColumns = true;
              //  grid1.CanUserAddRows = false;
            }
         }
    }
}
