using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities;
using Resumes.Domain.Entities.Exceptions;

namespace Resumes.Domain.Tests.Entities;

public class UserTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void User_Create_ShouldInitializeProperties()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();

        // Act
        var user = User.Create(name, emailAddress, roleId);

        // Assert
        user.Id.Should().NotBe(Guid.Empty);
        user.Name.Should().Be(name);
        user.EmailAddress.Should().Be(emailAddress);
        user.RoleId.Should().Be(roleId);
    }

    [Fact]
    public void User_SetName_WithValidName_ShouldUpdateName()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();
        
        var newName = _fixture.Create<string>();

        // Act
        var user = User.Create(name, emailAddress, roleId);
        user.SetName(newName);

        // Assert
        user.Name.Should().Be(newName);
    }
    
    [Fact]
    public void User_SetName_WithNullName_ShouldThrowArgumentException()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();

        // Act
        var user = User.Create(name, emailAddress, roleId);
        Action act = () => user.SetName(null!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void User_SetEmailAddress_WithValidAddress_ShouldUpdateEmailAddress()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();
        
        var newGeneratedEmailAddress = _fixture.Create<string>() + "@example.com";
        var newEmailAddress = new EmailAddress(newGeneratedEmailAddress);

        // Act
        var user = User.Create(name, emailAddress, roleId);
        user.SetEmailAddress(newEmailAddress);

        // Assert
        user.EmailAddress.Should().Be(newEmailAddress);
    }
    
    [Fact]
    public void User_SetName_WithNullEmailAddress_ShouldThrowArgumentException()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();

        // Act
        var user = User.Create(name, emailAddress, roleId);
        Action act = () => user.SetEmailAddress(null!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void User_SetRoleId_WithValidId_ShouldUpdateRoleId()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();
        
        var newRoleId = _fixture.Create<Guid>();

        // Act
        var user = User.Create(name, emailAddress, roleId);
        user.SetRoleId(newRoleId);

        // Assert
        user.RoleId.Should().Be(newRoleId);
    }

    [Fact]
    public void User_SetRoleId_WithEmptyId_ShouldThrowEmptyGuidException()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
        var emailAddress = new EmailAddress(generatedEmailAddress);
        var roleId = _fixture.Create<Guid>();

        // Act
        var user = User.Create(name, emailAddress, roleId);
        Action act = () => user.SetRoleId(Guid.Empty);

        // Assert
        act.Should().Throw<EmptyGuidException>();
    }
}