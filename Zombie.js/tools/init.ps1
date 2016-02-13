param($installPath, $toolsPath, $package, $project)
Move-Item -Force -Path "$toolsPath\node_modules" -Destination "$installPath..\..\..\node_modules"
