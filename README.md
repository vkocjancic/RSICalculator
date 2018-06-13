[![NuGet Info](https://buildstats.info/nuget/RSICalculator.Lib?includePreReleases=true)](https://www.nuget.org/packages/RSICalculator.Lib/)

# Relative Strength Index (RSI) Calculator 
Open-source library for calculation of Relative Strength Index, a momentum oscillator for measurement of speed and change in price movements.

# Usage

## Using difference calculation as input

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

## Using candlestick object as input

```csharp
var candlesticks = new Lib.Candlestick[] { 
                    new Lib.Candlestick(10M, 11M),              new Lib.Candlestick(11M, 10.3125M),     new Lib.Candlestick(10.3125M, 10.8125M),
                    new Lib.Candlestick(10.8125M, 8.8125M),     new Lib.Candlestick(8.8125M, 8.125M),   new Lib.Candlestick(8.125M, 8.5M),
                    new Lib.Candlestick(8.5M, 9.625M),          new Lib.Candlestick(9.625M, 11.6875M),  new Lib.Candlestick(11.6875M, 11.4375M),
                    new Lib.Candlestick(11.4375M, 10.875M),     new Lib.Candlestick(10.875M, 8.4375M),  new Lib.Candlestick(8.4375M, 10.1875M),
                    new Lib.Candlestick(10.1875M, 11.5625M),    new Lib.Candlestick(11.5625M, 10.5625M) };
var numberOfSamplesUsedForCalculation = candlesticks.Length;
var calculator = new Lib.RSICalculator(numberOfSamplesUsedForCalculation);
var rsi = calculator.CalculateInitialValue(candlesticks);
...
var currentCandlestick = new Lib.Candlestick(2M, 1M);
rsi = calculator.CalculateNextValue(rsi, currentCandlestick);
...
```
