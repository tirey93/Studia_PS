
using Lab1_ClientGUI.Services;
using System.ComponentModel;

namespace Lab1_ClientGUI.ViewModels
{
    public class MainViewModel
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

        public MainViewModel(EchoClientService echoClientService)
        {
            _echoClientService = echoClientService;

            _echoClientService.LogChanged += newLog =>
            {
                LogInput += newLog + "\n";
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
