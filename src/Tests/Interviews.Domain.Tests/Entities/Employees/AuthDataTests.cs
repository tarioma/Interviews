using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Employees;

public class AuthDataTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Init_CorrectPassword_SuccessInit()
    {
        // Arrange
        var password = _fixture.Create<string>();

        // Act
        var authData = new AuthData(password);

        // Assert
        authData.PasswordHash.Should().NotBeNullOrEmpty();
        authData.Salt.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpacePassword_ThrowsArgumentException(string? password)
    {
        // Act
        var action = () => new AuthData(password!);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(password));
    }

    [Fact]
    public void Init_TooLongPassword_ThrowsArgumentException()
    {
        // Arrange
        var password = _fixture.GenerateString(AuthData.MaxPasswordLength + 1);

        // Act
        var action = () => new AuthData(password);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(password));
    }
}