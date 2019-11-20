using DSSIO;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace WpfCatalogExplorer
{
    class DSSTable : System.ComponentModel.INotifyPropertyChanged
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
        private void ReadData()
        {
          //set datatable;
          _dssFile = new DssFile(FilePath);
          _table = _dssFile.Catalog;
          NotifyPropertyChanged(nameof(DataTable));
          //throw new NotImplementedException();
        }

        public void EditTimeSeries(int line)
        {

        }

        public void InsertTimeSeries(int line)
        {
            
            NotifyPropertyChanged(nameof(DataTable));
        }

        public void RemoveTimeSeries(int line)
        {
            
            NotifyPropertyChanged(nameof(DataTable));
        }

        protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
