using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Buttons
{
    class WeatherForecastButtons
    {
        internal static IReplyMarkup SelectingWeatherForecastButton()
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
    }
}
