using Microsoft.EntityFrameworkCore;

namespace webapp.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<Docente> Docentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Alumno>().HasKey(a => a.idAlumno);
            modelBuilder.Entity<Materias>().HasKey(m => m.IdMateria);
            modelBuilder.Entity<Docente>().HasKey(d => d.idDocente);
        }
    }
}
