using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OMDataService.OfferHitWorkerService.JobClasses;
using OMSDataService.DataInterfaces;
using Quartz;
using Quartz.Impl;

namespace OMDataService.OfferHitWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private StdSchedulerFactory _schedulerFactory;
        private CancellationToken _stopppingToken;
        private IScheduler _scheduler;
        private IHelperRepository _helper;


        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // DO YOUR STUFF HERE
            //await base.StartAsync(cancellationToken);
            await StartJobs();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // DO YOUR STUFF HERE
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stopppingToken = stoppingToken;
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        protected async Task StartJobs()
        {
            _schedulerFactory = new StdSchedulerFactory();

            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();


            //var jobOfferHit = JobBuilder.Create<JobClasses.GetTickValues>()
            //.WithIdentity("jobOfferHit")
            //.Build();

            //var triggerOfferHit = TriggerBuilder.Create()
            //       .WithIdentity("hitOfferTrigger")
            //       .StartNow()
            //       .ForJob("jobOfferHit")
            //       .WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForever(2))
            //        //.WithSchedule(CronScheduleBuilder.CronSchedule("0 0/2 17-18 * * ?"))  //cronSchedule(""))
            //       //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(15, 10)) // execute job daily at 3:00
            //       //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(19, 16)) // execute job daily at 3:00
            //       .Build();

            //_scheduler.ScheduleJob(jobOfferHit, triggerOfferHit);

            IJobDetail tickValueJob = JobBuilder.Create<GetTickValues>()
                .WithIdentity("GetTickValueJob", "group")
                .Build();

            ITrigger tickValueTrigger = TriggerBuilder.Create()
                .WithIdentity("getTickValues", "group")
                .StartNow()
                
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(2)

                    .RepeatForever())
            .Build();


            IJobDetail emailJob = JobBuilder.Create<SendEmails>()
              .WithIdentity("sendEmailJob", "group")
              .Build();

            ITrigger emailTrigger = TriggerBuilder.Create()
                .WithIdentity("sendEmailTrigger", "group")
                .StartNow()

                .WithSimpleSchedule(x => x
                     .WithIntervalInMinutes(1)

                    .RepeatForever())
            .Build();


            //IJobDetail offerHitJob = JobBuilder.Create<OfferHitProcessing>()
            //   .WithIdentity("processOfferHitJob", "group")
            //   .Build();

            //ITrigger offerHitTrigger = TriggerBuilder.Create()
            //    .WithIdentity("processOffersHit", "group")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(20)
            //        .RepeatForever())
            //.Build();


            await _scheduler.ScheduleJob(tickValueJob, tickValueTrigger, _stopppingToken);
            await _scheduler.ScheduleJob(emailJob, emailTrigger, _stopppingToken);
        }

    public override void Dispose()
        {
            // DO YOUR STUFF HERE
        }
    }
}
