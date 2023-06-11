# DC_WeatherApi
API for gathering and storing weather forecasts by location coordinates

Web API is built in .net 7 so you'll need the latest .net 7 hosting bundle: https://dotnet.microsoft.com/en-us/download/dotnet/7.0

I chose to use Azure CosmosDB due to how you can partition groups of location forecasts by user. Update appsettings with the approriate info:
![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/5b188998-d3d6-47cf-824e-1728617d1a45)


I set up the local CosmosDB Emulator provided here: https://learn.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21#download-the-emulator
![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/43a4d657-283e-48a6-8499-7597abf522fb)

There are basic happy path unit tests - a decent amount of coverage:

![image](https://github.com/cbseirani/DC_WeatherApi/assets/34148393/67e9ad62-40dd-4179-a089-33c61388e8d7)



