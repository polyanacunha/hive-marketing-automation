using Hive.Domain.Validation;

namespace Hive.Domain.Entities;

public sealed class Post : Entity
{
    public string Url { get; private set; }
    public string Legenda { get; private set; }

    public Post(string legenda)
    {
        ValidateDomain(legenda);
    }

    public Post(int id, string legenda)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(legenda);
    }   

    public void Update(string legenda)
    {
        ValidateDomain(legenda);
    }

    private void ValidateDomain(string legenda)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(legenda),
            "Invalid! Post subtitle is required.");

        DomainExceptionValidation.When(legenda.Length < 3,
           "Invalid video subtitle, too short, minimum 3 characters.");

        Legenda = legenda;
    }
}
