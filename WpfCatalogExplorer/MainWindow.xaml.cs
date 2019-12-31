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

        private void EditTimeSeries(object sender, RoutedEventArgs e)
        {
            if (_canExecute())
            {
                DssTable vm = (DssTable)DataContext;
                //vm.EditTimeSeries(dataGrid.SelectedCells[0].Item);
            }
        }

        private void ViewTimeSeries(object sender, RoutedEventArgs e)
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
                DisplayPairedDataTimeSeries(dssPath, reader);
            }
        }

        private void DisplayPairedDataTimeSeries(DssPath dssPath, DssReader reader)
        {
            PairedData ts = reader.GetPairedData(dssPath);
            ValueAndTimeTable ValueTimeDisplay = new ValueAndTimeTable(ts, catalogProperties);
            ValueTimeDisplay.Show();
            reader.Dispose();
        }

        private void DisplayRegularTimeSeries(DssPath dssPath, DssReader reader)
        {
            TimeSeries ts = reader.GetTimeSeries(dssPath);
            ValueAndTimeTable ValueTimeDisplay = new ValueAndTimeTable(ts, catalogProperties);
            ValueTimeDisplay.Show();
            reader.Dispose();
        }

        private void InsertTimeSeries(object sender, RoutedEventArgs e)
        {
            if (_canExecute())
            {
                //vm.InsertTimeSeries(dataGrid.SelectedIndex);
            }
        }

        private void RemoveTimeSeries(object sender, RoutedEventArgs e)
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
                    if (i == 4) // skip date 
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
                    if (i == 4 || i == 5) // skip date and time
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
        
    }
}
