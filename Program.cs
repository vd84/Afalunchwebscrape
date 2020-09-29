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
        static Task Main (string[] args) {
            DiwineScrape diwine = new DiwineScrape();
            diwine.Scrape();



        }
    }


}