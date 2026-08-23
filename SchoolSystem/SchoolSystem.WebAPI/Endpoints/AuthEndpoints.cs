using MediatR;
using SchoolSystem.Application.Features.Auth.StudentCommands.Login;
using SchoolSystem.Application.Features.Auth.StudentCommands.Register;
using SchoolSystem.Application.Features.Auth.TeacherCommands.Login;
using SchoolSystem.Application.Features.Auth.TeacherCommands.Register;

namespace SchoolSystem.WebAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/login").WithTags("Login");

            group.MapPost("/student", async (StudentLoginCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            group.MapPost("/teacher", async (TeacherLoginCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            var group2 = app.MapGroup("api/register").WithTags("Register");

            group2.MapPost("/student", async (StudentRegisterCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            group2.MapPost("/teacher", async (TeacherRegisterCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

        }
    }
}
