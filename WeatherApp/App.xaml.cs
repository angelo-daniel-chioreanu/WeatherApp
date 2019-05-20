using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public WeatherContext WeatherContext { get => weatherContext; }
        public WeatherClient WeatherClient { get => weatherClient; }

        private WeatherContext weatherContext = new WeatherContext();
        private WeatherClient weatherClient = new WeatherClient();
    }
}
