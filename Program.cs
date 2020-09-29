using System.Threading.Tasks;
using Diwine.Scrapers;

namespace Afalunchwebscrape {
    class Program {
        static async Task Main (string[] args) {
            //Diwine
            DiwineScrape diwine = new DiwineScrape();
            System.Console.WriteLine("Scraping diwine");
            var diwineLuncherDennaVecka = await diwine.Scrape();


        }
    }


}