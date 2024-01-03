using System.ComponentModel.DataAnnotations;
using AutoFixture;
using FluentAssertions;
using Resumes.Domain.Entities;

namespace Resumes.Domain.Tests.Entities
{
    public class EmailAddressTests
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public void EmailAddress_WithValidValue_ShouldNotThrowException()
        {
            // Arrange
            var validEmail = _fixture.Create<string>() + "@example.com";

            // Act
            Action act = () => _ = new EmailAddress(validEmail);

            // Assert
            act.Should().NotThrow<ValidationException>();
        }

        [Fact]
        public void EmailAddress_WithInvalidValue_ShouldThrowValidationException()
        {
            // Arrange
            var invalidEmail = _fixture.Create<string>();

            // Act
            Action act = () => _ = new EmailAddress(invalidEmail);

            // Assert
            act.Should().Throw<ValidationException>().WithMessage("Email-адрес невалиден");
        }
    }
}