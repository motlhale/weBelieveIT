using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using weBelieveIT.data;
using weBelieveIT.models;
using weBelieveIT.Presentation;
using weBelieveIT.resultModels;

namespace weBelieveIT.services
{
    public class TeamService
    {
        private readonly ApplicationDbContext dbContext;

        public TeamService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<TeamOtd> GetAllTeams()
        {
            var teams = new List<TeamOtd>();
            dbContext.Team.Include(itm => itm.Members).ToList().ForEach(team =>
            {
                teams.Add(new TeamOtd
                {
                    ID = team.ID.ToString(),
                    Name = team.Name,
                    Member = GetAllTeamMembers(team.Members)
                });
            });
            return teams;
        }

        private List<TeamMemberOtd> GetAllTeamMembers(List<Member> members)
        {
            var teamMembers = new List<TeamMemberOtd>();
            foreach(var member in members)
            {
                teamMembers.Add(new TeamMemberOtd
                {
                    Email = member.Email,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Role = member.Role
                });
            }
            return teamMembers;
        }
        public bool CreateTeam(TeamDto team)
        {
            dbContext.Team.Add(new Team
            {
                Name = team.Name
            });
            if(dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool AddMemberToTeam(Guid id,string email)
        {
            var memberInDb = dbContext.Member.FirstOrDefault(itm => itm.Email == email);
            var teamInDb = dbContext.Team.Include(itm => itm.Members).FirstOrDefault(itm => itm.ID == id);
            if(memberInDb != null)
            {
                var memberInTeam = teamInDb.Members.FirstOrDefault(itm => itm.Email == email);
                if(memberInTeam == null)
                {
                    teamInDb.Members.Add(memberInDb);
                    dbContext.Update(teamInDb);
                    if(dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveMemberFromTeam(Guid id, string email)
        {
            var memberInDb = dbContext.Member.FirstOrDefault(itm => itm.Email == email);
            var teamInDb = dbContext.Team.Include(itm => itm.Members).FirstOrDefault(itm => itm.ID == id);
            if(memberInDb != null)
            {
                var memberInTeam = teamInDb.Members.FirstOrDefault(itm => itm.Email == email);
                if(memberInTeam != null)
                {
                    teamInDb.Members.Remove(memberInDb);
                    dbContext.Update(teamInDb);
                    if(dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveTeam(Guid id)
        {
            var team = dbContext.Team.FirstOrDefault(itm => itm.ID == id);
            if(team != null)
            {
                dbContext.Team.Remove(team);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}