using System.ComponentModel;

namespace RenameIt.ViewModels.Base
{
    /// <summary>
    /// Base ViewModel other view models inherit from to get OnPropertyChanged event.
    /// </summary>
    class ViewModel : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler _propertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this._propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        public void OnPropertyChanged(string propertyName)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
