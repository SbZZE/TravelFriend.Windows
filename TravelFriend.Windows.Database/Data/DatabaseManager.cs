using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class DatabaseManager
    {
        private static readonly Lazy<DatabaseManager> _instance = new Lazy<DatabaseManager>(() => new DatabaseManager());
        /// <summary>
        /// 加载单例
        /// </summary>
        public static DatabaseManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private DatabaseManager()
        {

        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUser(string userName)
        {
            return SqliteHelper.Instance.Query<User>($"Select * from User").FirstOrDefault();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUser(User user)
        {
            return SqliteHelper.Instance.Add<User>(user);
        }
    }
}
