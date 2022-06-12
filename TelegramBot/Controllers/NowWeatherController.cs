using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using TelegramBot.Localization;
using TelegramBot.LocalizationFacade;
using TelegramBot.LocalizationFacade.Model;

namespace TelegramBot
{
    static class NowWeatherController
    {
        public static LocalizationInterface localization = new LocalizationInterface(new EnglishLocalization(), new PolishLocalization(), new UkrainianLocalization());
        public static NowWeatherResponse GetWeatherFromWebSite(string city, string url)
        {
            try
            {
                var urlAddress = url;
                string response;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlAddress.ToString());
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                NowWeatherResponse nowWeatherResponse = JsonConvert.DeserializeObject<NowWeatherResponse>(response);

                return nowWeatherResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
         

        public static string GetWeatherInfoNow(NowWeatherResponse nowWeatherResponse, bool language)
        {
            if (language == true)
            {
                return localization.DisplayInfoOnUkraine(nowWeatherResponse);
            }
            return localization.DisplayInfoOnPolish(nowWeatherResponse);
        }

        public static (decimal, decimal) GetCityCoords(NowWeatherResponse nowWeatherResponse)
        {
            if (nowWeatherResponse != null)
            {
                return (nowWeatherResponse.Coord.Lat, nowWeatherResponse.Coord.Lon);
            }

            return (0, 0);
        }
    }
}
