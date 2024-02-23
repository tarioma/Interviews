﻿using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using GuardClauses;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.Employees;

public class Employee
{
    public const int MaxNameLength = 100;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public Guid RoleId { get; private set; }

    public Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        Guard.Against.GuidIsEmpty(id);

        Id = id;
        SetName(name);
        SetEmailAddress(emailAddress);
        SetRoleId(roleId);
    }
    
    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();
        
        return new Employee(id, name, emailAddress, roleId);
    }

    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        
        Name = name.Trim();
    }
    
    [MemberNotNull(nameof(EmailAddress))]
    private void SetEmailAddress(EmailAddress emailAddress)
    {
        Guard.Against.Null(emailAddress);
        
        EmailAddress = emailAddress;
    }
    
    private void SetRoleId(Guid roleId)
    {
        Guard.Against.GuidIsEmpty(roleId);
        
        RoleId = roleId;
    }
}