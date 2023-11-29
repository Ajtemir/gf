using Application.Common.Dto;
using MediatR;

namespace Application.WeatherForecasts.Queries;

public class GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecast>>
{
    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastQuery request,
            CancellationToken cancellationToken)
        {
            var records = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-22, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });

            return Task.FromResult(records);
        }
    }
}