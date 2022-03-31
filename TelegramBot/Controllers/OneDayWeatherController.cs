using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Localization;

namespace TelegramBot
{
    static class OneDayWeatherController
    {
        
        public static OneDayWeatherResponse GetWeatherFromWebSite(string city)
        {
            try
            {
                string urlAddress = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=94ec0cde62edeab74471251a77d69697"; // &lang=ua - автоматическое переведенная инфрмация с сайтф на укр
                string response;

                string testUrl = "https://api.openweathermap.org/data/2.5/onecall?lat=50,4333&lon=30,5167&exclude=alerts&appid=94ec0cde62edeab74471251a77d69697";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                OneDayWeatherResponse weatherResponse = JsonConvert.DeserializeObject<OneDayWeatherResponse>(response);

                return weatherResponse;
            }
            catch
            {
                return null;
            }
        }
         

        public static string GetWeatherInfo(OneDayWeatherResponse weatherResponse)
        {
            if (weatherResponse != null)
            {
                var day = DayOfWeekLocalization.DayLocalization(DateTime.Now.DayOfWeek);
                var dayForUkraine = DayOfWeekLocalization.DayLocalization(DateTime.Now.AddHours(1).DayOfWeek);
                var weatherDeskriptionOnUkrainian = WeatherDescriptionLocalization.GetWeatherDescriptionOnUkrainian(
                    weatherResponse.Weather.ToList().FirstOrDefault().Description);

                //TODO: DateTime.Now.ToLocalTime() ? проверка
                return $"Час України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {dayForUkraine}" +
                    $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {day}\n" +
                    $"\nМісто: {weatherResponse.Name} 🏙️" +
                    $"\nКраїна: {weatherResponse.Sys.Country} | Час: {DateTime.UtcNow.AddSeconds(weatherResponse.Timezone).ToShortTimeString()} 🕣" +
                    $"\nТемпература: {Math.Round(weatherResponse.Main.Temp)}℃ 🌡️" +
                    $"\nТиск: {weatherResponse.Main.Pressure} гПа ⏱️" +
                    $"\nВологість: {weatherResponse.Main.Humidity}% 💦" +
                    $"\nШвидкість вітру: {weatherResponse.Wind.Speed} м/с 💨" +
                    $"\nОпис : {weatherDeskriptionOnUkrainian}";

                #region Nenyzhnuj kod
                //string clouds = string.Empty;
                //if (weatherResponse.Clouds.All <= 30)
                //{
                //    clouds = "Ясно";
                //}
                //else if (weatherResponse.Clouds.All > 30 && weatherResponse.Clouds.All <= 98)
                //{
                //    clouds = "Похмуро";
                //}
                //else if (weatherResponse.Clouds.All > 98)
                //{
                //    clouds = "Хмарно";
                //}

                //$"\nХмарність: {clouds}" +
                //    $"\nМаксимальна температура: {Math.Round(weatherResponse.Main.Temp_max)} ℃" +
                //    $"\nМінімальна температура: {Math.Round(weatherResponse.Main.Temp_min)} ℃" +
                #endregion
            }

            return null;
        }

    }
}
