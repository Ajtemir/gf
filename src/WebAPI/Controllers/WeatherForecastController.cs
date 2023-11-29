using Application.Common.Dto;
using Application.WeatherForecasts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetWeatherForecastQuery(), cancellationToken));
    }
}