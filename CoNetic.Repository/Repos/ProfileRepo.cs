using CoNetic.Core.ReposInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoNetic.Core.Models;
using CoNetic.Repository.Identity;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.AspNetCore.Identity;
namespace CoNetic.Repository.Repos
{
    public class ProfileRepo : IGenericRepo<User>
    {
        private readonly AppIdentityDbContext Context;

        public ProfileRepo(AppIdentityDbContext Context)
        {
        this.Context = Context;
        }

        public User Get(string UserId)
        {
            return Context.Users.Where(x => x.Id == UserId).Include(x => x.experiences)
            .Include(x => x.skills).ToList().First();
        }

        public void insert(User item)
        {
            Context.Add(item);  
        }

        public void save()
        {
            Context.SaveChanges();
        }

         public void update(User item)
        {

            // Remove old experiences & skills
            Context.Skills.RemoveRange(Context.Skills.Where(s => s.UserId == item.Id));

            Context.Experiences.RemoveRange(Context.Experiences.Where(s => s.UserId == item.Id));

            Context.Users.Update(item);
            Context.SaveChanges();

        }

    }


    
}
