using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Buttons
{
    class CityButtons
    {
        internal static IReplyMarkup CitySelectionButton()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = $"Вроцлав" },  new KeyboardButton { Text = "Кропивницький" } },
                }
            };
        }
    }
}
