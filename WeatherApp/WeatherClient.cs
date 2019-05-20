using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace WeatherApp
{
    public class WeatherClient
    {
        public WeatherClient()
        {
            httpClient.BaseAddress = baseAddress;
        }

        public async Task<WeatherInfo> GetWeather(string APPID, string ZIP)
        {
            WeatherInfo weatherInfo = new WeatherInfo { ZIP = ZIP };

            var pairs = new Dictionary<string, string>
                        {
                            { "zip", ZIP + ",us" },
                            { "units", "imperial" },
                            { "APPID", APPID }
                        };

            var content = new FormUrlEncodedContent(pairs);
            var query = content.ReadAsStringAsync().Result;

            string response;

            string temperature;
            string conditions;

            try
            {
                response = await httpClient.GetStringAsync(requestUri + "?" + query);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                object objResponse = serializer.DeserializeObject(response);

                Dictionary<string, object> mapResponse = (Dictionary<string, object>)objResponse;
                Dictionary<string, object> mapMain = (Dictionary<string, object>)mapResponse["main"];
                object[] mapWeatherCollection = (object[])mapResponse["weather"];
                Dictionary<string, object> mapWeather = (Dictionary<string, object>)mapWeatherCollection[0];

                temperature = mapMain["temp"].ToString();
                conditions = mapWeather["main"].ToString();
            }
            catch (Exception)
            {
                weatherInfo.ErrorMessage = errorHTTP;

                return weatherInfo;
            }

            weatherInfo.Temperature = temperature;
            weatherInfo.Conditions = conditions;

            return weatherInfo;
        }

        private HttpClient httpClient = new HttpClient();

        private readonly Uri baseAddress = new Uri("https://api.openweathermap.org");
        private const string requestUri = "/data/2.5/weather";

        private const string errorHTTP = "Error from Weather Server";
    }
/*
    public class WeatherResponse
    {
        Weather[] weather;
        Main main;

        public class Weather
        {
            string main;
        }

        public class Main
        {
            string temp;
        }
    }
*/
    public class WeatherInfo
    {
        public string ZIP { get; set; }
        public string Temperature { get; set; }
        public string Conditions { get; set; }
        public string ErrorMessage { get; set; }
    }
}
