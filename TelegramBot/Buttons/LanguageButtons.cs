using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Buttons
{
    public class LanguageButtons
    {
        internal static IReplyMarkup LanguageSelectionButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = $"Ukrainian" },  new KeyboardButton { Text = "Polish" } },
                }
            };
        }
    }
}
