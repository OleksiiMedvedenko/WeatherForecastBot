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
        public static WeatherResponce GetWeatherFromWebSite(string url)
        {
            try
            {
                string urlAddress = url;
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


        public static string GetTodayWeatherInfo(WeatherResponce todayWeatherResponce, bool language)
        {
            if (language == true)
            {
                return localization.DisplayInfoOnUkrOnToday(todayWeatherResponce);
            }
            return localization.DisplayInfoOnPolishOnToday(todayWeatherResponce);
        }

        public static string GetTomorrowWeatherInfo(WeatherResponce tomorrowWeatherResponce, bool language)
        {
            if (tomorrowWeatherResponce != null)
            {
                if (language == true)
                {
                    return localization.DisplayInfoOnUkraineOnTomorrow(tomorrowWeatherResponce);
                }

                return localization.DisplayInfoOnPolishOnTomorrow(tomorrowWeatherResponce);
            }

            return null;
        }

        public static string GetWeekWeatherInfo(WeatherResponce weekWeatherResponce, bool language)
        {
            if (language == true)
            {
                return localization.DisplayInfoOnUkraineOnWeek(weekWeatherResponce);
            }

            return localization.DisplayInfoOnPolishOnWeek(weekWeatherResponce);
        }
    }
}
