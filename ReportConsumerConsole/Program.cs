using MassTransit;
using Report.API.Events;
using ReportBusConfigurator;
using System;
using System.Threading.Tasks;
using GreenPipes;

namespace ReportConsumerConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.ReportConsumerQueue, endpoint =>
                {
                    endpoint.Consumer<ReportConsumer>();
                    endpoint.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                });
            });

            await bus.StartAsync();
            await Task.Run(() => {
                Console.WriteLine("Listening for report commands..... Press enter to exit");
                Console.ReadLine();
            });
            await bus.StopAsync();
        }
    }
}
