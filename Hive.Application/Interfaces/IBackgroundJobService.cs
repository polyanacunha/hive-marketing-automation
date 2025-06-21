using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IBackgroundJobService
    {
        void EnqueueVideoProcessing(int jobId);
    }
}
