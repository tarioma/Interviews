using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Requests.Events;

namespace Interviews.Domain.Tests.Entities.Requests.Events;

public class RequestApprovedEventTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var dateTime = _fixture.Create<DateTime>();
        var requestId = _fixture.Create<Guid>();

        // Act
        var requestApprovedEvent = new RequestApprovedEvent(id, dateTime, requestId);

        // Assert
        requestApprovedEvent.Id.Should().Be(id);
        requestApprovedEvent.DateTime.Should().Be(dateTime);
        requestApprovedEvent.RequestId.Should().Be(requestId);
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        var dateTime = _fixture.Create<DateTime>();
        var requestId = _fixture.Create<Guid>();

        // Act
        var action = () => new RequestApprovedEvent(id, dateTime, requestId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }

    [Fact]
    public void Init_DefaultDateTime_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var dateTime = default(DateTime);
        var requestId = _fixture.Create<Guid>();

        // Act
        var action = () => new RequestApprovedEvent(id, dateTime, requestId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(dateTime));
    }

    [Fact]
    public void Init_EmptyGuidRequestId_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var dateTime = _fixture.Create<DateTime>();
        var requestId = Guid.Empty;

        // Act
        var action = () => new RequestApprovedEvent(id, dateTime, requestId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(requestId));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var requestId = _fixture.Create<Guid>();

        // Act
        var requestApprovedEvent = RequestApprovedEvent.Create(requestId);

        // Assert
        requestApprovedEvent.Id.Should().NotBeEmpty();
        requestApprovedEvent.DateTime.Should().NotBe(default);
        requestApprovedEvent.RequestId.Should().Be(requestId);
    }
}