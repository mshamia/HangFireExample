using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireBL
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        void ReccuringJob();
        void DelayedJob();
        void ContinuationJob();
    }
}
