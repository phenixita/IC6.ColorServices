# Pass number of cores to stress test cpu and script will hit each core with 100% utlization.
# NOTE: If you want to stress test all cores simply don't pass any argument.

function StressCPUCores {
param ([int]$numberOfCores)

if ( ($numberOfCores -eq $null) -or ( $numberOfCores -eq ''))
{
  $numberOfCores = Get-WmiObject -class Win32_processor | Select -ExpandProperty NumberOfCores;
}
Write-Host "Number of cores to target: " $numberOfCores;

foreach ($counter in 1..$numberOfCores){
    Start-Job -ScriptBlock{
    $result = 1
        foreach ($number in 1..10){
            $result = $result * $number
            Invoke-WebRequest https://localhost:44312/MemoryIntensive
        }# end foreach
    }# end Start-Job
}# end foreach

}

StressCPUCores $args[0]