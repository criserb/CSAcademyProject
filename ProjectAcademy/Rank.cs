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
using Finisar.SQLite;

namespace ProjectAcademy
{
    static class Rank
    {
        // We use these three SQLite objects:
        private static SQLiteConnection _sqlite_conn;
        private static SQLiteCommand _sqlite_cmd;
        private static SQLiteDataReader _sqlite_datareader;
        static Rank()
        {
            // create a new database connection:
            _sqlite_conn = new SQLiteConnection("Data Source=Rank.db;Version=3;New=True;Compress=True;");
        }
        public static void Add(string nick, int time, Point dim)
        {
            _sqlite_conn.Open();
            _sqlite_cmd = _sqlite_conn.CreateCommand();

            _sqlite_conn.Close();
        }
        public static void CheckConnection()
        {
            // open the connection:
            _sqlite_conn.Open();

            // create a new SQL command:
            _sqlite_cmd = _sqlite_conn.CreateCommand();

            // Let the SQLiteCommand object know our SQL-Query:
            _sqlite_cmd.CommandText = "CREATE TABLE Ranking (Nick varchar(100), Time [sec] integer primary key, 'Dimension [width x height]' varchar(100));";

            // Now lets execute the SQL ;D
            _sqlite_cmd.ExecuteNonQuery();

            // Lets insert something into our new table:
            _sqlite_cmd.CommandText = "INSERT INTO Ranking (Nick, Time) VALUES ('Test Text 1', 21);";

            // And execute this again ;D
            _sqlite_cmd.ExecuteNonQuery();

            // ...and inserting another line:
            _sqlite_cmd.CommandText = "INSERT INTO Ranking (Nick, Time) VALUES ('Test Text 2', 212);";

            // And execute this again ;D
            _sqlite_cmd.ExecuteNonQuery();

            // My Lines                                                                             //
            _sqlite_cmd.CommandText = "INSERT INTO Ranking (Nick, Time) VALUES ('Test Text 3', 11);";   //
                                                                                                        //
                                                                                                        // And execute this again ;D                                                            //
            _sqlite_cmd.ExecuteNonQuery();                                                           //
                                                                                                     //
                                                                                                     // \\ My Lines                                                                          //

            // But how do we read something out of our table ?
            // First lets build a SQL-Query again:
            _sqlite_cmd.CommandText = "SELECT * FROM Ranking";

            // Now the SQLiteCommand object can give us a DataReader-Object:
            _sqlite_datareader = _sqlite_cmd.ExecuteReader();

            // The SQLiteDataReader allows us to run through the result lines:
            while (_sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                // Print out the content of the Time field:
                string data = _sqlite_datareader.GetString(1);
                MessageBox.Show(data);
            }

            // We are ready, now lets cleanup and close our connection:
            _sqlite_conn.Close();
        }
    }
}
