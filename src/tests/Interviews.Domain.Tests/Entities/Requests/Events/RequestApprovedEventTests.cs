using FluentAssertions;
using Interviews.Domain.Entities.Requests.Events;

namespace Interviews.Domain.Tests.Entities.Events;

public class RequestApprovedEventTests
{
    private readonly Guid _id;
    private readonly DateTime _dateTime;
    private readonly Guid _requestId;

    public RequestApprovedEventTests()
    {
        _id = Guid.NewGuid();
        _dateTime = DateTime.UtcNow;
        _requestId = Guid.NewGuid();
    }
    
    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Act
        var requestApprovedEvent = new RequestApprovedEvent(_id, _dateTime, _requestId);
        
        // Assert
        requestApprovedEvent.Id.Should().Be(_id);
        requestApprovedEvent.DateTime.Should().Be(_dateTime);
        requestApprovedEvent.RequestId.Should().Be(_requestId);
    }
    
    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        
        // Act
        var action = () => new RequestApprovedEvent(id, _dateTime, _requestId);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }
    
    [Fact]
    public void Init_EmptyGuidRequestId_ThrowsArgumentException()
    {
        // Arrange
        var requestId = Guid.Empty;
        
        // Act
        var action = () => new RequestApprovedEvent(_id, _dateTime, requestId);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(requestId));
    }
}