﻿using AutoFixture;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Tests.Tools;

public class WorkflowStepTemplateCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize(new EmailAddressCustomization());

        var name = fixture.Create<string>();
        var order = fixture.Create<int>();
        var employeeId = fixture.Create<Guid>();
        var roleId = Guid.Empty;

        fixture.Customize<WorkflowStepTemplate>(composer =>
            composer.FromFactory(() =>
                new WorkflowStepTemplate(name, order, employeeId, roleId)));
    }
}