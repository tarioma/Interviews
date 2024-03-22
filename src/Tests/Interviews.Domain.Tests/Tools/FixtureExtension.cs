using AutoFixture;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Tests.Tools;

internal static class FixtureExtension
{
    internal static string GenerateString(this IFixture fixture, int length) =>
        new(fixture.CreateMany<char>(length).ToArray());

    internal static DateOnly GenerateDateOfBirth(this IFixture fixture, int age) =>
        DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-age));

    internal static HashSet<WorkflowStepTemplate> GenerateWorkflowStepTemplatesWithEmployeeId(
        this IFixture fixture, int count = 1)
    {
        var name = fixture.Create<string>();
        var employeeId = fixture.Create<Guid>();
        var roleId = Guid.Empty;

        return Enumerable.Range(0, count)
            .Select(order => new WorkflowStepTemplate(name, order, employeeId, roleId))
            .ToHashSet();
    }

    internal static HashSet<WorkflowStep> GenerateWorkflowStepsWithEmployeeId(this IFixture fixture, int count = 1) =>
        fixture.GenerateWorkflowStepTemplatesWithEmployeeId(count)
            .Select(wst => WorkflowStep.Create(wst))
            .ToHashSet();

    internal static Employee GenerateEmployeeWithId(this IFixture fixture, Guid id)
    {
        fixture.Customize(new EmailAddressCustomization());

        var name = fixture.Create<string>();
        var emailAddress = fixture.Create<EmailAddress>();
        var roleId = fixture.Create<Guid>();
        var authData = fixture.Create<AuthData>();

        return new Employee(id, name, emailAddress, roleId, authData);
    }

    internal static Workflow GenerateWorkflowWithStepsCount(this IFixture fixture, int count)
    {
        var workflowTemplateId = fixture.Create<Guid>();
        var name = fixture.Create<string>();
        var steps = fixture.GenerateWorkflowStepsWithEmployeeId(count);
        return new Workflow(workflowTemplateId, name, steps);
    }
}