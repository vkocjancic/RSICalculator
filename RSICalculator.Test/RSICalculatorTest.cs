using System;
using Xunit;

namespace RSICalculator.Test
{
    public class RSICalculatorTest
    {

        #region Declarations

        public readonly int MIN_SAMPLES = 14;
            
        #endregion

        #region Tests

        public class ConstructorTest : RSICalculatorTest {

            [Fact]
            public void NumberOfSamplesNotSet_throwsArgumentOutOfRangeException() => Assert.Throws<ArgumentOutOfRangeException>(testCode: () => { new Lib.RSICalculator(0); });

            [Fact]
            public void NumberOfSamplesLessThan10_throwsArgumentOutOfRangeException() => Assert.Throws<ArgumentOutOfRangeException>(testCode: () => { new Lib.RSICalculator(9); });

            [Fact]
            public void NumberOfSamplesGreaterOrEquals10_propertyNumberOfSamplesGetsSet() {
                var expected = 10;
                var calculator = new Lib.RSICalculator(expected);
                Assert.Equal(expected, calculator.NumberOfSamples);
            }

        }

        public class CalculateInitialValueTest : RSICalculatorTest {

            [Fact]
            public void DataArrayNull_throwsArgumentNullException() {
                var calculator = new Lib.RSICalculator(MIN_SAMPLES);
                decimal[] expected = null;
                Assert.Throws<ArgumentNullException>(testCode: () => { calculator.CalculateInitialValue(expected); });
            }

            [Fact]
            public void DataArrayHasLessSamplesThanSpecified_throwsArgumentException() {
                var calculator = new Lib.RSICalculator(MIN_SAMPLES);
                Assert.Throws<ArgumentException>(testCode: () => { calculator.CalculateInitialValue(new decimal[] {}); });
            }

            [Fact]
            public void DataArraySet_RSIValueGetsCalculated() {
                var expected = new Lib.RSIValue(0.5848M, 0.5446M, 4);
                var differences = new decimal[] { 
                    1.0000M,    -0.6875M,   0.5000M,    -2.0000M,   -0.6875M, 
                    0.3750M,    1.1250M,    2.0625M,    -0.2500M,   -0.5625M, 
                    -2.4375M,   1.7500M,    1.3750M,    -1.0000M };
                var calculator = new Lib.RSICalculator(14);
                var actual = calculator.CalculateInitialValue(differences);
                Assert.Equal(expected, actual);
            }
            
            [Fact]
            public void CandlestickArrayNull_throwsArgumentNullException() {
                var calculator = new Lib.RSICalculator(MIN_SAMPLES);
                Lib.Candlestick[] expected = null;
                Assert.Throws<ArgumentNullException>(testCode: () => { calculator.CalculateInitialValue(expected); });
            }

            [Fact]
            public void CandlestickHasLessSamplesThanSpecified_throwsArgumentException() {
                var calculator = new Lib.RSICalculator(MIN_SAMPLES);
                Assert.Throws<ArgumentException>(testCode: () => { calculator.CalculateInitialValue(new Lib.Candlestick[] {}); });
            }

            [Fact]
            public void CandlesticksSet_RSIValueGetsCalculated() {
                var expected = new Lib.RSIValue(0.5848M, 0.5446M, 4);
                var candlesticks = new Lib.Candlestick[] { 
                    new Lib.Candlestick(10M, 11M),              new Lib.Candlestick(11M, 10.3125M),     new Lib.Candlestick(10.3125M, 10.8125M),
                    new Lib.Candlestick(10.8125M, 8.8125M),     new Lib.Candlestick(8.8125M, 8.125M),   new Lib.Candlestick(8.125M, 8.5M),
                    new Lib.Candlestick(8.5M, 9.625M),          new Lib.Candlestick(9.625M, 11.6875M),  new Lib.Candlestick(11.6875M, 11.4375M),
                    new Lib.Candlestick(11.4375M, 10.875M),     new Lib.Candlestick(10.875M, 8.4375M),  new Lib.Candlestick(8.4375M, 10.1875M),
                    new Lib.Candlestick(10.1875M, 11.5625M),    new Lib.Candlestick(11.5625M, 10.5625M) };
                var calculator = new Lib.RSICalculator(14);
                var actual = calculator.CalculateInitialValue(candlesticks);
                Assert.Equal(expected, actual);
            }

        }

        public class CalculateNextValueTest : RSICalculatorTest {

            [Fact]
            public void PreviousRSINotPassed_throwsArgumentNullException() => 
                Assert.Throws<ArgumentNullException>(testCode: () => { 
                    var calculator = new Lib.RSICalculator(MIN_SAMPLES); 
                    calculator.CalculateNextValue(null, 0M); 
                });

            [Fact]
            public void PreviousRSIPassed_RSIGetsCalculated() {
                var expected = new Lib.RSIValue(0.5430M, 0.5771M, 4);
                var calculator = new Lib.RSICalculator(MIN_SAMPLES);
                var actual = calculator.CalculateNextValue(new Lib.RSIValue(0.5848M, 0.5446M, 4), -1M);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void PreviousRSINotPassedWtihCandlestick_throwsArgumentNullException() => 
                Assert.Throws<ArgumentNullException>(testCode: () => { 
                    var calculator = new Lib.RSICalculator(MIN_SAMPLES); 
                    calculator.CalculateNextValue(null, null); 
                });

            [Fact]
            public void PreviousRSIPassedNoCandlestick_throwsArgumentNullException() => 
                Assert.Throws<ArgumentNullException>(testCode: () => { 
                    var calculator = new Lib.RSICalculator(MIN_SAMPLES); 
                    calculator.CalculateNextValue(new Lib.RSIValue(0.5848M, 0.5446M, 4), null); 
                });

            [Fact]
            public void PreviousRSIPassedWithCandlestick_RSIGetsCalculated() {
                var expected = new Lib.RSIValue(0.5430M, 0.5771M, 4);
                var calculator = new Lib.RSICalculator(MIN_SAMPLES);
                var actual = calculator.CalculateNextValue(new Lib.RSIValue(0.5848M, 0.5446M, 4), new Lib.Candlestick(2M, 1M));
                Assert.Equal(expected, actual);
            }

        }
            
        #endregion

    }
}
