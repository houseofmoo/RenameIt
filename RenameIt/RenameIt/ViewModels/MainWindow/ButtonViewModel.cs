using System;
using System.Windows.Input;

namespace RenameIt.ViewModels.MainWindow
{
    /// <summary>
    /// ViewModel for buttons.
    /// </summary>
    class ButtonViewModel
    {
        #region private fields
        private ICommand _clickCommand;
        #endregion

        #region public properties
        /// <summary>
        /// ClickCommand
        /// </summary>
        public ICommand ClickCommand
        {
            get { return _clickCommand; }
            private set { _clickCommand = value; }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Define the method fired when the button is clicked by passing an <see cref="Action"/> method.
        /// </summary>
        /// <param name="customMethod"></param>
        public ButtonViewModel(Action customMethod)
        {
            ClickCommand = new Commands.RelayCommand(customMethod);
        }

        /// <summary>
        /// Define the method fired when the button is clicked by passing an <see cref="Action"/> method.
        /// </summary>
        /// <param name="customMethod"></param>
        public ButtonViewModel(Action customMethod, bool active)
        {
            ClickCommand = new Commands.RelayCommand(customMethod, active);
        }
        #endregion
    }
}
