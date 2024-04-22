namespace Craftable.PostCodes.Domain.Entities
{
    public class Address
    {
        public Address(string postCode, double latitude, double longitude, string district)
        {
            this.PostCode = postCode;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.District = district;
        }
        public string PostCode { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string District { get; }
    }
}