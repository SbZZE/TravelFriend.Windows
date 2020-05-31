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
        public static ObservableCollection<Team> GetCreatedTeam()
        {
            var teams = SqliteHelper.Instance.Query<Team>($"Select * from Team").Where(x => x.IsLeader).ToList();
            return new ObservableCollection<Team>(teams);
        }

        public static ObservableCollection<Team> GetJoinedTeam()
        {
            var teams = SqliteHelper.Instance.Query<Team>($"Select * from Team").Where(x => !x.IsLeader).ToList();
            return new ObservableCollection<Team>(teams);
        }
    }
}
