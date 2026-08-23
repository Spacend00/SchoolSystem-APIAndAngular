using MediatR;
using SchoolSystem.Application.Features.Students.DeleteStudents;
using SchoolSystem.Application.Features.Students.Queries.GetAllActiveStudents;
using SchoolSystem.Application.Features.Students.Queries.GetAllStudents;
using SchoolSystem.Application.Features.Students.Queries.GetStudentByEmail;
using SchoolSystem.Application.Features.Students.Queries.GetStudentById;
using SchoolSystem.Application.Features.Students.UpdateStudents;

namespace SchoolSystem.WebAPI.Endpoints
{
    public static class StudentEndpoints
    {
        public static void MapStudentEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/students").WithTags("Students");

            group.MapDelete("/delete/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new SoftDeleteStudentCommand(id));
                return Results.NoContent();
            });

            group.MapPut("/update/{id:guid}", async (Guid id, UpdateStudentCommand command, IMediator mediator) =>
            {
                command.Id = id;
                await mediator.Send(command);
                return Results.NoContent();
            });

            group.MapGet("/getall", async (IMediator mediator) =>
            {
                var students = await mediator.Send(new GetAllStudentsQuery());
                return Results.Ok(students);
            });

            group.MapGet("/getall-active", async (IMediator mediator) =>
            {
                var students = await mediator.Send(new GetAllActiveStudentsQuery());
                return Results.Ok(students);
            });

            group.MapGet("/getby-id/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var student = await mediator.Send(new GetStudentByIdQuery(id));
                return Results.Ok(student);
            });

            group.MapGet("/getby-email/{email}", async (string email, IMediator mediator) =>
            {
                var student = await mediator.Send(new GetStudentByEmailQuery(email));
                return Results.Ok(student);
            });
        }
    }
}
