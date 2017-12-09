[![NuGet Info](https://buildstats.info/nuget/RSICalculator.Lib?includePreReleases=true)](https://www.nuget.org/packages/Giraffe/)

# Relative Strength Index (RSI) Calculator 
Open-source library for calculation of Relative Strength Index, a momentum oscillator for measurement of speed and change in price movements.

# Usage

Difference is calculated between start and end value.

```csharp
var differences = new decimal[] { 
                    1.0000M,    -0.6875M,   0.5000M,    -2.0000M,   -0.6875M, 
                    0.3750M,    1.1250M,    2.0625M,    -0.2500M,   -0.5625M, 
                    -2.4375M,   1.7500M,    1.3750M,    -1.0000M };
var numberOfSamplesUsedForCalculation = differences.Length;
var calculator = new Lib.RSICalculator(numberOfSamplesUsedForCalculation);
var rsi = calculator.CalculateInitialValue(differences);
...
var currentDifference = -1M;
rsi = calculator.CalculateNextValue(rsi, currentDifference);
...
```
