using System.ComponentModel;

namespace WpfCatalogExplorer
{
  class MainWindowVM : System.ComponentModel.INotifyPropertyChanged
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

    protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler PropertyChanged;
  }
}
