using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.RabbitMQ;
using TravelFriend.Windows.RabbitMQ.Observe;
using TravelFriend.Windows.Styles;

namespace TravelFriend.Windows
{
    public class MainWindowViewModel : BaseViewModel, IObserver
    {
        public MainWindowViewModel()
        {
            NotifyManager.UserInfoSubject.Add(this);
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
                Change("UserName");
            }
        }


        private string _nickName;
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
                _nickName = string.IsNullOrEmpty(value) ? AccountManager.Instance.Account : value;
                Change("NickName");
            }
        }

        private string _address;
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
                _address = string.IsNullOrEmpty(value) ? RStrings.China : value;
                Change("Address");
            }
        }

        private string _gender;
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value != "1" ? "0" : "1";
                Change("Gender");
            }
        }

        private string _birthday = "2020.12.31";
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = string.IsNullOrEmpty(value) ? "2020.12.31" : value;
                Change("Birthday");
            }
        }

        private string _signature = RStrings.UpdateDataEdit;
        /// <summary>
        /// 个签
        /// </summary>
        public string Signature
        {
            get
            {
                return _signature;
            }
            set
            {
                _signature = string.IsNullOrEmpty(value) ? RStrings.UpdateDataEdit : value;
                Change("Signature");
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

        public void Update()
        {
            var user = UserManager.GetUserByUserName(AccountManager.Instance.Account);
            if (user != null)
            {
                UserName = user.UserName;
                NickName = user.NickName;
                Address = user.Address;
                Gender = user.Gender;
                Birthday = user.Birthday;
                Signature = user.Signature;
            }
        }
    }
}
