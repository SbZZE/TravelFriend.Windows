using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database
{
    public class AccountManager
    {
        //加载单例
        private static readonly Lazy<AccountManager> _instance = new Lazy<AccountManager>(() => new AccountManager());
        //获取单例
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
        private string _account;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public string Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                OnAccountChanged();
            }
        }

        /// <summary>
        /// 监听用户是否发生变化
        /// </summary>
        public event EventHandler AccountChanged;
        protected virtual void OnAccountChanged()
        {
            AccountChanged?.Invoke(this, null);
        }
    }
}
