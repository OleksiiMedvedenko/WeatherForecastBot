
using System.Text.Json.Serialization;

namespace TelegramBot
{
    public class NowWeatherInfo
    {
        //Переменные так написанны поскольку когда мы десириализируем данные , они могут записываться только в такие переменные - это сделал разработчик самого сайта с которого мы берем информацию

        public float Temp { get; set; }
        public float Temp_max { get; set; }
        public float Temp_min { get; set; }

        /// <summary>
        /// Cisniencie WeatherResponse => Main
        /// </summary>
        public float Pressure { get; set; }

        /// <summary>
        /// wilgotność  WeatherResponse => Main
        /// </summary>
        public float Humidity { get; set; }

        /// <summary>
        /// prędkość wiatru WeatherResponse => win
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// pochmurny! WeatherResponse => Clouds
        /// </summary>
        public float All { get; set; }

        /// <summary>
        /// WeatherResponse => Sys
        /// </summary>
        public string Country { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// długość geograficzna miasta
        /// </summary>
        public decimal Lon { get; set; }
        /// <summary>
        /// szerokość geograficzna miasta
        /// </summary>
        public decimal Lat { get; set; }

    }
}
