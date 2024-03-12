using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.WorkflowTemplates;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Requests;

public class WorkflowTests
{
    private readonly Fixture _fixture;

    public WorkflowTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new CompositeCustomization(
            new EmailAddressCustomization(),
            new WorkflowTemplateCustomization()
        ));
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var workflowTemplateId = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();

        // Act
        var workflow = new Workflow(workflowTemplateId, name, steps);

        // Assert
        workflow.WorkflowTemplateId.Should().Be(workflowTemplateId);
        workflow.Name.Should().Be(name.Trim());
        workflow.Steps.Should().Equal(steps);
    }

    [Fact]
    public void Init_EmptyGuidWorkflowTemplateId_ThrowsArgumentException()
    {
        // Arrange
        var workflowTemplateId = Guid.Empty;
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();

        // Act
        var action = () => new Workflow(workflowTemplateId, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(workflowTemplateId));
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string name)
    {
        // Arrange
        var workflowTemplateId = _fixture.Create<Guid>();
        var steps = _fixture.GenerateWorkflowSteps();

        // Act
        var action = () => new Workflow(workflowTemplateId, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_TooLongName_ThrowsArgumentException()
    {
        // Arrange
        var workflowTemplateId = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength + 1);
        var steps = _fixture.GenerateWorkflowSteps();

        // Act
        var action = () => new Workflow(workflowTemplateId, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Theory]
    [InlineData(null!)]
    [InlineData(default(HashSet<WorkflowStep>))]
    public void Init_NullOrEmptySteps_ThrowsArgumentException(IReadOnlyCollection<WorkflowStep> steps)
    {
        // Arrange
        var workflowTemplateId = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);

        // Act
        var action = () => new Workflow(workflowTemplateId, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(steps));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();

        // Act
        var workflow = Workflow.Create(workflowTemplate);

        // Assert
        workflow.WorkflowTemplateId.Should().Be(workflowTemplate.Id);
        workflow.Name.Should().Be(workflowTemplate.Name);
        workflow.Steps.Should().NotBeEmpty();
    }

    [Fact]
    public void IsApproved_CorrectReturn()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        foreach (var step in workflow.Steps)
        {
            var id = (Guid)step.EmployeeId!;
            var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
            var emailAddress = _fixture.Create<EmailAddress>();
            var roleId = _fixture.Create<Guid>();
            var employee = new Employee(id, employeeName, emailAddress, roleId);
            step.Approve(employee);
        }

        // Act
        var isApproved = workflow.IsApproved();

        // Assert
        isApproved.Should().BeTrue();
    }

    [Fact]
    public void IsRejected_CorrectReturn()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        var step = workflow.Steps.First();
        var id = (Guid)step.EmployeeId!;
        var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var employee = new Employee(id, employeeName, emailAddress, roleId);
        step.Reject(employee);

        // Act
        var isRejected = workflow.IsRejected();

        // Assert
        isRejected.Should().BeTrue();
    }

    [Fact]
    public void Approve_AllStepsApproved()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        // Act
        foreach (var step in workflow.Steps)
        {
            var id = (Guid)step.EmployeeId!;
            var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
            var emailAddress = _fixture.Create<EmailAddress>();
            var roleId = _fixture.Create<Guid>();
            var employee = new Employee(id, employeeName, emailAddress, roleId);
            workflow.Approve(employee);
        }
        var isApproved = workflow.Steps.All(s => s.Status == Status.Approved);

        // Assert
        isApproved.Should().BeTrue();
    }

    [Fact]
    public void Approve_AlreadyApproved_ThrowsException()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        foreach (var step in workflow.Steps)
        {
            var id = (Guid)step.EmployeeId!;
            var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
            var emailAddress = _fixture.Create<EmailAddress>();
            var roleId = _fixture.Create<Guid>();
            var employee = new Employee(id, employeeName, emailAddress, roleId);
            workflow.Approve(employee);
        }

        // Act & Assert
        {
            var step = workflow.Steps.First();
            var id = (Guid)step.EmployeeId!;
            var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
            var emailAddress = _fixture.Create<EmailAddress>();
            var roleId = _fixture.Create<Guid>();
            var employee = new Employee(id, employeeName, emailAddress, roleId);
            var action = () => workflow.Approve(employee);

            action.Should().Throw<Exception>();
        }
    }

    [Fact]
    public void Reject_AnyStepRejected()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        var step = workflow.Steps.First();
        var id = (Guid)step.EmployeeId!;
        var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var employee = new Employee(id, employeeName, emailAddress, roleId);

        // Act
        workflow.Reject(employee);
        var isRejected = workflow.Steps.Any(s => s.Status == Status.Rejected);

        // Assert
        isRejected.Should().BeTrue();
    }

    [Fact]
    public void Reject_AlreadyRejected_ThrowsException()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        var step = workflow.Steps.First();
        var id = (Guid)step.EmployeeId!;
        var employeeName = _fixture.GenerateString(Employee.MaxNameLength);
        var emailAddress = _fixture.Create<EmailAddress>();
        var roleId = _fixture.Create<Guid>();
        var employee = new Employee(id, employeeName, emailAddress, roleId);
        workflow.Reject(employee);

        // Act
        var action = () => workflow.Reject(employee);

        // Assert
        action.Should().Throw<Exception>();
    }

    [Fact]
    public void Restart_AllStepsWithStatusPending()
    {
        // Arrange
        var workflowTemplate = _fixture.Create<WorkflowTemplate>();
        var name = _fixture.GenerateString(Workflow.MaxNameLength);
        var steps = _fixture.GenerateWorkflowSteps();
        var workflow = new Workflow(workflowTemplate.Id, name, steps);

        // Act
        workflow.Restart();
        var isRestarted = workflow.Steps.All(s => s.Status == Status.Pending);

        // Assert
        isRestarted.Should().BeTrue();
    }
}