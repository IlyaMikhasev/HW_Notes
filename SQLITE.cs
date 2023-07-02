using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace HW_Notes
{
    internal class SQLITE
    {

        string my_query;
        SQLiteCommand myQuery;
        SQLiteConnection myConnection;
        public SQLiteDataReader _dr;
        public SQLITE()
        {
            my_query = @"CREATE TABLE IF NOT EXISTS MyNote (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                  @"Note TEXT(500) NOT NULL,"+
                                  @"N_Date TEXT(20));";
            sqlConnect(my_query);
        }
        private void sqlConnect(string _my_query)
        {
            string _source = "Data Source=NOTES.db;";
            string _cache = "Cache=Shared;";
            string _mode = "Mode=ReadWriteCreate;";
            myConnection = new SQLiteConnection(_source + _cache + _mode);
            myConnection.Open();
            myQuery = new SQLiteCommand(_my_query, myConnection);
            _dr = myQuery.ExecuteReader();

        }
        public void AddNote(string text,string date)
        {
            my_query = @"INSERT INTO MyNote(Note,N_Date) " +
                @"VALUES('" + text + @"','"+date+"');";
            sqlConnect(my_query);
            MessageBox.Show("заметка внесена", "Журнал заметок", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public string SelectNote(string date)
        {
            my_query = @"SELECT Note FROM MyNote WHERE N_Date = '" + date+"';";
            sqlConnect(my_query);
            try
            {
                string result = string.Empty;
                if (_dr.HasRows)
                {
                    while (_dr.Read())
                    {
                        return  _dr.GetString(0);
                    }
                }
            }
            catch (Exception e)
            {
                return $"Error:{e.Message}";
            }
            return "null";
        }
    }
}
