using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedXml.HW.CheckInputXml
{
    [TestClass]
    public class InputFileValidatorTest
    {
        private InputFileValidator _fileValidator;

        [TestInitialize]
        public void Init()
        {
            string xsdSchema = "XMLSchemaBooks.xsd"; 
            _fileValidator = new InputFileValidator(xsdSchema);
        }

        [TestMethod]
        public void TestValidateCorrectFile()
        {
            string inputFile = @".\CorrectFiles\books.xml";
            var errors = _fileValidator.ValidateErrors(inputFile);
            Assert.IsTrue(!errors.Any());

        }


        [TestMethod]
        public void TestValidateIncorrectFile()
        {
            string inputFile = @".\IncorrectFiles\books_wrong_format.xml";
            var errors = _fileValidator.ValidateErrors(inputFile);
            Assert.IsTrue(errors.Select(x=>x.Contains("has invalid child element 'WRONG'")).Any());
        }

        [TestMethod]
        public void TestValidateIncorrectIsbn()
        {
            string inputFile = @".\IncorrectFiles\books_wrong_isbn.xml";
            var errors = _fileValidator.ValidateErrors(inputFile);
            Assert.IsTrue(errors.Select(x=>x.Contains(@"'http://library.by/catalog:isbn' - The Pattern constraint failed")).Any());
        }

        [TestMethod]
        public void TestValidateIncorrectGenre()
        {
            string inputFile = @".\IncorrectFiles\books_wrong_genre.xml";
            var errors = _fileValidator.ValidateErrors(inputFile);
            Assert.IsTrue(errors.Select(x => x.Contains(@"'http://library.by/catalog:genre' - The Enumeration constraint failed.")).Any());
        }

        [TestMethod]
        public void TestValidateIncorrectDateFormat()
        {
            string inputFile = @".\IncorrectFiles\books_wrong_date_format.xml";
            var errors = _fileValidator.ValidateErrors(inputFile);
            Assert.IsTrue(errors.Select(x => x.Contains("is not a valid Date value.")).Any());
        }

        [TestMethod]
        public void TestValidateNotUniqId()
        {
            string inputFile = @".\IncorrectFiles\books_not_uniq_id.xml";
            var errors = _fileValidator.ValidateErrors(inputFile);
            Assert.IsTrue(errors.Select(x => x.Contains("There is a duplicate key sequence")).Any());
        }

    }
}
