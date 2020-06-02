using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TravelFriend.Windows.Database.Model;

namespace TravelFriend.Windows.Database.Data
{
    public class TeamManager
    {
        /// <summary>
        /// 获取我创建的团队信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Team> GetCreatedTeam()
        {
            var teams = SqliteHelper.Instance.Query<Team>($"Select * from Team").Where(x => x.IsLeader).ToList();
            return new ObservableCollection<Team>(teams);
        }

        /// <summary>
        /// 获取我加入的团队信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Team> GetJoinedTeam()
        {
            var teams = SqliteHelper.Instance.Query<Team>($"Select * from Team").Where(x => !x.IsLeader).ToList();
            return new ObservableCollection<Team>(teams);
        }

        /// <summary>
        /// 根据团队id获取团队成员
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static ObservableCollection<TeamMember> GetTeamMembers(string teamId)
        {
            var members = SqliteHelper.Instance.Query<TeamMember>($"Select * from TeamMember where TeamId='{teamId}'").OrderByDescending(x => x.IsLeader).ToList();
            return new ObservableCollection<TeamMember>(members);
        }

        /// <summary>
        /// 根据团队id获取团队相册
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static ObservableCollection<TeamAlbum> GetTeamAlbums(string teamId)
        {
            var albums = SqliteHelper.Instance.Query<TeamAlbum>($"Select * from TeamAlbum where TeamId='{teamId}'").ToList();
            return new ObservableCollection<TeamAlbum>(albums);
        }
    }
}
