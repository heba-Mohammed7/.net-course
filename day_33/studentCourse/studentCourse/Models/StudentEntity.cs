using studentCourse.Models.Base;

namespace studentCourse.Models;

public class StudentEntity : BaseEntity
{
    public string Name { get; set; }
    public int Age { get; set; }
    public virtual ICollection<LearnEntity> Learns { get; set; } = new List<LearnEntity>();
}