using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlataformaEducativa.Models
{
    [Table("students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        // Relación Many-to-Many con Course
        [JsonIgnore] // Evita serialización circular en APIs
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public long? UserId { get; set; }
        public UserSec? User { get; set; }
    }

}
