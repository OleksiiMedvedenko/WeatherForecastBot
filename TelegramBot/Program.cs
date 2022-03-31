using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        private static string Token { get; set; } = "5226846542:AAGsUUU6NnpAykz8UZKJLKPXlK_f9qhDIRE";

        private static TelegramBotClient client;
        private static OneDayWeatherResponse weatherResponse;

        [Obsolete]
        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message.Text != null)
            {
                Console.WriteLine($"Message was received from [{message.Chat.Username}] [{DateTime.Now.ToShortTimeString()}] : {message.Text}");

                switch (message.Text)
                {
                    case "Wroclaw":
                        weatherResponse = OneDayWeatherController.GetWeatherFromWebSite("Wroclaw");
                        var Wroclaw = await client.SendTextMessageAsync(message.Chat.Id, OneDayWeatherController.GetWeatherInfo(weatherResponse),
                            replyToMessageId: message.MessageId, replyMarkup: GetButtons());
                        break;

                    case "Kropyvnytskyi":
                        weatherResponse = OneDayWeatherController.GetWeatherFromWebSite("Kropyvnytskyi");
                        var Kropyvnytskyi = await client.SendTextMessageAsync(message.Chat.Id, OneDayWeatherController.GetWeatherInfo(weatherResponse),
                            replyToMessageId: message.MessageId, replyMarkup: GetButtons());
                        break;

                    case "Послать русский корабль на х*й":
                        var str = await client.SendTextMessageAsync(message.Chat.Id, "Ruskiy vojenyj korabl - idi naxyj!", replyMarkup: GetButtons());

                        var pic = await client.SendPhotoAsync(chatId: message.Chat.Id,
                            photo: "https://i1.sndcdn.com/artworks-1nniBHIeZ6z5fYnS-AzbPvg-t500x500.jpg",
                            replyMarkup: GetButtons());
                        break;

                    default:
                        weatherResponse = OneDayWeatherController.GetWeatherFromWebSite(message.Text);

                        if (weatherResponse!=null)
                        {
                            var anyCity = await client.SendTextMessageAsync(message.Chat.Id, OneDayWeatherController.GetWeatherInfo(weatherResponse),
                            replyToMessageId: message.MessageId, replyMarkup: GetButtons());

                            break;
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!", replyMarkup: GetButtons());
                        }

                        await client.SendTextMessageAsync(message.Chat.Id, "Оберіть ваше місто, або напишіть його самостійно на будь якій мові", 
                            replyMarkup: GetButtons());
                        break;

                        //var sticker = await client.SendStickerAsync(
                        //    chatId: message.Chat.Id,
                        //    sticker: "https://cdn.tlgrm.app/stickers/96b/f1e/96bf1eca-a75d-3b7c-b620-bb5f2cdac89f/192/1.webp",
                        //    replyToMessageId: message.MessageId,
                        //    replyMarkup: GetButtons());

                        //var pic = await client.SendPhotoAsync(chatId: message.Chat.Id,
                        //    photo: "https://scontent-vie1-1.xx.fbcdn.net/v/t39.30808-6/276262322_5303012999730593_2356718401733600899_n.jpg?_nc_cat=104&ccb=1-5&_nc_sid=09cbfe&_nc_ohc=zaCkZdsGqKUAX-FaZr0&tn=HZR5UL9zvrEjBtMF&_nc_ht=scontent-vie1-1.xx&oh=00_AT_Bz9bhTfCQTOENnax_o-QBrmC5wMLwHs_8_GtgKVwT2g&oe=6248AA28",
                        //    replyMarkup: GetButtons());
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = $"Wroclaw" },  new KeyboardButton { Text = "Kropyvnytskyi" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Послать русский корабль на х*й" } }
                }
            };
        }
    }
}
