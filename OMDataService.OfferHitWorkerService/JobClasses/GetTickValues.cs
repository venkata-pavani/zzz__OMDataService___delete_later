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

using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OMDataService.OfferHitWorkerService.JobClasses
{
    [DisallowConcurrentExecution]
    public class GetTickValues : IJob
    {
        //private IHelperRepository _repo;
        private ApiContext _context;
        private readonly IMapper _mapper;
        //private OMSDataService.EF.ApiContext context;

        private HelperRepository repo;


        //public GetTickValues(IHelperRepository repo, ILogger<Worker> logger)
        //{
        //    _repo = repo;
        //    _logger = logger;
        //}
        public GetTickValues()
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            
            _context = new ApiContext(Global.GetConnectionString());
            repo = new HelperRepository(_context, _mapper);
            try
            {
                var getTicks = await repo.GetTicksForOffers();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }

        }

        
        
    }
}
