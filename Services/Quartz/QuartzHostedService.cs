using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz.Spi;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;

namespace API.Services.Quartz
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // WEST EUROPE saat dilimi için TimeZone ayarları
            TimeZoneInfo west = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            TimeZoneInfo turk = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            // Quartz.NET zamanlayıcı (scheduler) oluşturma
            IScheduler scheduler = await schedulerFactory.GetScheduler(cancellationToken);
            scheduler.JobFactory = jobFactory;

            // İşlem (job) oluşturma
            IJobDetail job = JobBuilder.Create<MyJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // İşlem tetikleyici (trigger) oluşturma
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(18, 50)
                    .InTimeZone(west)) // WEST EUROPE saat dilimi
                .Build();

            // İşlemi zamanlama
            await scheduler.ScheduleJob(job, trigger, cancellationToken);

            // Zamanlayıcıyı başlatma
            await scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Zamanlayıcıyı durdurma
            IScheduler scheduler = await schedulerFactory.GetScheduler(cancellationToken);
            await scheduler.Shutdown(cancellationToken);
        }
    }

}
