using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.LocalizationFacade.Model
{
    public class EnglishLocalization
    {
        public string GetUKUrlForNow(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=uk&appid=94ec0cde62edeab74471251a77d69697";
        }



        public string DisplayInfoForNow(NowWeatherResponse nowWeatherResponse)
        {
            return $"Ukraine Time   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\nWarsaw Time 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\n🌍🌎🌏" +
                    $"\nCity: {nowWeatherResponse.Name} 🏙️" +
                    $"\nCountry: {nowWeatherResponse.Sys.Country} | Час: {DateTime.UtcNow.AddSeconds(nowWeatherResponse.Timezone).ToShortTimeString()} ⌚" +
                    $"\nTemperature: {Math.Round(nowWeatherResponse.Main.Temp)}℃ 🌡️" +
                    $"\nPressure: {nowWeatherResponse.Main.Pressure} гПа ⏱️" +
                    $"\nHumidity: {nowWeatherResponse.Main.Humidity}% 💦" +
                    $"\nWind speed: {nowWeatherResponse.Wind.Speed} м/с 💨" +
                    $"\nDescription : {nowWeatherResponse.Weather.ToList().FirstOrDefault().Description}";
        }
    }
}
