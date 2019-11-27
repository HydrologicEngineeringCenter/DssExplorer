using DSSIO;
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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DSSTable();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DSS files|*.dss";
            if (dlg.ShowDialog() == true)
            {
                DSSTable vm = (DSSTable)DataContext;
                vm.FilePath = dlg.FileName;
            }
        }

        private void EditTimeSeries(object sender, RoutedEventArgs e)
        {
            if (_canExecute())
            {
                DSSTable vm = (DSSTable)DataContext;
                //vm.EditTimeSeries(dataGrid.SelectedCells[0].Item);
            }
        }

        private void ViewTimeSeries(object sender, RoutedEventArgs e)
        {
            string selectedPath = GetPathFromRow();
            DSSPath dssPath = new DSSPath(selectedPath);
            DSSTable vm = (DSSTable)DataContext;
            DSSReader reader = new DSSReader(vm.FilePath);
            DSSTimeSeries ts = reader.GetTimeSeries(dssPath);
            ValueAndTimeTable ValueTimeDisplay = new ValueAndTimeTable(ts);
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
            for (int i = 0; i < 6; i++) // only get A-F parts of table
            {
                if (i == 3)
                {
                    selectedPath += "/";
                    continue;
                }
                selectedPath += dataRow.Row.ItemArray[i].ToString() + "/";
            }
            return selectedPath;
        }
    }
}
