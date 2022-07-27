using Compi.DynamicServiceInjection.Common;

namespace Compi.DynamicServiceInjection.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IService _service;


        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IService service)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _service = service;


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                //using var scope = _serviceProvider.CreateScope();
                //var service = scope.ServiceProvider.GetService(type);


                _service.Execute();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}