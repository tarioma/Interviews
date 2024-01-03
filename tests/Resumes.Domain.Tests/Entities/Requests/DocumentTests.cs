using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities;
using Resumes.Domain.Entities.Requests;

namespace Resumes.Domain.Tests.Entities.Requests;

public class DocumentTests
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public void Document_Create_ShouldInitializeProperties()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
            var emailAddress = new EmailAddress(generatedEmailAddress);
            var birthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
            var experience = 1;

            // Act
            var document = Document.Create(name, emailAddress, birthday, experience);

            // Assert
            document.Name.Should().Be(name);
            document.EmailAddress.Should().Be(emailAddress);
            document.Birthday.Should().Be(birthday);
            document.Experience.Should().Be(experience);
        }
        
        [Fact]
        public void Document_SetName_WithValidName_ShouldUpdateName()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var newName = _fixture.Create<string>();
            var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
            var emailAddress = new EmailAddress(generatedEmailAddress);
            var birthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
            var experience = 1;

            // Act
            var document = Document.Create(name, emailAddress, birthday, experience);
            document.SetName(newName);

            // Assert
            document.Name.Should().Be(newName);
        }
        
        [Fact]
        public void Document_SetEmailAddress_WithValidEmailAddress_ShouldUpdateEmailAddress()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
            var emailAddress = new EmailAddress(generatedEmailAddress);
            var newGeneratedEmailAddress = _fixture.Create<string>() + "@example.com";
            var newEmailAddress = new EmailAddress(newGeneratedEmailAddress);
            var birthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
            var experience = 1;

            // Act
            var document = Document.Create(name, emailAddress, birthday, experience);
            document.SetEmailAddress(newEmailAddress);

            // Assert
            document.EmailAddress.Should().Be(newEmailAddress);
        }
        
        [Fact]
        public void Document_SetBirthday_WithValidBirthday_ShouldUpdateBirthday()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
            var emailAddress = new EmailAddress(generatedEmailAddress);
            var birthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
            var newBirthday = new DateOnly(DateTime.UtcNow.Year - 51, 1, 1);
            var experience = 1;

            // Act
            var document = Document.Create(name, emailAddress, birthday, experience);
            document.SetBirthday(newBirthday);

            // Assert
            document.Birthday.Should().Be(newBirthday);
        }
        
        [Fact]
        public void Document_SetExperience_WithValidExperience_ShouldUpdateExperience()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var generatedEmailAddress = _fixture.Create<string>() + "@example.com";
            var emailAddress = new EmailAddress(generatedEmailAddress);
            var birthday = new DateOnly(DateTime.UtcNow.Year - 50, 1, 1);
            var experience = 1;
            var newExperience = 2;

            // Act
            var document = Document.Create(name, emailAddress, birthday, experience);
            document.SetExperience(newExperience);

            // Assert
            document.Experience.Should().Be(newExperience);
        }
    }