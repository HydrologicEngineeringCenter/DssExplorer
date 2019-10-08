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
          DSSPathCollection paths = _dssWriter.GetCondensedPathNames(true, true, true);
          var tbl = paths.ToDataTable();
        //tbl.Rows.RemoveAt(12);
        return tbl;
      }
    }

    public void Delete(string path)
    {
      _dssWriter.DeleteRecord(path);
    }


  }
}
