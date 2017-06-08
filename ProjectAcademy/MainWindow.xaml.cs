using System;
using System.Media;
using System.Windows;
using System.Windows.Media;
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
            LoadSounds();
        }
        private void LoadSounds()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.CurrentProjectFolder(), "Resources");
            MainMenu.ButtonClickSound = new SoundPlayer(path + "/ButtonClick.wav");
            MainMenu.ButtonClickSound.Load();
            MainMenu.PlayerWalkSound = new SoundPlayer(path + "/PlayerWalk.wav");
            MainMenu.PlayerWalkSound.Load();
            MainMenu.SliderClickSound = new SoundPlayer(path + "/SliderClick.wav");
            MainMenu.SliderClickSound.Load();
            MainMenu.Yeah = new SoundPlayer(path + "/Yeah.wav");
            MainMenu.Yeah.Load();
        }
    }
}
