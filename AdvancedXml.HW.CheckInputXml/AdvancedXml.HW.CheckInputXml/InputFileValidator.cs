using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace AdvancedXml.HW.CheckInputXml
{
    internal class InputFileValidator
    {
        private readonly XmlReaderSettings _settings;

        private IList<string> _fileErrors = new List<string>();

        public InputFileValidator(string xsdSchema)
        {
            var settings = new XmlReaderSettings();

            settings.Schemas.Add("http://library.by/catalog", xsdSchema);
            settings.ValidationEventHandler +=
                delegate (object sender, ValidationEventArgs e)
                {
                    string errorMessage = string.Format("[line_{0}:pos_{1}] {2}", e.Exception.LineNumber, e.Exception.LinePosition, e.Message);
                    _fileErrors.Add(errorMessage);
                    Console.WriteLine(errorMessage);
                };

            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            this._settings = settings;

        }

        public IList<string> ValidateErrors(string inputFile)
        {

            _fileErrors.Clear();

            using (XmlReader reader = XmlReader.Create(inputFile, _settings))
            {
                while (reader.Read()) ;
            }

            return _fileErrors.ToList();
        }

    }
}
