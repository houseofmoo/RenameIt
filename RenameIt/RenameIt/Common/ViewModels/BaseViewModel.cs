using System.Collections.Generic;
using System.ComponentModel;

namespace Common.ViewModels
{
    /// <summary>
    /// Base ViewModel other view models inherit from to get OnPropertyChanged event.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler _propertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this._propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        /// <summary>
        /// When a property's value is updated, this even fires
        /// </summary>
        /// <param name="propertyName">Name of property that has been updated</param>
        private void OnPropertyChanged(string propertyName)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Used to set the value of a property
        /// </summary>
        /// <typeparam name="T">Values type</typeparam>
        /// <param name="property">Property to be updated</param>
        /// <param name="newValue">Value property will be updated to</param>
        /// <param name="propertyName">Name of property to be updated</param>
        public void SetProperty<T>(ref T property, T newValue, string propertyName)
        {
            // if current and new value are equal do nothing
            if (EqualityComparer<T>.Default.Equals(property, newValue))
                return;

            // set current to new value
            property = newValue;

            // invoke property changed event
            this.OnPropertyChanged(propertyName);
        }
    }
}
