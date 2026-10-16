using ClothingStore.Api.Common;
using Microsoft.AspNetCore.Mvc;
using AppException = ClothingStore.Api.Common.ApplicationException;
namespace ClothingStore.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
    {
        try { return await action(); }
        catch (AppException ex)
        {
            return Problem(detail: ex.Message, statusCode: ex.Kind switch
            {
                ErrorKind.NotFound => StatusCodes.Status404NotFound,
                ErrorKind.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            });
        }
    }
}
