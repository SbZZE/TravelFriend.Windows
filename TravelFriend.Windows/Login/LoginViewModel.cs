using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Common;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows
{
    /// <summary>
    /// 登录的视图模型
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            var user = GetUser();
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                UserName = user.UserName;
                Password = user.Password;
                NickName = user.NickName;
                if (user.Avatar != null)
                {
                    Avatar = ImageHelper.ByteArrayToBitmapImage(user.Avatar);
                }
                IsLoginEnable = true;
            }
        }

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
                Change(nameof(UserName));
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
                Change(nameof(Password));
            }
        }

        private string _nickName = RStrings.ClickToLogin;
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = string.IsNullOrEmpty(value) ? UserName : value;
                Change(nameof(NickName));
            }
        }

        private ImageSource _avatar = new BitmapImage(new Uri("/Resources/DefaultBigAvatar.png", UriKind.Relative));
        public ImageSource Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
                Change(nameof(Avatar));
            }
        }

        private bool _isLoginEnable = false;
        public bool IsLoginEnable
        {
            get
            {
                return _isLoginEnable;
            }
            set
            {
                _isLoginEnable = value;
                Change(nameof(IsLoginEnable));
            }
        }

        /// <summary>
        /// 获取本地数据库中的第一个用户
        /// </summary>
        /// <returns></returns>
        private User GetUser()
        {
            return UserManager.GetFirstUser();
        }
    }
}
