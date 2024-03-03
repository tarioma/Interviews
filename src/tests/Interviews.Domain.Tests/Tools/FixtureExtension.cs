using AutoFixture;

namespace Interviews.Domain.Tests.Tools;

public static class FixtureExtension
{
    public static string GenerateString(this IFixture fixture, int length) =>
        new(fixture.CreateMany<char>(length).ToArray());

    public static DateOnly GenerateDateOfBirth(this IFixture fixture, int age) =>
        DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-age));

    public static int GenerateNonNegativeNumber(this IFixture fixture) =>
        Math.Abs(fixture.Create<int>());
}