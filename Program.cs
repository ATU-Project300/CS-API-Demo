// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var repositories = await ProcessRepositoriesAsync(client);

foreach (var repo in repositories)
{
    Console.WriteLine($"Title: {repo.Title}");
    Console.WriteLine($"Year: {repo.Year}");
    Console.WriteLine($"Description: {repo.Description}");
    Console.WriteLine($"Image: {repo.Image}");
    Console.WriteLine($"Consoles: {repo.Consoles}");
    Console.WriteLine($"Emulator: {repo.Emulator}");
    Console.ReadLine();
}

static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
{
    await using Stream stream =
        await client.GetStreamAsync("http://localhost:3000/games");
    var repositories =
        await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
    return repositories ?? new();
}