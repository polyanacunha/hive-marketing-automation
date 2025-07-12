using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Enum
{
    public enum StatusCampaign
    {
        ACTIVE,  
        PAUSED,  
        ARCHIVED,   
        DELETED,
        COMPLETED,   
        IN_REVIEW,   
        PENDING, 
        DISAPPROVED, 
        ERROR,  
        DRAFT,
    }
}
