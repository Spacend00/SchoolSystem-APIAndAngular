
using MediatR;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Features.Courses.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        public CreateCourseCommandHandler(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<CreateCourseResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course(request.Name, request.Description, request.Goal, request.Summary, request.TargetGroup, request.Gains, request.Requirements, request.ImageData, request.TeacherId);
            var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId);
            if (teacher == null) throw new Exception("Öğretmen bilgisi bulunamadı.");
            await _courseRepository.CreateAsync(course);
            await _courseRepository.SaveChangesAsync();

            return new CreateCourseResponse
            {
                Id = course.Id,
            };
        }
    }
}
