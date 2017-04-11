using System;
using System.IO;
using System.Linq;
using System.Xml.Xsl;
using AdvancedXml.HW.CheckInputXml;

namespace AdvancedXml.HW.HtmlReport
{
    internal class ReportBuilder
    {
        private readonly string _inputFileXsd;

        public ReportBuilder(string inputFileXsd = @".\Schemas\XMLSchemaBooks.xsd")
        {
            _inputFileXsd = inputFileXsd;
        }

        public void CreateReport(string inputFile, string transformationFile, string outputFile)
        {
            InputFileValidator validator = new InputFileValidator(_inputFileXsd);

            var errors = validator.ValidateErrors(inputFile);
            if (errors.Any())
            {
                throw new ArgumentException("There are errors in the input file", inputFile);
            }

            var xsl = new XslCompiledTransform();
            xsl.Load(transformationFile);
            var xsltArgs = GetXsltParams();
                
            using (var stream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                xsl.Transform(inputFile, xsltArgs, Console.Out);
                xsl.Transform(inputFile, xsltArgs, stream);
            }
        }

        private XsltArgumentList GetXsltParams()
        {
            string reportTitle = $"Current funds by genre ({DateTime.Now.ToShortDateString()})" ;

            XsltArgumentList argsList = new XsltArgumentList();
            argsList.AddParam("Report_Title", "", reportTitle);

            return argsList;
        }
    }
}
