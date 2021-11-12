using dots.forms;
using Microsoft.EntityFrameworkCore;
using System;


namespace dots.services
{
    
    class InfoDbContext : DbContext
    {
        public DbSet<InfoForm> InfoForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=game_dotnet;Trusted_Connection=True;");
        }

    }
}
