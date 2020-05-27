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
        /// 查询第一个用户
        /// </summary>
        /// <returns></returns>
        public static User GetFirstUser()
        {
            var user = SqliteHelper.Instance.Query<User>($"Select * from User").Where(x => x.IsRememberPassword).ToList();
            user.Reverse();
            return user.FirstOrDefault();
        }

        public static List<string> GetAllUserName()
        {
            var result = new List<string>();
            var users = SqliteHelper.Instance.Query<User>($"Select * from User");
            users.Where(x => x.IsRememberPassword).ToList().ForEach(x => result.Add(x.UserName.ToString()));
            result.Reverse();
            return result;
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
        /// 把用户删了再加到最后
        /// </summary>
        /// <param name="user"></param>
        public static void SetUserToLast(User user)
        {
            if (IsUserExist(user))
            {
                var result = SqliteHelper.Instance.Delete<User>(GetUserByUserName(user.UserName));
            }
            SqliteHelper.Instance.Add<User>(user);
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
        /// 根据用户名删除
        /// </summary>
        /// <param name="userName"></param>
        public static void DeleteUserByUserName(string userName)
        {
            SqliteHelper.Instance.Delete<User>(GetUserByUserName(userName));
        }
    }
}
