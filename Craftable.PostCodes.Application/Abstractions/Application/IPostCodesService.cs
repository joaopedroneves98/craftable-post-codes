namespace Craftable.PostCodes.Application.Abstractions.Application
{
    using DTO;
    public interface IPostCodesService
    {
        Task<AddressDTO> GetAddressDetailsAsync(string postCode);
    }
}