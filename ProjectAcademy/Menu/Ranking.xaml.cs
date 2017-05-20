using System;
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
using System.Windows.Shapes;
using System.Threading;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Data;

namespace ProjectAcademy
{
    /// <summary>
    /// Interaction logic for Ranking.xaml
    /// </summary>
    public partial class Ranking : Page
    {
        private SQLiteDataAdapter m_oDataAdapter = null;
        private DataSet m_oDataSet = null;
        private DataTable m_oDataTable = null;
        public Ranking()
        {
            InitializeComponent();
            InitBinding();
        }


        private void InitBinding()
        {
            if (Rank.IsDataBaseExist())
            {
                SQLiteConnection oSQLiteConnection =
                    new SQLiteConnection("Data Source=Highscores.sqlite");
                SQLiteCommand oCommand = oSQLiteConnection.CreateCommand();
                oCommand.CommandText = "SELECT * FROM Highscores";
                m_oDataAdapter = new SQLiteDataAdapter(oCommand.CommandText,
                    oSQLiteConnection);
                SQLiteCommandBuilder oCommandBuilder =
                    new SQLiteCommandBuilder(m_oDataAdapter);
                m_oDataSet = new DataSet();
                m_oDataAdapter.Fill(m_oDataSet);
                m_oDataTable = m_oDataSet.Tables[0];
                lstItems.DataContext = m_oDataTable.DefaultView;
            }
            else
            {
                MessageBox.Show("Scoreboard does not exist. First play the game");
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove the scoreboard?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Rank.ResetDataBase();
                InitBinding();
            }
        }
    }
}
