using LevisBot.DAL.DAO;
using System.Data.Entity;

namespace LevisBot.DAL
{
  public class LevisInitializer : DropCreateDatabaseAlways<LevisContext>
  {
    protected override void Seed(LevisContext context)
    {
      var user = new User { FirstName = "Jamie", LastName = "Vardy" };
      context.Users.Add(user);

      var sve = new Course { Name = "SVE" };
      var mus = new Course { Name = "MUS" };
      context.Courses.Add(sve);
      context.Courses.Add(mus);

      context.Grades.Add(new Grade
      {
        Course = sve,
        Result = 20,
        Number = 1,
        Type = GradeableType.Assignment,
        MaxResult = 20,
        Student = user
      });

      context.Grades.Add(new Grade
      {
        Course = mus,
        Result = 1,
        Student = user
      });

      context.SaveChanges();
    }
  }
}
