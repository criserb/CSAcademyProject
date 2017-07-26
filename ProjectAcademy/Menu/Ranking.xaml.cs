using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Media;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for Ranking.xaml
    /// </summary>
    public partial class Ranking : Page
    {
        public Ranking()
        {
            InitializeComponent();
            InitBinding();

        }
        private void InitBinding()
        {
            if (Rank.IsDataBaseExist())
            {
                List<Record> list = Rank.GetSortedList();
                lstItems.ItemsSource = list;
            }
            else
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Scoreboard does not exist. First play the game");
            }
        }
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            this.NavigationService.Navigate(new MainMenu());
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.ButtonClickSound.Play();
            SystemSounds.Asterisk.Play();
            if (MessageBox.Show("Are you sure you want to remove the scoreboard?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Rank.ResetDataBase();
                InitBinding();
            }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Record> listForSearch = new List<Record>();
            foreach (var item in Rank.GetSortedList())
            {
                if (searchBox.Text.Length > 0)
                {
                    if (item.Nick.Contains(searchBox.Text))
                        listForSearch.Add(item);
                }
            }
            if (listForSearch.Count > 0)
            {
                lstItems.ItemsSource = listForSearch;
            }
            else
                lstItems.ItemsSource = Rank.GetSortedList();
        }
    }
}
