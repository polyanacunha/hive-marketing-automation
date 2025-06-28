using Hive.Domain.Validation;

namespace Hive.Domain.Entities
{
    public class TargetAudience : Entity
    {
        public string Description { get; set; }
        private readonly List<ClientProfile> _clientProfiles = new();
        public IReadOnlyCollection<ClientProfile> ClientProfiles => _clientProfiles.AsReadOnly();

        private TargetAudience(){}
        public TargetAudience(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required");
            Description = description;
        }
    }
}
