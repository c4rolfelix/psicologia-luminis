using Microsoft.EntityFrameworkCore; 
using Luminis.Models; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.AspNetCore.Identity; 

namespace Luminis.Data
{
    public class LuminisDbContext : IdentityDbContext<IdentityUser>
    {
        public LuminisDbContext(DbContextOptions<LuminisDbContext> options) : base(options)
        {
        }

        public DbSet<Psicologo> Psicologos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Psicologo>()
                .HasMany(p => p.Especialidades)
                .WithMany(e => e.Psicologos)
                .UsingEntity(j => j.ToTable("PsicologoEspecialidades"));

            modelBuilder.Entity<Especialidade>().HasData(
                new Especialidade { Id = 1, Nome = "Terapia Cognitivo-Comportamental (TCC)" },
                new Especialidade { Id = 2, Nome = "Psicanálise" },
                new Especialidade { Id = 3, Nome = "Terapia Humanista" },
                new Especialidade { Id = 4, Nome = "Gestalt-terapia" },
                new Especialidade { Id = 5, Nome = "Terapia Sistêmica" },
                new Especialidade { Id = 6, Nome = "Psicologia Positiva" },
                new Especialidade { Id = 7, Nome = "Neuropsicologia" },
                new Especialidade { Id = 8, Nome = "Psicologia Organizacional" },
                new Especialidade { Id = 9, Nome = "Psicologia Escolar" },
                new Especialidade { Id = 10, Nome = "Psicologia Hospitalar" },
                new Especialidade { Id = 11, Nome = "Psicologia do Esporte" },
                new Especialidade { Id = 12, Nome = "Psicologia Jurídica" },
                new Especialidade { Id = 13, Nome = "Psicologia Social" },
                new Especialidade { Id = 14, Nome = "Orientação Profissional" },
                new Especialidade { Id = 15, Nome = "Terapia de Casal e Família" },
                new Especialidade { Id = 16, Nome = "Psicopedagogia" },
                new Especialidade { Id = 17, Nome = "Saúde Mental Perinatal" }
            );
        }
    }
}
