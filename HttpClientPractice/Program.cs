using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpClientPractice;

class Program
{
    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
    };
    static async Task Main(string[] args)
    {
        await GetAsync(sharedClient);
    }

    static async Task GetAsync(HttpClient httpClient)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("todos/3");

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        var todoItem = await httpClient.GetFromJsonAsync<TodoItem>("todos/1");

        // Expected output:
        //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
        //   {
        //     "userId": 1,
        //     "id": 3,
        //     "title": "fugiat veniam minus",
        //     "completed": false
        //   }
    }
}

class TodoItem
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
}