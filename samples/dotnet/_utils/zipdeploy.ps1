[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $sampleName,
    [Parameter()]
    [string]
    $samplesSuffix    
)

az webapp deployment source config-zip -g lenussamples-rg -n lenussamples-$sampleName-$samplesSuffix --src .\artifacts\publish.zip
