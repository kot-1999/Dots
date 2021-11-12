using dots.forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dots.services
{
    class CommentsDbContext:DbContext
    {
        public DbSet<CommentForm> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=game_dotnet;Trusted_Connection=True;");
        }
    }
}
