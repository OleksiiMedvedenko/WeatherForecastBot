using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.LocalizationFacade.Model
{
    public class PolishLocalization
    {
        public string GetInfo(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&lang=pl&appid=94ec0cde62edeab74471251a77d69697";
        }
    }
}
