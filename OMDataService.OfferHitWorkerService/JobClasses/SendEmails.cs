using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OMDataService.OfferHitWorkerService.Classes;
using OMSDataService.DataInterfaces;
using OMSDataService.DataRepositories;
using OMSDataService.EF;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDataService.OfferHitWorkerService.JobClasses
{
    [DisallowConcurrentExecution]
    public class SendEmails : IJob
    {
        private ApiContext _context;
        private readonly IMapper _mapper;

        private HelperRepository repo;

        public SendEmails()
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {

            _context = new ApiContext(Global.GetConnectionString());
            repo = new HelperRepository(_context, _mapper);
            try
            {
                var sendEmails = await repo.SendOfferEmailItems();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }

        }

    }

}



