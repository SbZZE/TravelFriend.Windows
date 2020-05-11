using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelFriend.Windows.Database
{
    public class Config
    {
        public static string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"TravelFriend");//C:\Users\xxxxxx\AppData\Roaming


    }
}
