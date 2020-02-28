using Microsoft.EntityFrameworkCore;
 
namespace Hobby_Hub.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> users{get;set;}
        public DbSet<Hobby> Hobbies{get;set;}
        public DbSet<Eventss> Eventsses{get;set;}
       
    }
}