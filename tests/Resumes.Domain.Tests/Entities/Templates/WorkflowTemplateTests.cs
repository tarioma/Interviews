using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities;
using Resumes.Domain.Entities.Requests;
using Resumes.Domain.Entities.Templates;

namespace Resumes.Domain.Tests.Entities.Templates;

public class WorkflowTemplateTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void WorkflowTemplate_Create_ShouldInitializeProperties()
    {
        // Arrange
        var workflowTemplateName = _fixture.Create<string>();

        // Act
        var workflowTemplate = WorkflowTemplate.Create(workflowTemplateName);

        // Assert
        workflowTemplate.Id.Should().NotBe(Guid.Empty);
        workflowTemplate.Name.Should().Be(workflowTemplateName);
        workflowTemplate.Steps.Should().NotBeNull();
        workflowTemplate.Steps.Should().BeEmpty();
    }

    [Fact]
    public void WorkflowTemplate_SetName_WithValidName_ShouldUpdateName()
    {
        // Arrange
        var workflowTemplateName = _fixture.Create<string>();
        var newWorkflowTemplateName = _fixture.Create<string>();

        // Act
        var workflowTemplate = WorkflowTemplate.Create(workflowTemplateName);
        workflowTemplate.SetName(newWorkflowTemplateName);

        // Assert
        workflowTemplate.Name.Should().Be(newWorkflowTemplateName);
    }
    
    [Fact]
    public void WorkflowTemplate_CreateRequest_ShouldCreateRequestWithCorrectWorkflowNameAndId()
    {
        // Arrange
        var workflowTemplateName = _fixture.Create<string>();
        
        var userName = _fixture.Create<string>();
        var userGeneratedEmailAddress = _fixture.Create<string>() + "@example.com";
        var userEmailAddress = new EmailAddress(userGeneratedEmailAddress);
        var userRoleId = _fixture.Create<Guid>();
        
        var documentName = _fixture.Create<string>();
        var documentGeneratedEmailAddress = _fixture.Create<string>() + "@example.com";
        var documentEmailAddress = new EmailAddress(documentGeneratedEmailAddress);
        var documentBirthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
        var documentExperience = 1;

        // Act
        var workflowTemplate = WorkflowTemplate.Create(workflowTemplateName);
        var user = User.Create(userName, userEmailAddress, userRoleId);
        var document = Document.Create(documentName, documentEmailAddress, documentBirthday, documentExperience);
        var request = workflowTemplate.CreateRequest(user, document);

        // Assert
        request.User.Should().Be(user);
        request.Document.Should().Be(document);
    }
}