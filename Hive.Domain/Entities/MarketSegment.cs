
using Hive.Domain.Validation;

namespace Hive.Domain.Entities
{
    public class MarketSegment : Entity
    {
        public string Description { get; private set; }
        private readonly List<ClientProfile> _clientProfiles = new();
        public IReadOnlyCollection<ClientProfile> ClientProfiles => _clientProfiles.AsReadOnly();
        private MarketSegment() { }
        public MarketSegment(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required");
            Description = description;
        }
    }
}
