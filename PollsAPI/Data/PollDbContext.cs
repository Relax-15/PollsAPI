using Microsoft.EntityFrameworkCore;
using PollsAPI.Entities;


namespace PollsAPI.Data;

public class PollDbContext : DbContext
{
    public PollDbContext(DbContextOptions options) : base(options)
    {

    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<Vote>Votes { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // Define relationships
    //     modelBuilder.Entity<User>()
    //         .HasMany(u => u.Polls)
    //         .WithOne(p => p.User)
    //         .HasForeignKey(p => p.UserId);
    // }
}