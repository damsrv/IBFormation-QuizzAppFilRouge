using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Domain
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUserHandledById(string id);

        Task<ApplicationUser> GetUserById(string id);

    }

    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;


        public DbUserRepository(ApplicationDbContext context) 
        { 
            
            this.context = context;


        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await context.ApplicationUsers
                .Where(user => user.Id == id)
                .ToListAsync();

            return user[0];

        }

        public async Task<List<ApplicationUser>> GetUserHandledById(string id)
        {
            var users = await context.ApplicationUsers
                .Where(u => u.HandleBy == id)
                .ToListAsync();
                
            return users;
        }


    }
}
