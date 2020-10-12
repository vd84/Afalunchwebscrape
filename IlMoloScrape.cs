using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using maträtter;
using System.Collections.Generic;

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
            var lunchItems = document.All
                .Where (m => m.LocalName == "div" && m.ClassName == "dagensdag");
            //Create dictionary to store the menu
            Dictionary<int, List<MatRätt>> veckodagar = new Dictionary<int, List<MatRätt>> ();
            var index = 0;

            foreach (var day in lunchItems)
            {
                List<MatRätt> maträtterPerDag = new List<MatRätt> ();
                foreach (var rätt in day.QuerySelectorAll(".dagens"))
                {
                    var title = rätt.QuerySelector("h4").TextContent;
                    var ingredients = rätt.QuerySelector("p").TextContent;
                    int price;

                    if(title.Substring(0,6) == "DAGENS") price = 130; 
                    else price = int.Parse(ingredients.Substring(ingredients.Length-4, 4));
                    maträtterPerDag.Add (
                        new MatRätt () {
                            Id = index,
                            Title = title,
                            Ingredients = ingredients,
                            Price = price,
                            IdOfRestaurant = 2
                        }
                    );
                }
            veckodagar.Add (index, maträtterPerDag);
            index++;
            }

            //ToString
            foreach (var x in veckodagar) {
                System.Console.WriteLine ("######################### DAG " + x.Key + " ##########################");
                foreach (var maträtt in x.Value) {
                    System.Console.WriteLine("Name: " + maträtt.Title + "\nPrice: " + maträtt.Price + "\nIngredients: " + maträtt.Ingredients);
                }
            }
            return veckodagar;
        }
    }
}