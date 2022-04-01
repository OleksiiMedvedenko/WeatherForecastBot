using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Buttons;
using TelegramBot.Controllers;
using TelegramBot.Model.WeatherToDay;

namespace TelegramBot
{
    class Program
    {

        private static string Token { get; set; } = "5226846542:AAGsUUU6NnpAykz8UZKJLKPXlK_f9qhDIRE";

        private static TelegramBotClient _client;
        private static NowWeatherResponse _nowWeatherResponse;
        private static WeatherResponce _weatherResponce;

        private static string _forecastDateEntry = string.Empty;

        [Obsolete]
        static void Main(string[] args)
        {
            _client = new TelegramBotClient(Token);
            _client.StartReceiving(new UpdateType[] { UpdateType.Message });
            _client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            _client.StopReceiving();
        }

        [Obsolete]

        // - каткое описание работы этого метода, есть некий цикл While сперва от телеграмма мы получем
        // сообщение какую погоду нам нужно получить, затем мы записываем значение в переменную (_choiceDay) чтобы сохранить наш выбор, 
        // ведь дальше мы запрашивае город в котором хотим узнать погоду 
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            
            var messageFromTG = e.Message;

            if (_forecastDateEntry == "")
            {
                _forecastDateEntry = messageFromTG.Text;
            }

            if (messageFromTG.Text != null)
            {
                Console.WriteLine($"Message was received from [{messageFromTG.Chat.Username}] [{DateTime.Now.ToShortTimeString()}] : {messageFromTG.Text}");

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

                    default:
                        _forecastDateEntry = string.Empty;
                        await _client.SendTextMessageAsync(messageFromTG.Chat.Id, "Не знаю такої команди!",
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                        break;
                }
            }
        }

        private static async void GetWeatherForecastForNow(Message message)
        {

            switch (message.Text)
            {
                case "Вроцлав":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Вроцлав");
                    var Wroclaw = await _client.SendTextMessageAsync(message.Chat.Id, NowWeatherController.GetWeatherInfoNow(_nowWeatherResponse), 
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    var Kropyvnytskyi = await _client.SendTextMessageAsync(message.Chat.Id, NowWeatherController.GetWeatherInfoNow(_nowWeatherResponse),
                         replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                    break;

                default:
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);

                    if (_nowWeatherResponse != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, NowWeatherController.GetWeatherInfoNow(_nowWeatherResponse),
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!", 
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
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
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsKROP.Item1, coordsKROP.Item2);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                    break;

                default:

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsAnyCity.Item1, coordsAnyCity.Item2);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTodayWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!",
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
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
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsKROP.Item1, coordsKROP.Item2);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                    break;

                default:

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsAnyCity.Item1, coordsAnyCity.Item2);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetTomorrowWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!",
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
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
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                    return;

                case "Кропивницький":
                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite("Кропивницький");
                    (decimal, decimal) coordsKROP = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsKROP.Item1, coordsKROP.Item2);

                    var Krop = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                    break;

                default:

                    _nowWeatherResponse = NowWeatherController.GetWeatherFromWebSite(message.Text);
                    (decimal, decimal) coordsAnyCity = NowWeatherController.GetCityCoords(_nowWeatherResponse);

                    _weatherResponce = WeatherController.GetWeatherFromWebSite(coordsAnyCity.Item1, coordsAnyCity.Item2);

                    if (_weatherResponce != null)
                    {
                        var anyCity = await _client.SendTextMessageAsync(message.Chat.Id, WeatherController.GetWeekWeatherInfo(_weatherResponce),
                        replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());

                        break;
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(message.Chat.Id, "Перевірте чи поправно введена назва міста!",
                            replyMarkup: WeatherForecastButtons.SelectingWeatherForecastButton());
                    }

                    break;
            }
        }
    }
}
