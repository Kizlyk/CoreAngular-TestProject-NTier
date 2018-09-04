using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using ProductCatalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ProductCatalogTests
{
    //integration test examples
    public class IntegrationTests
    {
        //private readonly TestServer _server;
        //private readonly HttpClient _client;
        private readonly string websiteBaseURL = "http://localhost:7772";

        public IntegrationTests()
        {
            //not working
            /*var applicationPath = Path.GetFullPath(@"../../../../ProductCatalog");

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            _client = _server.CreateClient();*/
        }

        [Fact]
        //only works when site is running
        public void GetAllProducts()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(websiteBaseURL);

            var response = client.GetAsync("api/products").Result;

            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            Assert.Contains("First Product", responseString);
        }

        [Fact]
        //only works when site is running
        public void GetAllProducts_FilterByPriceGreaterThan100()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(websiteBaseURL);

            var response = client.GetAsync("api/products?$filter=price gt 100").Result;

            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            //parse and check there are no items with incorrect price
        }

        [Fact]
        //only works when site is running
        public void GetAllProducts_SortByPriceDesc_FirstTwoItems()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(websiteBaseURL);

            var response = client.GetAsync("api/products?$orderby=price desc&$top=2").Result;

            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            //parse and check there are only two items, and price of the first > price of the second
        }
    }
}
