using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities.Templates;

namespace Resumes.Domain.Tests.Entities.Templates;

public class WorkflowStepTemplateTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void WorkflowStepTemplate_Create_ShouldInitializeProperties()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var order = _fixture.Create<int>();
        var userId = _fixture.Create<Guid>();
        var roleId = _fixture.Create<Guid>();

        // Act
        var workflowStepTemplate = WorkflowStepTemplate.Create(name, order, userId, roleId);

        // Assert
        workflowStepTemplate.Name.Should().Be(name);
        workflowStepTemplate.Order.Should().Be(order);
        workflowStepTemplate.UserId.Should().Be(userId);
        workflowStepTemplate.RoleId.Should().Be(roleId);
    }
    
    [Fact]
    public void WorkflowStepTemplate_Create_WithNegativeOrder_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var userId = _fixture.Create<Guid>();
        var roleId = _fixture.Create<Guid>();
        var negativeOrder = _fixture.Create<int>() * -1;

        // Act
        Action act = () => WorkflowStepTemplate.Create(name, negativeOrder, userId, roleId);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}