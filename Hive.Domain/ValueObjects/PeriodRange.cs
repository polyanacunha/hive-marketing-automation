using Hive.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.ValueObjects
{
    public class PeriodRange
    {
        public DateTime Initial {  get; set; }
        public DateTime End { get; private set; }

        public PeriodRange(DateTime initial, DateTime end)
        {
            DomainExceptionValidation.When(initial < DateTime.UtcNow || end < DateTime.UtcNow, 
                "Período inválido: as datas devem ser maior à data e hora atuais.");

            DomainExceptionValidation.When(end < initial , "Período inválido: a data final não pode ser anterior à data inicial.");

            Initial = initial;
            End = end;
        }
    }
}
