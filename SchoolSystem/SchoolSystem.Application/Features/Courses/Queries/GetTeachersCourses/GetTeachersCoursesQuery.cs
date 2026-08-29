
using MediatR;

namespace SchoolSystem.Application.Features.Courses.Queries.GetTeachersCourses
{
    public class GetTeachersCoursesQuery : IRequest<List<CourseQueryDto>>
    {
        public Guid TeacherId { get; set; }
        public GetTeachersCoursesQuery(Guid techerId) => TeacherId = techerId;
    }
}
