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
    /// Interaction logic for TimeSeriesWindow.xaml
    /// </summary>
    public partial class TimeSeriesWindow : Window
    {
        public TimeSeriesWindow(TimeSeries ts, CatalogProperties catalogProperties)
        {
            InitializeComponent();
            DataContext = new DssDataTable(ts, catalogProperties);
            this.Title = ts.Path.FullPath;
        }

        private void ShowQualityChecked(object sender, RoutedEventArgs e)
        {
            ShowColumn("Quality");
        }
        
        private void ShowQualityUnchecked(object sender, RoutedEventArgs e)
        {
            HideColumn("Quality");
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

        private void HideColumn(string column)
        {
            var c = dg.Columns.First(x => x.Header.ToString() == column);
            int idx = dg.Columns.IndexOf(c);
            dg.Columns[idx].Visibility = Visibility.Collapsed;
        }

        private void ShowColumn(string column)
        {
            var c = dg.Columns.First(x => x.Header.ToString() == column);
            int idx = dg.Columns.IndexOf(c);
            dg.Columns[idx].Visibility = Visibility.Visible;
        }

        private void dg_Loaded(object sender, RoutedEventArgs e)
        {
            HideColumn("Quality");
        }
    }
}
