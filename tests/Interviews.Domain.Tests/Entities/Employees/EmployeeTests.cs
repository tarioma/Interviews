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
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();

        // Act
        var employee = new Employee(id, name, emailAddress, roleId);

        // Assert
        employee.Id.Should().Be(id);
        employee.Name.Should().Be(name);
        employee.EmailAddress.Should().Be(emailAddress);
        employee.RoleId.Should().Be(roleId);
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }

    [Fact]
    public void Init_EmptyGuidRoleId_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = Guid.Empty;

        // Act
        var action = () => new Employee(id, name, emailAddress, roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(roleId));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();

        // Act
        var employee = Employee.Create(name, emailAddress, roleId);

        // Assert
        employee.Id.Should().NotBeEmpty();
        employee.Name.Should().Be(name);
        employee.EmailAddress.Should().Be(emailAddress);
        employee.RoleId.Should().Be(roleId);
    }

    [Fact]
    public void SetName_ValidName_NameChangedSuccessfully()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(Employee.MaxNameLength);

        // Act
        employee.SetName(name);

        // Assert
        employee.Name.Should().Be(name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetName_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string? name)
    {
        // Arrange
        var employee = _fixture.Create<Employee>();

        // Act
        var action = () => employee.SetName(name!);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void SetName_TooLongName_ThrowsArgumentException()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(Employee.MaxNameLength + 1);

        // Act
        var action = () => employee.SetName(name);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void SetEmailAddress_ValidEmailAddress_EmailAddressChangedSuccessfully()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var emailAddress = _fixture.Create<EmailAddress>();

        // Act
        employee.SetEmailAddress(emailAddress);

        // Assert
        employee.EmailAddress.Should().Be(emailAddress);
    }

    [Fact]
    public void SetEmailAddress_NullEmailAddress_ThrowsArgumentNullException()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        EmailAddress emailAddress = null!;

        // Act
        var action = () => employee.SetEmailAddress(emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(emailAddress));
    }
}