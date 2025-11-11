using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlataformaEducativa.Models
{
    [Table("courses")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string CourseName { get; set; }

        // Relación Many-to-One con Professor
        public long ProfessorId { get; set; } // FK
        public Professor Professor { get; set; }

        // Relación Many-to-Many con Student
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }

}
