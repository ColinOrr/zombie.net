param (
  [string] $key = $(Read-Host "API Key")
)

nuget push zombie.js.5.0.5.*.nupkg -apikey $key -source https://api.nuget.org/v3/index.json
