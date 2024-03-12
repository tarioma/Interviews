using AutoFixture;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Tests.Tools;

internal static class FixtureExtension
{
    internal static string GenerateString(this IFixture fixture, int length) =>
        new(fixture.CreateMany<char>(length).ToArray());

    internal static DateOnly GenerateDateOfBirth(this IFixture fixture, int age) =>
        DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-age));

    internal static int GenerateNonNegativeNumber(this IFixture fixture) =>
        Math.Abs(fixture.Create<int>());

    internal static IReadOnlyCollection<WorkflowStepTemplate> GenerateWorkflowStepTemplates(this IFixture fixture) =>
        Enumerable.Range(0, 10).Select(order =>
        {
            var name = fixture.GenerateString(WorkflowStepTemplate.MaxNameLength);
            var employeeId = fixture.Create<Guid>();
            var roleId = Guid.Empty;

            return new WorkflowStepTemplate(name, order, employeeId, roleId);
        }).ToHashSet();
}