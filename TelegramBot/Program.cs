﻿using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Buttons;
using TelegramBot.Controllers;
using TelegramBot.LocalizationFacade;
using TelegramBot.LocalizationFacade.Model;
using TelegramBot.Model.WeatherToDay;

namespace TelegramBot
{
    class Program
    {
        private static string Token { get; set; } = "5226846542:AAGsUUU6NnpAykz8UZKJLKPXlK_f9qhDIRE";

        public static LocalizationInterface localization = new LocalizationInterface(new EnglishLocalization(), new PolishLocalization(), new UkrainianLocalization());
        
        private static TelegramBotClient _client;
        private static NowWeatherResponse _nowWeatherResponse;
        private static WeatherResponce _weatherResponce;

        private static bool choiceLanguage;

        private static string _languageDataEntry = string.Empty;
        private static string _forecastDateEntry = string.Empty;

        private static int indexer = 0;

        [Obsolete]
        static void Main(string[] args)
        {
            _client = new TelegramBotClient(Token);
            _client.StartReceiving(new UpdateType[] { UpdateType.Message });
            _client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            _client.StopReceiving();
        }

        // - каткое описание работы этого метода, есть некий цикл While сперва от телеграмма мы получем
        // сообщение какую погоду нам нужно получить, затем мы записываем значение в переменную (_choiceDay) чтобы сохранить наш выбор, 
        // ведь дальше мы запрашивае город в котором хотим узнать погоду 
        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var messageFromTG = e.Message;

            if (_languageDataEntry == "")
            {
                _languageDataEntry = messageFromTG.Text;
            }

            if (messageFromTG.Text != null)
            {
                Console.WriteLine($"Message was received from [{messageFromTG.Chat.Username}] [{DateTime.Now.ToShortTimeString()}] : {messageFromTG.Text} - {messageFromTG.From.Id} {messageFromTG.From.LanguageCode}");

                switch (_languageDataEntry)
                {
                    case "Ukrainian":

                        choiceLanguage = true;
                        if (_languageDataEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Виберіть час, на який ви хочете отримати прогноз погоди",
                                replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());
                        }

                        if (!messageFromTG.Text.Equals($"Ukrainian") && !messageFromTG.Text.Equals("Polish")) // что бы код не выполнился когда мы будем вводить какую погоду мы хотим получить, без этой проверки краш приложения - исправить- сделать по нормальному  !
                        {
                            if (indexer >= 2)
                            {
                                indexer++;
                                _languageDataEntry = string.Empty;
                                indexer = 0;
                            }

                            GetTimeForecast(messageFromTG);
                        }
                        break;

                    case "Polish":

                        choiceLanguage = false;

                        if (_languageDataEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Wybierz czas, dla którego chcesz uzyskać prognozę pogody",
                                replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnPolish());
                        }

                        if (!messageFromTG.Text.Equals($"Polish") && !messageFromTG.Text.Equals("Ukrainian"))
                        {

                            if (indexer >= 2)
                            {
                                indexer++;
                                _languageDataEntry = string.Empty;
                                indexer = 0;
                            }

                            GetTimeForecast(messageFromTG);
                        }
                        break;

