using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LevisBot.DAL.DAO
{
  public class Grade
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public User Student { get; set; }

    [Required]
    public Course Course { get; set; }

    [Required]
    public int Result { get; set; }

    [Required]
    public GradeableType Type { get; set; }
  }
}
