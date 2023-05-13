using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    //API и URL ключ для получения данных о погоде
    private const string ApiKey = "957b7a4402647decff3b8162a591a350";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

    //Метод для получения данных 
    public async Task<WeatherData> GetWeatherDataAsync(string city)
    {
        using (var client = new HttpClient())
        {
            //Формируем URL для запроса
            var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";

            //Отправляем GET-запрос и получаем ответ
            var response = await client.GetAsync(url);

            //Если запрос выполнен успешно, то парсим полученный JSON и возвращаем данные о погоде
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WeatherData>(json);
                return data;
            }
            //Если запрос выполнен неуспешно(Например город не найден)
            else
            {
                throw new Exception($"Failed to get weather data. Status code: {response.StatusCode}");
            }
        }
    }
}
// Хранения данных о погоде в целом
public class WeatherData
{
    [JsonProperty("main")]
    public TemperatureData Main { get; set; }

    [JsonProperty("weather")]
    public DescriptionData[] Weather { get; set; }

    [JsonProperty("wind")]
    public SpeedData Wind { get; set; }
}
// Хранения данных о температуре
public class TemperatureData
{
    [JsonProperty("temp")]
    public double Temperature { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }
}
// Хранения данных о описании
public class DescriptionData
{
    [JsonProperty("description")]
    public string Description { get; set; }
}
// Хранения данных о скорости ветра
public class SpeedData
{
    [JsonProperty("speed")]
    public double Speed { get; set; }
}