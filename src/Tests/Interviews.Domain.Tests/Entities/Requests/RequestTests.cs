using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.Requests.Events;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Requests;

public class RequestTests
{
    private readonly Fixture _fixture;

    public RequestTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new DocumentCustomization());
        _fixture.Customize(new WorkflowTemplateCustomization());
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var document = _fixture.Create<Document>();
        var workflow = _fixture.Create<Workflow>();
        var employeeId = _fixture.Create<Guid>();

        // Act
        var request = new Request(id, document, workflow, employeeId);

        // Assert
        request.Id.Should().Be(id);
        request.Document.Should().Be(document);
        request.Workflow.Should().Be(workflow);
        request.EmployeeId.Should().Be(employeeId);
        request.Events.Should().BeEmpty();
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        var document = _fixture.Create<Document>();
        var workflow = _fixture.Create<Workflow>();
        var employeeId = _fixture.Create<Guid>();

        // Act
        var action = () => new Request(id, document, workflow, employeeId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }

    [Fact]
    public void Init_NullDocument_ThrowsArgumentNullException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        Document document = null!;
        var workflow = _fixture.Create<Workflow>();
        var employeeId = _fixture.Create<Guid>();

        // Act
        var action = () => new Request(id, document, workflow, employeeId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(document));
    }

    [Fact]
    public void Init_NullWorkflow_ThrowsArgumentNullException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var document = _fixture.Create<Document>();
        Workflow workflow = null!;
        var employeeId = _fixture.Create<Guid>();

        // Act
        var action = () => new Request(id, document, workflow, employeeId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(workflow));
    }

    [Fact]
    public void Init_EmptyGuidRoleId_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var document = _fixture.Create<Document>();
        var workflow = _fixture.Create<Workflow>();
        var employeeId = Guid.Empty;

        // Act
        var action = () => new Request(id, document, workflow, employeeId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(employeeId));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var document = _fixture.Create<Document>();
        var workflow = _fixture.Create<Workflow>();
        var employeeId = _fixture.Create<Guid>();

        // Act
        var request = Request.Create(document, workflow, employeeId);

        // Assert
        request.Id.Should().NotBeEmpty();
        request.Document.Should().Be(document);
        request.Workflow.Should().Be(workflow);
        request.EmployeeId.Should().Be(employeeId);
        request.Events.Should().OnlyContain(e => e is RequestCreatedEvent);
    }

    [Fact]
    public void IsApproved_CorrectReturn()
    {
        // Arrange
        var request = _fixture.Create<Request>();
        var step = request.Workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        request.Approve(employee);

        // Act
        var isApproved = request.IsApproved();

        // Assert
        isApproved.Should().BeTrue();
    }

    [Fact]
    public void IsRejected_CorrectReturn()
    {
        // Arrange
        var request = _fixture.Create<Request>();
        var step = request.Workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        request.Reject(employee);

        // Act
        var isApproved = request.IsRejected();

        // Assert
        isApproved.Should().BeTrue();
    }

    [Fact]
    public void Approve_OneStep_AllStepsApprovedAndRequestApprovedEventAdded()
    {
        // Arrange
        var request = _fixture.Create<Request>();
        var step = request.Workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        var comment = _fixture.Create<string>();

        // Act
        request.Approve(employee, comment);

        // Assert
        request.Workflow.Steps
            .Should()
            .Contain(s => s.Status == Status.Approved && s.Comment == comment);
        request.Events.Should().Contain(e => e is RequestApprovedEvent);
    }

    [Fact]
    public void Approve_MoreThenOneStep_AllStepsApprovedAndRequestNextStepEventAdded()
    {
        // Arrange
        var workflow = _fixture.GenerateWorkflowWithStepsCount(2);
        var document = _fixture.Create<Document>();
        var employeeId = _fixture.Create<Guid>();
        var request = Request.Create(document, workflow, employeeId);
        var step = request.Workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        var comment = _fixture.Create<string>();

        // Act
        request.Approve(employee, comment);

        // Assert
        request.Workflow.Steps
            .Should()
            .Contain(s => s.Status == Status.Approved && s.Comment == comment);
        request.Events.Should().Contain(e => e is RequestNextStepEvent);
    }

    [Fact]
    public void Reject_AnyStepRejectedAndEventAdded()
    {
        // Arrange
        var request = _fixture.Create<Request>();
        var step = request.Workflow.Steps.First();
        var employee = _fixture.GenerateEmployeeWithId((Guid)step.EmployeeId!);
        var comment = _fixture.Create<string>();

        // Act
        request.Reject(employee, comment);

        // Assert
        request.Workflow.Steps
            .Should()
            .Contain(s => s.Status == Status.Rejected && s.Comment == comment);
        request.Events.Should().Contain(e => e is RequestRejectedEvent);
    }

    [Fact]
    public void Restart_AllStepsWithStatusPending()
    {
        // Arrange
        var request = _fixture.Create<Request>();
        var document = _fixture.Create<Document>();

        // Act
        request.Restart(document);

        // Assert
        request.Workflow.Steps
            .Should()
            .OnlyContain(step => step.Status == Status.Pending && step.Comment == null);
        request.Events.Should().Contain(e => e is RequestRestartedEvent);
    }
}