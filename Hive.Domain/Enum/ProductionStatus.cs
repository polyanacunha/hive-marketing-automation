using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Enum
{
    public enum ProductionStatus
    {
        Pending, 
        ScriptGenerated, 
        ClipsGenerating, 
        Completed, 
        Failed
    }
}
