using System;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class TriageServiceContractTests
    {
        [Test]
        public void TriageService_WhenSeverityCalled_ThenValidValueIsReturned()
        {
            //Given
            //Given
            var bugTitle = "test";
            var bugDescirption = "test";
            var url = $"http://workshopaa.azurewebsites.net/api/severity?title={bugTitle}&description={bugDescirption}";
            //When
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Then
            var expectedSeverity = "3";
            response.Content.ReadAsStringAsync().Result.ShouldBe(expectedSeverity);
        }

        [Test]
        public void TriageService_WhenCalledWithMissingTitle_ThenStatusCodeIsNotSucess()
        {
            //Given
            var bugTitle = string.Empty;
            var bugDescirption = string.Empty;
            var url = $"http://workshopaa.azurewebsites.net/api/severity?title={bugTitle}&description={bugDescirption}";
            //When
            var client = new HttpClient();
            //Then
            client.GetAsync(url).Result.IsSuccessStatusCode.ShouldBeFalse();
        }

        [Test]
        public void TriageService_WhenPriorityCalled_ThenValidValueIsReturned()
        {
            //Given
            var bugTitle = "test";
            var bugDescirption = "test";
            var url = $"http://workshopaa.azurewebsites.net/api/priority?title={bugTitle}&description={bugDescirption}";
            //When
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Then
            var expectedPriority = 1;
            Int32.Parse(response.Content.ReadAsStringAsync().Result).ShouldBe(expectedPriority);
        }
    }
}