using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SCITest.Infrastructure.ExternalServices.Weather.Models
{
    public class WeatherResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CurrentWeather? Current { get; set; }
    }

    public class CurrentWeather
    {
        public DateTime Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public double Temperature2m { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed10m { get; set; }
        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }
}
