using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlataformaEducativa.Models
{
    [Table("professors")]
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        // Relación One-to-Many con Course
        [JsonIgnore] // Evita serialización circular en APIs
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public long? UserId { get; set; }
        public UserSec? User { get; set; }
    }

}
