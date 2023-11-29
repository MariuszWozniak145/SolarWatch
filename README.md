# SolarWatch
An application used to display sunrise and sunset information for the specified location. The principle of operation:
1) on the basis of the entered information about a given city (city, state, country), it is checked whether the city appears in the project database if not, a query is made to the API: https://openweathermap.org/api, from which information about latitude and longitude is retrieved and stored in the prject database
2) based on latitude, longitude and date, it is checked if sunrise and sunset information is present in the project database for the given information if not a query is executed to API: https://sunrise-sunset.org/api, from which sunrise and sunset information is retrieved and saved to the project database
## Languages and tools:
- C#
- ASP.NET MVC
- Entity framework
- Identity framework
- MSSQL
