using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizzAppFilRouge.Data;
using QuizzAppFilRouge.Data.Entities;

namespace QuizzAppFilRouge.Domain
{
    public interface IPassageRepository
    {
        Task CreatePassage(Passage passage); 

    }

    public class DbPassageRepository : IPassageRepository
    {
        private readonly ApplicationDbContext context;


        public DbPassageRepository(ApplicationDbContext context) 
        { 
            
            this.context = context;


        }

        public async Task CreatePassage(Passage passage)
        {
            await context.AddAsync(passage);
            context.SaveChanges();
        }
    }
}
