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
    /// Interaction logic for LocationInfoView.xaml
    /// </summary>
    public partial class LocationInfoWindow : Window
    {
        private LocationInformation li;
        public double XCoordinate { get { return li.XOrdinate; } }
        public double YCoordinate { get { return li.YOrdinate; } }
        public double ZCoordinate { get { return li.ZOrdiante; } }
        public string CoordinateSystem { get { return li.CoordinateSystem.ToString(); } }
        public int CoordinateID { get { return li.CoordinateID; } }
        public int HorizontalUnits { get { return li.HorizontalUnits; } }
        public int HorizontalDatum { get { return li.HorizontalDatum; } }
        public int VerticalUnits { get { return li.VerticalUnits; } }
        public int VerticalDatum { get { return li.VerticalDatum; } }
        public string TimeZoneName { get { return li.TimeZoneName; } }
        public string Supplemental { get { return li.Supplemental; } }

        public LocationInfoWindow(LocationInformation li, CatalogProperties catalogProperties)
        {
            InitializeComponent();
            DataContext = this;
            this.li = li;            
        }

        public void DisableEditFeatures()
        {

        }
    }
}
