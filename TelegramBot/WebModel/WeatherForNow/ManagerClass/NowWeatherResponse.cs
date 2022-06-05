
namespace TelegramBot
{
    public class NowWeatherResponse
    {
        public NowWeatherInfo Main { get; set; }
        public NowWeatherInfo Wind { get; set; }
        public NowWeatherInfo Clouds { get; set; }
        public NowWeatherInfo[] Weather { get; set; }
        public NowWeatherInfo Coord { get; set; }

        /// <summary>
        /// ustawienia regionalne
        /// </summary>
        public NowWeatherInfo Sys { get; set; }
        /// <summary>
        /// city name
        /// </summary>
        public string Name { get; set; }

        public int Timezone { get; set; }
    }
}
