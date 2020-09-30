﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diwine.Scrapers;
using IlMolo.Scrapers;
using Newtonsoft;
using RabbitMQ.Client;

namespace Afalunchwebscrape {
    class Program {
        static async Task Main (string[] args) {
            //Diwine
            DiwineScrape diwine = new DiwineScrape ();
            System.Console.WriteLine ("Scraping diwine");
            var diwineLuncherDennaVecka = await diwine.Scrape ();
            StringBuilder sb = new StringBuilder ();
            List<string> listJsonWeekDays = new List<string> ();


            string jsonAllDays = Newtonsoft.Json.JsonConvert.SerializeObject (diwineLuncherDennaVecka, Newtonsoft.Json.Formatting.Indented);

            System.Console.WriteLine (jsonAllDays);

            /* IlMoloScrape ilmolo = new IlMoloS
            crape ();
            System.Console.WriteLine ("Scraping ilmolo");
            await ilmolo.Scrape (); */

            var factory = new ConnectionFactory () { HostName = "localhost" };
            using (var connection = factory.CreateConnection ()) {
                using (var channel = connection.CreateModel ()) {
                    channel.QueueDeclare (queue: "insertveckansluncher",
                        durable : false,
                        exclusive : false,
                        autoDelete : false,
                        arguments : null);

                    var body = Encoding.UTF8.GetBytes (jsonAllDays);

                    channel.BasicPublish (exchange: "",
                        routingKey: "insertveckansluncher",
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