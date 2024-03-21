﻿using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Employees;

public class RoleTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(Role.MaxNameLength);

        // Act
        var role = new Role(id, name);

        // Assert
        role.Id.Should().Be(id);
        role.Name.Should().Be(name);
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentNullException()
    {
        // Arrange
        var id = Guid.Empty;
        var name = _fixture.GenerateString(Role.MaxNameLength);

        // Act
        var action = () => new Role(id, name);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var name = _fixture.GenerateString(Role.MaxNameLength);

        // Act
        var role = Role.Create(name);

        // Assert
        role.Id.Should().NotBeEmpty();
        role.Name.Should().Be(name);
    }

    [Fact]
    public void SetName_ValidName_NameChangedSuccessfully()
    {
        // Arrange
        var role = _fixture.Create<Role>();
        var name = _fixture.GenerateString(Role.MaxNameLength);

        // Act
        role.SetName(name);

        // Assert
        role.Name.Should().Be(name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetName_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string? name)
    {
        // Arrange
        var role = _fixture.Create<Role>();

        // Act
        var action = () => role.SetName(name!);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void SetName_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        var role = _fixture.Create<Role>();
        var name = _fixture.GenerateString(Role.MaxNameLength + 1);

        // Act
        var action = () => role.SetName(name);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
}