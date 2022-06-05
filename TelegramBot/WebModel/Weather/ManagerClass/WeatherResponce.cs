

namespace TelegramBot.Model.WeatherToDay
{
    public class WeatherResponce
    {
        public WeatherInfo[] Daily { get; set; }
        
        /// <summary>
        /// City / Region
        /// </summary>
        public string Timezone { get; set; }
        /// <summary>
        ///  strefa czasowa - a dokładniej dodanie czasu do UTC
        /// </summary>
        public double Timezone_offset { get; set; }
    }
}
