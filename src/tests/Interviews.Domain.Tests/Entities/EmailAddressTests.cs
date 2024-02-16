using AutoFixture;
using Interviews.Domain.Entities;

namespace Interviews.Domain.Tests.Entities;

public class EmailAddressTests
{
    private readonly Fixture _fixture = new();
    
    [Theory]
    [InlineData("test@example.com")]
    [InlineData(" test@example.com")]
    [InlineData("test@example.com\n")]
    [InlineData("\rtest@example.com\t")]
    public void Init_CorrectEmailAddress_SuccessInit(string value)
    {
        // Arrange
        var expectedValue = value.Trim().ToUpperInvariant();

        // Act
        var emailAddress = new EmailAddress(value);
        
        // Assert
        Assert.Equal(expectedValue, emailAddress.Value);
    }
    
    [Fact]
    public void Init_VeryLongEmailAddress_ThrowsArgumentException()
    {
        // Arrange
        const int invalidLength = EmailAddress.MaxValueLength + 1;
        var value = string.Join(string.Empty, _fixture.CreateMany<char>(invalidLength));
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new EmailAddress(value));
        Assert.StartsWith($"Максимальная длина {EmailAddress.MaxValueLength} символов.", ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
    }
    
    [Theory]
    [InlineData("@")]
    [InlineData("@example.com")]
    [InlineData("test@")]
    [InlineData("x")]
    public void Init_InvalidEmailAddress_ThrowsArgumentException(string value)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new EmailAddress(value));
        Assert.StartsWith("Адрес навалиден.", ex.Message);
        Assert.Equal(nameof(value), ex.ParamName);
    }
}