using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class SqliteHelper
    {
        public SqliteHelper()
        {
            var db = new SQLiteConnection("travelfriend.db");
            db.CreateTable<User>();
        }
    }
}
