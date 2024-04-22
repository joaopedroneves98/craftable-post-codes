namespace Craftable.PostCodes.Infrastructure.PostCodes
{
    using Application.Abstractions.Infrastructure;

    using Domain.Entities;

    using Exceptions;

    using MarkEmbling.PostcodesIO;

    using Microsoft.Extensions.Logging;

    public class PostCodesGateway : IPostCodesGateway
    {
        private readonly ILogger<PostCodesGateway> _logger;
        private readonly IPostcodesIOClient _client;

        public PostCodesGateway(
            ILogger<PostCodesGateway> logger, 
            IPostcodesIOClient client)
        {
            this._logger = logger;
            this._client = client;
        }

        public async Task<Address> GetPostCodeAsync(string postcode)
        {
            try
            {
                var result = await this._client.LookupAsync(postcode);

                if (result is null)
                {
                    throw new NotFoundException("Post Code not found.");
                }

                return new Address(
                    result.Postcode, 
                    result.Latitude,
                    result.Longitude,
                    result.AdminDistrict
                );
            }
            catch (Exception ex)
            {
                // This log could be removed as the Middleware already logs the exception message,
                // however I decided to leave it here to give more context that the error was thrown
                // when requesting the external API
                this._logger.LogError("Error when requesting PostCodes IO API");

                throw;
            }
        }
    }
}