# Pass number of cores to stress test cpu and script will hit each core with 100% utlization.
# NOTE: If you want to stress test all cores simply don't pass any argument.

function StressCPUCores {
param ([string]$url, [int]$numberOfCores, [int]$iterations)

if ( ($iterations -eq $null) -or ( $iterations -eq ''))
{
  $iterations = 3
}


if ( ($url -eq $null) -or ( $url -eq ''))
{
  $url = 'http://localhost:8000/'
}

if ( ($numberOfCores -eq $null) -or ( $numberOfCores -eq ''))
{
  $numberOfCores = Get-WmiObject -class Win32_processor | Select -ExpandProperty NumberOfCores;
}
Write-Host "Number of cores to target: " $numberOfCores;

foreach ($counter in 1..$numberOfCores){
    Start-Job -ScriptBlock{
    
        foreach ($number in 1..$iterations){
             
            Invoke-WebRequest $url
            
        }# end foreach
    }# end Start-Job
}# end foreach

}

StressCPUCores $args[0] $args[1] $args[2]