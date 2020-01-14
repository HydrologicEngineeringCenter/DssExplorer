using Hec.Dss;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfCatalogExplorer
{
    /// <summary>
    /// Interaction logic for ValueAndTimeTable.xaml
    /// </summary>
    public partial class CatalogSelectionWindow : Window
    {
        public CatalogSelectionWindow(TimeSeries ts, CatalogProperties catalogProperties)
        {
            InitializeComponent();
            DataContext = new CatalogSelectionView(ts, catalogProperties);
            this.Title = ts.Path.FullPath;
        }

        public CatalogSelectionWindow(PairedData pd, CatalogProperties catalogProperties)
        {
            InitializeComponent();
            DataContext = new CatalogSelectionView(pd, catalogProperties);
            this.Title = pd.Path;
        }
    }
}
