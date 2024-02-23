using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Employees;

public class RoleTests
{
    private readonly Fixture _fixture;

    public RoleTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var id = Guid.NewGuid();
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
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string name)
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var action = () => new Role(id, name);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Init_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int invalidLength = Role.MaxNameLength + 1;
        var name = _fixture.GenerateString(invalidLength);

        // Act
        var action = () => new Role(id, name);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var name = _fixture.GenerateString(Role.MaxNameLength);

        // Act
        var role = Role.Create(name);

        // Assert
        role.Should().NotBeNull();
        role.Id.Should().NotBe(Guid.Empty);
        role.Name.Should().NotBeNullOrWhiteSpace();
    }
}