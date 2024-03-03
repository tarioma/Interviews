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
        _fixture.Customize(new CompositeCustomization(
            new WorkflowStepCustomization(),
            new WorkflowStepTemplateCustomization()
        ));
    }

    [Fact]
    public void Init_CorrectParamsAndNullEmployeeId_SuccessInit()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        Guid? employeeId = null;
        var roleId = _fixture.Create<Guid>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(roleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Init_CorrectParamsAndEmptyGuidEmployeeId_SuccessInit()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        Guid? employeeId = Guid.Empty;
        var roleId = _fixture.Create<Guid>();
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(roleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Init_CorrectParamsAndNullRoleId_SuccessInit()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = null;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(roleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Init_CorrectParamsAndEmptyGuidRoleId_SuccessInit()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = _fixture.Create<Guid>();
        Guid? roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var workflowStep = new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(roleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }

    [Fact]
    public void Init_NullEmployeeIdAndNullRoleId_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        Guid? employeeId = null;
        Guid? roleId = null;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Init_EmptyGuidEmployeeIdAndNullRoleId_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = Guid.Empty;
        Guid? roleId = null;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Init_NullEmployeeIdAndEmptyGuidRoleId_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        Guid? employeeId = null;
        var roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Init_EmptyGuidEmployeeIdAndEmptyGuidRoleId_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var status = Status.Pending;
        var employeeId = Guid.Empty;
        var roleId = Guid.Empty;
        var comment = _fixture.GenerateString(WorkflowStep.MaxCommentLength);

        // Act
        var action = () => new WorkflowStep(name, order, status, employeeId, roleId, comment);

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
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string name)
    {
        // Arrange
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
    public void Init_VeryLongName_ThrowsArgumentException()
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
    public void Create_NullWorkflowStepTemplate_ThrowArgumentNullException()
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
    public void Approve_ByEmployeeId_SuccessApproved()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.GenerateNonNegativeNumber();
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
    public void Approve_ByRoleId_SuccessApproved()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.GenerateNonNegativeNumber();
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
    public void Reject_ByEmployeeId_SuccessRejected()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.GenerateNonNegativeNumber();
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
    public void Reject_ByRoleId_SuccessRejected()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var name = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
        var order = _fixture.GenerateNonNegativeNumber();
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
    public void ToPending_Success()
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