using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.LocalizationFacade.Model
{
    public class UkrainianLocalization
    {
        public string GetInfo(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=ua&appid=94ec0cde62edeab74471251a77d69697";
        }
    }
}
