

namespace TelegramBot.Model.WeatherToDay
{
    public class WeatherInfo
    {
        /// <summary>
        /// Температура
        /// </summary>
        public TemperatureInfo Temp { get; set; }

        /// <summary>
        /// давление 
        /// </summary>
        public float Pressure { get; set; }

        /// <summary>
        /// влажность  
        /// </summary>
        public float Humidity { get; set; }

        /// <summary>
        /// скорость ветра 
        /// </summary>
        public float Wind_speed { get; set; }

        /// <summary>
        /// облачность!
        /// </summary>
        public float Clouds { get; set; }

        /// <summary>
        /// Вероятность осадков
        /// </summary>
        public float Pop { get; set; }

        public WeatherInfoDescription[] Weather { get; set; }

    }
}
