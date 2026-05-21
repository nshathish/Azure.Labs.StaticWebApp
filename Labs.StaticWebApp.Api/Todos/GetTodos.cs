using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Labs.StaticWebApp.Api.Todos;

public class GetTodos(ILogger<GetTodos> logger)
{
    [Function(nameof(GetTodos))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos")]
        HttpRequest req)
    {
        logger.LogInformation("GetTodos");
        TodoStore.AllowLoopbackCors(req);
        return new OkObjectResult(TodoStore.Items.Values.OrderBy(t => t.Id).ToArray());
    }
}
