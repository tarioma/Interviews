using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities;

namespace Resumes.Domain.Tests.Entities;

public class RoleTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Role_Create_ShouldInitializeProperties()
    {
        // Arrange
        var name = _fixture.Create<string>();

        // Act
        var role = Role.Create(name);

        // Assert
        role.Id.Should().NotBe(Guid.Empty);
        role.Name.Should().Be(name);
    }

    [Fact]
    public void Role_SetName_WithValidName_ShouldUpdateName()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var newName = _fixture.Create<string>();

        // Act
        var role = Role.Create(name);
        role.SetName(newName);

        // Assert
        role.Name.Should().Be(newName);
    }

    [Fact]
    public void Role_SetName_WithNullOrEmptyName_ShouldThrowArgumentException()
    {
        // Arrange
        var name = _fixture.Create<string>();

        // Act
        var role = Role.Create(name);
        Action act = () => role.SetName(null!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
