

namespace TelegramBot.Model.WeatherToDay
{
    public class WeatherInfo
    {
        /// <summary>
        /// Temperature
        /// </summary>
        public TemperatureInfo Temp { get; set; }

        /// <summary>
        /// Cisniencie 
        /// </summary>
        public float Pressure { get; set; }

        /// <summary>
        /// wilgotność  
        /// </summary>
        public float Humidity { get; set; }

        public float Wind_speed { get; set; }

        public float Clouds { get; set; }

        /// <summary>
        /// Szansa na opady
        /// </summary>
        public float Pop { get; set; }

        public WeatherInfoDescription[] Weather { get; set; }

    }
}
