namespace PlataformaEducativa.DTOs
{
    public class CourseCreateDto
    {
        public string CourseName { get; set; } = null!;
        public long ProfessorId { get; set; }
        public List<long> StudentIds { get; set; } = new();
    }

}
