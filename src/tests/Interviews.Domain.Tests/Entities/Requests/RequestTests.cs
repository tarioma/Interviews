// using AutoFixture;
// using FluentAssertions;
// using Interviews.Domain.Entities;
// using Interviews.Domain.Entities.Employees;
// using Interviews.Domain.Entities.Requests;
// using Interviews.Domain.Tests.Tools;
//
// namespace Interviews.Domain.Tests.Entities.Requests;
//
// public class RequestTests
// {
//     private readonly Fixture _fixture;
//
//     public RequestTests()
//     {
//         _fixture = new Fixture();
//         _fixture.Customize(new CompositeCustomization(
//             new DocumentCustomization(),
//             new WorkflowStepCustomization(),
//             new WorkflowStepTemplateCustomization()
//         ));
//     }
//
//     [Fact]
//     public void Init_CorrectParams_SuccessInit()
//     {
//         // Arrange
//         var id = _fixture.Create<Guid>();
//         var document = _fixture.Create<Document>();
//         var workflow = _fixture.Create<Workflow>();
//         var employeeId = _fixture.Create<Guid>();
//
//         // Act
//         var request = new Request(id, document, workflow, employeeId);
//
//         // Assert
//         request.Id.Should().Be(id);
//         request.Document.Should().Be(document);
//         request.Workflow.Should().Be(workflow);
//         request.EmployeeId.Should().Be(employeeId);
//         request.Events.Should().NotBeEmpty();
//     }
// }