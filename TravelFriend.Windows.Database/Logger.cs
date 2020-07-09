using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelFriend.Windows.Database
{
    public class Logger
    {
        public async static void Log(string content)
        {
            await File.AppendAllTextAsync("DEBUG.txt",DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss") + Environment.NewLine);
            await File.AppendAllTextAsync("DEBUG.txt", content + Environment.NewLine);
        }
    }
}
