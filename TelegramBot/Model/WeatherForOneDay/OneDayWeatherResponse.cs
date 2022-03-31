using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    class OneDayWeatherResponse
    {
        public OneDayWeatherInfo Main { get; set; }
        public OneDayWeatherInfo Wind { get; set; }
        public OneDayWeatherInfo Clouds { get; set; }
        public OneDayWeatherInfo[] Weather { get; set; }

        /// <summary>
        /// региональные параметры 
        /// </summary>
        public OneDayWeatherInfo Sys { get; set; }
        /// <summary>
        /// city name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// часовой пояс - точнее прибавка времени к UTC
        /// </summary>
        public int Timezone { get; set; }
    }
}
