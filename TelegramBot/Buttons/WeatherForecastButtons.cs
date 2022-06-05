using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Buttons
{
    class WeatherForecastButtons
    {
        internal static IReplyMarkup SelectingWeatherForecastButtonOnUkraine()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = $"Прогноз погоди на дану годину" },  new KeyboardButton { Text = "Прогноз погоди на сьогодні" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Прогноз погоди на завтра" }, new KeyboardButton { Text = "Прогноз погоди на сім днів" } },
                }
            };
        }

        internal static IReplyMarkup SelectingWeatherForecastButtonOnPolish()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = $"Prognoza pogody na daną godzinę" },  new KeyboardButton { Text = "Prognoza pogody na dziś" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Prognoza pogody na jutro" }, new KeyboardButton { Text = "Siedmiodniowa prognoza pogody" } },
                }
            };
        }
    }
}
