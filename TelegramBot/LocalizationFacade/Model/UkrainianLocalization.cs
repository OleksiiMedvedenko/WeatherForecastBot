using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Model.WeatherToDay;

namespace TelegramBot.LocalizationFacade.Model
{
    public class UkrainianLocalization
    {
        public string GetUkrUrlForNow(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=ua&appid=94ec0cde62edeab74471251a77d69697";
        }

        public string GetUkrhUrl(decimal lat, decimal lon)
        {
            return $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=alerts&lang=ua&units=metric&appid=94ec0cde62edeab74471251a77d69697";
        }

        public string DisplayInfoForNow(NowWeatherResponse nowWeatherResponse)
        {
            return $"Український час   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.AddHours(1).DayOfWeek}" +
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

        public string DisplayInfoOnToday(WeatherResponce todayWeatherResponce)
        {
            return $"Український час  🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.AddHours(1).DayOfWeek}" +
                $"\nCzas Warszawy 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                $"\n🌍🌎🌏" +
                $"\nКраїна: {todayWeatherResponce.Timezone} 🏙️" +
                $"\nЧас: {DateTime.UtcNow.AddSeconds(todayWeatherResponce.Timezone_offset).ToShortTimeString()} ⌚" +
                $"\nТемпература вранці: {Math.Round(todayWeatherResponce.Daily[0].Temp.Morn)}℃  🌡️ ☀️🕗" +
                $"\nТемпература в обід: {Math.Round(todayWeatherResponce.Daily[0].Temp.Day)}℃    🌡️ 🌞🕑" +
                $"\nТемпература ввечері: {Math.Round(todayWeatherResponce.Daily[0].Temp.Eve)}℃  🌡️ 🌙🕓" +
                $"\nТемпература в ночі: {Math.Round(todayWeatherResponce.Daily[0].Temp.Night)}℃     🌡️ 🌚🕙" +
                $"\nТиск: {todayWeatherResponce.Daily[0].Pressure} гПа ⏱️" +
                $"\nВологість: {todayWeatherResponce.Daily[0].Humidity}% 💦" +
                $"\nШвидкість вітру: {todayWeatherResponce.Daily[0].Wind_speed} м/с 💨" +
                $"\nХмарність: {todayWeatherResponce.Daily[0].Clouds} % 🌥️" +
                $"\nОпади: {todayWeatherResponce.Daily[0].Pop * 100}% 🌧️" +
                $"\nОпис : {todayWeatherResponce.Daily[0].Weather.ToList().FirstOrDefault().Description}";
        }

        public string DisplayInfoOnTomorrow(WeatherResponce todayWeatherResponce)
        {
            if (todayWeatherResponce != null)
            {
                return $"Прогноз погоди на: {DateTime.Now.AddDays(1).ToShortDateString()}📆\n" +
                    $"\nЧас України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.AddHours(1).DayOfWeek}" + // &??
                    $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                    $"\n🌍🌎🌏" +
                    $"\nРегіон: {todayWeatherResponce.Timezone} 🏙️" +
                    $"\nЧас: {DateTime.UtcNow.AddSeconds(todayWeatherResponce.Timezone_offset).ToShortTimeString()} ⌚" +
                    $"\nТемпература вранці: {Math.Round(todayWeatherResponce.Daily[1].Temp.Morn)}℃ 🌡️ ☀️🕗" +
                    $"\nТемпература вдень: {Math.Round(todayWeatherResponce.Daily[1].Temp.Day)}℃   🌡️ 🌞🕑" +
                    $"\nТемпература ввечері: {Math.Round(todayWeatherResponce.Daily[1].Temp.Eve)}℃ 🌡️ 🌙🕓" +
                    $"\nТемпература вночі: {Math.Round(todayWeatherResponce.Daily[1].Temp.Night)}℃    🌡️ 🌚🕙" +
                    $"\nТиск: {todayWeatherResponce.Daily[1].Pressure} гПа ⏱️" +
                    $"\nВологість: {todayWeatherResponce.Daily[1].Humidity}% 💦" +
                    $"\nШвидкість вітру: {todayWeatherResponce.Daily[1].Wind_speed} м/с 💨" +
                    $"\nХмарність: {todayWeatherResponce.Daily[1].Clouds} % 🌥️" +
                    $"\nЙмовірність опадів: {todayWeatherResponce.Daily[1].Pop * 100}% 🌧️" +
                    $"\nОпис : {todayWeatherResponce.Daily[1].Weather.ToList().FirstOrDefault().Description}";
            }

            return null;
        }

        public string DisplayInfoOnWeek(WeatherResponce todayWeatherResponce)
        {
            StringBuilder Info = new StringBuilder();
            if (todayWeatherResponce != null)
            {
                for (int i = 1; i <= 7; i++)
                {
                    var DayInfo = $"\n\nПрогноз погоди на: {DateTime.Now.AddDays(i).ToShortDateString()}📆\n" +
                        $"\nЧас України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {DateTime.Now.AddHours(1).DayOfWeek}" +
                        $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {DateTime.Now.DayOfWeek}" +
                        $"\n🌍🌎🌏" +
                        $"\nРегіон: {todayWeatherResponce.Timezone} 🏙️" +
                        $"\nЧас: {DateTime.UtcNow.AddSeconds(todayWeatherResponce.Timezone_offset).ToShortTimeString()} ⌚" +
                        $"\nТемпература вранці: {Math.Round(todayWeatherResponce.Daily[i].Temp.Morn)}℃   🌡️ ☀️🕗" +
                        $"\nТемпература вдень: {Math.Round(todayWeatherResponce.Daily[i].Temp.Day)}℃   🌡️ 🌞🕑" +
                        $"\nТемпература ввечері: {Math.Round(todayWeatherResponce.Daily[i].Temp.Eve)}℃  🌡️ 🌙🕓" +
                        $"\nТемпература вночі: {Math.Round(todayWeatherResponce.Daily[i].Temp.Night)}℃      🌡️ 🌚🕙" +
                        $"\nТиск: {todayWeatherResponce.Daily[i].Pressure} гПа ⏱️" +
                        $"\nВологість: {todayWeatherResponce.Daily[i].Humidity}% 💦" +
                        $"\nШвидкість вітру: {todayWeatherResponce.Daily[i].Wind_speed} м/с 💨" +
                        $"\nХмарність: {todayWeatherResponce.Daily[i].Clouds} % 🌥️" +
                        $"\nЙмовірність опадів: {todayWeatherResponce.Daily[i].Pop * 100}% 🌧️" +
                        $"\nОпис : {todayWeatherResponce.Daily[i].Weather.ToList().FirstOrDefault().Description}\n" +
                        $"\n-----------------------------------";

                    Info.Append(DayInfo);
                }

                return Info.ToString();
            }

            return null;
        }
    }
}
