using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Tests.Tools;
using Xunit.Abstractions;

namespace Interviews.Domain.Tests.Entities;

public class EmailAddressTests
{
    private readonly Fixture _fixture;

    public EmailAddressTests(ITestOutputHelper testOutputHelper)
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
        Assert.NotNull(emailAddress.Value);
        Assert.NotEmpty(emailAddress.Value);
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
            .WithMessage($"Максимальная длина {EmailAddress.MaxValueLength} символов. (Parameter '{nameof(value)}')")
            .WithParameterName(nameof(value));
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("@")]
    [InlineData("a@")]
    [InlineData("@b")]
    public void Init_InvalidEmailAddress_ThrowsArgumentException(string value)
    {
        // Act
        var action = () => new EmailAddress(value);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithMessage($"Адрес навалиден. (Parameter '{nameof(value)}')")
            .WithParameterName(nameof(value));
    }
}