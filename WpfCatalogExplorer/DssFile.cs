using DSSIO;
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
        private DSSWriter _dssWriter;
        public DssFile(string pathToDssFile)
        {
            _pathToDssFile = pathToDssFile;
            _dssWriter = new DSSWriter(pathToDssFile);
        }

        public void Dispose()
        {
            _dssWriter.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataTable Catalog
        {
            get
            {
                DSSPathCollection paths = _dssWriter.GetCatalog(true);
                var tbl = paths.ToDataTable();
                return tbl;
            }
        }

        public void Delete(string path)
        {
            _dssWriter.DeleteRecord(path);
        }

        public void InsertEmptyTimeSeries(string path)
        {
            if (path == "")
                return;
            DSSTimeSeries ts = new DSSTimeSeries();
            ts.Path = path;
            _dssWriter.Write(ts);
        }

        public void AppendToRegularTimeSeries(string pathname, DSSTimeSeries ts)
        {
            ts.Path = pathname;
            _dssWriter.Write(ts);
        }

        public void AppendToIrregularTimeSeries(string pathname, DSSTimeSeries ts)
        {
            ts.Path = pathname;
            _dssWriter.Write(ts);
        }

        public void RemoveFromIrregularTimeSeries(string pathname, int valueIndex)
        {
            var cur_st = _dssWriter.GetTimeSeries(pathname);

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
