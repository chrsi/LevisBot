using LevisBot.DAL.DAO;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LevisBot.DAL
{
  public class LevisContext : DbContext
  {
    public LevisContext() : base("LevisDb")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) => modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
  }
}
