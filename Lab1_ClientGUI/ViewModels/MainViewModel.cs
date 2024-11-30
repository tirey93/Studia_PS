
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
        private string _sendInput;
        public string SendInput
        {
            get { return _sendInput; }
            set 
            { 
                if(_sendInput != value)
                {
                    _sendInput = value;
                    OnPropertyChanged(nameof(SendInput));
                }
            }
        }

        private readonly EchoClientService _echoClientService;

        private ICommand _connectCommand;
        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new SimpleCommandHandler(Connect));
        private ICommand _sendCommand;
        public ICommand SendCommand => _sendCommand ?? (_sendCommand = new SimpleCommandHandler(Send));

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

        public void Send()
        {
            try
            {
                _echoClientService.Send(SendInput);
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
