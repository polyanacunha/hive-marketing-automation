using Hive.Domain.Validation;

namespace Hive.Domain.Entities
{
    public class TargetAudience : Entity
    {
        public string Description { get; set; }

        private TargetAudience(){}
        public TargetAudience(string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required");
            Description = description;
        }
    }
}
