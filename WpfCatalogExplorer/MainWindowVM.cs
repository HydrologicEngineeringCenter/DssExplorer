using DSSIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCatalogExplorer
{
    class MainWindowVM : System.ComponentModel.INotifyPropertyChanged
    {
        private string _filePath;
        private System.Data.DataTable _table;
        
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; NotifyPropertyChanged(); ReadData();  }
        }
        public System.Data.DataTable DataTable
        {
            get { return _table; }
        }
        private void ReadData()
        {
            //set datatable;
            using (DSSReader r = new DSSReader(FilePath))
            {
                DSSPathCollection paths = r.GetCondensedPathNames(true, true, true);
                _table = paths.ToDataTable();
            }
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
