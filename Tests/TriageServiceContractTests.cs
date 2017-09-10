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
        public void TriageService_WhenSeverityCalled_ValidValueIsReturned()
        {
            //Given
            var url = "http://localhost:55086/api/triagebug/severity";
            var bugTitle = "test";
            
            //When
            var client = new HttpClient();
            HttpResponseMessage response = client.PostAsync(url, new StringContent(
                new JavaScriptSerializer().Serialize(bugTitle), Encoding.UTF8, "application/json")).Result;

            //Then
            response.Content.ReadAsStringAsync().Result.ShouldBe("3");
        }

        [Test]
        public void TriageService_WhenPriorityCalled_ValidValueIsReturned()
        {
            //Given
            var url = "http://localhost:55086/api/triagebug/priority";
            var bugTitle = "test";
            var urlWithParam = $"{url}?title={bugTitle}";

            //When
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(urlWithParam).Result;

            //Then
            response.Content.ReadAsStringAsync().Result.ShouldBe("1");
        }
    }
}