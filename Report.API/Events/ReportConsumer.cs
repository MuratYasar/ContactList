using Entities.DTOs;
using Entities.ReportConsumeModel;
using MassTransit;
using Newtonsoft.Json;
using Report.DAL.Abstract;
using Report.DAL.Concrete;
using ReportBusConfigurator;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.API.Events
{
    public class ReportConsumer : IConsumer<Entities.DataModel.Report>
    {        
        public ReportConsumer()
        {
            var bus = BusConfigurator.ConfigureBus(factory =>
            {
                factory.ReceiveEndpoint(RabbitMqConstants.ReportConsumerQueue, endpoint =>
                {
                    endpoint.Consumer<ReportConsumer>();
                });
            });

            bus.StartAsync();
        }

        public async Task Consume(ConsumeContext<Entities.DataModel.Report> context)
        {
            if (context.Message != null)
            {
                var client = new RestClient("http://localhost:7000/api/report/PrepareFinalReport");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("application/json", JsonConvert.SerializeObject(context.Message), ParameterType.RequestBody);

                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responsedata = JsonConvert.DeserializeObject<Entities.DataModel.Report>(response.Content);
                    Console.WriteLine($"Repor ID:{responsedata.Id} is prepared for you..");
                }
                else
                {
                    Console.WriteLine($"Repor ID:{context.Message.Id} error occurred.. {response.StatusCode.ToString()}");
                }
            }

            Console.WriteLine($"{context.Message.ReportName} - The report has been saved into the database and ready for you to display.");
        }
    }
}
