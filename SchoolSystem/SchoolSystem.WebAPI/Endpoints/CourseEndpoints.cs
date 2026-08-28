using MediatR;
using SchoolSystem.Application.Features.Courses.CreateCourse;
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

            group.MapPost("/create", async (CreateCourseCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            group.MapDelete("/delete/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new SoftDeleteCourseCommand(id));
                return Results.NoContent();
            }).RequireAuthorization(options => options.RequireRole("Teacher"));

            group.MapPut("/update", async (UpdateCourseCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.NoContent();
            }).RequireAuthorization(options => options.RequireRole("Teacher"));

            group.MapGet("/getall", async (IMediator mediator) =>
            {
                var courses = await mediator.Send(new GetAllCoursesQuery());
                return Results.Ok(courses);
            });

            group.MapGet("/getall-active", async (IMediator mediator) =>
            {
                var courses = await mediator.Send(new GetAllActiveCoursesQuery());
                return Results.Ok(courses);
            });

            group.MapGet("/getby-id/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var course = await mediator.Send(new GetCourseByIdQuery(id));
                return Results.Ok(course);
            });
        }
    }
}
