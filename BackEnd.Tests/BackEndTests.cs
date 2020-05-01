using BackEnd.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq; 
using Microsoft.Extensions.Options;
using BackEnd.Config;
using BackEnd.Services.Implementations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace BackEnd.Tests
{
    [TestClass]
    public class BackEndTests
    {
 
        
        [TestMethod]
        public void Test_GetLargestNumber()
        {

            //assemble
            var testArray = new int[] { 3, 5, 6, 78 };
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);
            //act
            var Result = mockService.GetLargestNumber(testArray);

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual(78,Result);
            Console.WriteLine(Result);
        }

        [TestMethod]
        public void Test_GetSmallestNumber()
        {

            //assemble
            var testArray = new int[] { 3, 5, 6, 78 };
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);
             

            //act
            var Result = mockService.GetSmallestNumber(testArray);

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual(3, Result);
        }

        [TestMethod]
        public void Test_AnagramCheck()
        {

            //assemble
            var FirstString = "CAT";
            var SecondString = "ACT";
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);

            //act
            var Result = mockService.AnagramCheck(FirstString,SecondString);

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual(true, Result);
        }

        [TestMethod]
        public void Test_IsValidAUMobileNumber()
        {

            //assemble
            var ValidNumber1 =  "0418160502";
            var InValidNumber1 =  "041816050277";
            var ValidNumber2 = "0061418160502";
            var InValidNumber2 = "0289768956";
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);

            //act
            var ValidResult1 = mockService.IsValidAUMobileNumber(ValidNumber1);
            var ValidResult2 = mockService.IsValidAUMobileNumber(ValidNumber2);
            var InValidResult1 = mockService.IsValidAUMobileNumber(InValidNumber1);
            var InValidResult2 = mockService.IsValidAUMobileNumber(InValidNumber2);
            //assert 
            Assert.AreEqual(true, ValidResult1);
            Assert.AreEqual(true, ValidResult2);
            Assert.AreEqual(false, InValidResult1);
            Assert.AreEqual(false, InValidResult2);
        }

        [TestMethod]
        public void Test_RemoveDupesPreferred()
        {

            //assemble
            var Givenstring = "oneoneoneone";
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);

            //act
            var Result = mockService.RemoveDupesPreferred(Givenstring);

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual("one", Result);
        }

        [TestMethod]
        public void Test_RemoveDupesTwo()
        {

            //assemble
            var Givenstring = "oneoneoneone";
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);

            //act
            var Result = mockService.RemoveDupesTwo(Givenstring);

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual("one", Result);
        }

        [TestMethod]
        public void Test_RemoveDupesLeastPreffered()
        {

            //assemble
            var Givenstring = "oneoneoneone";
            var mockFaceBook = Substitute.For<IFacebook>();
            var mockService = Substitute.For<BackEndSvc>(mockFaceBook);

            //act
            var Result = mockService.RemoveDupesLeastPreffered(Givenstring);

            //assert
            Assert.IsNotNull(Result);
            Assert.AreEqual("one", Result);
        }
      
    }

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;

        public string Input { get; private set; }
        public int NumberOfCalls { get; private set; }

        public MockHttpMessageHandler(string response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;
            if (request.Content != null) // Could be a GET-request without a body
            {
                Input = await request.Content.ReadAsStringAsync();
            }
            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_response)
            };
        }
    }


}
