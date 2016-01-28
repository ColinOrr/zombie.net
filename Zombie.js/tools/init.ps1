param($installPath, $toolsPath, $package, $project)
New-Item -Force -ItemType directory "$installPath..\..\..\node_modules"
Move-Item -Force -Path "$toolsPath\zombie" -Destination "$installPath..\..\..\node_modules\zombie"
