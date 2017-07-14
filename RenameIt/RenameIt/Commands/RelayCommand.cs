using System;
using System.Windows.Input;

namespace RenameIt.Commands
{
    /// <summary>
    /// Relays a command.
    /// </summary>
    class RelayCommand : ICommand
    {
        #region private fields
        /// <summary>
        /// Action to run.
        /// </summary>
        private Action _action;

        /// <summary>
        /// Checks if we can execute the action.
        /// </summary>
        private bool _canExecute;
        #endregion

        #region public events
        /// <summary>
        /// The event thats fired when <see cref="CanExecute(object)"/> value has changed. 
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region constructor
        /// <summary>
        /// Constructor that sets the <see cref="Action"/> with arguments 
        /// and the CanExecute field to true.
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            this._action = action;
            this._canExecute = true;
        }

        /// <summary>
        /// Sets the <see cref="Action"/> and CanExecute field using arguments.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action action, bool canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }
        #endregion

        #region commands
        /// <summary>
        /// Relay command can execute based on constructor value.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute;
        }

        /// <summary>
        /// Executes the actoin.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this._action();
        }
        #endregion
    }
}
