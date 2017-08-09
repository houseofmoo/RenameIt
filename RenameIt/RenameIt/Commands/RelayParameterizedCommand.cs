using System;
using System.Windows.Input;

namespace RenameIt.Commands
{
    class RelayParameterizedCommand : ICommand
    {
        #region fields
        /// <summary>
        /// Action to run.
        /// </summary>
        private Action<object> _action;

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
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayParameterizedCommand(Action<object> action)
        {
            this._action = action;
            this._canExecute = true;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayParameterizedCommand(Action<object> action, bool canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }
        #endregion

        #region commands
        /// <summary>
        /// Relay command can always execute.
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
            _action(parameter);
        }
        #endregion
    }
}
