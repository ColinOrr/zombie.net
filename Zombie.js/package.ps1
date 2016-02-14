param (
  [int] $build = $(Read-Host "Build Number")
)

npm install
Push-Location ..\node_modules\zombie
npm install --production
Pop-Location
nuget pack zombie.js.nuspec -version 4.2.1.$build
