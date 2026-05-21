using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace Labs.StaticWebApp.Api.Todos;

internal static class TodoStore
{
    private static int _nextId = 5;

    public static readonly ConcurrentDictionary<int, TodoItem> Items = new(
        new TodoItem[]
        {
            new(1, "Buy groceries",       IsCompleted: true),
            new(2, "Walk the dog",         IsCompleted: false),
            new(3, "Read a book",          IsCompleted: false),
            new(4, "Write unit tests",     IsCompleted: true),
            new(5, "Deploy to production", IsCompleted: false),
        }.ToDictionary(t => t.Id)
    );

    public static int NextId() => Interlocked.Increment(ref _nextId);

    public static void AllowLoopbackCors(HttpRequest req)
    {
        if (req.Headers.TryGetValue("Origin", out var originValues)
            && Uri.TryCreate(originValues.ToString(), UriKind.Absolute, out var originUri)
            && originUri.IsLoopback)
        {
            req.HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", originUri.GetLeftPart(UriPartial.Authority));
            req.HttpContext.Response.Headers.Append("Vary", "Origin");
        }
    }

    public record TodoItem(int Id, string Title, bool IsCompleted);
    public record TodoInput(string? Title, bool IsCompleted);
}
