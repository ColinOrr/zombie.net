param (
  [string] $key = $(Read-Host "API Key")
)

nuget push zombie.js.4.2.1.*.nupkg -apikey $key
