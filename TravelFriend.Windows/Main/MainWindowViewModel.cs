using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.RabbitMQ;
using TravelFriend.Windows.RabbitMQ.Observe;

namespace TravelFriend.Windows
{
    public class MainWindowViewModel : BaseViewModel, IObserver
    {
        public MainWindowViewModel()
        {
            NotifyManager.UserInfoSubject.Add(this);//订阅头像变化
        }

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

        private string _gender = "0";
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
                _gender = value;
                Change("Gender");
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
                NickName = user.NickName;
                Address = user.Address;
                Gender = user.Gender;
            }
        }
    }
}
