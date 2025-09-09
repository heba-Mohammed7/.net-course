using studentCourse.Models.Base;

namespace studentCourse.Models;

public class CourseEntity : BaseEntity
{
    public string Cname { get; set; }
    public string Code { get; set; }
    public int Hours { get; set; }
    public virtual ICollection<LearnEntity> Learns { get; set; } = new List<LearnEntity>();
}