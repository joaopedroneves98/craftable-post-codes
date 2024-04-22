namespace Craftable.PostCodes.Application.Tests.Services
{
    using Abstractions.Application;

    using Craftable.PostCodes.Application.Services;

    using FluentAssertions;

    public class GeoCalculatorServiceTests
    {
        private readonly IGeoCalculatorService _geoCalculatorService;
        
        public GeoCalculatorServiceTests()
        {
            this._geoCalculatorService = new GeoCalculatorService();
        }

        [Theory]
        [InlineData(51.4700223, -0.4542955, 0)]
        [InlineData(51.5031688, -0.1100182, 24.12)]
        public void GetDistanceToHeathrow_ValidValues_ShouldReturnDistance(double latitude, double longitude, double expectedResult)
        {
            // Arrange 

            // Act
            var result = this._geoCalculatorService.GetDistanceToHeathrow(latitude, longitude);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}