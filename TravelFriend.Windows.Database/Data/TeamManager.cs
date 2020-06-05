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
        public static ObservableCollection<Team> GetCreatedTeam(string userName)
        {
            var teams = SqliteHelper.Instance.Query<Team>($"Select * from Team where UserName='{userName}'").Where(x => x.IsLeader).ToList();
            return new ObservableCollection<Team>(teams);
        }

        /// <summary>
        /// 获取我加入的团队信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Team> GetJoinedTeam(string userName)
        {
            var teams = SqliteHelper.Instance.Query<Team>($"Select * from Team where UserName='{userName}'").Where(x => !x.IsLeader).ToList();
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

        /// <summary>
        /// 根据团队Id获取团队信息
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static Team GetTeamByTeamId(string teamId)
        {
            return SqliteHelper.Instance.Query<Team>($"Select * from Team where TeamId='{teamId}'").FirstOrDefault();
        }

        /// <summary>
        /// 更新团队
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int UpdateTeam(Team team)
        {
            return SqliteHelper.Instance.Update<Team>(team);
        }
    }
}
