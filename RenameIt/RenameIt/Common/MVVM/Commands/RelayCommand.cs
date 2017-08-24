using System;
using System.Windows.Input;

namespace Common.Commands
{
    /// <summary>
    /// Relays a command
    /// </summary>
    class RelayCommand<T> : ICommand
    {
        #region fields
        private readonly Action<T> _action = null;
        private readonly Func<T, bool> _canExecuteAction = null;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<T> execute) : this(execute, null) { }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="action">The execution logic.</param>
        /// <param name="canExecuteAction">The execution status logic.</param>
        public RelayCommand(Action<T> action, Func<T, bool> canExecuteAction)
        {
            if (action == null)
                throw new ArgumentNullException("execute", "execute parameter cannot be null");

            this._action = action;
            this._canExecuteAction = canExecuteAction;
        }
        #endregion

        #region ICommand members
        ///<summary>
        /// Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>True if this command can be executed; otherwise, false</returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecuteAction == null || this._canExecuteAction((T)parameter);
        }

        ///<summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        /// Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            this._action((T)parameter);
        }
        #endregion
    }
}
