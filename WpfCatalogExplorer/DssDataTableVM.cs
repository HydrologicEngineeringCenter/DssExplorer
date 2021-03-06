﻿using Hec.Dss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCatalogExplorer
{
    public class DssDataTableVM : System.ComponentModel.INotifyPropertyChanged
    {
        private DataTable _table = new DataTable();
        private List<TimeSeriesPoint> _points = new List<TimeSeriesPoint>();
        public object Record;
        private CatalogProperties _catalogProperties;
        public DataTable Table
        {
            get { return _table; }
        }

        public List<TimeSeriesPoint> Points
        {
            get { return _points; }
        }

        public DssDataTableVM(TimeSeries ts, CatalogProperties catalogProperties)
        {
            Record = ts;
            _catalogProperties = catalogProperties;
            _table = ts.ToDataTable();
            _points = ts.ToTimeSeriesPoints();
            RoundValues(ts, catalogProperties);
            NotifyPropertyChanged(nameof(Table));
            NotifyPropertyChanged(nameof(Points));
        }

        public DssDataTableVM(PairedData pd, CatalogProperties catalogProperties)
        {
            Record = pd;
            _catalogProperties = catalogProperties;
            _table = pd.ToDataTable();
            RoundValues(pd, catalogProperties);
            NotifyPropertyChanged(nameof(Table));
        }

        private void RoundValues(object record, CatalogProperties catalogProperties)
        {
            if (record is PairedData)
            {
                var pd = (PairedData)record;
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
            }
            else if (record is TimeSeries)
            {
                var ts = (TimeSeries)record;
                if (catalogProperties.round != CatalogProperties.Rounding.None)
                {
                    foreach (DataRow row in _table.Rows)
                    {
                        row["value"] = catalogProperties.Round((double)row["value"]);
                    }
                }
            }
        }

        protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
