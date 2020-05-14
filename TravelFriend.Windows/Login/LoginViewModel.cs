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

        private string _nickName;
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
                Change(nameof(NickName));
            }
        }
    }
}
