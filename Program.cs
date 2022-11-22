// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var GamesList = await ProcessRepositoriesAsync(client);

foreach (var game in GamesList)
{
    Console.WriteLine($"Title: {game.Title}");
    Console.WriteLine($"Year: {game.Year}");
    Console.WriteLine($"Description: {game.Description}");
    Console.WriteLine($"Image: {game.Image}");
    Console.WriteLine($"Consoles: {game.Consoles}");
    Console.WriteLine($"Emulator: {game.Emulator}");
    Console.ReadLine();
}

static async Task<List<GamesList>> ProcessRepositoriesAsync(HttpClient client)
{
    await using Stream stream =
        await client.GetStreamAsync("http://localhost:3000/games");
    var games =
        await JsonSerializer.DeserializeAsync<List<GamesList>>(stream);
    return games?? new();
}