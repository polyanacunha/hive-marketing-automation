using Hive.Domain.Validation;

namespace Hive.Domain.Entities;

public sealed class Midia : Entity
{
    public string Duration { get; private set; }
    public string Resolution { get; private set; }
    public string AspectRatio { get; private set; }
    public string Format { get; private set; }
    public string Size { get; private set; }
    public string Url { get; private set; }

    public Midia(string url)
    {
        ValidateDomain(url);
    }

    public Midia(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(name);
    }   

    public void Update(string url)
    {
        ValidateDomain(url);
    }

    private void ValidateDomain(string url)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(url),
            "Invalid! Url is required.");

        DomainExceptionValidation.When(Url.Length < 3,
           "Invalid url, too short.");

        Url = url;
    }
}
