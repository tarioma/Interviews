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
    private readonly string _name;
    private readonly DateOnly _dateOfBirth;
    private readonly EmailAddress _emailAddress;

    public DocumentTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new EmailAddressCustomization());

        _name = _fixture.GenerateString(Employee.MaxNameLength);
        _dateOfBirth = DateOnly.FromDateTime(
            DateTime.UtcNow.AddYears(-Document.MaxNameLength));
        _emailAddress = _fixture.Create<EmailAddress>();
    }
    
    [Fact]
    public void Init_CorrectParams_SuccessInit()
    {
        // Act
        var document = new Document(_name, _dateOfBirth, _emailAddress);
        
        // Assert
        document.Name.Should().Be(_name);
        document.DateOfBirth.Should().Be(_dateOfBirth);
        document.EmailAddress.Should().Be(_emailAddress);
    }
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    public void Init_NullEmptyOrWhiteSpaceName_ThrowsArgumentException(string name)
    {
        // Act
        var action = () => new Document(name, _dateOfBirth, _emailAddress);

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

        // Act
        var action = () => new Document(name, _dateOfBirth, _emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(name));
    }
    
    [Fact]
    public void Init_DefaultDateOfBirth_ThrowsArgumentException()
    {
        // Arrange
        var dateOfBirth = default(DateOnly);

        // Act
        var action = () => new Document(_name, dateOfBirth, _emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(dateOfBirth));
    }
    
    [Fact]
    public void Init_DateOfBirthIsTooLate_ThrowsArgumentException()
    {
        // Arrange
        const int invalidAge = Document.MinAcceptableAge - 1;
        var invalidDateOfBirth = DateTime.UtcNow.AddYears(-invalidAge);
        var dateOfBirth = DateOnly.FromDateTime(invalidDateOfBirth);

        // Act
        var action = () => new Document(_name, dateOfBirth, _emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(dateOfBirth));
    }
    
    [Fact]
    public void Init_NullEmailAddress_ThrowsArgumentNullException()
    {
        // Arrange
        EmailAddress emailAddress = null!;

        // Act
        var action = () => new Document(_name, _dateOfBirth, emailAddress);

        // Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(emailAddress));
    }
}