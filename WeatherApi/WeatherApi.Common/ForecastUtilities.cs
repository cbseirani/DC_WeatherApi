namespace WeatherApi.Common;

public static class ForecastUtilities
{
    public static string GenerateCoordinateKey(double latitude, double longitude)
    {
        return $"({latitude},{longitude})";
    }
}