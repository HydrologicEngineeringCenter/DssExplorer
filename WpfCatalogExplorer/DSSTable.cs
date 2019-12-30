using Hec.Dss;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace WpfCatalogExplorer
{
    class DssTable : System.ComponentModel.INotifyPropertyChanged
    {
        private string _filePath;
        private System.Data.DataTable _table;
        private DssFile _dssFile;

        public string FilePath
        {
          get { return _filePath; }
          set { _filePath = value; NotifyPropertyChanged(); ReadData(); }
        }
        public System.Data.DataTable DataTable
        {
          get { return _table; }
        }
        public DssFile File
        {
            get { return _dssFile; }
        }

        private void ReadData()
        {
          _dssFile = new DssFile(FilePath);
          _table = _dssFile.Catalog;
          NotifyPropertyChanged(nameof(DataTable));
        }

        protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
