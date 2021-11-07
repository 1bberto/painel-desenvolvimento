using Microsoft.EntityFrameworkCore;
using Painel.Entities;

namespace Painel
{
    public class PainelDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }

        public PainelDBContext(DbContextOptions<PainelDBContext> options) : base(options)
        {
        }
    }
}