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
    public class CatalogSelectionView : System.ComponentModel.INotifyPropertyChanged
    {
        private DataTable _table = new DataTable();
        public DataTable Table
        {
            get { return _table; }
        }
        public CatalogSelectionView(TimeSeries ts, CatalogProperties catalogProperties)
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

        public CatalogSelectionView(PairedData pd, CatalogProperties catalogProperties)
        {
            _table = pd.ToDataTable(true);
            if (catalogProperties.round != CatalogProperties.Rounding.None)
            {
                foreach (DataRow row in _table.Rows)
                {
                    row["stage"] = catalogProperties.Round((double)row["stage"]);
                    if (pd.Labels != null && pd.Labels.Count != 0)
                    {
                        foreach (var col in pd.Labels)
                        {
                            row[col] = catalogProperties.Round((double)row[col]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < pd.Values.Count; i++)
                        {
                            row[String.Format("value{0}", i + 1)] = catalogProperties.Round((double)row[String.Format("value{0}", i + 1)]);
                        }
                    }
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
