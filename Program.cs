using System;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace Afalunchwebscrape {
    class Program {
        static async System.Threading.Tasks.Task Main (string[] args) {
            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader ();
            // Create a new browsing context
            var context = BrowsingContext.New (config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = await context.OpenAsync ("https://diwinestockholm.se/lunch/");
            // Log the data to the console
            System.Console.WriteLine(document.ChildNodes.ToHtml());
            var siteHtml = document.ChildNodes.ToHtml();
            //var dagensLunchRows = document.QuerySelectorAll();
        }
    }
}