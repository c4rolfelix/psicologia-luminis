﻿using Microsoft.EntityFrameworkCore;
using Luminis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;

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
                .Property(p => p.SenhaHash)
                .IsRequired(false);

            modelBuilder.Entity<Psicologo>()
                .HasMany(p => p.Especialidades)
                .WithMany(e => e.Psicologos)
                .UsingEntity(j => j.ToTable("PsicologoEspecialidades"));

            modelBuilder.Entity<PsicologoEspecialidade>()
                .HasKey(pe => new { pe.PsicologosId, pe.EspecialidadesId });

            // --- SEED DATA PARA ROLE "Admin" E USUÁRIO ADMINISTRADOR ---
            string adminRoleId = "a18c66e9-0260-466d-a789-21800f123456";
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            string adminUserId = "b18c66e9-0260-466d-a789-21800f123457";
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@luminis.com",
                NormalizedUserName = "ADMIN@LUMINIS.COM",
                Email = "admin@luminis.com",
                NormalizedEmail = "ADMIN@LUMINIS.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "SuaSenhaAdmin@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });
            // --- FIM DO SEED DATA PARA ADMIN ---

            // --- SEED DATA PARA O PSICÓLOGO DE TESTE ---
            string psicologoUserId = "c18c66e9-0260-466d-a789-21800f123458";

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = psicologoUserId,
                UserName = "psicologo@teste.com",
                NormalizedUserName = "PSICOLOGO@TESTE.COM",
                Email = "psicologo@teste.com",
                NormalizedEmail = "PSICOLOGO@TESTE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "SenhaTeste@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            modelBuilder.Entity<Psicologo>().HasData(new Psicologo
            {
                Id = 1,
                Nome = "Dr.",
                Sobrenome = "Teste",
                CRP = "01/12345",
                Email = "psicologo@teste.com",
                Biografia = "Olá, sou o Dr. Teste, psicólogo com experiência em TCC.",
                FotoUrl = "https://placehold.co/400x400/87CEFA/000000?text=PSICÓLOGO+TESTE",
                WhatsApp = "5599999999999",
                WhatsAppUrl = "https://wa.me/5599999999999",
                CPF = "000.000.000-00", // <<< NOVO: CPF de teste
                DataNascimento = new DateTime(1985, 5, 20),
                Ativo = true,
                EmDestaque = false,
                DataCadastro = DateTime.Now
            });

            modelBuilder.Entity<PsicologoEspecialidade>().HasData(
                new PsicologoEspecialidade { PsicologosId = 1, EspecialidadesId = 1 },
                new PsicologoEspecialidade { PsicologosId = 1, EspecialidadesId = 2 }
            );
            // --- FIM DO SEED DATA PARA PSICÓLOGO DE TESTE ---

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