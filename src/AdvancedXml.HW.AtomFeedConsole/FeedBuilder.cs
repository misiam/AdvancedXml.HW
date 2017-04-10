using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;
using AdvancedXml.HW.CheckInputXml;

namespace AdvancedXml.HW.AtomFeedConsole
{
    internal class FeedBuilder
    {
        private readonly string _inputFileXsd;
        private readonly string _xsltFile;


        public FeedBuilder(string inputFileXsd = @".\Schemas\XMLSchemaBooks.xsd", string xsltFile = @".\TransformToAtom.xslt")
        {
            _inputFileXsd = inputFileXsd;
            _xsltFile = xsltFile;
        }

        public void CreateFeed(string inputFile, string outputFile)
        {
            InputFileValidator validator = new InputFileValidator(_inputFileXsd);

            var errors = validator.ValidateErrors(inputFile);
            if (errors.Any())
            {
                throw new ArgumentException("There are errors in the input file", inputFile);
            }

            string atomFeed = "";
            XsltSettings xslSettings = new XsltSettings {EnableScript = true};

            var xsl = new XslCompiledTransform();
            xsl.Load(_xsltFile, xslSettings, null);

            using (var stream = new FileStream(outputFile, FileMode.Truncate, FileAccess.Write))
            {
                xsl.Transform(inputFile, null, Console.Out);
                xsl.Transform(inputFile, null, stream);
            }
        }

    }
}
