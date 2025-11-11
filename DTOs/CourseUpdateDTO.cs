namespace PlataformaEducativa.DTOs
{
    public class CourseUpdateDto
    {
        public string? CourseName { get; set; }
        public long? ProfessorId { get; set; }
        public List<long>? StudentIds { get; set; }
    }

}
