using EnnoTest3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnnoTest3.Areas.Manage.ViewModels
{
    public class TeamViewModel
    {
        public PagenatedList<Team> PagenatedTeams { get; set; }
    }
}
