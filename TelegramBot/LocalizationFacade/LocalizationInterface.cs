using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.LocalizationFacade.Model;
using TelegramBot.Model.WeatherToDay;

namespace TelegramBot.LocalizationFacade
{
    public class LocalizationInterface
    {
        private EnglishLocalization _englishLocalization { get; set; }
        private PolishLocalization _polishLocalization { get; set; }
        private UkrainianLocalization _ukrainianLocalization { get; set; }

        public LocalizationInterface(EnglishLocalization englishLocalization, PolishLocalization polishLocalization, UkrainianLocalization ukrainianLocalization)
        {
            _englishLocalization = englishLocalization;
            _polishLocalization = polishLocalization;
            _ukrainianLocalization = ukrainianLocalization;
        }

        public string GetInfoOnEnglish(string city)
        {
            return _englishLocalization.GetUKUrlForNow(city);
        }
        public string DisplayInfoOnEnglish(NowWeatherResponse nowWeatherResponse)
        {
            return _englishLocalization.DisplayInfoForNow(nowWeatherResponse);
        }




        public string GetInfoOnPolish(string city)
        {
            return _polishLocalization.GetPolishUrlForNow(city);
        }
        public string GetInfoOnPolish(decimal lat, decimal lon)
        {
            return _polishLocalization.GetPolishUrl(lat, lon);
        }
        public string DisplayInfoOnPolish(NowWeatherResponse nowWeatherResponse)
        {
            return _polishLocalization.DisplayInfoForNow(nowWeatherResponse);
        }
        public string DisplayInfoOnPolishOnToday(WeatherResponce weatherResponse)
        {
            return _polishLocalization.DisplayInfoOnToday(weatherResponse);
        }
        public string DisplayInfoOnPolishOnTomorrow(WeatherResponce weatherResponce)
        {
            return _polishLocalization.DisplayInfoOnTomorrow(weatherResponce);
        }
        public string DisplayInfoOnPolishOnWeek(WeatherResponce weatherResponce)
        {
            return _polishLocalization.DisplayInfoOnWeek(weatherResponce);
        }




        public string GetnfoOnUkrainian(string city)
        {
            return _ukrainianLocalization.GetUkrUrlForNow(city);
        }
        public string GetnfoOnUkrainian(decimal lat, decimal lon)
        {
            return _ukrainianLocalization.GetUkrhUrl(lat, lon);
        }
        public string DisplayInfoOnUkraine(NowWeatherResponse nowWeatherResponse)
        {
            return _ukrainianLocalization.DisplayInfoForNow(nowWeatherResponse);
        }
        public string DisplayInfoOnUkrOnToday(WeatherResponce weatherResponse)
        {
            return _ukrainianLocalization.DisplayInfoOnToday(weatherResponse);
        }
        public string DisplayInfoOnUkraineOnTomorrow(WeatherResponce weatherResponce)
        {
            return _ukrainianLocalization.DisplayInfoOnTomorrow(weatherResponce);
        }
        public string DisplayInfoOnUkraineOnWeek(WeatherResponce weatherResponce)
        {
            return _ukrainianLocalization.DisplayInfoOnWeek(weatherResponce);
        }
    }
}
