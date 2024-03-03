using AutoFixture;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Tests.Tools;

public class WorkflowStepCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var name = fixture.GenerateString(WorkflowStep.MaxNameLength);
        var order = fixture.GenerateNonNegativeNumber();
        var status = Status.Pending;
        var employeeId = fixture.Create<Guid>();
        var roleId = Guid.Empty;
        var comment = fixture.GenerateString(WorkflowStep.MaxCommentLength);

        fixture.Customize<WorkflowStep>(composer =>
            composer.FromFactory(() =>
                new WorkflowStep(name, order, status, employeeId, roleId, comment)));
    }
}