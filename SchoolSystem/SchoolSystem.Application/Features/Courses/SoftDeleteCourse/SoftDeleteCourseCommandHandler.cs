
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.SoftDeleteCourse
{
    public class SoftDeleteCourseCommandHandler : IRequestHandler<SoftDeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        public SoftDeleteCourseCommandHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        public async Task Handle(SoftDeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null) throw new Exception("Bu id'ye ait kurs bulunamadı.");
            if (!course.IsActive) throw new Exception("Bu kurs zaten aktif değil.");

            course.Deactivate();
            await _courseRepository.SaveChangesAsync();
        }
    }
}
