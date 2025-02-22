using Microsoft.EntityFrameworkCore;
using ToDoListSecTask.Models;

namespace ToDoListSecTask.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ToDoList> toDoLists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-RB8K8R4\\SQLEXPRESS;Initial Catalog=ItiMvc;Integrated Security=True;TrustServerCertificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ToDoList>().HasKey(x => x.id);

            modelBuilder.Entity<ToDoList>().Property(x => x.Name).HasMaxLength(50).IsRequired().IsUnicode(false);
            modelBuilder.Entity<ToDoList>().Property(x => x.Description).HasMaxLength(50).IsRequired().IsUnicode(false); ;
            modelBuilder.Entity<ToDoList>().Property(x => x.DeadLine).IsRequired();
            modelBuilder.Entity<ToDoList>().Property(x => x.File).IsRequired().IsUnicode(false);
        }



    }
}
