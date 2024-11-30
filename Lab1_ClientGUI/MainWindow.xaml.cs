using Lab1_ClientGUI.Services;
using Lab1_ClientGUI.ViewModels;
using System.Windows;

namespace Lab1_ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EchoClientService _echoClientService;

        public MainWindow(EchoClientService echoClientService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(echoClientService);
            _echoClientService = echoClientService;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _echoClientService.Connect();
            }
            catch (Exception ex)
            {
                LogInput1.Text += "ERROR: " + ex.Message + "\n";
            }
        }
    }
}