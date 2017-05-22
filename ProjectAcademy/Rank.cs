using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace ProjectAcademy
{
    static class Rank
    {
        private static SQLiteConnection _sqlite_conn;
        private static SQLiteCommand _sqlite_cmd;
        private static SQLiteDataReader _sqlite_datareader;
        static Rank()
        {
            // Create a new database connection
            _sqlite_conn = new SQLiteConnection("Data Source=Highscores.sqlite;Version=3;");
        }
        public static void Add(string nick, int time, Point dim)
        {
            _sqlite_conn.Open();
            string sql = "insert into Highscores (Nick, Time, Dimension) values ('" +
                nick + "'," + time + ",'" + dim.X + " x " + dim.Y + "')";
            _sqlite_cmd = new SQLiteCommand(sql, _sqlite_conn);
            _sqlite_cmd.ExecuteNonQuery();
            _sqlite_conn.Close();
        }

        public static void ResetDataBase()
        {
            _sqlite_conn.Open();

            string sqlTrunc = "DELETE FROM Highscores";
            _sqlite_cmd = new SQLiteCommand(sqlTrunc, _sqlite_conn);
            _sqlite_cmd.ExecuteNonQuery();

            _sqlite_conn.Close();
        }
        public static void CreateDataBase()
        {
            _sqlite_conn.Open();

            // Let the SQLiteCommand object know our SQL-Query:
            string sql = "create table Highscores (Nick varchar(50), Time int, Dimension varchar(7))";

            _sqlite_cmd = new SQLiteCommand(sql, _sqlite_conn);
            _sqlite_cmd.ExecuteNonQuery();

            // We are ready, now lets cleanup and close our connection:
            _sqlite_conn.Close();
        }
        public static bool IsDataBaseExist()
        {
            if (File.Exists(@"Highscores.sqlite"))
                return true;
            else return false;
        }
        public static List<Record> GetSortedList()
        {
            int count = 1;
            _sqlite_conn.Open();
            List<Record> list = new List<Record>();
            string sql = "SELECT * FROM Highscores";
            _sqlite_cmd = new SQLiteCommand(sql, _sqlite_conn);
            _sqlite_datareader = _sqlite_cmd.ExecuteReader();
            while (_sqlite_datareader.Read())
            {
                list.Add(new Record(_sqlite_datareader.GetString(0), _sqlite_datareader.GetInt32(1), _sqlite_datareader.GetString(2)));
            }
            _sqlite_conn.Close();
            List<Record> sortedList = list.OrderBy(o => o.Time).ToList();
            foreach (var item in sortedList)
            {
                item.Number = count;
                ++count;
            }
            return sortedList;
        }
    }
}
