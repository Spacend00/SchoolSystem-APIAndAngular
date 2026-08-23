
namespace SchoolSystem.Domain.Entities
{
    public class StudentCourse
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!;
        public DateTime EnrolledAt { get; private set; }
        public DateTime? UnEnrolledAt { get; private set; }
        public bool IsActive { get; private set; }

        private StudentCourse() { }
        public StudentCourse(Guid studentId, Guid courseId)
        {
            Id = Guid.NewGuid();
            StudentId = studentId;
            CourseId = courseId;
            EnrolledAt = DateTime.UtcNow;
            IsActive = true;
        }
        public void UnEnroll()
        {
            IsActive = false;
            UnEnrolledAt = DateTime.UtcNow;
        }
        public void ReEnroll()
        {
            IsActive = true;
            UnEnrolledAt = null;
        }
    }
}
