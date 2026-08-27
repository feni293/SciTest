using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.DTOs.Weather
{
    public class WeatherDto
    {
        public string City { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Temperature { get; set; }

        public double WindSpeed { get; set; }

        public int WeatherCode { get; set; }

        public DateTime Time { get; set; }
    }
}
