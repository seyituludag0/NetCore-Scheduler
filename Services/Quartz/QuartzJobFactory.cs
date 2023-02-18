using Quartz.Spi;
using Quartz;

namespace API.Services.Quartz
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QuartzJobFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

            return job;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
