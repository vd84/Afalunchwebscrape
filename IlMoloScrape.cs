using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using maträtter;
using System.Collections.Generic;
using System;


namespace IlMolo.Scrapers {
    public class IlMoloScrape {

        public IlMoloScrape () {

        }

        public async Task<Dictionary<int, List<MatRätt>>> Scrape() {
                        // Load default configuration
            var config = Configuration.Default.WithDefaultLoader ();
            // Create a new browsing context
            var context = BrowsingContext.New (config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = await context.OpenAsync ("https://ilmolo.se/lunch/");
            // Log the data to the console
            //test
            var lunchItems = document.All
                .Where (m => m.LocalName == "div" && m.ClassName == "menywrapper shadow animated fadeInUp");

            foreach (var item in lunchItems) {
                 foreach (var day in item.QuerySelectorAll(".dagensdag"))
                 {
                    Console.WriteLine("######################### NY DAG ##########################");
                    foreach (var rätt in day.QuerySelectorAll(".dagens"))
                    {
                        var name = rätt.QuerySelector("h4").TextContent;
                        var ingredienser = rätt.QuerySelector("p").TextContent;
                        int pris;

                        if(name.Substring(0,6) == "DAGENS") pris = 130; 
                        else pris = int.Parse(ingredienser.Substring(ingredienser.Length-4, 4));

                        Console.WriteLine(pris);
                    }
                 }
            }
            Dictionary<int, List<MatRätt>> veckodagar = new Dictionary<int, List<MatRätt>> ();

            return veckodagar;
        }

    }
}