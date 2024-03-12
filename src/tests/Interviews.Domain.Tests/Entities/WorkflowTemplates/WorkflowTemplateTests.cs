using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.WorkflowTemplates;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.WorkflowTemplates;

public class WorkflowTemplateTests
{
    private readonly Fixture _fixture;

    public WorkflowTemplateTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new CompositeCustomization(
            new WorkflowStepTemplateCustomization(),
            new DocumentCustomization()
        ));
    }

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength);
        var steps = _fixture.GenerateWorkflowStepTemplates();

        // Act
        var workflowStep = new WorkflowTemplate(id, name, steps);

        // Assert
        workflowStep.Id.Should().Be(id);
        workflowStep.Name.Should().Be(name.Trim());
        workflowStep.Steps.Should().Equal(steps);
    }

    [Fact]
    public void Init_EmptyGuidId_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.Empty;
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength);
        var steps = _fixture.GenerateWorkflowStepTemplates();

        // Act
        var action = () => new WorkflowTemplate(id, name, steps);

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
        // Arrange
        var id = _fixture.Create<Guid>();
        var steps = _fixture.GenerateWorkflowStepTemplates();

        // Act
        var action = () => new WorkflowTemplate(id, name, steps);

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
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength + 1);
        var steps = _fixture.GenerateWorkflowStepTemplates();

        // Act
        var action = () => new WorkflowTemplate(id, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Theory]
    [InlineData(null!)]
    [InlineData(default(HashSet<WorkflowStepTemplate>))]
    public void Init_NullOrEmptySteps_ThrowsArgumentException(IReadOnlyCollection<WorkflowStepTemplate> steps)
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength);

        // Act
        var action = () => new WorkflowTemplate(id, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(steps));
    }

    [Fact]
    public void Init_InvalidSteps_ThrowsArgumentException()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength);
        var steps = new[] { 1, 0, 4, 2 }.Select(order =>
        {
            var stepName = _fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
            var employeeId = _fixture.Create<Guid>();
            var roleId = Guid.Empty;

            return new WorkflowStepTemplate(stepName, order, employeeId, roleId);
        }).ToHashSet();

        // Act
        var action = () => new WorkflowTemplate(id, name, steps);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(steps));
    }

    [Fact]
    public void Create_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength);
        var steps = _fixture.GenerateWorkflowStepTemplates();

        // Act
        var workflowStep = WorkflowTemplate.Create(name, steps);

        // Assert
        workflowStep.Id.Should().NotBeEmpty();
        workflowStep.Name.Should().Be(name.Trim());
        workflowStep.Steps.Should().Equal(steps);
    }

    [Fact]
    public void CreateRequest_CorrectParams_SuccessCreateAndReturn()
    {
        // Arrange
        var employee = _fixture.Create<Employee>();
        var document = _fixture.Create<Document>();
        var id = _fixture.Create<Guid>();
        var name = _fixture.GenerateString(WorkflowTemplate.MaxNameLength);
        var steps = _fixture.GenerateWorkflowStepTemplates();
        var workflowTemplate = new WorkflowTemplate(id, name, steps);

        // Act
        var request = workflowTemplate.CreateRequest(employee, document);

        // Assert
        request.Id.Should().NotBeEmpty();
        request.Document.Should().Be(document);
        request.Workflow.Should().NotBeNull();
        request.EmployeeId.Should().Be(employee.Id);
        request.Events.Should().NotBeNull();
    }
}