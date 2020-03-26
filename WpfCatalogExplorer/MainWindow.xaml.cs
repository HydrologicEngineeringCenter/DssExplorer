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
        private DssFile dssFile { get { return ((DssCatalogTableVM)DataContext).File; } }
        public MainWindow()
        {
            catalogProperties = new CatalogProperties();
            InitializeComponent();
            DataContext = new DssCatalogTableVM();
        }

        private void OpenDssFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DSS files|*.dss";
            if (dlg.ShowDialog() == true)
            {
                DssCatalogTableVM catalog = (DssCatalogTableVM)DataContext;
                catalog.FilePath = dlg.FileName;
                this.Title = "DSS Explorer: " + dlg.FileName;

                catalogProperties.File = ((DssCatalogTableVM)DataContext).File;

            }
        }

        private void EditCatalogSelection(object sender, RoutedEventArgs e)
        {
            if (!_canExecute())
                return;

            DssPath dssPath = (DssPath)dataGrid.SelectedItem;
            DssCatalogTableVM catalog = (DssCatalogTableVM)DataContext;
            if (dssPath.RecordType == RecordType.RegularTimeSeries || dssPath.RecordType == RecordType.IrregularTimeSeries)
            {
                TimeSeries ts = catalog.File.GetTimeSeries(dssPath, compression: catalogProperties.compression);
                TimeSeriesWindow tsWindow = new TimeSeriesWindow(ts, catalogProperties, dssFile);
                tsWindow.Show();
            }
            else if (dssPath.RecordType == RecordType.PairedData)
            {
                PairedData pd = catalog.File.GetPairedData(dssPath);
                PairedDataWindow pdWindow = new PairedDataWindow(pd, catalogProperties, dssFile);
                pdWindow.Show();
            }
            else if (dssPath.RecordType == RecordType.LocationInfo)
            {
                LocationInformation li = catalog.File.GetLocationInformation(dssPath);
                LocationInfoWindow liWindow = new LocationInfoWindow(li, catalogProperties);
                liWindow.Show();
            }
        }

        private void ViewCatalogSelection(object sender, RoutedEventArgs e)
        {
            if (!_canExecute())
                return;

            DssPath dssPath = (DssPath)dataGrid.SelectedItem;
            DssCatalogTableVM catalog = (DssCatalogTableVM)DataContext;
            if (dssPath.RecordType == RecordType.RegularTimeSeries || dssPath.RecordType == RecordType.IrregularTimeSeries)
            {
                TimeSeries ts = catalog.File.GetTimeSeries(dssPath, compression: catalogProperties.compression);
                TimeSeriesWindow tsWindow = new TimeSeriesWindow(ts, catalogProperties, dssFile);
                tsWindow.DisableEditFeatures();
                tsWindow.Show();
            }
            else if (dssPath.RecordType == RecordType.PairedData)
            {
                PairedData pd = catalog.File.GetPairedData(dssPath);
                PairedDataWindow pdWindow = new PairedDataWindow(pd, catalogProperties, dssFile);
                pdWindow.DisableEditFeatures();
                pdWindow.Show();
            }
            else if (dssPath.RecordType == RecordType.LocationInfo)
            {
                LocationInformation li = catalog.File.GetLocationInformation(dssPath);
                LocationInfoWindow liWindow = new LocationInfoWindow(li, catalogProperties);
                liWindow.Show();
            }
        }

        private void CatalogInsert(object sender, RoutedEventArgs e)
        {
            if (!_canExecute())
                return;
        }

        private void CatalogRemove(object sender, RoutedEventArgs e)
        {
            if (!_canExecute())
                return;
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

        private void CopyPath(object sender, RoutedEventArgs e)
        {
            if (!_canExecute())
                return;

            Clipboard.SetText(((DssPath)dataGrid.SelectedItem).FullPath);
        }
    }
}
