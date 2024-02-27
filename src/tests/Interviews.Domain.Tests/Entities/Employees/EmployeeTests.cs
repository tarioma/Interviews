using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Employees;

public class EmployeeTests
{
    private readonly Fixture _fixture;
    private readonly Guid _id;
    private readonly string _name;
    private readonly EmailAddress _emailAddress;
    private readonly Guid _roleId;

    public EmployeeTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new EmailAddressCustomization());

        _id = Guid.NewGuid();
        _name = _fixture.GenerateString(Employee.MaxNameLength);
        _emailAddress = _fixture.Create<EmailAddress>();
        _roleId = Guid.NewGuid();
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Act
        var employee = new Employee(_id, _name, _emailAddress, _roleId);

        // Assert
        employee.Id.Should().Be(_id);
        employee.Name.Should().Be(_name);
        employee.EmailAddress.Should().Be(_emailAddress);
        employee.RoleId.Should().Be(_roleId);
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        
        // Act
        var action = () => new Employee(id, _name, _emailAddress, _roleId);

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
        var action = () => new Employee(_id, name, _emailAddress, _roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(Employee.MaxNameLength + 1);

        // Act
        var action = () => new Employee(_id, name, _emailAddress, _roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Init_NullEmailAddress_ThrowsArgumentNullException()
    {
        // Arrange
        EmailAddress emailAddress = null!;

        // Act
        var action = () => new Employee(_id, _name, emailAddress, _roleId);

        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(emailAddress));
    }

    [Fact]
    public void Init_EmptyGuidRoleId_ThrowsArgumentException()
    {
        // Arrange
        var roleId = Guid.Empty;

        // Act
        var action = () => new Employee(_id, _name, _emailAddress, roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(roleId));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Act
        var employee = Employee.Create(_name, _emailAddress, _roleId);

        // Assert
        employee.Id.Should().NotBe(Guid.Empty);
        employee.Name.Should().Be(_name);
        employee.EmailAddress.Should().Be(_emailAddress);
        employee.RoleId.Should().Be(_roleId);
    }
}