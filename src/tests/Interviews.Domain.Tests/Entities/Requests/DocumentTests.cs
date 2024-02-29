using AutoFixture;
using FluentAssertions;
using Interviews.Domain.Entities;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Tests.Tools;

namespace Interviews.Domain.Tests.Entities.Requests;

public class DocumentTests
{
    private readonly Fixture _fixture;

    public DocumentTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new EmailAddressCustomization());
    }
    
    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Arrange
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-Document.MinAcceptableAge));
        var emailAddress = _fixture.Create<EmailAddress>();
        
        // Act
        var document = new Document(name, dateOfBirth, emailAddress);
        
        // Assert
        document.Name.Should().Be(name);
        document.DateOfBirth.Should().Be(dateOfBirth);
        document.EmailAddress.Should().Be(emailAddress);
    }
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string name)
    {
        // Arrange
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-Document.MinAcceptableAge));
        var emailAddress = _fixture.Create<EmailAddress>();
        
        // Act
        var action = () => new Document(name, dateOfBirth, emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }

    [Fact]
    public void Init_VeryLongName_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(Document.MaxNameLength + 1);
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-Document.MinAcceptableAge));
        var emailAddress = _fixture.Create<EmailAddress>();

        // Act
        var action = () => new Document(name, dateOfBirth, emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Init_DefaultDateOfBirth_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var dateOfBirth = default(DateOnly);
        var emailAddress = _fixture.Create<EmailAddress>();

        // Act
        var action = () => new Document(name, dateOfBirth, emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(dateOfBirth));
    }
    
    [Fact]
    public void Init_DateOfBirthIsTooLate_ThrowsArgumentException()
    {
        // Arrange
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-Document.MinAcceptableAge + 1));
        var emailAddress = _fixture.Create<EmailAddress>();

        // Act
        var action = () => new Document(name, dateOfBirth, emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(dateOfBirth));
    }
    
    [Fact]
    public void Init_NullEmailAddress_ThrowsArgumentNullException()
    {
        // Arrange
        var name = _fixture.GenerateString(Employee.MaxNameLength);
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-Document.MinAcceptableAge));
        EmailAddress emailAddress = null!;

        // Act
        var action = () => new Document(name, dateOfBirth, emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(emailAddress));
    }
}