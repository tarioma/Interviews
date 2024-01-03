using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities;
using Resumes.Domain.Entities.Requests;

namespace Resumes.Domain.Tests.Entities.Requests;

public class RequestTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Request_Create_ShouldInitializeProperties()
    {
        // Arrange
        var userName = _fixture.Create<string>();
        var userGeneratedEmailAddress = _fixture.Create<string>() + "@example.com";
        var userEmailAddress = new EmailAddress(userGeneratedEmailAddress);
        var userRoleId = _fixture.Create<Guid>();
        
        var documentName = _fixture.Create<string>();
        var documentGeneratedEmailAddress = _fixture.Create<string>() + "@example.com";
        var documentEmailAddress = new EmailAddress(documentGeneratedEmailAddress);
        var documentBirthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
        var documentExperience = 1;
        
        var workflowName = _fixture.Create<string>();
        var workflowTemplateId = _fixture.Create<Guid>();
        
        // Act
        var user = User.Create(userName, userEmailAddress, userRoleId);
        var document = Document.Create(documentName, documentEmailAddress, documentBirthday, documentExperience);
        var workflow = Workflow.Create(workflowName, workflowTemplateId);

        var request = Request.Create(user, document, workflow);

        // Assert
        request.Id.Should().NotBeEmpty();
        request.User.Should().Be(user);
        request.Document.Should().Be(document);
        request.Workflow.Should().Be(workflow);
        request.Events.Should().BeEmpty();
    }
}