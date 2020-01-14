using Hec.Dss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCatalogExplorer
{
      /// <summary>
      /// Class to read/write DSS file objects
      /// </summary>
    public class DssFile
    {
        private string _pathToDssFile;
        private DssWriter _Writer;
        public DssFile(string pathToDssFile)
        {
            _pathToDssFile = pathToDssFile;
            _Writer = new DssWriter(pathToDssFile);
        }

        public void Dispose()
        {
            _Writer.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataTable Catalog
        {
            get
            {
                DssPathCollection paths = _Writer.GetCatalog(true);
                var tbl = paths.ToDataTable();
                return tbl;
            }
        }

        public void Delete(string path)
        {
            _Writer.DeleteRecord(path);
        }

        public void InsertEmptyTimeSeries(DssPath path)
        {
            TimeSeries ts = new TimeSeries();
            ts.Path = path;
            _Writer.Write(ts);
        }

        public void AppendToRegularTimeSeries(DssPath path, TimeSeries ts)
        {
            ts.Path = path;
            _Writer.Write(ts);
        }

        public void AppendToIrregularTimeSeries(DssPath path, TimeSeries ts)
        {
            ts.Path = path;
            _Writer.Write(ts);
        }

        public void RemoveFromIrregularTimeSeries(string pathname, int valueIndex)
        {
            var cur_st = _Writer.GetTimeSeries(pathname);

            // remove value from value array in TimeSeries
            var valueList = cur_st.Values.ToList();
            valueList.RemoveAt(valueIndex);
            cur_st.Values = valueList.ToArray();

            // remove respected time stamp for removed value in value array
            var dateList = cur_st.Times.ToList();
            dateList.RemoveAt(valueIndex);
            cur_st.Times = dateList.ToArray();
            
        }

    }
}
