using System.Security.AccessControl;
using Hive.Domain.Validation;

namespace Hive.Domain.Entities;

public sealed class Campaing : Entity
{
    public string Name { get; private set; }
    public int UsuarioId { get; private set; }

    public string CampaingType { get; private set; }

    public string Message { get; private set; }

    public double Budget { get; private set; }

    public DateTime InitialDate { get; private set; }

    public DateTime FinalDate { get; private set; }

    public string CampaingStatus { get; private set; }

    public string TargetPublic { get; private set; }


    public Campaing(string name)
    {
        ValidateDomain(name);
    }

    public Campaing(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(name);
    }   

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(name.Length < 3,
           "Invalid name, too short, minimum 3 characters");

        Name = name;
    }
}
