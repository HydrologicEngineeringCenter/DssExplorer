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
        private PairedData pd { get { return ((PairedData)((DssDataTableVM)DataContext).Record); } }
        public PairedDataWindow(PairedData pd, CatalogProperties catalogProperties, DssFile dssFile)
        {
            InitializeComponent();
            DataContext = new DssDataTableVM(pd, catalogProperties);
            this.Title = pd.Path.FullPath;

            PdSaveEvent += dssFile.PdSave;
        }

        public void DisableEditFeatures()
        {
            FileMenu.Visibility = Visibility.Collapsed;
            dg.IsReadOnly = true;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            PdSaveEvent(pd);
            MessageBox.Show("Paired Data has been saved.");
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {

        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        public delegate void PdSaveEventHandler(PairedData pd);
        public event PdSaveEventHandler PdSaveEvent;
    }
}
