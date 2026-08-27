using SCITest.Application.DTOs.Weather;
using SCITest.Infrastructure.ExternalServices.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.Handlers.Queries.Weather
{
    public class GetWeatherHandler
    {
        private readonly IWeatherService _weatherService;

        public GetWeatherHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<WeatherDto?> HandleAsync(
            string city,
            CancellationToken cancellationToken)
        {
            return await _weatherService.GetWeatherAsync(
                city,
                cancellationToken);
        }
    }
}
