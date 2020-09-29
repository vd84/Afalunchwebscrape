using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using maträtter;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Diwine.Scrapers {
    public class DiwineScrape {

        public DiwineScrape () {

        }

        public async void Scrape() {
                        // Load default configuration
            var config = Configuration.Default.WithDefaultLoader ();
            // Create a new browsing context
            var context = BrowsingContext.New (config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = await context.OpenAsync ("https://diwinestockholm.se/lunch/");
            // Log the data to the console
            var lunchItems = document.All
                .Where (m => m.LocalName == "tbody" && m.ClassName == "lunch-day-content");

            Dictionary<int, List<MatRätt>> veckodagar = new Dictionary<int, List<MatRätt>> ();
            var index = 0;

            foreach (var item in lunchItems) {
                List<MatRätt> maträtterPerDag = new List<MatRätt> ();
                foreach (var rätt in item.QuerySelectorAll (".lunch-menu-item")) {

                    var title = rätt.QuerySelector (".td_title").TextContent.Replace (System.Environment.NewLine, " ").Trim ();
                    var price = int.Parse (rätt.QuerySelector (".td_price").TextContent.Replace (System.Environment.NewLine, " ").Trim ().Split (" ") [0]);
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
                System.Console.WriteLine (x.Key);

                foreach (var maträtt in x.Value) {
                    System.Console.WriteLine (maträtt.Name);
                    System.Console.WriteLine (maträtt.Pris);
                }

            }

            System.Console.WriteLine("Testa multiplier");
            MatRätt t1 = new MatRätt
            {
                Id = 1,
                Name = "test",
                Pris = 100
            };
            System.Console.WriteLine(t1.CalcaulateDiscountedPricePercent(20));

        }

    }
}