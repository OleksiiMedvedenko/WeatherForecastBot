using System.Linq;
using TelegramBot.Localization.UkraineLokalization.DescriptionLocalization;

namespace TelegramBot.Localization
{
    class WeatherDescriptionLocalization
    {
        /// <summary>
        /// Здесь мы обрабатываем все данные из двух классов. Мы получаем данные из сайта weatherDescriptionOnEngl - 
        /// потом мы сравниваем есть ли в записаном списке DescriptionOnEng.EngDescription такие данные потом мы ищем идекс под котором записаны 
        /// эти данные в списке и вытягиваем значение из этого же переведенного списка DescriptionOnEng.EngDescription (списки зеркальны 
        /// тоесть под индексом 1 в английском списке записана информация и под индексом 1 в украинском списке записана уже преведенная 
        /// эта иннформация, дальше мы проверяем значение на null, если мы не нашли ничего подобного в наших списках, значет 
        /// возвращаем английское значение и нужно будет обновитьь наш список)
        /// </summary>
        /// <param name="weatherDescriptionOnEngl"></param>
        /// <returns></returns>
        public static string GetWeatherDescriptionOnUkrainian(string weatherDescriptionOnEngl)
        {
            try
            {
                var descriptionOnEnglish = DescriptionOnEng.EngDescription.FirstOrDefault(x => x.Equals(weatherDescriptionOnEngl));
                var indexThisDescInEngList = DescriptionOnEng.EngDescription.IndexOf(descriptionOnEnglish);

                var urkDescription = DescriptionOnUkrainian.UkrDescription.ElementAt(indexThisDescInEngList);

                if (urkDescription != null)
                {
                    return urkDescription;
                }

                return weatherDescriptionOnEngl;
            }
            catch
            {
                return weatherDescriptionOnEngl;
            }
        }
    }
}
