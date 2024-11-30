
using Lab1_ClientGUI.Commands;
using Lab1_ClientGUI.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Lab1_ClientGUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _logInput;
        public string LogInput
        {
            get { return _logInput; }
            set 
            { 
                if(_logInput != value)
                {
                    _logInput = value;
                    OnPropertyChanged(nameof(LogInput));
                }
            }
        }

        private readonly EchoClientService _echoClientService;

        private ICommand _connectCommand;
        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new SimpleCommandHandler(Connect));

        public MainViewModel(EchoClientService echoClientService)
        {
            _echoClientService = echoClientService;

            _echoClientService.LogChanged += newLog =>
            {
                LogInput += newLog + "\n";
            };
        }

        public void Connect()
        {
            try
            {
                _echoClientService.Connect();
            }
            catch (Exception ex)
            {
                LogInput += "ERROR: " + ex.Message + "\n";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
