using DSSIO;
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
    /// Interaction logic for ValueAndTimeTable.xaml
    /// </summary>
    public partial class ValueAndTimeTable : Window
    {
        public ValueAndTimeTable(DSSTimeSeries ts)
        {
            InitializeComponent();
            DataContext = new ValueTimeTable(ts);
        }
    }
}
