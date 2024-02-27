using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities;

public class EmailAddressTests
{
    private readonly Fixture _fixture;

    public EmailAddressTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new EmailAddressCustomization());
    }

    [Fact]
    public void Init_CorrectEmailAddress_SuccessInit()
    {
        // Act
        var emailAddress = _fixture.Create<EmailAddress>();
        
        // Assert
        emailAddress.Value.Should()
            .NotBeNullOrWhiteSpace()
            .And
            .NotBeLowerCased();
    }
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceValue_ThrowsArgumentNullException(string value)
    {
        // Act
        var action = () => new EmailAddress(value);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(value));
    }

    [Fact]
    public void Init_VeryLongValue_ThrowsArgumentException()
    {
        // Arrange
        var value = _fixture.GenerateString(EmailAddress.MaxValueLength + 1);
        
        // Act
        var action = () => new EmailAddress(value);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(value));
    }

    [Theory]
    [InlineData("a")]
    [InlineData("@")]
    [InlineData("a@")]
    [InlineData("@a")]
    public void Init_InvalidEmailAddress_ThrowsArgumentException(string value)
    {
        // Act
        var action = () => new EmailAddress(value);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(value));
    }
}