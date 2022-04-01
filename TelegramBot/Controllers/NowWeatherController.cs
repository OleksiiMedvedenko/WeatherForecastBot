using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using TelegramBot.Localization;

namespace TelegramBot
{
    static class NowWeatherController
    {
        
        public static NowWeatherResponse GetWeatherFromWebSite(string city)
        {
            try
            {
                string urlAddress = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=94ec0cde62edeab74471251a77d69697"; // &lang=ua - автоматическое переведенная инфрмация с сайтф на укр
                string response;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                NowWeatherResponse nowWeatherResponse = JsonConvert.DeserializeObject<NowWeatherResponse>(response);

                return nowWeatherResponse;
            }
            catch
            {
                return null;
            }
        }
         

        public static string GetWeatherInfoNow(NowWeatherResponse nowWeatherResponse)
        {
            if (nowWeatherResponse != null)
            {
                var day = DayOfWeekLocalization.DayLocalization(DateTime.Now.DayOfWeek);
                var dayForUkraine = DayOfWeekLocalization.DayLocalization(DateTime.Now.AddHours(1).DayOfWeek);
                var weatherDeskriptionOnUkrainian = WeatherDescriptionLocalization.GetWeatherDescriptionOnUkrainian(
                    nowWeatherResponse.Weather.ToList().FirstOrDefault().Description);

                //TODO: DateTime.Now.ToLocalTime() ? проверка
                return $"Час України   🇺🇦: {DateTime.Now.ToShortDateString()} | {DateTime.Now.AddHours(1).ToShortTimeString()}, {dayForUkraine}" +
                    $"\nЧас Варшави 🇵🇱: {DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}, {day}" +
                    $"\n🌍🌎🌏" +
                    $"\nМісто: {nowWeatherResponse.Name} 🏙️" +
                    $"\nКраїна: {nowWeatherResponse.Sys.Country} | Час: {DateTime.UtcNow.AddSeconds(nowWeatherResponse.Timezone).ToShortTimeString()} ⌚" +
                    $"\nТемпература: {Math.Round(nowWeatherResponse.Main.Temp)}℃ 🌡️" +
                    $"\nТиск: {nowWeatherResponse.Main.Pressure} гПа ⏱️" +
                    $"\nВологість: {nowWeatherResponse.Main.Humidity}% 💦" +
                    $"\nШвидкість вітру: {nowWeatherResponse.Wind.Speed} м/с 💨" +
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

        public static (decimal, decimal) GetCityCoords(NowWeatherResponse nowWeatherResponse)
        {
            return (nowWeatherResponse.Coord.Lat, nowWeatherResponse.Coord.Lon);
        }

    }
}
