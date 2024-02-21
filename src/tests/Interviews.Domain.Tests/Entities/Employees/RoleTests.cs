using AutoFixture;
using Interviews.Domain.Entities.Employees;

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
        var name = string.Join(string.Empty, _fixture.CreateMany<char>(Role.MaxNameLength));
        
        // Act
        var role = new Role(id, name);
        
        // Assert
        Assert.Equal(id, role.Id);
        Assert.Equal(name, role.Name);
    }
    
    [Fact]
    public void Init_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int invalidLength = Role.MaxNameLength + 1;
        var name = string.Join(string.Empty, _fixture.CreateMany<char>(invalidLength));

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Role(id, name));
        Assert.StartsWith($"Максимальная длина {Role.MaxNameLength} символов.", ex.Message);
        Assert.Equal(nameof(name), ex.ParamName);
    }
    
    [Fact]
    public void Create_CorrectName_SuccessInit()
    {
        // Arrange
        var name = string.Join(string.Empty, _fixture.CreateMany<char>(Role.MaxNameLength));

        // Act
        var role = Role.Create(name);
        
        // Assert
        Assert.Equal(name, role.Name);
        Assert.NotEqual(Guid.Empty, role.Id);
    }
    
    [Fact]
    public void Create_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        const int invalidLength = Role.MaxNameLength + 1;
        var name = string.Join(string.Empty, _fixture.CreateMany<char>(invalidLength));

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Role.Create(name));
        Assert.StartsWith($"Максимальная длина {Role.MaxNameLength} символов.", ex.Message);
        Assert.Equal(nameof(name), ex.ParamName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData("\t")]
    public void Create_EmptyName_ThrowsArgumentException(string name)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => Role.Create(name));
        Assert.Equal(nameof(name), ex.ParamName);
    }
}