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
    /// Interaction logic for PairedDataWindow.xaml
    /// </summary>
    public partial class PairedDataWindow : Window
    {
        public PairedDataWindow(PairedData pd, CatalogProperties catalogProperties)
        {
            InitializeComponent();
            DataContext = new DssDataTable(pd, catalogProperties);
            this.Title = pd.Path;
        }

        public void DisableEditFeatures()
        {
            FileMenu.Visibility = Visibility.Collapsed;
            dg.IsReadOnly = true;
        }

        private void Save(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {

        }
    }
}
