using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace TravelFriend.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ImageSource _isMax;
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
    }
}
