using System;

namespace AdvancedXml.HW.HtmlReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "books.xml";
            string transformationFile = "TransformToHtmlReport.xslt";
            string outputFile = "BooksReport.html";

            if (args.Length > 2)
            {
                outputFile = args[2];

            }
            if (args.Length > 1)
            {
                transformationFile = args[1];
            }

            if (args.Length > 0)
            {
                outputFile = args[0];
            }

            var reportBuilder = new ReportBuilder();

            reportBuilder.CreateReport(inputFile,transformationFile,outputFile);

            Console.ReadKey();
        }
    }
}
