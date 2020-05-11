using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication.CommandBuilder;
using WebApplication.Controllers;
using WebApplication.HandleThirdPartyOutput;
using WebApplication.Model;
using WebApplication.RunThirdParty;

namespace web_api_nunit_test
{
    public class Tests
    {
        /*
        ICommandCreator _creator = new CommandCreator();
        IProcessPython _python = new ProcessPython(null);
        IHandleOutput _outputHandler = new HandleOutput();
        BufferController _controller;

        [SetUp]
        public void Setup()
        {

            _controller = new BufferController(_creator, _python, _outputHandler);
        }

        [Test]
        public void Test1()
        {
            var testProducts = GetTestProducts();
            IActionResult result = _controller.Post(testProducts[0]); ;
            Assert.IsInstanceOf(typeof(ContentResult), result);
        }
        [Test]
        public void Test2()
        {
            var testProducts = GetTestProducts();
            IActionResult result = _controller.Post(testProducts[1]); ;
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
        }
        [Test]
        public void Test3()
        {
            var testProducts = GetTestProducts();
            IActionResult result = _controller.Post(testProducts[2]); ;
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
        }

        [Test]
        public void ClipController()
        {
            Clip naujas = new Clip { InputLayer = "{\"coordinates\":[-2.124156,51.899523],\"types\":\"Point\"}", OverlayLayer = "{\"coordinates\":[-2.124156,51.899523],\"types\":\"Point\"}" };

            var validationContext = new ValidationContext(naujas);

            var results = naujas.Validate(validationContext);

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual("Invalid geojson in InputLayer", results.First().ErrorMessage);
            Assert.AreEqual("Invalid geojson in OverlayLayer", results.ElementAt(1).ErrorMessage);
        }
        private List<BufferClass> GetTestProducts()
        {
            var testProducts = new List<BufferClass>();
            testProducts.Add(new BufferClass { InputLayer = "{\"coordinates\":[-2.124156,51.899523],\"type\":\"Point\"}", Distance = 500 });
            testProducts.Add(new BufferClass { InputLayer = "{\"coordinates\":[-2.124156,51.899523],\"type\":\"Point\"}", Distance = -500 });
            testProducts.Add(new BufferClass { InputLayer = "{\"coordinates\":[-2.124156,51.899523],\"type\":\"Point\"}" });
            testProducts.Add(new BufferClass { InputLayer = "{\"coordinatessa\":[-2.124156,51.899523],\"typse\":\"Point\"}", Distance = 500 });
            return testProducts;
        }
        */
    }
}