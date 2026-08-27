using SCITest.Application.DTOs.Weather;
using SCITest.Infrastructure.ExternalServices.Weather.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SCITest.Infrastructure.ExternalServices.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherServiceOption _options;

        public WeatherService(HttpClient httpClient, IOptions<WeatherServiceOption> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<WeatherDto?> GetWeatherAsync(string city, CancellationToken cancellationToken)
        {
            var location = await GetLocationAsync(city, cancellationToken);

            if (location is null)
                return null;

            var weather = await GetCurrentWeatherAsync(location.Latitude, location.Longitude, cancellationToken);

            if (weather?.Current is null)
                return null;

            return new WeatherDto
            {
                City = location.Name,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Temperature = weather.Current.Temperature2m,
                WindSpeed = weather.Current.WindSpeed10m,
                WeatherCode = weather.Current.WeatherCode,
                Time = weather.Current.Time
            };
        }

        private async Task<GeocodingResult?> GetLocationAsync(string city, CancellationToken cancellationToken)
        {
            var url =
                $"{_options.GeocodingBaseUrl}/v1/search" +
                $"?name={Uri.EscapeDataString(city)}" +
                "&count=1" +
                "&language=en" +
                "&format=json";

            var response = await _httpClient.GetFromJsonAsync<GeocodingResponse>(url, cancellationToken);

            return response?.Results?.FirstOrDefault();
        }

        private async Task<WeatherResponse?> GetCurrentWeatherAsync(double latitude, double longitude, CancellationToken cancellationToken)
        {
            var url =
                $"{_options.ForecastBaseUrl}/v1/forecast" +
                $"?latitude={latitude.ToString(CultureInfo.InvariantCulture)}" +
                $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}" +
                "&current=temperature_2m,wind_speed_10m,weather_code" +
                "&timezone=auto";

            return await _httpClient.GetFromJsonAsync<WeatherResponse>(url, cancellationToken);
        }
    }
}
