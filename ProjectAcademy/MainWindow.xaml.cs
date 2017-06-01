using System.Windows;
using System.Windows.Navigation;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly int lineLengh = 20;
        public static readonly int bound = 25;
        public MainWindow()
        {
            InitializeComponent();
            frame.NavigationService.Navigate(new MainMenu());
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
    }
}
