
using MediatR;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Application.Features.Enums.BranchEnum
{
    public class GetBranchByIdEnumQueryHandler : IRequestHandler<GetBranchByIdEnumQuery, EnumLookupDto>
    {
        public Task<EnumLookupDto> Handle(GetBranchByIdEnumQuery request, CancellationToken cancellationToken)
        {
            if (Enum.IsDefined(typeof(Branch), request.Id)) throw new InvalidOperationException("Bu id'ye ait branş bulunamadı.");

            var branch = (Branch)request.Id;
            var result = new EnumLookupDto
            {
                Id = (int)branch,
                Name = branch.ToString()
            };

            return Task.FromResult(result);
        }
    }
}
