using System;

namespace TelegramBot.Localization
{
    enum DayOfWeekmy
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    static class DayOfWeekLocalization
    {
        public static string DayLocalization(DayOfWeek dayOfWeek)
        {
            string dayInUkrainian = string.Empty;
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dayInUkrainian = "Неділя";
                    break;

                case DayOfWeek.Monday:
                    dayInUkrainian = "Понеділок";
                    break;
                case DayOfWeek.Tuesday:
                    dayInUkrainian = "Вівторок";
                    break;
                case DayOfWeek.Wednesday:
                    dayInUkrainian = "Середа";
                    break;
                case DayOfWeek.Thursday:
                    dayInUkrainian = "Четвер";
                    break;
                case DayOfWeek.Friday:
                    dayInUkrainian = "П'ятниця";
                    break;
                case DayOfWeek.Saturday:
                    dayInUkrainian = "Субота";
                    break;
                default:
                    break;
            }
            return dayInUkrainian;
        }
    }
}
