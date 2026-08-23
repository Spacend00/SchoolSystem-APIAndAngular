using MediatR;
using SchoolSystem.Application.Features.Teachers.Queries.GetAllActiveTeachers;
using SchoolSystem.Application.Features.Teachers.Queries.GetAllTeachers;
using SchoolSystem.Application.Features.Teachers.Queries.GetTeacherByEmail;
using SchoolSystem.Application.Features.Teachers.Queries.GetTeacherById;
using SchoolSystem.Application.Features.Teachers.SoftDeleteTeachers;
using SchoolSystem.Application.Features.Teachers.UpdateTeachers;

namespace SchoolSystem.WebAPI.Endpoints
{
    public static class TeacherEndpoints
    {
        public static void MapTeacherEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/teacher").WithTags("Teachers");

            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new SoftDeleteTeacherCommand(id));
                return Results.NoContent();
            });

            group.MapPut("/update", async (UpdateTeacherCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.NoContent();
            });

            group.MapGet("/all", async (IMediator mediator) =>
            {
                var teachers = await mediator.Send(new GetAllTeachersQuery());
                return Results.Ok(teachers);
            });

            group.MapGet("/all-active", async (IMediator mediator) =>
            {
                var teachers = await mediator.Send(new GetAllActiveTeachersQuery());
                return Results.Ok(teachers);
            });

            group.MapGet("/getby-id/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var teacher = await mediator.Send(new GetTeacherByIdQuery(id));
                return Results.Ok(teacher);
            });

            group.MapGet("/getby-email/{email}", async (string email, IMediator mediator) =>
            {
                var teacher = await mediator.Send(new GetTeacherByEmailQuery(email));
                return Results.Ok(teacher);
            });
        }
    }
}
