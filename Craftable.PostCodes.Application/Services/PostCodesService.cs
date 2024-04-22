namespace Craftable.PostCodes.Application.Services
{
    using Abstractions.Application;
    using Abstractions.Infrastructure;

    using DTO;

    using Microsoft.Extensions.Logging;

    public class PostCodesService : IPostCodesService
    {
        private readonly IGeoCalculatorService _geoCalculatorService; 
        private readonly IPostCodesGateway _postCodesGateway;
        
        public PostCodesService(
            IPostCodesGateway postCodesGateway,
            IGeoCalculatorService geoCalculatorService)
        {
            this._postCodesGateway = postCodesGateway;
            this._geoCalculatorService = geoCalculatorService;
        }

        public async Task<AddressDTO> GetAddressDetailsAsync(string postCode)
        {
            if (string.IsNullOrEmpty(postCode))
            {
                throw new ArgumentNullException(nameof(postCode), "Post Code can't be empty.");
            }
            
            var result = await this._postCodesGateway.GetPostCodeAsync(postCode);

            var distanceInKms = this._geoCalculatorService.GetDistanceToHeathrow(result.Latitude, result.Longitude);

            return new AddressDTO
            {
                Latitude = result.Latitude,
                Longitude = result.Longitude,
                PostCode = postCode,
                DistanceToHeathrowInKMs = double.Round(distanceInKms, 2),
                DistanceToHeathrowInMiles = double.Round(distanceInKms / 1.6, 2),
                District = result.District
            };
        }
    }
}