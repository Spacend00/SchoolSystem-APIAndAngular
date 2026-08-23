
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null) throw new Exception("Bu id'ye ait kurs bulunamadı.");
            if (!course.IsActive) throw new Exception("Bu kurs aktif değil.");

            course.UpdateCourse(request.Name, request.Credit);
            await _courseRepository.SaveChangesAsync();
        }
    }
}
