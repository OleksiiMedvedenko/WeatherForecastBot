using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.LocalizationFacade.Model;

namespace TelegramBot.LocalizationFacade
{
    public class Localization
    {
        private EnglishLocalization _englishLocalization { get; set; }
        private PolishLocalization _polishLocalization { get; set; }
        private UkrainianLocalization _ukrainianLocalization { get; set; }

        public Localization(EnglishLocalization englishLocalization, PolishLocalization polishLocalization, UkrainianLocalization ukrainianLocalization)
        {
            _englishLocalization = englishLocalization;
            _polishLocalization = polishLocalization;
            _ukrainianLocalization = ukrainianLocalization;
        }

        public string GetInfoOnEnglish(string city)
        {
            return _englishLocalization.GetInfo(city);
        }

        public string GetInfoOnPolish(string city)
        {
            return _polishLocalization.GetInfo(city);
        }

        public string GetnfoOnUkrainian(string city)
        {
            return _ukrainianLocalization.GetInfo(city);
        }
    }
}
