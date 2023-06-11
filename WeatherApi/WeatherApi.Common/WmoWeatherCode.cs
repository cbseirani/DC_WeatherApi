namespace WeatherApi.Common;

/*
       From: https://open-meteo.com/en/docs
       WMO Weather interpretation codes (WW)
       Code	Description
       0	Clear sky
       1, 2, 3	Mainly clear, partly cloudy, and overcast
       45, 48	Fog and depositing rime fog
       51, 53, 55	Drizzle: Light, moderate, and dense intensity
       56, 57	Freezing Drizzle: Light and dense intensity
       61, 63, 65	Rain: Slight, moderate and heavy intensity
       66, 67	Freezing Rain: Light and heavy intensity
       71, 73, 75	Snow fall: Slight, moderate, and heavy intensity
       77	Snow grains
       80, 81, 82	Rain showers: Slight, moderate, and violent
       85, 86	Snow showers slight and heavy
       95 *	Thunderstorm: Slight or moderate
       96, 99 *	Thunderstorm with slight and heavy hail
 */
public enum WmoWeatherCode
{
    ClearSky = 0,
    MainlyClear = 1,
    PartlyCloudy = 2,
    Overcast = 3,
    Fog = 45,
    DepositingRimeFog = 48,
    LightDrizzle = 51,
    ModerateDrizzle = 53,
    DenseDrizzle = 55,
    LightFreezingDrizzle = 56,
    DenseFreezingDrizzle = 57,
    SlightRain = 61,
    ModerateRain = 63,
    HeavyRain = 65,
    LightFreezingRain = 66,
    HeavyFreezingRain = 67,
    SlightSnow = 71,
    ModerateSnow = 73,
    HeavySnow = 75,
    SnowGrains = 77,
    SlightRainShower = 80,
    ModerateRainShower = 81,
    HeavyRainShower = 82,
    SlightSnowShower = 85,
    HeavySnowShower = 86,
    Thunderstorm = 95,
    SlightHailThunderstorm = 96,
    HeavyHailThunderstorm = 99,   
}