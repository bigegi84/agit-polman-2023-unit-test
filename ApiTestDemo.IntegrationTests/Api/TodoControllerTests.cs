using System.Net;
using System.Net.Http.Json;
using ApiTestDemo.Dto;
using ApiTestDemo.IntegrationTests.TestSuite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTestDemo.IntegrationTests.Api;

[TestFixture]
public class TodoControllerTests : ApiIntegrationTestFixture
{

    [Test]
    public async Task Get_Valid_HelloWorld()
    {
        var httpResponseMessage = await HttpClient.GetAsync("api/todos/HelloWorld");
        var body = await httpResponseMessage.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);

        Assert.That((string)json["result"], Is.EqualTo("Hello World"));
    }

    [Test]
    public async Task Get_Invalid_HelloWorld()
    {

        var httpResponseMessage = await HttpClient.GetAsync("api/todos/HelloWorld");
        var body = await httpResponseMessage.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);

        Assert.That((string)json["result"], Is.Not.EqualTo("Wrong"));
    }
    //[Test]
    //public async Task Get_Valid_Penambahan()
    //{

    //    var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos/Penambahan", new {
    //        x = 1,
    //        y = 1
    //    });
    //    var body = await httpResponseMessage.Content.ReadAsStringAsync();
    //    JObject json = JObject.Parse(body);

    //    Assert.That((string)json["result"], Is.EqualTo("2"));
    //}
    [Test]
    public async Task Get_Invalid_Penambahan()
    {

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos/Penambahan", new
        {
            x = 1,
            y = 1
        });
        var body = await httpResponseMessage.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);

        Assert.That((string)json["result"], Is.EqualTo("2"));
    }
    [Test]
    public async Task Post_Pengurangan_Valid()
    {

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos/Pengurangan", new
        {
            x = 1,
            y = 1
        });
        var body = await httpResponseMessage.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);

        Assert.That((string)json["result"], Is.EqualTo("0"));
    }
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    [TestCase(null, 1)]
    public async Task Post_Pengurangan_Invalid(int? x, int? y)
    {

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos/Pengurangan", new
        {
            x, y
        });
        Console.WriteLine(httpResponseMessage.ToString());
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    [Test]
    public async Task Post_Pembagian_Valid()
    {

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos/Pembagian", new
        {
            x = 1,
            y = 1
        });
        var body = await httpResponseMessage.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);
        Console.WriteLine((string)json["result"]);
        Assert.That((string)json["result"], Is.EqualTo("1"));
    }
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    [TestCase(null, 1)]
    public async Task Post_Pembagian_Invalid(int? x, int? y)
    {

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos/Pembagian", new
        {
            x,
            y
        });
        Console.WriteLine(httpResponseMessage.ToString());
        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    //
    [Test]
    public async Task Post_ValidTodo_Returns201Created()
    {
        var todo = new TodoForCreationDto { Title = "Title", Description = "Description" };

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos", todo);

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test]
    public async Task Post_ValidTodo_ReturnsExpectedDto()
    {
        var todo = new TodoForCreationDto { Title = "Title", Description = "Description" };

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos", todo);
        var todoFromResponse = await httpResponseMessage.DeserializeAsync<TodoDto>();

        Assert.Multiple(() =>
        {
            Assert.That(todoFromResponse, Is.Not.Null);
            Assert.That(todoFromResponse!.Id, Is.EqualTo(1));
            Assert.That(todoFromResponse.Title, Is.EqualTo("Title"));
            Assert.That(todoFromResponse.Description, Is.EqualTo("Description"));
            Assert.That(todoFromResponse.IsCompleted, Is.EqualTo(false));
        });
    }

    [Test]
    public async Task Post_ValidTodo_ReturnsExpectedLocationHeader()
    {
        var todo = new TodoForCreationDto { Title = "Title", Description = "Description" };

        var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/todos", todo);

        Assert.That(httpResponseMessage.Headers.Location, Is.EqualTo(new Uri("https://localhost/api/todos/1")));
    }

    [Test]
    public async Task Get_ExistingTodo_Returns200Ok()
    {
        var todo = new TodoForCreationDto { Title = "Title", Description = "Description" };
        await HttpClient.PostAsJsonAsync("api/todos", todo);

        var httpResponseMessage = await HttpClient.GetAsync("api/todos/1");

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Get_NonExistingTodo_Returns404NotFound()
    {
        var httpResponseMessage = await HttpClient.GetAsync("api/todos/1");

        Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}