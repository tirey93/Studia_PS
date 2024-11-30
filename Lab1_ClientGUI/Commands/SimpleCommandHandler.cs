using System.Windows.Input;

namespace Lab1_ClientGUI.Commands
{
    internal class SimpleCommandHandler : ICommand
    {
        private readonly Action action;

        public event EventHandler CanExecuteChanged;

        public SimpleCommandHandler(Action action)
        {
            this.action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
