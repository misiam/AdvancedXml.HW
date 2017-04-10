using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedXml.HW.AtomFeedConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "books.xml";
            string outputFile = "feed.atom";
            
            if (args.Length > 1)
            {
                outputFile = args[1];

            }
            if (args.Length > 0)
            {
                inputFile = args[0];

            }
            
            var feedBuilder = new FeedBuilder();
            feedBuilder.CreateFeed(inputFile, outputFile);

            Console.ReadKey();


        }
    }
}
