
using MediatR;

namespace SchoolSystem.Application.Features.Courses.Queries.GetAllActiveCourses
{
    public class GetAllActiveCoursesQuery : IRequest<List<CourseQueryDto>>
    {
    }
}
