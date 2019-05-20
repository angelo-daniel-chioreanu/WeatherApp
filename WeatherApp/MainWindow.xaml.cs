using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = App.WeatherContext;

            this.Timer.Tick += new EventHandler(timer_Tick);
            this.Timer.Interval = App.WeatherContext.Interval;
            this.Timer.Start();
        }

        public App App { get => (App)Application.Current; }

        public DispatcherTimer Timer { get => timer; }

        public async Task UpdateWeather()
        {
            var client = App.WeatherClient;
            var APPID = App.WeatherContext.APPID;
            var ZIP = App.WeatherContext.ZIP;

            if (String.IsNullOrEmpty(APPID) || String.IsNullOrEmpty(ZIP))
                return;

            WeatherInfo weatherInfo = await client.GetWeather(APPID, ZIP);

            if (String.IsNullOrEmpty(weatherInfo.ErrorMessage))
            {
                App.WeatherContext.Temperature = weatherInfo.Temperature;
                App.WeatherContext.Conditions = weatherInfo.Conditions;
                App.WeatherContext.Message = String.Empty;
            }
            else
            {
                App.WeatherContext.Temperature = String.Empty;
                App.WeatherContext.Conditions = String.Empty;
                App.WeatherContext.Message = weatherInfo.ErrorMessage;
            }
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            await UpdateWeather();
        }

        private async void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            OptionsDialogBox dlg = new OptionsDialogBox
            {
                Owner = this
            };

            // Initialize dialog box property values
            dlg.APPID = App.WeatherContext.APPID;
            dlg.ZIP = App.WeatherContext.ZIP;

            dlg.ShowDialog();

            if (dlg.DialogResult ?? false)
            {
                // Use new dialog box property values
                App.WeatherContext.APPID = dlg.APPID;
                App.WeatherContext.ZIP = dlg.ZIP;

                App.WeatherContext.Message = String.Empty;
                App.WeatherContext.Temperature = String.Empty;
                App.WeatherContext.Conditions = String.Empty;

                await UpdateWeather();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private DispatcherTimer timer = new DispatcherTimer();
    }
}
