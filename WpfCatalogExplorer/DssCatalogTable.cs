using Hec.Dss;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace WpfCatalogExplorer
{
    class DssCatalogTable : System.ComponentModel.INotifyPropertyChanged
    {
        private string _filePath;
        private System.Data.DataTable _table;
        private DssFile _dssFile;

        public string FilePath
        {
          get { return _filePath; }
          set { _filePath = value; ReadData(); }
        }
        public System.Data.DataTable DataTable
        {
          get { return _table; }
        }

        public DssPathCollection PathCollection
        {
            get
            { 
                if (_dssFile == null) { return null; }
                return _dssFile.PathCollection; 
            }

        }

        public DssFile File
        {
            get { return _dssFile; }
        }

        private void ReadData()
        {
          _dssFile = new DssFile(FilePath);
          _table = _dssFile.DataTable;
          NotifyPropertyChanged(nameof(DataTable));
          NotifyPropertyChanged(nameof(PathCollection));
        }

        protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
