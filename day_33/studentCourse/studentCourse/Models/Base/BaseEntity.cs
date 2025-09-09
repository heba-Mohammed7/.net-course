using System.ComponentModel.DataAnnotations;
namespace studentCourse.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
