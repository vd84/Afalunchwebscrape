using System;
using System.Text;
using System.Threading.Tasks;
using Diwine.Scrapers;
using IlMolo.Scrapers;
using RabbitMQ.Client;
using System.Threading;

namespace Afalunchwebscrape {
    class Program {
        static async Task Main (string[] args) {
            //Diwine
             DiwineScrape diwine = new DiwineScrape ();
            System.Console.WriteLine ("Scraping diwine");
            var diwineLuncherDennaVecka = await diwine.Scrape ();
            StringBuilder sb = new StringBuilder();



            /* IlMoloScrape ilmolo = new IlMoloScrape ();
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
                
                    var message = "test";

                    var body = Encoding.UTF8.GetBytes (message);
                    while (true)
                    {
                        Thread.Sleep(10);
                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                }

                Console.WriteLine (" Press [enter] to exit.");
                Console.ReadLine ();
            }
        }
    }

}

