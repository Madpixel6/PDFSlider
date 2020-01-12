using PDFSlider.ViewModels.Abstract;
using System.ComponentModel;

namespace PDFSlider.ViewModels
{
    public abstract class ViewModelBase : IViewModel
    {

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
