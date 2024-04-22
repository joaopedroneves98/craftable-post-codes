namespace Craftable.PostCodes.Infrastructure.Tests.PostCodes
{
    using Application.Abstractions.Infrastructure;

    using AutoFixture;

    using Craftable.PostCodes.Infrastructure.PostCodes;

    using Exceptions;

    using FluentAssertions;

    using MarkEmbling.PostcodesIO;
    using MarkEmbling.PostcodesIO.Results;

    using Microsoft.Extensions.Logging;

    using Moq;

    public class PostCodesGatewayTests
    {
        private readonly Mock<ILogger<PostCodesGateway>> _loggerMock;
        private readonly Mock<IPostcodesIOClient> _clientMock;

        private readonly IPostCodesGateway _postCodesGateway;

        private readonly IFixture _fixture;

        public PostCodesGatewayTests()
        {
            this._loggerMock = new Mock<ILogger<PostCodesGateway>>();
            this._clientMock = new Mock<IPostcodesIOClient>(MockBehavior.Strict);

            this._fixture = new Fixture();

            this._postCodesGateway = new PostCodesGateway(this._loggerMock.Object, this._clientMock.Object);
        }

        [Fact]
        public async Task GetPostCodeAsync_ValidPostCode_ShouldReturnAddressInformation()
        {
            // Arrange
            var postCode = this._fixture.Create<string>();
            var postCodeResult = this._fixture.Create<PostcodeResult>();

            this._clientMock
                .Setup(x => x.LookupAsync(postCode))
                .ReturnsAsync(postCodeResult);

            // Act
            var result = await this._postCodesGateway.GetPostCodeAsync(postCode);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(postCodeResult, options => options.ExcludingMissingMembers());

            this._clientMock.VerifyAll();
            this._loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetPostCodeAsync_InvalidPostCode_ShouldThrowNotFoundException()
        {
            // Arrange
            var postCode = this._fixture.Create<string>();
            PostcodeResult? postCodeResult = null;

            this._clientMock
                .Setup(x => x.LookupAsync(postCode))
                .ReturnsAsync(postCodeResult);
            
            this._loggerMock
                .Setup(x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
                .Verifiable();

            // Act
            var result = async () => await this._postCodesGateway.GetPostCodeAsync(postCode);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();

            this._clientMock.VerifyAll();
            this._loggerMock.VerifyAll();
        }
        
        
        [Fact]
        public async Task GetPostCodeAsync_ClientError_ShouldThrowException()
        {
            // Arrange
            var postCode = this._fixture.Create<string>();

            this._clientMock
                .Setup(x => x.LookupAsync(postCode))
                .ThrowsAsync(new Exception());
            
            this._loggerMock
                .Setup(x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
                .Verifiable();

            // Act
            var result = async () => await this._postCodesGateway.GetPostCodeAsync(postCode);

            // Assert
            await result.Should().ThrowAsync<Exception>();

            this._clientMock.VerifyAll();
            this._loggerMock.VerifyAll();
        }
    }
}