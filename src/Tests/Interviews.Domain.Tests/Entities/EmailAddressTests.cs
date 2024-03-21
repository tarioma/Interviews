using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities;

public class EmailAddressTests
{
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("t@t")]
    public void Init_CorrectValue_SuccessInit(string value)
    {
        // Arrange
        var expectedValue = value.ToUpperInvariant();

        // Act
        var emailAddress = new EmailAddress(value);

        // Assert
        emailAddress.Value.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("@")]
    [InlineData("a@")]
    [InlineData("@a")]
    public void Init_InvalidValue_ThrowsArgumentException(string value)
    {
        // Act
        var action = () => new EmailAddress(value);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(value));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceValue_ThrowsArgumentException(string? value)
    {
        // Act
        var action = () => new EmailAddress(value!);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(value));
    }

    [Fact]
    public void Init_TooLongValue_ThrowsArgumentException()
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
}