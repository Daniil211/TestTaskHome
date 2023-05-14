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
        // Создаем экземпляр сервиса для получения данных о погоде
        private readonly WeatherService _weatherService = new WeatherService();
        // Перемееная для данных о погоде
        private WeatherData _weatherData;

        public GetWeatherData()
        {
            InitializeComponent();
            DataContext = this;
        }

        // Получены данные или нет?
        private bool _isWeatherDataLoaded;

        // Свойства отражения полученнных данных
        public string Temperature => _isWeatherDataLoaded ? _weatherData?.Main?.Temperature.ToString("0.#") + "°C" : string.Empty;
        public string Description => _isWeatherDataLoaded ? _weatherData?.Weather?[0]?.Description : string.Empty;
        public string WindSpeed => _isWeatherDataLoaded ? _weatherData?.Wind?.Speed.ToString("0.#") + " m/s" : string.Empty;

        private async void OnGetWeatherClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Данные для города
                _weatherData = await _weatherService.GetWeatherDataAsync(cityTextBox.Text);
                // Если получены установим true
                _isWeatherDataLoaded = true;
                // Обновление
                OnPropertyChanged(nameof(Temperature));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(WindSpeed));
            }
            catch (Exception ex)
            { 
                MessageBox.Show($"Unable to get weather data for this reason:: {ex.Message}");
            }
        }

        //Обьявление события срабатывающего на изменение
        public event PropertyChangedEventHandler PropertyChanged;

        //Метода вызова
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //Уведомление для всех подписанных объектов об изменении свойства
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

