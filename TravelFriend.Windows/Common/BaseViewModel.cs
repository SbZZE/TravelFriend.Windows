using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelFriend.Windows
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region 属性通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Change(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
