using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diwine.Scrapers;
using IlMolo.Scrapers;
using Newtonsoft;
using RabbitMQ.Client;

namespace Sender.Ilmolo {
    class IlmoloSender {
        public async Task SenderIlmoloMenu () {
            //Ilmolo
            IlMoloScrape ilmolo = new IlMoloScrape ();
            System.Console.WriteLine ("Scraping ilmolo");
            var IlMoloLuncherDennaVecka = await ilmolo.Scrape ();
            
            StringBuilder sb = new StringBuilder ();
            List<string> listJsonWeekDays = new List<string> ();


            string jsonAllDays = Newtonsoft.Json.JsonConvert.SerializeObject (IlMoloLuncherDennaVecka, Newtonsoft.Json.Formatting.Indented);

            System.Console.WriteLine (jsonAllDays);

            var factory = new ConnectionFactory () { HostName = "localhost" };
            using (var connection = factory.CreateConnection ()) {
                using (var channel = connection.CreateModel ()) {
                    channel.QueueDeclare (queue: "insertilmolomenu",
                        durable : false,
                        exclusive : false,
                        autoDelete : false,
                        arguments : null);

                    var body = Encoding.UTF8.GetBytes (jsonAllDays);

                    channel.BasicPublish (exchange: "",
                        routingKey: "insertilmolomenu",
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