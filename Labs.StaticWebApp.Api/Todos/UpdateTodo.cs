using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Labs.StaticWebApp.Api.Todos;

public class UpdateTodo(ILogger<UpdateTodo> logger)
{
    [Function(nameof(UpdateTodo))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "todos/{id:int}")]
        HttpRequest req, int id)
    {
        logger.LogInformation("UpdateTodo {Id}", id);
        TodoStore.AllowLoopbackCors(req);

        if (!TodoStore.Items.TryGetValue(id, out var existing))
            return new NotFoundResult();

        var body = await req.ReadFromJsonAsync<TodoStore.TodoInput>();
        if (string.IsNullOrWhiteSpace(body?.Title))
            return new BadRequestObjectResult("Title is required.");

        var updated = existing with { Title = body.Title.Trim(), IsCompleted = body.IsCompleted };
        TodoStore.Items[id] = updated;

        return new OkObjectResult(updated);
    }
}
