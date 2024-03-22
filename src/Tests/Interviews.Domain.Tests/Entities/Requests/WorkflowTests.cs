using AutoFixture;
using FluentAssertions;
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
        _fixture.Customize(new WorkflowTemplateCustomization());
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var workflowTemplateId = _fixture.Create<Guid>();
        var name = _fixture.Create<string>();
        var steps = _fixture.GenerateWorkflowStepsWithEmployeeId();

        // Act
        var workflow = new Workflow(workflowTemplateId, name, steps);

        // Assert
        workflow.WorkflowTemplateId.Should().Be(workflowTemplateId);
        workflow.Name.Should().Be(name);
        workflow.Steps.Should().Equal(steps);
    }

    [Fact]
    public void Init_EmptyGuidWorkflowTemplateId_ThrowsArgumentException()
    {
        // Arrange
        var workflowTemplateId = Guid.Empty;
        var name = _fixture.Create<string>();
        var steps = _fixture.GenerateWorkflowStepsWithEmployeeId();

        // Act
        var action = () => new Workflow(workflowTemplateId, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(workflowTemplateId));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string? name)
    {
        // Arrange
        var workflowTemplateId = _fixture.Create<Guid>();
        var steps = _fixture.GenerateWorkflowStepsWithEmployeeId();

        // Act
        var action = () => new Workflow(workflowTemplateId, name!, steps);

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
        var steps = _fixture.GenerateWorkflowStepsWithEmployeeId();

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
        var name = _fixture.Create<string>();

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
        var workflow = _fixture.Create<Workflow>();
        var step = workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        step.Approve(employee);

        // Act
        var isApproved = workflow.IsApproved();

        // Assert
        isApproved.Should().BeTrue();
    }

    [Fact]
    public void IsRejected_CorrectReturn()
    {
        // Arrange
        var workflow = _fixture.Create<Workflow>();
        var step = workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
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
        var workflow = _fixture.Create<Workflow>();
        var step = workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        var comment = _fixture.Create<string>();

        // Act
        workflow.Approve(employee, comment);

        // Assert
        workflow.Steps
            .Should()
            .Contain(s => s.Status == Status.Approved && s.Comment == comment);
    }

    [Fact]
    public void Approve_AlreadyApproved_ThrowsException()
    {
        // Arrange
        var workflow = _fixture.Create<Workflow>();
        var step = workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        workflow.Approve(employee);

        // Act
        var action = () => workflow.Approve(employee);

        // Assert
        action.Should().Throw<Exception>();
    }

    [Fact]
    public void Reject_AnyStepRejected()
    {
        // Arrange
        var workflow = _fixture.Create<Workflow>();
        var step = workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        var comment = _fixture.Create<string>();

        // Act
        workflow.Reject(employee, comment);

        // Assert
        workflow.Steps
            .Should()
            .Contain(s => s.Status == Status.Rejected && s.Comment == comment);
    }

    [Fact]
    public void Reject_AlreadyRejected_ThrowsException()
    {
        // Arrange
        var workflow = _fixture.Create<Workflow>();
        var step = workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
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
        var workflow = _fixture.Create<Workflow>();

        // Act
        workflow.Restart();

        // Assert
        workflow.Steps
            .Should()
            .OnlyContain(step => step.Status == Status.Pending && step.Comment == null);
    }
}