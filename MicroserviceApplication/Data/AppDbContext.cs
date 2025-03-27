using Microsoft.EntityFrameworkCore;
using MicroserviceApplication.Models;

namespace MicroserviceApplication.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Poll> Polls { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Vote> Votes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-many: Poll -> Options
        modelBuilder.Entity<Poll>()
            .HasMany(p => p.Options)
            .WithOne(o => o.Poll)
            .HasForeignKey(o => o.PollId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Vote -> Poll relationship
        modelBuilder.Entity<Vote>()
            .HasOne(v => v.Poll)
            .WithMany() // If you want a collection of Votes in Poll, add ICollection<Vote> Votes property in Poll.
            .HasForeignKey(v => v.PollId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Vote -> Option relationship
        modelBuilder.Entity<Vote>()
            .HasOne(v => v.Option)
            .WithMany() // Similarly, you can add a collection in Option if needed.
            .HasForeignKey(v => v.OptionId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
    
}