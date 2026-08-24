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

            group.MapGet("/getby-id/{id:int}", async (int id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetBranchByIdEnumQuery(id));
                return Results.Ok(result);
            });
        }
    }
}
