using System;
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

        // - krótki opis działania tej metody, jest pętla While, najpierw otrzymamy z telegramu
        // wiadomość jaką pogodę potrzebujemy, następnie wpisujemy wartość do zmiennej (_choiceDay) aby zapamiętać nasz wybór,
        // w końcu pytamy o miasto, w którym chcemy poznać pogodę
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

                        if (!messageFromTG.Text.Equals($"Ukrainian") && !messageFromTG.Text.Equals("Polish")) // żeby kod nie był wykonywany, gdy wpiszemy jaką pogodę chcemy uzyskać, bez tego sprawdzenia aplikacja wywala - napraw to - zrób to jak zwykle !
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

                        if (_forecastDateEntry == messageFromTG.Text)
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Оберіть ваше місто, або напишіть його самостійно на будь якій мові",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Прогноз погоди на дану годину") && !messageFromTG.Text.Equals("Прогноз погоди на завтра")
                            && !messageFromTG.Text.Equals("Прогноз погоди на сім днів") && !messageFromTG.Text.Equals("Прогноз погоди на сьогодні"))
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

                        if (_forecastDateEntry == messageFromTG.Text) 
                        {
                            var now = await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Wybierz swoje miasto lub napisz w dowolnym języku",
                                replyMarkup: CityButtons.CitySelectionButton());
                        }

                        if (!messageFromTG.Text.Equals("Prognoza pogody na daną godzinę") && !messageFromTG.Text.Equals("Prognoza pogody na jutro")
                            && !messageFromTG.Text.Equals("Siedmiodniowa prognoza pogody") && !messageFromTG.Text.Equals("Prognoza pogody na dziś")) 
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

                    _languageDataEntry = "";

                    break;
            }
        }

        private static async void GetWeatherForecastForToday(Message message)
        {
            string urlForCity = String.Empty;
            string urlString = String.Empty;
            switch (message.Text)
            {
                case "Wroclaw":

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Wroclaw", urlForCity);
                    (decimal, decimal) coordsWRO = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsWRO.Item1, coordsWRO.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsWRO.Item1, coordsWRO.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";

                    return;

                case "Kiev":

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Kiev", urlForCity);
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);


                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsKROP.Item1, coordsKROP.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsKROP.Item1, coordsKROP.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";
                    break;

                default:

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text, urlForCity);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsAnyCity.Item1, coordsAnyCity.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsAnyCity.Item1, coordsAnyCity.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Check if the entered city name is correct!",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    _languageDataEntry = "";

                    break;
            }
        }

        private static async void GetWeatherForecastForTomorrow(Message message)
        {
            string urlString = String.Empty;
            string urlForCity = String.Empty;

            switch (message.Text)
            {
                case "Wroclaw":

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Wroclaw", urlForCity);
                    (decimal, decimal) coordsWRO = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsWRO.Item1, coordsWRO.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsWRO.Item1, coordsWRO.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";

                    return;

                case "Kiev":

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Kiev", urlForCity);
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsKROP.Item1, coordsKROP.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsKROP.Item1, coordsKROP.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";
                    break;

                default:

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text, urlForCity);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);


                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsAnyCity.Item1, coordsAnyCity.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsAnyCity.Item1, coordsAnyCity.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Check if the entered city name is correct!",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    _languageDataEntry = "";

                    break;
            }
        }

        private static async void GetWeatherForecastForWeek(Message message)
        {
            string urlString = String.Empty;
            string urlForCity = String.Empty;
            switch (message.Text)
            {
                case "Wroclaw":

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Wroclaw", urlForCity);
                    (decimal, decimal) coordsWRO = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsWRO.Item1, coordsWRO.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsWRO.Item1, coordsWRO.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";

                    return;

                case "Kiev":

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Kiev", urlForCity);
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsKROP.Item1, coordsKROP.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsKROP.Item1, coordsKROP.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    _languageDataEntry = "";
                    break;

                default:

                    if (choiceLanguage == true)
                    {
                        urlForCity = localization.GetnfoOnUkrainian(message.Text);
                    }
                    else
                    {
                        urlForCity = localization.GetInfoOnPolish(message.Text);
                    }

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text, urlForCity);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    if (choiceLanguage == true)
                    {
                        urlString = localization.GetnfoOnUkrainian(coordsAnyCity.Item1, coordsAnyCity.Item2);
                    }
                    else
                    {
                        urlString = localization.GetInfoOnPolish(coordsAnyCity.Item1, coordsAnyCity.Item2);
                    }

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(urlString);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce, choiceLanguage),
                        replyMarkup: LanguageButtons.LanguageSelectionButtons());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Check if the entered city name is correct!",
                            replyMarkup: LanguageButtons.LanguageSelectionButtons());
                    }

                    _languageDataEntry = "";

                    break;
            }
        }
    }
}
