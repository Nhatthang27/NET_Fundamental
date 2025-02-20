using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace IntergrationTest
{
    public class ApiIntergrationTests : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        private readonly WebApplicationFactory<Api.Program> _factory;
        private readonly string _baseUrl;

        public ApiIntergrationTests(WebApplicationFactory<Api.Program> factory)
        {
            _factory = factory;
            _baseUrl = "/api/to-do-item";
        }

        [Fact]
        public async Task Test1Async()
        {
            var client = _factory.CreateClient();

            // Initial Item
            long newId = new Random().NextInt64(1, long.MaxValue);
            var isExistResponse = await client.GetAsync($"{_baseUrl}/{newId:D}");
            while (isExistResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                newId = new Random().NextInt64(1, long.MaxValue);
                isExistResponse = await client.GetAsync($"{_baseUrl}/{newId:D}");
            }

            // Add new item
            var newItem = new TodoItem { Id = newId, Name = "Test Item", IsComplete = false };
            var addResponse = await client.PostAsJsonAsync($"{_baseUrl}", newItem);
            addResponse.EnsureSuccessStatusCode();

            // Get new item
            var addedItem = await client.GetFromJsonAsync<TodoItem>($"{_baseUrl}/{newId:D}");
            Assert.NotNull(addedItem);
            Assert.Equal(newItem.Id, addedItem.Id);
            Assert.Equal(newItem.Name, addedItem.Name);
            Assert.Equal(newItem.IsComplete, addedItem.IsComplete);
        }
    }
}