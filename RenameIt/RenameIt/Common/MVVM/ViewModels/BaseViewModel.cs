using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Common.Expressions; // Expressions extension library

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

        /// <summary>
        /// Runs a command if the updating flag is not set
        /// If the flag is true - the function is already running so we do not run the action
        /// If the flag is false - run the action
        /// Once the action is finished, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            // check if the flag property is true (meaning function is already running)
            if (updatingFlag.GetPropertyValue())
                return;

            // set property flag to true to indicate we're running
            updatingFlag.SetPropertyValue(true);

            try
            {
                // run the pass in action
                await action();
            }
            finally
            {
                // set property to false
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}
