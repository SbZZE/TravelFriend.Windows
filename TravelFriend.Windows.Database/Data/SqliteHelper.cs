using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class SqliteHelper
    {
        public SQLiteConnection db;

        private static readonly Lazy<SqliteHelper> _sqliteHelper = new Lazy<SqliteHelper>(() => new SqliteHelper());

        public static SqliteHelper Instance
        {
            get
            {
                return _sqliteHelper.Value;
            }
        }

        private SqliteHelper()
        {
            db = new SQLiteConnection("travelfriend.db");
            db.CreateTable<User>();
        }

        public int Add<T>(T model)
        {
            try
            {
                return db.Insert(model);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update<T>(T model)
        {
            return db.Update(model);
        }

        public int Delete<T>(T model)
        {
            return db.Delete(model);
        }

        public List<T> Query<T>(string sql) where T : new()
        {
            return db.Query<T>(sql);
        }

        public int Execute(string sql)
        {
            return db.Execute(sql);
        }

        public void DeleteDatabase()
        {
            db.DropTable<User>();
            db.CreateTable<User>();
        }
    }
}
