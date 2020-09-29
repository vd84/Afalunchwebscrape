using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using maträtter;
using System.Collections.Generic;


namespace Diwine.Scrapers {
    public class IlMoloScrape {

        public IlMoloScrape () {

        }

        public async void Scrape() {
                        // Load default configuration
            var config = Configuration.Default.WithDefaultLoader ();
            // Create a new browsing context
            var context = BrowsingContext.New (config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = await context.OpenAsync ("https://ilmolo.se/lunch/");
            // Log the data to the console
            


        }

    }
}