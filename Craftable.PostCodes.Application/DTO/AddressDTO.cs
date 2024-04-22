namespace Craftable.PostCodes.Application.DTO
{
    public class AddressDTO
    {
        public string PostCode { get; set; } = string.Empty;
        
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double DistanceToHeathrowInKMs { get; set; }
        
        public double DistanceToHeathrowInMiles { get; set; }

        public string District { get; set; } = string.Empty;
    }
}