using AlunosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Configuration.Context
{
    public class ApiAlunosContext : IdentityDbContext<IdentityUser>
    {
        public ApiAlunosContext(DbContextOptions<ApiAlunosContext> options) : base(options)
        {
        }
        public DbSet<Aluno> Alunos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Maria Antonieta",
                    Email = "mariaantonieta@gmailcom",
                    Idade = 43
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Lucas Junior",
                    Email = "lucasjunior@gmailcom",
                    Idade = 23
                });
        }
    }
}
