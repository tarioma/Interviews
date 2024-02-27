using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Requests;

public class DocumentTests
{
    private readonly Fixture _fixture;

    public DocumentTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new DocumentCustomization());
    }
    
    [Fact]
    public void Init_CorrectEmailAddress_SuccessInit()
    {
        // Act
        var document = _fixture.Create<Document>();
        
        // Assert
        document.Name.Should().NotBeNullOrWhiteSpace();
        document.DateOfBirth.Should().NotBe(default);
        document.EmailAddress.Should().NotBeNull();
    }
    
    
}