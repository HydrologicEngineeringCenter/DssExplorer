using DSSIO;
using Microsoft.Win32;
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
            DataContext = new MainWindowVM();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DSS files|*.dss";
            if (dlg.ShowDialog() == true)
            {
                MainWindowVM vm = (MainWindowVM)DataContext;
                vm.filePath = dlg.FileName;
                //Title = dlg.FileName;
                //using (DSSReader r = new DSSReader(dlg.FileName))
                //{
                //    DSSPathCollection paths = r.GetCondensedPathNames(true,true,true);
                //    grid1.DataTable = paths.ToDataTable();
                //}

            }
        }
 
    }
}
