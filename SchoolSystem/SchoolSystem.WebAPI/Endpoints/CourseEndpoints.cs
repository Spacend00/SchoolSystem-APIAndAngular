using MediatR;
using SchoolSystem.Application.Features.Courses.Queries.GetAllActiveCourses;
using SchoolSystem.Application.Features.Courses.Queries.GetAllCourses;
using SchoolSystem.Application.Features.Courses.Queries.GetCourseById;
using SchoolSystem.Application.Features.Courses.SoftDeleteCourse;
using SchoolSystem.Application.Features.Courses.UpdateCourse;

namespace SchoolSystem.WebAPI.Endpoints
{
    public static class CourseEndpoints
    {
        public static void MapCourseEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/course").WithTags("Courses");

            group.MapDelete("/delete/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new SoftDeleteCourseCommand(id));
                return Results.NoContent();
            });

            group.MapPut("/update", async (UpdateCourseCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.NoContent();
            });

            group.MapGet("/getall", async (IMediator mediator) =>
            {
                var teachers = await mediator.Send(new GetAllCoursesQuery());
                return Results.Ok(teachers);
            });

            group.MapGet("/getall-active", async (IMediator mediator) =>
            {
                var teachers = mediator.Send(new GetAllActiveCoursesQuery());
                return Results.Ok(teachers);
            });

            group.MapGet("/getby-id/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var teacher = mediator.Send(new GetCourseByIdQuery(id));
                return Results.Ok(teacher);
            });
        }
    }
}
