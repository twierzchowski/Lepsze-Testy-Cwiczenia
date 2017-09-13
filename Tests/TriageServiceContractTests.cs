﻿using System;
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
            var expectedSeverity = "3";
            response.Content.ReadAsStringAsync().Result.ShouldBe(expectedSeverity);
        }

        [Test]
        public void TriageService_WhenCalledWithMissingTitle_ExceptionIsThrown()
        {
            //Given
            var url = "http://localhost:55086/api/triagebug/severity";
            var bugTitle = string.Empty;
            //When
            var client = new HttpClient();
            Action action = () =>
            {
                client.PostAsync(url, new StringContent(
                    new JavaScriptSerializer().Serialize(bugTitle), Encoding.UTF8, "application/json")).Wait();
            };
            //Then
            Should.Throw<Exception>(action);
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
            var expectedPriority = "1";
            response.Content.ReadAsStringAsync().Result.ShouldBe(expectedPriority);
        }
    }
}