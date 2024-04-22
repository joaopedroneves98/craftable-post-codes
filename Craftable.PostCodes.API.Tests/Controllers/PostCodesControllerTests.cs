namespace Craftable.PostCodes.API.Tests.Controllers
{
    using Application.Abstractions.Application;
    using Application.DTO;

    using AutoFixture;

    using Craftable.PostCodes.API.Controllers;

    using FluentAssertions;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    public class PostCodesControllerTests
    {
        private readonly Mock<IPostCodesService> _postCodesServiceMock;

        private readonly IFixture _fixture;

        private readonly PostCodesController _controller;

        public PostCodesControllerTests()
        {
            this._fixture = new Fixture();
            this._postCodesServiceMock = new Mock<IPostCodesService>(MockBehavior.Strict);
            this._controller = new PostCodesController(this._postCodesServiceMock.Object);
        }
        
        [Fact]
        public async Task GetPostCodeAsync_ValidRequest_ShouldReturnOkResult()
        {
            // Arrange
            var postCode = this._fixture.Create<string>();
            var addressDto = this._fixture.Create<AddressDTO>();

            this._postCodesServiceMock
                .Setup(x => x.GetAddressDetailsAsync(postCode))
                .ReturnsAsync(addressDto);

            // Act
            var result = await this._controller.GetPostCodeAsync(postCode);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(addressDto);
            
            this._postCodesServiceMock.VerifyAll();
        }
    }
}