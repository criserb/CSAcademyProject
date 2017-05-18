﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
