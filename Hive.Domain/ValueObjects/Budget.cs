using Hive.Domain.Validation;


namespace Hive.Domain.ValueObjects
{
    public class Budget
    {
        public int Value { get;}
        public string Currency { get; }

        private Budget(int value) 
        {
            Value = value;
            Currency = "BRL";
        }

        private Budget() { }

        public static Budget Create(int value)
        {
            DomainExceptionValidation.When(value < 0,"O orçamento não pode ser negativo.");

            return new Budget(value);
        }
    }
}
