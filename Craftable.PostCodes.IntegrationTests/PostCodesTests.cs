namespace Craftable.PostCodes.IntegrationTests
{
    using Application.DTO;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Setup;

    public class PostCodesTests
    {
        private readonly PostCodesWebApplicationFactory _factory;

        private HttpClient GetClient() => this._factory.CreateClient();
        
        public PostCodesTests()
        {
            this._factory = new PostCodesWebApplicationFactory();
        }

        [Theory]
        [InlineData("N76RS")]
        [InlineData("SW46TA")]
        [InlineData("SW1A 0AA")]
        [InlineData("W1B3AG")]
        [InlineData("PO63TD")]
        public async Task GetPostCodeAsync_ValidPostCode_ShouldReturnAddressInformation(string postCode)
        {
            // Arrange
            var requestUri = "api/v1/PostCodes?postCode=" + postCode;

            // Act
            var result = await this.GetClient().GetAsync(requestUri);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessStatusCode.Should().BeTrue();

            var responseString = await result.Content.ReadAsStringAsync();
            var address = JsonConvert.DeserializeObject<AddressDTO>(responseString);

            address.Should().NotBeNull();
            address.PostCode.Should().Be(postCode);
            address.Latitude.Should().NotBe(0);
            address.Longitude.Should().NotBe(0);
            address.DistanceToHeathrowInMiles.Should().BeGreaterThan(0);
            address.DistanceToHeathrowInKMs.Should().BeGreaterThan(0);
        }
    }
}