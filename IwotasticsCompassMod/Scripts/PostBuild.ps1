# Compress mod metadata and DLL to zip archive
Get-ChildItem -Path ".\manifest.json", "..\icon.png", "..\README.md", "..\CHANGELOG.md", ".\bin\Debug\netstandard2.1\IwotasticsCompassMod.dll" |
  Compress-Archive -DestinationPath ".\bin\Debug\IwotasticsCompassMod.zip" -Force