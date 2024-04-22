namespace Craftable.PostCodes.Application.Abstractions.Infrastructure
{
    using Domain.Entities;
    
    public interface IPostCodesGateway
    {
        Task<Address> GetPostCodeAsync(string postcode);
    }
}