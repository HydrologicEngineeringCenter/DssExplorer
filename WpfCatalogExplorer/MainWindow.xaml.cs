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

            string selectedPath = GetPathFromRow();
            DssPath dssPath = new DssPath(selectedPath);
            DssTable vm = (DssTable)DataContext;
            DssReader reader = new DssReader(vm.FilePath);
            if (catalogProperties.RecordType((DataRowView)dataGrid.SelectedItem) == "RegularTimeSeries")
            {
                DisplayRegularTimeSeries(dssPath, reader);
            }
            else if (catalogProperties.RecordType((DataRowView)dataGrid.SelectedItem) == "PairedData")
            {
                DisplayPairedData(dssPath, reader);
            }
            else if (catalogProperties.RecordType((DataRowView)dataGrid.SelectedItem) == "LocationInfo")
            {
                DisplayLocationInfo(dssPath, reader);
            }
        }

        private void DisplayLocationInfo(DssPath dssPath, DssReader reader)
        {
            LocationInformation li = reader.GetLocationInfo(dssPath.FullPath);
            LocationInfoWindow liWindow = new LocationInfoWindow(li, catalogProperties);
            liWindow.Show();
            reader.Dispose();
        }

        private void DisplayPairedData(DssPath dssPath, DssReader reader)
        {
            PairedData pd = reader.GetPairedData(dssPath.FullPath);
            CatalogSelectionWindow pdWindow = new CatalogSelectionWindow(pd, catalogProperties);
            pdWindow.Show();
            reader.Dispose();
        }

        private void DisplayRegularTimeSeries(DssPath dssPath, DssReader reader)
        {
            TimeSeries ts = reader.GetTimeSeries(dssPath, compression: catalogProperties.compression);
            CatalogSelectionWindow tsWindow = new CatalogSelectionWindow(ts, catalogProperties);
            tsWindow.Show();
            reader.Dispose();
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

        private string GetPathFromRow()
        {
            DataRowView dataRow = (DataRowView)dataGrid.SelectedItem;
            string selectedPath = "/";
            if (catalogProperties.RecordType(dataRow) == "RegularTimeSeries") // check if selected time sereis is "Regular Time Series
            {
                for (int i = 1; i < 7; i++) // only get A-F parts of table
                {
                    if (i == 4) // skip D part 
                    {
                        selectedPath += "/";
                        continue;
                    }
                    selectedPath += dataRow.Row.ItemArray[i].ToString() + "/";
                }
            }
            else if (catalogProperties.RecordType(dataRow) == "PairedData") // check if selected time series is "Paired Data"
            {
                for (int i = 1; i < 7; i++) // only get A-F parts of table
                {
                    selectedPath += dataRow.Row.ItemArray[i].ToString() + "/";
                }
            }
            else if (catalogProperties.RecordType(dataRow) == "LocationInfo")
            {
                for (int i = 1; i < 7; i++) // only get A-F parts of table
                {
                    if (i == 4 || i == 5 || i == 6) // skip D, E, and F parts
                    {
                        selectedPath += "/";
                        continue;
                    }
                    selectedPath += dataRow.Row.ItemArray[i].ToString() + "/";
                }
            }
            return selectedPath;
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
