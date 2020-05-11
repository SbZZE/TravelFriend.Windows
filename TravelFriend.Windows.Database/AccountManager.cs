using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database
{
    public class AccountManager
    {
        //加载单例
        private static readonly Lazy<AccountManager> _instance = new Lazy<AccountManager>();
        public static AccountManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private AccountManager() { }

        /// <summary>
        /// 用户Token
        /// </summary>
        public string UserToken { get; set; }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public string Account { get; set; }
    }
}
