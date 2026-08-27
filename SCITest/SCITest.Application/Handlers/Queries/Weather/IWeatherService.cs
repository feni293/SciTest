using SCITest.Application.DTOs.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Infrastructure.ExternalServices.Weather
{
    public interface IWeatherService
    {
        Task<WeatherDto?> GetWeatherAsync(string city, CancellationToken cancellationToken);
    }
}
