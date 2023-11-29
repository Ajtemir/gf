using Microsoft.AspNetCore.Mvc;

namespace Application.IntegrationTests;

public static class OkObjectResultExtensions
{
    public static T GetObjectResultContent<T>(this ActionResult<T> result)
    {
        if (result.Result is not null)
        {
            var okObjectResult = ((OkObjectResult)(result.Result!));
            return (T)okObjectResult.Value!;
        }
        
        return result.Value!;
    }
}