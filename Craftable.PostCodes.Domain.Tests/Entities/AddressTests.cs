namespace Craftable.PostCodes.Domain.Tests.Entities
{
    using AutoFixture;

    using Craftable.PostCodes.Domain.Entities;

    using FluentAssertions;

    public class AddressTests
    {
        private readonly IFixture _fixture;

        public AddressTests()
        {
            this._fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ValidParameters_ShouldCreateObject()
        {
            // Arrange
            var postCode = this._fixture.Create<string>();
            var district = this._fixture.Create<string>();
            var latitude = this._fixture.Create<double>();
            var longitude = this._fixture.Create<double>();

            // Act
            var result = new Address(postCode, latitude, longitude, district);

            // Assert
            result.Should().NotBeNull();
            result.PostCode.Should().Be(postCode);
            result.Latitude.Should().Be(latitude);
            result.Longitude.Should().Be(longitude);
            result.District.Should().Be(district);
        }
    }
}