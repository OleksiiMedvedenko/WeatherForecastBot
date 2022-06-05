using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.LocalizationFacade.Model
{
    public class UkrainianLocalization
    {
        public string GetUkrUrlForNow(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=ua&appid=94ec0cde62edeab74471251a77d69697";
        }

        public string DisplayInfoForNow(NowWeatherResponse nowWeatherResponse)
        {
            return $"Український час   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\nВаршавський час 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\n🌍🌎🌏" +
                    $"\nМісто: {nowWeatherResponse.Name} 🏙️" +
                    $"\nКраїна: {nowWeatherResponse.Sys.Country} | Час: {DateTime.UtcNow.AddSeconds(nowWeatherResponse.Timezone).ToShortTimeString()} ⌚" +
                    $"\nТемпература: {Math.Round(nowWeatherResponse.Main.Temp)}℃ 🌡️" +
                    $"\nТиск: {nowWeatherResponse.Main.Pressure} гПа ⏱️" +
                    $"\nВологість повітря: {nowWeatherResponse.Main.Humidity}% 💦" +
                    $"\nШвидкість вітру: {nowWeatherResponse.Wind.Speed} м/с 💨" +
                    $"\nОпис : {nowWeatherResponse.Weather.ToList().FirstOrDefault().Description}";
        }
    }
}
