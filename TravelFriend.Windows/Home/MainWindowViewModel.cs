using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Database;

namespace TravelFriend.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _nickName = "隔壁的王王王";
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
                Change("NickName");
            }
        }

        private string _address = "中国 China";
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                Change("Address");
            }
        }

        private ImageSource _isMax = new BitmapImage(new Uri("/Resources/Gray/Max.png", UriKind.Relative));
        /// <summary>
        /// 是否最大化
        /// </summary>
        public ImageSource IsMax
        {
            get
            {
                return _isMax;
            }
            set
            {
                _isMax = value;
                Change("IsMax");
            }
        }

        private bool _isLogin;
        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
                Change("IsLogin");
            }
        }
    }
}
