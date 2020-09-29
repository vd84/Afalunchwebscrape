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
                .Where (m => m.LocalName == "div" && m.ClassName == "dagensdag");
            Dictionary<int, List<MatRätt>> veckodagar = new Dictionary<int, List<MatRätt>> ();
            var index = 0;

            foreach (var day in lunchItems)
            {
                List<MatRätt> maträtterPerDag = new List<MatRätt> ();
                foreach (var rätt in day.QuerySelectorAll(".dagens"))
                {
                    var title = rätt.QuerySelector("h4").TextContent;
                    var ingredienser = rätt.QuerySelector("p").TextContent;
                    int price;

                    if(title.Substring(0,6) == "DAGENS") price = 130; 
                    else price = int.Parse(ingredienser.Substring(ingredienser.Length-4, 4));
                    maträtterPerDag.Add (
                        new MatRätt () {
                            Id = index,
                            Name = title,
                            Pris = price
                        }
                    );
                }
            veckodagar.Add (index, maträtterPerDag);
            index++;
            }
    
            foreach (var x in veckodagar) {
                System.Console.WriteLine ("######################### DAG " + x.Key + " ##########################");

                foreach (var maträtt in x.Value) {
                    Console.WriteLine("Name: " + maträtt.Name + "\nPrice: " + maträtt.Pris);
                }

            }

            return veckodagar;
        }

    }
}