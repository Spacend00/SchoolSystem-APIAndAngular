
using MediatR;

namespace SchoolSystem.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQuery : IRequest<List<CourseQueryDto>>
    {
    }
}
