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
    public class UserServices
    {
        private readonly ApplicationDbContext dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateMember(MemberDto member)
        {
            if(GetMember(member.Email) == null)
            {
                dbContext.Member.Add(new Member
                {
                    Email = member.Email,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Role = member.Role
                });
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public MemberOtd GetMember(string email)
        {
            var member = dbContext.Member.Include(itm => itm.Team).FirstOrDefault(itm => itm.Email == email);
            
            if(member == null)
            {
                return null;
            }

            return new MemberOtd
            {
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = member.Role,
                Team = GetMemberTeam(member.Team)
            };
        }

        private MemberTeamOtd GetMemberTeam(Team team)
        {
            return new MemberTeamOtd
            {
                ID = team.ID.ToString(),
                Name = team.Name
            };
        }

        public bool UpdateMember(MemberDto member)
        {
            var memberInDb = GetMember(member.Email);

            if(memberInDb != null)
            {
                memberInDb.FirstName = member.FirstName ?? memberInDb.FirstName;
                memberInDb.LastName = member.LastName ?? memberInDb.LastName;
                memberInDb.Role = member.Role ?? memberInDb.Role;

                dbContext.Update(memberInDb);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveMember(string email)
        {
            var memberInDb = dbContext.Member.FirstOrDefault(itm => itm.Email == email);

            if(memberInDb != null)
            {
                dbContext.Member.Remove(memberInDb);

                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}