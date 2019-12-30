using Hec.Dss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCatalogExplorer
{
    public class ValueTimeTable : System.ComponentModel.INotifyPropertyChanged
    {
        private DataTable _table = new DataTable();
        public DataTable Table
        {
            get { return _table; }
        }
        public ValueTimeTable(TimeSeries ts, CatalogProperties catalogProperties)
        {
            _table = ts.ToDataTable(true);
            if (catalogProperties.round != CatalogProperties.Rounding.None)
            {
                foreach (DataRow row in _table.Rows)
                {
                    row["value"] = catalogProperties.Round((double)row["value"]);
                }
            }
            NotifyPropertyChanged(nameof(_table));
        }

        protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
