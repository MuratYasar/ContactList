using MassTransit;
using Newtonsoft.Json;
using ReportBusConfigurator;
using System;
using System.Net.Http;
using System.Text;
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
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(context.Message), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:7000/api/report/PrepareFinalReport", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var responsedata = JsonConvert.DeserializeObject<Entities.DataModel.Report>(apiResponse);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Console.WriteLine($"Repor ID:{responsedata.Id} is prepared for you..");
                            Console.WriteLine($"{context.Message.ReportName} - The report has been saved into the database and ready for you to display.");
                        }
                        else
                        {
                            Console.WriteLine($"Repor ID:{context.Message.Id} error occurred.. {response.StatusCode.ToString()}");
                        }
                    }
                }
            }
            
        }
    }
}
