using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelFriend.Windows.Database;
using TravelFriend.Windows.Database.Data;
using TravelFriend.Windows.Database.Model;
using TravelFriend.Windows.Http;
using TravelFriend.Windows.Http.UserInfo;
using TravelFriend.Windows.RabbitMQ.Observe;

namespace TravelFriend.Windows.RabbitMQ
{
    public class NotifyManager
    {
        public static Subject AvatarSubject = new AvatarSubject();
        public static Subject UserInfoSubject = new UserInfoSubject();

        /// <summary>
        /// 更新头像通知
        /// </summary>
        /// <param name="userName"></param>
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

        /// <summary>
        /// 更新个人资料通知
        /// </summary>
        /// <param name="userName"></param>
        public async static void UpdateUserInfo(string userName)
        {
            //获取个人资料
            var response = await HttpManager.Instance.GetAsync<GetUserInfoResponse>(new HttpRequest($"{ApiUtils.UserInfo}?username={userName}"));
            if (response.Ok)
            {
                User newUser = response.data;
                var user = UserManager.GetUserByUserName(userName);
                if (user != null)
                {
                    newUser.Avatar = user.Avatar;
                    newUser.Password = user.Password;
                }
                //把最近登录的账号信息存到本地数据库
                UserManager.UpdateUser(newUser);
                UserInfoSubject.Notify();
            }
        }
    }
}
