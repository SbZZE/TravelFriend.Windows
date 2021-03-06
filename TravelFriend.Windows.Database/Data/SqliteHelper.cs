﻿using SQLite;
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
            db.CreateTable<Team>();
            db.CreateTable<TeamAlbum>();
            db.CreateTable<TeamMember>();
            db.CreateTable<Upload>();
        }

        public int Add<T>(T model)
        {
            return db.Insert(model);
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
            db.DropTable<Team>();
            db.DropTable<TeamAlbum>();
            db.DropTable<TeamMember>();
            db.DropTable<Upload>();
            db.CreateTable<User>();
            db.CreateTable<Team>();
            db.CreateTable<TeamAlbum>();
            db.CreateTable<TeamMember>();
            db.CreateTable<Upload>();
        }
    }
}
