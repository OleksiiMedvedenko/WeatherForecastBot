using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Model.WeatherToDay;

namespace TelegramBot.LocalizationFacade.Model
{
    public class PolishLocalization
    {
        public string GetPolishUrlForNow(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=pl&appid=94ec0cde62edeab74471251a77d69697";
        }

        public string GetPolishUrl(decimal lat, decimal lon)
        {
            return $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=alerts&lang=pl&units=metric&appid=94ec0cde62edeab74471251a77d69697";
        }

        public string DisplayInfoForNow(NowWeatherResponse nowWeatherResponse)
        {
            return $"Czas Ukraine   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\nCzas Warszawy 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\n🌍🌎🌏" +
                    $"\nMiasto: {nowWeatherResponse.Name} 🏙️" +
                    $"\nKraj: {nowWeatherResponse.Sys.Country} | Час: {DateTime.UtcNow.AddSeconds(nowWeatherResponse.Timezone).ToShortTimeString()} ⌚" +
                    $"\nTemperatura: {Math.Round(nowWeatherResponse.Main.Temp)}℃ 🌡️" +
                    $"\nCisnienie: {nowWeatherResponse.Main.Pressure} гПа ⏱️" +
                    $"\nWilgotność: {nowWeatherResponse.Main.Humidity}% 💦" +
                    $"\nPrędkość wiatru: {nowWeatherResponse.Wind.Speed} м/с 💨" +
                    $"\nOpis : {nowWeatherResponse.Weather.ToList().FirstOrDefault().Description}";
        }

        public string DisplayInfoOnToday(WeatherResponce todayWeatherResponce)
        {
            return $"Час України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                $"\n🌍🌎🌏" +
                $"\nРегіон: {todayWeatherResponce.Timezone} 🏙️" +
                $"\nЧас: {DateTime.UtcNow.AddSeconds(todayWeatherResponce.Timezone_offset).ToShortTimeString()} ⌚" +
                $"\nТемпература вранці: {Math.Round(todayWeatherResponce.Daily[0].Temp.Morn)}℃  🌡️ ☀️🕗" +
                $"\nТемпература вдень: {Math.Round(todayWeatherResponce.Daily[0].Temp.Day)}℃    🌡️ 🌞🕑" +
                $"\nТемпература ввечері: {Math.Round(todayWeatherResponce.Daily[0].Temp.Eve)}℃  🌡️ 🌙🕓" +
                $"\nТемпература вночі: {Math.Round(todayWeatherResponce.Daily[0].Temp.Night)}℃     🌡️ 🌚🕙" +
                $"\nТиск: {todayWeatherResponce.Daily[0].Pressure} гПа ⏱️" +
                $"\nВологість: {todayWeatherResponce.Daily[0].Humidity}% 💦" +
                $"\nШвидкість вітру: {todayWeatherResponce.Daily[0].Wind_speed} м/с 💨" +
                $"\nХмарність: {todayWeatherResponce.Daily[0].Clouds} % 🌥️" +
                $"\nЙмовірність опадів: {todayWeatherResponce.Daily[0].Pop * 100}% 🌧️" +
                $"\nОпис : {todayWeatherResponce.Daily[0].Weather.ToList().FirstOrDefault().Description}";
        }


    }
}
