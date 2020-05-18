using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class DatabaseManager
    {
        //private static readonly Lazy<DatabaseManager> _instance = new Lazy<DatabaseManager>(() => new DatabaseManager());
        ///// <summary>
        ///// 加载单例
        ///// </summary>
        //public static DatabaseManager Instance
        //{
        //    get
        //    {
        //        return _instance.Value;
        //    }
        //}

        //private DatabaseManager()
        //{

        //}

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public static User GetFirstUser()
        {
            var user = SqliteHelper.Instance.Query<User>($"Select * from User");
            user.Reverse();
            return user.FirstOrDefault();
        }

        /// <summary>
        /// 通过用户名查询用户
        /// </summary>
        /// <returns></returns>
        public static User GetUserByUserName(string userName)
        {
            return SqliteHelper.Instance.Query<User>($"Select * from User where UserName='{userName}'").FirstOrDefault();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int UpdateUser(User user)
        {
            return SqliteHelper.Instance.Update<User>(user);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(User user)
        {
            var hasUser = SqliteHelper.Instance.Query<User>($"Select * from User where UserName='{user.UserName}'").FirstOrDefault();
            if (hasUser != null)
            {
                SqliteHelper.Instance.Delete<User>(user);
            }
            return SqliteHelper.Instance.Add<User>(user);
        }
    }
}
