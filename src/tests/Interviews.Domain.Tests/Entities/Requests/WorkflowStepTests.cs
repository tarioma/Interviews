using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Requests;

public class WorkflowStepTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var name = _fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = _fixture.Create<int>();
        var employeeId = _fixture.Create<Guid>();
        var roleId = _fixture.Create<Guid>();
        var status = Status.Pending;
        var comment = _fixture.Create<string?>();
        
        // Act
        var workflowStep = new WorkflowStep(name, order, employeeId, roleId, status, comment);
        
        // Assert
        workflowStep.Name.Should().Be(name);
        workflowStep.Order.Should().Be(order);
        workflowStep.EmployeeId.Should().Be(employeeId);
        workflowStep.RoleId.Should().Be(roleId);
        workflowStep.Status.Should().Be(status);
        workflowStep.Comment.Should().Be(comment);
    }
}