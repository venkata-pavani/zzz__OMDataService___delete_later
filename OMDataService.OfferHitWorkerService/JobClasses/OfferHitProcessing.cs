using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OMDataService.OfferHitWorkerService.JobClasses
{
    public class OfferHitProcessing : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await File.AppendAllLinesAsync(@"c:\temp\job1.txt", new[] { DateTime.Now.ToLongTimeString() });
        }
   
    }
}
