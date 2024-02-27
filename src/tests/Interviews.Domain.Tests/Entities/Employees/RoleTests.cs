using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Employees;

public class RoleTests
{
    private readonly Fixture _fixture;
    private readonly Guid _id;
    private readonly string _name;

    public RoleTests()
    {
        _fixture = new Fixture();
        _id = Guid.NewGuid();
        _name = _fixture.GenerateString(Role.MaxNameLength);
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Act
        var role = new Role(_id, _name);
        
        // Assert
        role.Id.Should().Be(_id);
        role.Name.Should().Be(_name);
    }
    
    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentNullException()
    {
        // Arrange
        var id = Guid.Empty;
        
        // Act
        var action = () => new Role(id, _name);
        
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
        // Act
        var action = () => new Role(_id, name);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Init_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(Role.MaxNameLength + 1);

        // Act
        var action = () => new Role(_id, name);
        
        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Act
        var role = Role.Create(_name);

        // Assert
        role.Id.Should().NotBe(Guid.Empty);
        role.Name.Should().Be(_name);
    }
}