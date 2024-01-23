$SteamLibraryInfoPath = "C:\Program Files (x86)\Steam\steamapps\libraryfolders.vdf"

if ( -not [System.IO.File]::Exists($SteamLibraryInfoPath) )
{
    Write-Host "Fatal error: Cannot find Steam." -ForegroundColor Red
    exit
}

# Get all of the user's Steam Library folders
$RawSteamLibsList = Select-String -Path $SteamLibraryInfoPath -Pattern "`"path`""

# Make an array of only the paths
$SteamLibsList = $RawSteamLibsList | % { $_.ToString().Split("`t")[4].Trim('"').Replace("\\", "\") }

# Ask user to select where Lethal Company is installed
Write-Host "Select the Steam library folder where you have installed Lethal Company from the list below [default: 1]:" -ForegroundColor Blue
foreach ($idx in (0..($SteamLibsList.Length - 1)))
{
    Write-Host "$($idx + 1). " -ForegroundColor Yellow -NoNewline
    Write-Host "$($SteamLibsList[$idx])"
}
$RawSelection = Read-Host -Prompt "Make a selection"
Write-Host ""
$Selection = (0)
if ( $RawSelection -ne "" )
{
    $Selection = (([int]$RawSelection) - 1)
}

# Validate selection
if ( ($Selection -lt 0) -or ($Selection -ge $SteamLibsList.Length) )
{
    Write-Host "Fatal error: Selection invalid." -ForegroundColor Red
    exit
}

# Check for Lethal Company
$LCPath = "$($SteamLibsList[$Selection])\steamapps\common\Lethal Company"
Write-Host "Checking for Lethal Company game files in " -ForegroundColor Blue -NoNewline
Write-Host "$LCPath" -ForegroundColor Yellow -NoNewline
Write-Host "..." -ForegroundColor Blue
if ( -not [System.IO.File]::Exists("$LCPath\Lethal Company.exe") )
{
    Write-Host "Fatal error: Lethal Company not found in selected library. Try again." -ForegroundColor Red
    exit
}
else
{
    Write-Host "Success! (Found Lethal Company game files.)" -ForegroundColor Green
}

Write-Host "Regenerating Managed folder..." -ForegroundColor Blue
Remove-Item ".\Managed" -Recurse -ErrorAction SilentlyContinue
Copy-Item "$LCPath\Lethal Company_Data\Managed" ".\Managed" -Recurse
Write-Host "Done!" -ForegroundColor Green