                    default:
                        _languageDataEntry = string.Empty;
                        await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Command not found",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                        break;
                }
            }
        }

        [Obsolete]
        private static async void GetTimeForecast(Message message)
        {
            var messageFromTG = message;

            if (_forecastDateEntry == "")
            {
                _forecastDateEntry = messageFromTG.Text;
            }

            if (messageFromTG.Text != null)
            {
                Console.WriteLine($"Message was received from [{messageFromTG.Chat.Username}] [{DateTime.Now.ToShortTimeString()}] : {messageFromTG.Text} - {messageFromTG.From.Id} {messageFromTG.From.LanguageCode}");

                switch (_forecastDateEntry)
                {
                    case "Прогноз погоди на дану годину":

                        if (_forecastDateEntry == messageFromTG.Text) // избегаем повторения, без єтой проверки код будет обрабатываться два раза - исправить- сделать по нормальному  !
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Оберіть ваше місто, або напишіть його самостійно на будь якій мові",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Прогноз погоди на дану годину") && !messageFromTG.Text.Equals("Прогноз погоди на завтра")
                            && !messageFromTG.Text.Equals("Прогноз погоди на сім днів") && !messageFromTG.Text.Equals("Прогноз погоди на сьогодні")) // что бы код не выполнился когда мы будем вводить какую погоду мы хотим получить, без этой проверки краш приложения - исправить- сделать по нормальному  !
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForNow(messageFromTG);
                        }
                        break;

                    case "Прогноз погоди на сьогодні":
                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Оберіть ваше місто, або напишіть його самостійно на будь якій мові",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Прогноз погоди на дану годину") && !messageFromTG.Text.Equals("Прогноз погоди на завтра")
                            && !messageFromTG.Text.Equals("Прогноз погоди на сім днів") && !messageFromTG.Text.Equals("Прогноз погоди на сьогодні"))
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForToday(messageFromTG);
                        }
                        break;

                    case "Прогноз погоди на завтра":

                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Оберіть ваше місто, або напишіть його самостійно на будь якій мові",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Прогноз погоди на дану годину") && !messageFromTG.Text.Equals("Прогноз погоди на завтра")
                            && !messageFromTG.Text.Equals("Прогноз погоди на сім днів") && !messageFromTG.Text.Equals("Прогноз погоди на сьогодні"))
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForTomorrow(messageFromTG);
                        }
                        break;

                    case "Прогноз погоди на сім днів":

                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Оберіть ваше місто, або напишіть його самостійно на будь якій мові",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Прогноз погоди на дану годину") && !messageFromTG.Text.Equals("Прогноз погоди на завтра")
                            && !messageFromTG.Text.Equals("Прогноз погоди на сім днів") && !messageFromTG.Text.Equals("Прогноз погоди на сьогодні"))
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForWeek(messageFromTG);
                        }

                        break;

                    case "Prognoza pogody na daną godzinę":

                        if (_forecastDateEntry == messageFromTG.Text) // избегаем повторения, без єтой проверки код будет обрабатываться два раза - исправить- сделать по нормальному  !
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Wybierz swoje miasto lub napisz w dowolnym języku",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Prognoza pogody na daną godzinę") && !messageFromTG.Text.Equals("Prognoza pogody na jutro")
                            && !messageFromTG.Text.Equals("Siedmiodniowa prognoza pogody") && !messageFromTG.Text.Equals("Prognoza pogody na dziś")) // что бы код не выполнился когда мы будем вводить какую погоду мы хотим получить, без этой проверки краш приложения - исправить- сделать по нормальному  !
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForNow(messageFromTG);
                        }
                        break;

                    case "Prognoza pogody na dziś":
                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Wybierz swoje miasto lub napisz w dowolnym języku",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Prognoza pogody na daną godzinę") && !messageFromTG.Text.Equals("Prognoza pogody na jutro")
                           && !messageFromTG.Text.Equals("Siedmiodniowa prognoza pogody") && !messageFromTG.Text.Equals("Prognoza pogody na dziś"))
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForToday(messageFromTG);
                        }
                        break;

                    case "Prognoza pogody na jutro":

                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Wybierz swoje miasto lub napisz w dowolnym języku",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Prognoza pogody na daną godzinę") && !messageFromTG.Text.Equals("Prognoza pogody na jutro")
                           && !messageFromTG.Text.Equals("Siedmiodniowa prognoza pogody") && !messageFromTG.Text.Equals("Prognoza pogody na dziś"))
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForTomorrow(messageFromTG);
                        }
                        break;

                    case "Siedmiodniowa prognoza pogody":

                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Wybierz swoje miasto lub napisz w dowolnym języku",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Prognoza pogody na daną godzinę") && !messageFromTG.Text.Equals("Prognoza pogody na jutro")
                           && !messageFromTG.Text.Equals("Siedmiodniowa prognoza pogody") && !messageFromTG.Text.Equals("Prognoza pogody na dziś"))
                        {
                            _forecastDateEntry = string.Empty;
                            GetWeatherForecastForWeek(messageFromTG);
                        }

                        break;

                    default:
                        _forecastDateEntry = string.Empty;
                        await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Command not found",
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());
                        break;
                }
            }
        }

        private static async void GetWeatherForecastForNow(Message message)
        {
            string urlString = String.Empty;
            

            switch (message.Text)
            {
                case "Wroclaw":

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Wroclaw", urlString);
                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, NowWeatherController.GetWeatherInfoNow(_nowWeatherResponse, choiceLanguage), 
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";
                    return;

                case "Kiev":

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Kiev", urlString);
                    var Kropyvnytskyi = await _client.SendTextMessageAsync(message.Chat.Id, NowWeatherController.GetWeatherInfoNow(_nowWeatherResponse, choiceLanguage),
                         replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";
                    break;

                default:

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text, urlString);

                    if (_nowWeatherResponse != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, NowWeatherController.GetWeatherInfoNow(_nowWeatherResponse, choiceLanguage),
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Check if the entered city name is correct!", 
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    break;
            }
        }

        private static async void GetWeatherForecastForToday(Message message)
        {

            switch (message.Text)
            {
                case "Вроцлав":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Вроцлав");
                    (decimal, decimal) coordsWRO = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsWRO.Item1, coordsWRO.Item2);

                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());

                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsKROP.Item1, coordsKROP.Item2);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());
                    break;

                default:

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsAnyCity.Item1, coordsAnyCity.Item2);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    break;
            }
        }

        private static async void GetWeatherForecastForTomorrow(Message message)
        {

            switch (message.Text)
            {
                case "Вроцлав":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Вроцлав");
                    (decimal, decimal) coordsWRO = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsWRO.Item1, coordsWRO.Item2);

                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());

                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsKROP.Item1, coordsKROP.Item2);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());
                    break;

                default:

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsAnyCity.Item1, coordsAnyCity.Item2);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    break;
            }
        }

        private static async void GetWeatherForecastForWeek(Message message)
        {

            switch (message.Text)
            {
                case "Вроцлав":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Вроцлав");
                    (decimal, decimal) coordsWRO = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsWRO.Item1, coordsWRO.Item2);

                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());

                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsKROP.Item1, coordsKROP.Item2);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());
                    break;

                default:

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsAnyCity.Item1, coordsAnyCity.Item2);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButtonOnUkraine());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    break;
            }
        }
    }
}
