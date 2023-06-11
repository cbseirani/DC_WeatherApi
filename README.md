# DC_WeatherApi
API for gathering and storing weather forecasts by location coordinates

I use Rider IDE (Early Access EAP is free for a while): https://www.jetbrains.com/rider/

Web API is built in .net 7 so you'll need the latest .net 7 hosting bundle: https://dotnet.microsoft.com/en-us/download/dotnet/7.0

Open Meteo is the weather forecast provider: https://open-meteo.com/

I chose to use Azure CosmosDB due to how you can partition groups of location forecasts by user. Update appsettings with the approriate info:
![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/5b188998-d3d6-47cf-824e-1728617d1a45)


I set up the local CosmosDB Emulator provided here: https://learn.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21#download-the-emulator
![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/43a4d657-283e-48a6-8499-7597abf522fb)

There are basic happy path unit tests - a decent amount of coverage:

![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/67e9ad62-40dd-4179-a089-33c61388e8d7)


Run/debug locally using the IIS Express configration(for windows users):

![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/06e6f8ad-9bd0-47eb-bad6-dadf510a1794)


Swagger to test the API:

![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/6668169c-5b3a-4af2-aae1-5baa12e7991b)

![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/fe6c0b7c-36de-44ae-9d2c-d1c6bdedba04)



