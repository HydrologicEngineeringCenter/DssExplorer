using Hec.Dss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private TimeSeries ts { get { return ((TimeSeries)((DssDataTable)DataContext).Record); } }
        public TimeSeriesWindow(TimeSeries ts, CatalogProperties catalogProperties, DssFile dssFile)
        {
            InitializeComponent();
            DataContext = new DssDataTable(ts, catalogProperties);
            this.Title = ts.Path.FullPath;
            TsSaveEvent += dssFile.TsSave;
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
            var points = new List<TimeSeriesPoint>();
            for (int i = 0; i < dg.Items.Count; i++)
            {
                points.Add((TimeSeriesPoint)dg.Items[i]);
            }

            var newTs = new TimeSeries();

            newTs.Path = ts.Path;
            newTs.Units = ts.Units;
            newTs.DataType = ts.DataType;
            newTs.StartDateTime = points[0].DateTime;
            newTs.LocationInformation = ts.LocationInformation;

            var vals = new List<double>();
            var times = new List<DateTime>();
            var qualities = new List<int>();
            for (int i = 0; i < points.Count; i++)
            {
                vals.Add(points[i].Value);
                times.Add(points[i].DateTime);
                if (ts.HasQuality)
                    qualities.Add(points[i].IntQuality);
            }

            newTs.Values = vals.ToArray();
            newTs.Times = times.ToArray();
            if (ts.HasQuality)
                newTs.Qualities = qualities.ToArray();

            TsSaveEvent(newTs);
            MessageBox.Show("Time Series has been saved.");

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

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        public delegate void TsSaveEventHandler(TimeSeries ts);
        public event TsSaveEventHandler TsSaveEvent;

    }
}
