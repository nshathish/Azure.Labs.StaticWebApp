using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Labs.StaticWebApp.Api.Todos;

public class CreateTodo(ILogger<CreateTodo> logger)
{
    [Function(nameof(CreateTodo))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos")]
        HttpRequest req)
    {
        logger.LogInformation("CreateTodo");
        TodoStore.AllowLoopbackCors(req);

        var body = await req.ReadFromJsonAsync<TodoStore.TodoInput>();
        if (string.IsNullOrWhiteSpace(body?.Title))
            return new BadRequestObjectResult("Title is required.");

        var item = new TodoStore.TodoItem(TodoStore.NextId(), body.Title.Trim(), IsCompleted: false);
        TodoStore.Items[item.Id] = item;

        return new ObjectResult(item) { StatusCode = StatusCodes.Status201Created };
    }
}
