using Hec.Dss;
using Microsoft.Win32;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CatalogProperties catalogProperties;
        public MainWindow()
        {
            catalogProperties = new CatalogProperties();
            InitializeComponent();
            DataContext = new DssTable();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DSS files|*.dss";
            if (dlg.ShowDialog() == true)
            {
                DssTable vm = (DssTable)DataContext;
                vm.FilePath = dlg.FileName;
            }
        }

        private void EditCatalogSelection(object sender, RoutedEventArgs e)
        {
            if (_canExecute())
            {
                DssTable vm = (DssTable)DataContext;
                //vm.EditTimeSeries(dataGrid.SelectedCells[0].Item);
            }
        }

        private void ViewCatalogSelection(object sender, RoutedEventArgs e)
        {

            DssPath dssPath = (DssPath)dataGrid.SelectedItem;
            DssTable vm = (DssTable)DataContext;
            //DssReader reader = new DssReader(FIle.FilePath);
            if (dssPath.RecordType == RecordType.RegularTimeSeries || dssPath.RecordType == RecordType.IrregularTimeSeries)
            {
                TimeSeries ts = vm.File.GetTimeSeries(dssPath, compression: catalogProperties.compression);
                CatalogSelectionWindow tsWindow = new CatalogSelectionWindow(ts, catalogProperties);
                tsWindow.Show();
            }
            else if (dssPath.RecordType == RecordType.PairedData)
            {
                PairedData pd = vm.File.GetPairedData(dssPath);
                CatalogSelectionWindow pdWindow = new CatalogSelectionWindow(pd, catalogProperties);
                pdWindow.Show();
            }
            else if (dssPath.RecordType == RecordType.LocationInfo)
            {
                LocationInformation li = vm.File.GetLocationInformation(dssPath);
                LocationInfoWindow liWindow = new LocationInfoWindow(li, catalogProperties);
                liWindow.Show();
            }
        }

        private void CatalogInsert(object sender, RoutedEventArgs e)
        {
            if (_canExecute())
            {
                //vm.InsertTimeSeries(dataGrid.SelectedIndex);
            }
        }

        private void CatalogRemove(object sender, RoutedEventArgs e)
        {
            if (_canExecute())
            {
                //vm.RemoveTimeSeries(dataGrid.SelectedIndex);
            }
        }

        private bool _canExecute()
        {
            if (dataGrid.SelectedCells.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void SetRounding(object sender, RoutedEventArgs e)
        {
            catalogProperties.SetRounding(RoundingMenu.Items.IndexOf(sender));
        }

        private void SetCompression(object sender, RoutedEventArgs e)
        {
            catalogProperties.SetCompression(CompressionMenu.Items.IndexOf(sender));
        }
        
    }
}
