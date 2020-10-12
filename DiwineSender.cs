using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diwine.Scrapers;
using IlMolo.Scrapers;
using Newtonsoft;
using RabbitMQ.Client;

namespace Sender.Diwine {
    class DiwineSender {
        public async Task SenderDiwineMenu () {
            //Diwine
            DiwineScrape diwine = new DiwineScrape ();
            System.Console.WriteLine ("Scraping diwine");
            var diwineLuncherDennaVecka = await diwine.Scrape ();
            StringBuilder sb = new StringBuilder ();
            List<string> listJsonWeekDays = new List<string> ();

            string jsonAllDays = Newtonsoft.Json.JsonConvert.SerializeObject (diwineLuncherDennaVecka, Newtonsoft.Json.Formatting.Indented);
            System.Console.WriteLine (jsonAllDays);

            var factory = new ConnectionFactory () { HostName = "http://localhost:5672", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection ()) {
                using (var channel = connection.CreateModel ()) {
                    channel.QueueDeclare (queue: "insertdiwinemenu",
                        durable : false,
                        exclusive : false,
                        autoDelete : false,
                        arguments : null);

                    var body = Encoding.UTF8.GetBytes (jsonAllDays);

                    channel.BasicPublish (exchange: "",
                        routingKey: "insertdiwinemenu",
                        basicProperties : null,
                        body : body);
                    Console.WriteLine (" [x] Sent {0}", jsonAllDays);
                }
                Console.WriteLine (" Press [enter] to exit.");
                Console.ReadLine ();
            }
        }
    }

}