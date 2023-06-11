namespace WeatherApi.Common;

public static class ForecastUtilities
{
    public static string GenerateCoordinatesKey(double latitude, double longitude)
    {
        return $"({latitude},{longitude})";
    }

    public static (double, double) GetCoordinatesFromKey(string key)
    {
        var arr = key.Split(',');
        var latitude = double.Parse(arr[0].Replace("(", string.Empty));
        var longitude = double.Parse(arr[1].Replace(")", string.Empty));
        return (latitude, longitude);
    }
}