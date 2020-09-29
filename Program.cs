using System;
using System.Text;
using System.Threading.Tasks;
using Diwine.Scrapers;
using IlMolo.Scrapers;
using RabbitMQ.Client;
using System.Threading;
using Newtonsoft;

namespace Afalunchwebscrape {
    class Program {
        static async Task Main (string[] args) {
            //Diwine
             DiwineScrape diwine = new DiwineScrape ();
            System.Console.WriteLine ("Scraping diwine");
            var diwineLuncherDennaVecka = await diwine.Scrape ();
            StringBuilder sb = new StringBuilder();

            var listOfMealsDiwineMonday = diwineLuncherDennaVecka[0];

            string jsonMonday = Newtonsoft.Json.JsonConvert.SerializeObject(listOfMealsDiwineMonday, Newtonsoft.Json.Formatting.Indented);


            /* IlMoloScrape ilmolo = new IlMoloS
            crape ();
            System.Console.WriteLine ("Scraping ilmolo");
            await ilmolo.Scrape (); */

            var factory = new ConnectionFactory () { HostName = "localhost" };
            using (var connection = factory.CreateConnection ()) {
                using (var channel = connection.CreateModel ()) {
                    channel.QueueDeclare (queue: "hello",
                        durable : false,
                        exclusive : false,
                        autoDelete : false,
                        arguments : null);
                

                    var body = Encoding.UTF8.GetBytes (jsonMonday);

                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);
                        Console.WriteLine(" [x] Sent {0}", jsonMonday);
                    
                }

                Console.WriteLine (" Press [enter] to exit.");
                Console.ReadLine ();
            }
        }
    }

}

