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
            return $"Czas Ukraine   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                $"\nCzas Warszawy 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                $"\n🌍🌎🌏" +
                $"\nKraj: {todayWeatherResponce.Timezone} 🏙️" +
                $"\nGodzinę: {DateTime.UtcNow.AddSeconds(todayWeatherResponce.Timezone_offset).ToShortTimeString()} ⌚" +
                $"\nTemperatura rano: {Math.Round(todayWeatherResponce.Daily[0].Temp.Morn)}℃  🌡️ ☀️🕗" +
                $"\nTemperatura w południu: {Math.Round(todayWeatherResponce.Daily[0].Temp.Day)}℃    🌡️ 🌞🕑" +
                $"\nTemperatura wieczorem: {Math.Round(todayWeatherResponce.Daily[0].Temp.Eve)}℃  🌡️ 🌙🕓" +
                $"\nTemperatura w nocy: {Math.Round(todayWeatherResponce.Daily[0].Temp.Night)}℃     🌡️ 🌚🕙" +
                $"\nCisnienie: {todayWeatherResponce.Daily[0].Pressure} гПа ⏱️" +
                $"\nWilgotność: {todayWeatherResponce.Daily[0].Humidity}% 💦" +
                $"\nPrędkość wiatru: {todayWeatherResponce.Daily[0].Wind_speed} м/с 💨" +
                $"\nZachmurzenie: {todayWeatherResponce.Daily[0].Clouds} % 🌥️" +
                $"\nSzansa na opady: {todayWeatherResponce.Daily[0].Pop * 100}% 🌧️" +
                $"\nOpis : {todayWeatherResponce.Daily[0].Weather.ToList().FirstOrDefault().Description}";
        }


    }
}
