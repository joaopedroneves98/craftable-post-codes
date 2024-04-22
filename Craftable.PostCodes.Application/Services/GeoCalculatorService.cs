namespace Craftable.PostCodes.Application.Services
{
    using Abstractions.Application;

    using Geolocation;
    
    public class GeoCalculatorService : IGeoCalculatorService
    {
        private readonly Coordinate _heathrowCoordinates = new Coordinate(51.4700223, -0.4542955);
        
        public double GetDistanceToHeathrow(double latitude, double longitude)
        {
            var postCodeCoordinates = new Coordinate(latitude, longitude);
            
            return GeoCalculator.GetDistance(
                postCodeCoordinates, 
                this._heathrowCoordinates,
                2,
                distanceUnit: DistanceUnit.Kilometers);
        }
    }
}