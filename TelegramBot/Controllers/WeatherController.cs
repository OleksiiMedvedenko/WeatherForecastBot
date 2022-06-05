using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TelegramBot.Localization;
using TelegramBot.LocalizationFacade;
using TelegramBot.LocalizationFacade.Model;
using TelegramBot.Model.WeatherToDay;

namespace TelegramBot.Controllers
{
    class WeatherController
    {

        public static LocalizationInterface localization = new LocalizationInterface(new EnglishLocalization(), new PolishLocalization(), new UkrainianLocalization());
        public static WeatherResponce GetWeatherFromWebSite(decimal lat, decimal lon)
        {
            try
            {
                string urlAddress = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=alerts&units=metric&appid=94ec0cde62edeab74471251a77d69697"; // &lang=ua - автоматическое переведенная инфрмация с сайтф на укр
                
                string response;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                WeatherResponce todayWeatherResponce = JsonConvert.DeserializeObject<WeatherResponce>(response);

                return todayWeatherResponce;
            }
            catch
            {
                return null;
            }
        }


        public static string GetTodayWeatherInfo(WeatherResponce todayWeatherResponce)
        {
            return localization.DisplayInfoOnPolishOnToday(todayWeatherResponce);
        }

        public static string GetTomorrowWeatherInfo(WeatherResponce todayWeatherResponce)
        {
            if (todayWeatherResponce != null)
            {
                var day = DayOfWeekLocalization.DayLocalization(DateTime.Now.DayOfWeek);
                var dayForUkraine = DayOfWeekLocalization.DayLocalization(DateTime.Now.AddHours(1).DayOfWeek);
                var weatherDeskriptionOnUkrainianOnToday = WeatherDescriptionLocalization.GetWeatherDescriptionOnUkrainian(
                    todayWeatherResponce.Daily[1].Weather.ToList().FirstOrDefault().Description);


                //TODO: DateTime.Now.ToLocalTime() ? проверка
                return $"Прогноз погоди на: {DateTime.Now.AddDays(1).ToShortDateString()}📆\n" +
                    $"\nЧас України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {dayForUkraine}" +
                    $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {day}" +
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
                    $"\nОпис : {weatherDeskriptionOnUkrainianOnToday}";
            }

            return null;
        }

        public static string GetWeekWeatherInfo(WeatherResponce todayWeatherResponce)
        {
            StringBuilder Info = new StringBuilder();
            if (todayWeatherResponce != null)
            {
                for (int i = 1; i <= 7; i++)
                {
                    var day = DayOfWeekLocalization.DayLocalization(DateTime.Now.DayOfWeek);
                    var dayForUkraine = DayOfWeekLocalization.DayLocalization(DateTime.Now.AddHours(1).DayOfWeek);
                    var weatherDeskriptionOnUkrainianOnToday = WeatherDescriptionLocalization.GetWeatherDescriptionOnUkrainian(
                        todayWeatherResponce.Daily[i].Weather.ToList().FirstOrDefault().Description);


                    //TODO: DateTime.Now.ToLocalTime() ? проверка
                    var DayInfo = $"\n\nПрогноз погоди на: {DateTime.Now.AddDays(i).ToShortDateString()}📆\n" +
                        $"\nЧас України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {dayForUkraine}" +
                        $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {day}" +
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
                        $"\nОпис : {weatherDeskriptionOnUkrainianOnToday}\n" +
                        $"\n-----------------------------------";

                    Info.Append(DayInfo);
                }

                return Info.ToString();
            }

            return null;
        }
    }
}
