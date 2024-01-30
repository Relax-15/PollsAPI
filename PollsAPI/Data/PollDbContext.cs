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
}