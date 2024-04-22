namespace Craftable.PostCodes.Application.Tests.Services
{
    using Abstractions.Application;
    using Abstractions.Infrastructure;

    using AutoFixture;

    using Craftable.PostCodes.Application.Services;

    using Domain.Entities;

    using FluentAssertions;

    using Microsoft.Extensions.Logging;

    using Moq;

    public class PostCodesServiceTests
    {
        private readonly Mock<IGeoCalculatorService> _geoCalculatorServiceMock;
        private readonly Mock<IPostCodesGateway> _postCodesGatewayMock;

        private readonly IPostCodesService _postCodesService;

        private readonly IFixture _fixture;
        
        public PostCodesServiceTests()
        {
            this._geoCalculatorServiceMock = new Mock<IGeoCalculatorService>(MockBehavior.Strict);
            this._postCodesGatewayMock = new Mock<IPostCodesGateway>(MockBehavior.Strict);

            this._fixture = new Fixture();
            
            this._postCodesService = new PostCodesService(
                this._postCodesGatewayMock.Object,
                this._geoCalculatorServiceMock.Object);
        }

        [Fact]
        public async Task GetAddressDetailsAsync_EmptyPostCode_ShouldThrowException()
        {   
            // Arrange
            var input = string.Empty;
            
            // Act
            var result = async () => await this._postCodesService.GetAddressDetailsAsync(input);

            // Assert
            await result.Should().ThrowAsync<ArgumentNullException>();
            
            this._geoCalculatorServiceMock.VerifyNoOtherCalls();
            this._postCodesGatewayMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task GetAddressDetailsAsync_ValidPostCode_ShouldReturnAddressDetails()
        {   
            // Arrange
            var postCode = this._fixture.Create<string>();
            var domainAddress = this._fixture.Create<Address>();
            var distanceInKms = this._fixture.Create<double>();

            this._postCodesGatewayMock
                .Setup(x => x.GetPostCodeAsync(postCode))
                .ReturnsAsync(domainAddress);

            this._geoCalculatorServiceMock
                .Setup(x => x.GetDistanceToHeathrow(domainAddress.Latitude, domainAddress.Longitude))
                .Returns(distanceInKms);
            
            // Act
            var result = await this._postCodesService.GetAddressDetailsAsync(postCode);

            // Assert
            result.Should().NotBeNull();
            result.Longitude.Should().Be(domainAddress.Longitude);
            result.Latitude.Should().Be(domainAddress.Latitude);
            result.PostCode.Should().Be(postCode);
            result.District.Should().Be(domainAddress.District);
            result.DistanceToHeathrowInKMs.Should().BeApproximately(distanceInKms, 2);
            result.DistanceToHeathrowInMiles.Should().BeApproximately(distanceInKms / 1.6, 2);
            
            this._geoCalculatorServiceMock.VerifyAll();
            this._postCodesGatewayMock.VerifyAll();
        }
    }
}