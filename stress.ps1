function StressTest {
  param (
    [int]$iterations = 300,
    [string]$url = 'http://localhost:8000/'
  )

  foreach ($number in 1..$iterations){    
    Invoke-WebRequest $url
  }
}


StressTest