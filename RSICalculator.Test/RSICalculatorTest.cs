using System;
using Xunit;

namespace RSICalculator.Test
{
    public class RSICalculatorTest
    {

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
                var calculator = new Lib.RSICalculator(14);
                Assert.Throws<ArgumentNullException>(testCode: () => { calculator.CalculateInitialValue(null); });
            }

            [Fact]
            public void DataArrayHasLessSamplesThanSpecified_throwsArgumentException() {
                var calculator = new Lib.RSICalculator(14);
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

        }

        public class CalculateNextValueTest : RSICalculatorTest {

            [Fact]
            public void PreviousRSINotPassed_throwsArgumentNullException() => 
                Assert.Throws<ArgumentNullException>(testCode: () => { 
                    var calculator = new Lib.RSICalculator(14); calculator.CalculateNextValue(null, 0M); 
                });

            [Fact]
            public void PreviousRSIPassed_RSIGetsCalculated() {
                var expected = new Lib.RSIValue(0.5430M, 0.5771M, 4);
                var calculator = new Lib.RSICalculator(14);
                var actual = calculator.CalculateNextValue(new Lib.RSIValue(0.5848M, 0.5446M, 4), -1M);
                Assert.Equal(expected, actual);
            }

        }
            
        #endregion

    }
}
