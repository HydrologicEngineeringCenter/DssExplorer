using Hec.Dss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hec.Dss.TimeWindow;

namespace WpfCatalogExplorer
{
      /// <summary>
      /// Class to read/write DSS file objects
      /// </summary>
    public class DssFile
    {
        private string _pathToDssFile;
        private DssWriter _writer;
        public DssFile(string pathToDssFile)
        {
            _pathToDssFile = pathToDssFile;
            _writer = new DssWriter(pathToDssFile);
        }

        public void Dispose()
        {
            _writer.Dispose();
            GC.SuppressFinalize(this);
        }

        public LocationInformation GetLocationInformation(DssPath dssPath) 
        {
            return _writer.GetLocationInfo(dssPath.FullPath);
        }
        
        public TimeSeries GetTimeSeries(DssPath dssPath, ConsecutiveValueCompression compression = ConsecutiveValueCompression.None) 
        {
            return _writer.GetTimeSeries(dssPath, compression: compression);
        }
        
        public PairedData GetPairedData(DssPath dssPath) 
        {
            return _writer.GetPairedData(dssPath.FullPath);
        }


        public DataTable DataTable
        {
            get
            {
                DssPathCollection paths = _writer.GetCatalog(true);
                var tbl = paths.ToDataTable();
                return tbl;
            }
        }

        public DssPathCollection PathCollection
        {
            get
            {
                return _writer.GetCatalog(true);
            }
        }

        public void TsSave(TimeSeries ts)
        {
            _writer.Write(ts);
        }

        public void PdSave(PairedData pd)
        {
            _writer.Write(pd);
        }

    }
}
