using Entities.ReportConsumeModel;
using MassTransit;
using ReportBusConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.API.Events
{
    public class ReportConsumer : IConsumer<IReportConsumer>
    {
        public ReportConsumer()
        {
            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.ConsumerQueue, endpoint =>
                {
                    endpoint.Consumer<ReportConsumer>();
                });
            });

            bus.StartAsync();
        }

        public Task Consume(ConsumeContext<IReportConsumer> context)
        {
            throw new NotImplementedException();
        }
    }
}
