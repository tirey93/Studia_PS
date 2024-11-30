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
        public MainWindow(EchoClientService echoClientService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(echoClientService);
        }
    }
}