using Hive.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Hive.Domain.Tests;

public class CampaingUnitTest1
{
    [Fact(DisplayName = "Create Campaing With Valid State")]
    public void CreateCampaing_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Campaing(1, "Campaing Name ");
        action.Should()
             .NotThrow<Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCampaing_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Campaing(-1, "Campaing Name ");
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>()
             .WithMessage("Invalid Id value.");
    }

    [Fact]
    public void CreateCampaing_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Campaing(1, "Ca");
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact]
    public void CreateCampaing_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Campaing(1, "");
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCampaing_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Campaing(1, null);
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>();
    }
}
