using Microsoft.EntityFrameworkCore;

namespace WebTalkApi.Entities
{
    public class WebTalkDbContext : DbContext
    {
        private readonly string _connection = "Server=(localdb)\\mssqllocaldb;Database=WebTalkDb;Trusted_Connection=True;";

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.HashedPassword)
                .IsRequired();





            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Chats);
        }


    }
}
