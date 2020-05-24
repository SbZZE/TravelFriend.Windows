using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.RabbitMQ.Observe;

namespace TravelFriend.Windows.RabbitMQ
{
    public class NotifyManager
    {
        public static Subject AvatarSubject = new AvatarSubject();

        public async static void UpdateAvatar(string userName)
        {
            var user = UserManager.GetUserByUserName(userName);
            if (user != null)
            {
                //获取头像
                using (MemoryStream ms = new MemoryStream())
                {
                    var res = await HttpManager.Instance.DownloadAsync(new HttpRequest($"{ApiUtils.Avatar}?username={userName}&isCompress=true"), ms);
                    if (ms != null && ms.Length > 0)
                    {
                        ms.Position = 0;
                        using (BinaryReader br = new BinaryReader(ms))
                        {
                            user.Avatar = br.ReadBytes((int)ms.Length);
                        }
                        UserManager.UpdateUser(user);
                    }
                    AvatarSubject.Notify();
                }
            }
        }
    }
}
