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
        return (double.Parse(arr[0].Remove('(')), double.Parse(arr[1].Remove(')')));
    }
}