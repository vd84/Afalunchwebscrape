using System.Threading.Tasks;
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