using Hive.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Hive.Domain.Tests;

public class MidiaUnitTest1
{
    [Fact]
    public void CreateMidia_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Midia("https://videourl.com");
        action.Should()
            .NotThrow<Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateMidia_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Midia(-1,"https://videourl.com");

        action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value.");
    }

    [Fact]
    public void CreateMidia_WithNullImageName_NoDomainException()
    {
        Action action = () => new Midia("");
        action.Should().Throw<Domain.Validation.DomainExceptionValidation>();
    }

}
