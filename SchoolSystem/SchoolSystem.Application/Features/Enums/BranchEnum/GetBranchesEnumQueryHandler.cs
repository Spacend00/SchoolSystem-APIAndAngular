
using MediatR;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Application.Features.Enums.BranchEnum
{
    public class GetBranchesEnumQueryHandler : IRequestHandler<GetBranchesEnumQuery, List<EnumLookupDto>>
    {
        public Task<List<EnumLookupDto>> Handle(GetBranchesEnumQuery request, CancellationToken cancellationToken)
        {
            var branches = Enum.GetValues(typeof(Branch))
                .Cast<Branch>()
                .Select(b => new EnumLookupDto
                {
                    Id = (int)b,
                    Name = b.ToString()
                }).ToList();

            return Task.FromResult(branches);
        }
    }
}
