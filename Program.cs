using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diwine.Scrapers;
using IlMolo.Scrapers;
using Newtonsoft;
using RabbitMQ.Client;
using Sender.Diwine;
using Sender.Ilmolo;

namespace Afalunchwebscrape {
    class Program {
        static async Task Main (string[] args) {
            DiwineSender diwine = new DiwineSender();
            await diwine.SenderDiwineMenu(); 
            IlmoloSender ilmolo = new IlmoloSender();
            await ilmolo.SenderIlmoloMenu(); 
        }
    }

}
