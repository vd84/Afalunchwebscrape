using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using maträtter;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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