using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LouvOgRathApp.Shared.Entities;

namespace LouvOgRathApp.ClientSide.ClientControllers
{
    public class ClientGuiLogic
    { 
        /// <summary>
        /// used for getting the weather in vejle
        /// </summary>
        /// <returns></returns>
        public WeatherApi Weather()
        {
            using (WebClient webclient = new WebClient())
            {
                string json = webclient.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=vejle&mode=json&units=metric&appid=e9b3b99c257810d4beaf8dc7be2a0c44");
                return WeatherApi.FromJson(json);
            }
        }
    }
}
