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
            for (int i = 1; i < 7; i++) // only get A-F parts of table
            {
                if (i == 4) // skip date 
                {
                    selectedPath += "/";
                    continue;
                }
                selectedPath += dataRow.Row.ItemArray[i].ToString() + "/";
            }
            return selectedPath;
        }

        private void SetRounding(object sender, RoutedEventArgs e)
        {
            catalogProperties.SetRounding(RoundingMenu.Items.IndexOf(sender));
        }
        
    }
}
