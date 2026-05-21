using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Labs.StaticWebApp.Api.Todos;

public class DeleteTodo(ILogger<DeleteTodo> logger)
{
    [Function(nameof(DeleteTodo))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "todos/{id:int}")]
        HttpRequest req, int id)
    {
        logger.LogInformation("DeleteTodo {Id}", id);
        TodoStore.AllowLoopbackCors(req);

        if (!TodoStore.Items.TryRemove(id, out _))
            return new NotFoundResult();

        return new NoContentResult();
    }
}
