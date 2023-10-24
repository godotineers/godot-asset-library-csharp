using GodotAssetLibrary.Application.Attributes;
using GodotAssetLibrary.Application.Results.Core;
using GodotAssetLibrary.Contracts.Repositories;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Core
{
    [UseCache]
    public class GetLicenses : IRequest<GetLicensesResult>
    {

        public class GetLicensesHandler : IRequestHandler<GetLicenses, GetLicensesResult>
        {
            public GetLicensesHandler(
                        ILicenseRepository licenseRepository)
            {
                LicenseRepository = licenseRepository;
            }

            public ILicenseRepository LicenseRepository { get; }

            public async Task<GetLicensesResult> Handle(GetLicenses request, CancellationToken cancellationToken)
            {
                return new GetLicensesResult
                {
                    Licenses = (await LicenseRepository.GetLicenses(cancellationToken)).ToList(),
                };
            }
        }
    }
}
