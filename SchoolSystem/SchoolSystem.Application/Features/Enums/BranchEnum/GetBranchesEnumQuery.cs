
using MediatR;

namespace SchoolSystem.Application.Features.Enums.BranchEnum
{
    public class GetBranchesEnumQuery : IRequest<List<EnumLookupDto>>
    {
    }
}
