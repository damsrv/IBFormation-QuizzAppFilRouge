using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Domain
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUserHandledById(string id);


    }

    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;


        public DbUserRepository(ApplicationDbContext context) 
        { 
            
            this.context = context;


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
