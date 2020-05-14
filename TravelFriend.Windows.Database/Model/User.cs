using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.Database.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public byte[] Avatar { get; set; }
    }
}
