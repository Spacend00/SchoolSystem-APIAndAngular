using MediatR;
using SchoolSystem.Application.Features.Enums.BranchEnum;

namespace SchoolSystem.WebAPI.Endpoints
{
    public static class EnumEndpoints
    {
        public static void MapEnumEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/enums").WithTags("Enums");

            group.MapGet("/get-branches", async (IMediator mediator) =>
            {
                var branches = await mediator.Send(new GetBranchesEnumQuery());
                return Results.Ok(branches);
            });
        }
    }
}
