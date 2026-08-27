using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Infrastructure.ExternalServices.Weather
{
    public class WeatherServiceOption
    {
        public string GeocodingBaseUrl { get; set; } = string.Empty;
        public string ForecastBaseUrl { get; set; } = string.Empty;
    }
}
