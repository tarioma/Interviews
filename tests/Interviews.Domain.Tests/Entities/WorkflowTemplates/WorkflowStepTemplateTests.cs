using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.WorkflowTemplates;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.WorkflowTemplates;

public class WorkflowStepTemplateTests
{
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Init_CorrectParamsWithoutEmployeeId_SuccessInit(object? employeeId)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        Guid? certainEmployeeId = employeeId is null ? null : Guid.Empty;
        var roleId = _fixture.Create<Guid>();

        // Act
        var workflowStep = new WorkflowStepTemplate(name, order, certainEmployeeId, roleId);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(certainEmployeeId);
        workflowStep.RoleId.Should().Be(roleId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Init_CorrectParamsWithoutRoleId_SuccessInit(object? roleId)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        var employeeId = _fixture.Create<Guid>();
        Guid? certainRoleId = roleId is null ? null : Guid.Empty;

        // Act
        var workflowStep = new WorkflowStepTemplate(name, order, employeeId, certainRoleId);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(certainRoleId);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "")]
    public void Init_WithoutEmployeeIdAndRoleId_ThrowsArgumentException(object? employeeId, object? roleId)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        Guid? certainEmployeeId = employeeId is null ? null : Guid.Empty;
        Guid? certainRoleId = roleId is null ? null : Guid.Empty;

        // Act
        var action = () => new WorkflowStepTemplate(name, order, certainEmployeeId, certainRoleId);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Init_EmployeeIdAndRoleIdDefined_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        var employeeId = _fixture.Create<Guid>();
        var roleId = _fixture.Create<Guid>();

        // Act
        var action = () => new WorkflowStepTemplate(name, order, employeeId, roleId);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string? name)
    {
        // Arrange
        var order = _fixture.Create<int>();
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = null;

        // Act
        var action = () => new WorkflowStepTemplate(name!, order, employeeId, roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_TooLongName_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength + 1);
        var order = _fixture.Create<int>();
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = null;

        // Act
        var action = () => new WorkflowStepTemplate(name, order, employeeId, roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_NegativeOrder_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = -1;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = null;

        // Act
        var action = () => new WorkflowStepTemplate(name, order, employeeId, roleId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(order));
    }
}