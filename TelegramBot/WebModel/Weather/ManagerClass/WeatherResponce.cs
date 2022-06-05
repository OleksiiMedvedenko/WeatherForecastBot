

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
        /// часовой пояс - точнее прибавка времени к UTC
        /// </summary>
        public double Timezone_offset { get; set; }
    }
}
