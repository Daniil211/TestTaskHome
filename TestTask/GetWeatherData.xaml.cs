using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
                MessageBox.Show($"Unable to get weather data for this reason:: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var form = new NavigationMenu();
            form.Show();
            this.Close();
        }
    }
}