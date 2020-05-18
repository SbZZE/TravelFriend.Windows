using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class UserManager
    {
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
        /// 查找用户是否存在数据库中
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsUserExist(User user)
        {
            var hasUser = SqliteHelper.Instance.Query<User>($"Select * from User where UserName='{user.UserName}'").FirstOrDefault();
            if (hasUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            if (IsUserExist(user))
            {
                SqliteHelper.Instance.Delete<User>(GetUserByUserName(user.UserName));
            }
            return SqliteHelper.Instance.Add<User>(user);
        }
    }
}
