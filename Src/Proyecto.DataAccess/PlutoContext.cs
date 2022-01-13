using Microsoft.EntityFrameworkCore;
using Proyecto.Entities.Bases;

namespace Proyecto.DataAccess
{
    public class PlutoContext : DbContext
    {
        public PlutoContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EntityBase>();
            base.OnModelCreating(modelBuilder);
        }

    }
}