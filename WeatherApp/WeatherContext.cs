using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherApp
{
    public class WeatherContext : BindableObject
    {
        public string APPID
        {
            set
            {
                SetProperty(ref valueAPPID, value);
            }
            get
            {
                return valueAPPID;
            }
        }

        public string ZIP
        {
            set
            {
                SetProperty(ref valueZIP, value);
            }
            get
            {
                return valueZIP;
            }
        }

        public string Temperature
        {
            set
            {
                SetProperty(ref valueTemperature, value);
            }
            get
            {
                return valueTemperature;
            }
        }

        public string Conditions
        {
            set
            {
                SetProperty(ref valueConditions, value);
            }
            get
            {
                return valueConditions;
            }
        }

        public string Message
        {
            set
            {
                SetProperty(ref valueMessage, value);
            }
            get
            {
                return valueMessage;
            }
        }

        public TimeSpan Interval
        {
            set
            {
                SetProperty(ref valueInterval, value);
            }
            get
            {
                return valueInterval;
            }
        }

        private string valueAPPID;
        private string valueZIP;
        private string valueTemperature;
        private string valueConditions;

        // Error Message, initialized for Options settings request
        private string valueMessage = "Uninitialized app, use Options button.";

        // Update Interval, initialized for 60 seconds
        private TimeSpan valueInterval = new TimeSpan(0, 0, 60);
    }

    public class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
