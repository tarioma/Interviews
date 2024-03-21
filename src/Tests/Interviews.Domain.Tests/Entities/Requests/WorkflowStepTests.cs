using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.WorkflowTemplates;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Requests;

public class WorkflowStepTests
{
    private readonly Fixture _fixture;

    public WorkflowStepTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new WorkflowStepTemplateCustomization());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Init_CorrectParamsWithoutEmployeeId_SuccessInit(object? employeeId)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        Guid? certainEmployeeId = employeeId is null ? null : Guid.Empty;
        var roleId = _fixture.Create<Guid>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = new WorkflowStep(name, order, status, certainEmployeeId, roleId, comment);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(certainEmployeeId);
        workflowStep.RoleId.Should().Be(roleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Init_CorrectParamsWithoutRoleId_SuccessInit(object? roleId)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? certainRoleId = roleId is null ? null : Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = new WorkflowStep(name, order, status, employeeId, certainRoleId, comment);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(certainRoleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "")]
    public void Init_WithoutEmployeeIdAndRoleId_ThrowsArgumentException(object? employeeId, object? roleId)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        Guid? certainEmployeeId = employeeId is null ? null : Guid.Empty;
        Guid? certainRoleId = roleId is null ? null : Guid.Empty;

        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, certainEmployeeId, certainRoleId, comment);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Init_EmployeeIdAndRoleIdDefined_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        var roleId = _fixture.Create<Guid>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

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
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name!, order, status, employeeId, roleId, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_TooLongName_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength + 1);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_NegativeOrder_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = -1;
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(order));
    }

    [Fact]
    public void Init_DefaultStatus_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = default(Status);
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(status));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_EmptyOrWhiteSpaceComment_ThrowsArgumentException(string comment)
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(comment));
    }

    [Fact]
    public void Init_TooLongComment_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength + 1);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(comment));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var workflowStepTemplate = _fixture.Create<WorkflowStepTemplate>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Assert
        workflowStep.Name.Should().Be(workflowStepTemplate.Name);
        workflowStep.Order.Should().Be(workflowStepTemplate.Order);
        workflowStep.Status.Should().Be(Status.Pending);
        workflowStep.EmployeeId.Should().Be(workflowStepTemplate.EmployeeId);
        workflowStep.RoleId.Should().Be(workflowStepTemplate.RoleId);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Create_NullWorkflowStepTemplate_ThrowsArgumentNullException()
    {
        // Arrange
        WorkflowStepTemplate workflowStepTemplate = null!;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => WorkflowStep.Create(workflowStepTemplate, comment);

        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(workflowStepTemplate));
    }

    [Fact]
    public void Approve_WithEmployeeId_SuccessApproved()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        var workflowStepTemplate = new WorkflowStepTemplate(name, order, employeeId: employee.Id);
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Act
        workflowStep.Approve(employee, comment);

        // Assert
        workflowStep.Status.Should().Be(Status.Approved);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Approve_WithRoleId_SuccessApproved()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        var workflowStepTemplate = new WorkflowStepTemplate(name, order, roleId: employee.RoleId);
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Act
        workflowStep.Approve(employee, comment);

        // Assert
        workflowStep.Status.Should().Be(Status.Approved);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Approve_EmployeeWithoutRights_ThrowsArgumentException()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var workflowStepTemplate = _fixture.Create<WorkflowStepTemplate>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Act
        var action = () => workflowStep.Approve(employee, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(employee));
    }

    [Fact]
    public void Reject_WithEmployeeId_SuccessRejected()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        var workflowStepTemplate = new WorkflowStepTemplate(name, order, employeeId: employee.Id);
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Act
        workflowStep.Reject(employee, comment);

        // Assert
        workflowStep.Status.Should().Be(Status.Rejected);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Reject_WithRoleId_SuccessRejected()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.Create<int>();
        var workflowStepTemplate = new WorkflowStepTemplate(name, order, roleId: employee.RoleId);
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Act
        workflowStep.Reject(employee, comment);

        // Assert
        workflowStep.Status.Should().Be(Status.Rejected);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Reject_EmployeeWithoutRights_ThrowsArgumentException()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var workflowStepTemplate = _fixture.Create<WorkflowStepTemplate>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);
        var workflowStep = WorkflowStep.Create(workflowStepTemplate, comment);

        // Act
        var action = () => workflowStep.Reject(employee, comment);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(employee));
    }

    [Fact]
    public void ToPending_SuccessChanged()
    {
        // Arrange
        var workflowStep = _fixture.Create<WorkflowStep>();

        // Act
        workflowStep.ToPending();

        // Assert
        workflowStep.Status.Should().Be(Status.Pending);
        workflowStep.Comment.Should().BeNull();
    }
}