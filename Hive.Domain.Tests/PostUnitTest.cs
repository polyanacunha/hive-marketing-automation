using Hive.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Hive.Domain.Tests;

public class PostUnitTest1
{
    [Fact(DisplayName = "Create Post With Valid State")]
    public void CreatePost_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Post(1, "Post subtitle ");
        action.Should()
             .NotThrow<Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreatePost_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Post(-1, "Post subtitle ");
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>()
             .WithMessage("Invalid Id value.");
    }

    [Fact]
    public void CreatePost_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Post(1, "su");
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid subtitle, too short, minimum 3 characters");
    }

    [Fact]
    public void CreatePost_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Post(1, "");
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid subtitle. subtitle is required");
    }

    [Fact]
    public void CreatePost_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Post(1, null);
        action.Should()
            .Throw<Domain.Validation.DomainExceptionValidation>();
    }
}
