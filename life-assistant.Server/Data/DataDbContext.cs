using Microsoft.EntityFrameworkCore;

public class DataDbContext : DbContext
{
    public DbSet<PolarDailyActivity> PolarDailyActivities { get; set; }
    public DbSet<PolarUser> PolarUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PolarDailyActivity>().ToTable("PolarDailyActivity", schema: "dbo");

        modelBuilder.Entity<PolarUser>().ToTable("PolarUser", schema: "dbo");

        modelBuilder.Entity<PolarUser>()
            .HasMany(user => user.PolarDailyActivities)
            .WithOne(activity => activity.User)
            .HasForeignKey(activity => activity.UserId);
    }

    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {

    }
}