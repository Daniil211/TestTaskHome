using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    private const string ApiKey = "957b7a4402647decff3b8162a591a350";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

    public async Task<WeatherData> GetWeatherDataAsync(string city)
    {
        using (var client = new HttpClient())
        {
            var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WeatherData>(json);
                return data;
            }
            else
            {
                throw new Exception($"Failed to get weather data. Status code: {response.StatusCode}");
            }
        }
    }
}

public class WeatherData
{
    [JsonProperty("main")]
    public TemperatureData Main{ get; set; }

    [JsonProperty("weather")]
    public DescriptionData[] Weather { get; set; }

    [JsonProperty("wind")]
    public SpeedData Wind { get; set; }
}

public class TemperatureData
{
    [JsonProperty("temp")]
    public double Temperature { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }
}

public class DescriptionData
{
    [JsonProperty("description")]
    public string Description { get; set; }
}

public class SpeedData
{
    [JsonProperty("speed")]
    public double Speed { get; set; }
}