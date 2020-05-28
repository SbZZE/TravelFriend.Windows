using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Main
{
    public class UpdatePersonalDataViewModel : BaseViewModel
    {
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
                _nickName = value;
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
                _address = value;
                Change("Address");
            }
        }

        private bool _isMale;
        /// <summary>
        /// 是否男
        /// </summary>
        public bool IsMale
        {
            get
            {
                return _isMale;
            }
            set
            {
                _isMale = value;
                Change("IsMale");
            }
        }

        private bool _isFamale;
        /// <summary>
        /// 是否女
        /// </summary>
        public bool IsFamale
        {
            get
            {
                return _isFamale;
            }
            set
            {
                _isFamale = value;
                Change("IsFamale");
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get
            {
                return IsMale && !IsFamale ? "1" : "0";
            }
            set
            {
                if (value == "1")
                {
                    IsMale = true;
                    IsFamale = false;
                }
                else
                {
                    IsMale = false;
                    IsFamale = true;
                }
            }
        }

        private string _birthday;
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
                _birthday = value;
                Change("Birthday");
            }
        }

        private string _signature;
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
                _signature = value;
                Change("Signature");
            }
        }
    }
}
