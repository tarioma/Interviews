using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Employees;

public class EmployeeTests
{
    private readonly Fixture _fixture;

    public EmployeeTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new EmailAddressCustomization());
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.Create<string>();
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var authData = _fixture.Create<AuthData>();

        // Act
        var employee = new Employee(id, name, emailAddress, roleId, authData);

        // Assert
        employee.Id.Should().Be(id);
        employee.Name.Should().Be(name);
        employee.EmailAddress.Should().Be(emailAddress);
        employee.RoleId.Should().Be(roleId);
        employee.AuthData.Should().Be(authData);
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        var name = _fixture.Create<string>();
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var authData = _fixture.Create<AuthData>();

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId, authData);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string? name)
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var authData = _fixture.Create<AuthData>();

        // Act
        var action = () => new Employee(id, name!, emailAddress, roleId, authData);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_TooLongName_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(Employee.MaxNameLength + 1);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var authData = _fixture.Create<AuthData>();

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId, authData);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_NullEmailAddress_ThrowsArgumentNullException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.Create<string>();
        EmailAddress emailAddress = null!;
        var roleId = _fixture.Create<Guid>();
        var authData = _fixture.Create<AuthData>();

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId, authData);

        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(emailAddress));
    }

    [Fact]
    public void Init_EmptyGuidRoleId_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.Create<string>();
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = Guid.Empty;
        var authData = _fixture.Create<AuthData>();

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId, authData);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(roleId));
    }

    [Fact]
    public void Init_NullAuthData_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.Create<string>();
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        AuthData authData = null!;

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId, authData);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(authData));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var password = _fixture.Create<AuthData>();

        // Act
        var employee = Employee.Create(name, emailAddress, roleId, password);

        // Assert
        employee.Id.Should().NotBeEmpty();
        employee.Name.Should().Be(name);
        employee.EmailAddress.Should().Be(emailAddress);
        employee.RoleId.Should().Be(roleId);
        employee.AuthData.Should().NotBeNull();
    }
}