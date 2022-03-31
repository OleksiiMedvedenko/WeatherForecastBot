using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    class OneDayWeatherInfo
    {
        //Переменные так написанны поскольку когда мы десириализируем данные , они могут записываться только в такие переменные - это сделал разработчик самого сайта с которого мы берем информацию

        public float Temp { get; set; }
        public float Temp_max { get; set; }
        public float Temp_min { get; set; }

        /// <summary>
        /// давление WeatherResponse => Main
        /// </summary>
        public float Pressure { get; set; }

        /// <summary>
        /// влажность  WeatherResponse => Main
        /// </summary>
        public float Humidity { get; set; }

        /// <summary>
        /// скорость ветра WeatherResponse => win
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// облачность! WeatherResponse => Clouds
        /// </summary>
        public float All { get; set; }

        /// <summary>
        /// WeatherResponse => Sys
        /// </summary>
        public string Country { get; set; }

        public string Description { get; set; }

    }
}
