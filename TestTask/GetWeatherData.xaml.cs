using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class GetWeatherData : Window, INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService = new WeatherService();
        private WeatherData _weatherData;

        public GetWeatherData()
        {
            InitializeComponent();
            DataContext = this;
        }

        private bool _isWeatherDataLoaded;

        public string Temperature => _isWeatherDataLoaded ? _weatherData?.Main?.Temperature.ToString("0.#") + "°C" : string.Empty;
        public string Description => _isWeatherDataLoaded ? _weatherData?.Weather?[0]?.Description : string.Empty;
        public string WindSpeed => _isWeatherDataLoaded ? _weatherData?.Wind?.Speed.ToString("0.#") + " m/s" : string.Empty;

        private async void OnGetWeatherClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _weatherData = await _weatherService.GetWeatherDataAsync(cityTextBox.Text);
                _isWeatherDataLoaded = true;
                OnPropertyChanged(nameof(Temperature));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(WindSpeed));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get weather data: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}