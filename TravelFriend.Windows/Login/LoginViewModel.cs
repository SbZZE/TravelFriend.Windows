using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelFriend.Windows
{
    /// <summary>
    /// 登录的视图模型
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                Change(nameof(_userName));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                Change(nameof(_password));
            }
        }
    }
}
