using AutoFixture;

namespace Interviews.Domain.Tests.Tools;

public static class FixtureExtension
{
    public static string GenerateString(this IFixture fixture, int length) =>
        new(fixture.CreateMany<char>(length).ToArray());
}