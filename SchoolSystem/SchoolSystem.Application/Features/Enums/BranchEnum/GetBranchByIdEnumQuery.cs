
using MediatR;

namespace SchoolSystem.Application.Features.Enums.BranchEnum
{
    public class GetBranchByIdEnumQuery : IRequest<EnumLookupDto>
    {
        public int Id { get; set; }
        public GetBranchByIdEnumQuery(int id) => Id = id;
    }
}
