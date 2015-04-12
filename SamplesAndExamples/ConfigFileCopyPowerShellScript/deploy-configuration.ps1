# Simple configuration deployment example.

$currentServer = $env:computername
$pathToConfigs = ".\source"
$pathToWebRoot = ".\target"

$currentServerConfigFiles = Get-ChildItem $pathToConfigs -Force | Where-Object {$_.Name -match $currentServer}

foreach ($sourceFile in $currentServerConfigFiles) {
	$filename = $sourceFile.ToString()
	$targetFile = $filename -Replace "$currentServer.", "" # PS replace is case insensitive by default.
	
	$source = "$pathToConfigs\$filename"
	$target = "$pathToWebRoot\$targetFile"
	Copy-Item -Force -Path $source -Destination $target
	echo "Copied config file '$source' to '$target'."
}
