using AutoFixture;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Tests.Tools;

public class WorkflowTemplateCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var id = fixture.Create<Guid>();
        var name = fixture.GenerateString(WorkflowTemplate.MaxNameLength);
        var steps = fixture.GenerateWorkflowStepTemplates();

        fixture.Customize<WorkflowTemplate>(composer =>
            composer.FromFactory(() =>
                new WorkflowTemplate(id, name, steps)));
    }
}