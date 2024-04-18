using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using todo_list.api.Entities;

namespace todo_list.api.Data
{
    public class ApplicationBDContext : DbContext
    {
        public ApplicationBDContext(DbContextOptions<ApplicationBDContext> options) : base(options)
        {

        }

        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TODO> TODO { get; set; }

    }
}
