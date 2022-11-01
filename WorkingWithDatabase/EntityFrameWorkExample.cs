using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithDatabase
{
    public class EntityFrameWorkExample
    {
        public async Task MakeEf()
        {
            //закомментили след код, т.вызываем пустой конструктор в DbCOntext и мтеод OnConfiguring
            //var connectionString = "Server=localhost;Database=AdoNetExample;Trusted_Connection=True;Encrypt=False";
            //var builder = new DbContextOptionsBuilder();
            //builder.UseSqlServer(connectionString);
            //using var context = new MyDbContext(builder.Options);

            using var context = new MyDbContext();
            //dbcontext class всегда должен быть унаследован, so we created a new class mydbcontext
            var users = await context.Users.ToArrayAsync();

            foreach(var user in users)
            {
                Console.WriteLine($"{user.UserId}: {user.FirstName} {user.LastName} {user.BirthDate}");
            }
        }
    }
}
