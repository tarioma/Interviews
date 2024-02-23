﻿using AutoFixture;

namespace Interviews.Domain.Tests.Tools;

public static class FixtureExtension
{
    public static string GenerateString(this IFixture fixture, int length)
    {
        return new string(fixture.CreateMany<char>(length).ToArray());
    }
}