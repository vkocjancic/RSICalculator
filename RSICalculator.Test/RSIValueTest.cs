using System;
using Xunit;

namespace RSICalculator.Test
{

    public class RSIValueTest {

        #region Tests

        public class ConstructorTest : RSIValueTest {

            [Fact]
            public void DataSet_propertiesSet() {
                var value = new Lib.RSIValue(1M, 2M, 4);
                Assert.Equal(1M, value.AverageGain);
                Assert.Equal(2M, value.AverageLoss);
                Assert.Equal(0.5M, value.RS);
                Assert.Equal(33.333M, value.RSI);
            }

            [Fact]
            public void Precision0_setToMax28() {
                var value = new Lib.RSIValue(1M, 2M, 0);
                Assert.Equal(1M, value.AverageGain);
                Assert.Equal(2M, value.AverageLoss);
                Assert.Equal(0.5M, value.RS);
                Assert.Equal(33.333333333333333333333333333M, value.RSI);
            }

            [Fact]
            public void AverageLoss0_SetToMinimumNon0ValueForPrecision() {
                var value = new Lib.RSIValue(1M, 0M, 4);
                Assert.Equal(1M, value.AverageGain);
                Assert.Equal(0.0001M, value.AverageLoss);
                Assert.Equal(10000M, value.RS);
                Assert.Equal(99.990M, value.RSI);
            }

        }

        public class EqualsTest : RSIValueTest {

            [Fact]
            public void ValuesAreNull_returnsTrue() {
                Assert.True((Lib.RSIValue)null == (Lib.RSIValue)null);
            }

            [Fact]
            public void LeftValueIsNull_returnsFalse() {
                Assert.False(((Lib.RSIValue)null) == (new Lib.RSIValue(1M, 2M, 4)));
            }

            [Fact]
            public void RightValueIsNull_returnsFalse() {
                Assert.False((new Lib.RSIValue(1M, 2M, 4)).Equals((Lib.RSIValue)null));
            }

            [Fact]
            public void ValuesAreEqual_returnsTrue() {
                var value1 = new Lib.RSIValue(1M, 2M, 4);
                var value2 = new Lib.RSIValue(1M, 2M, 4);
                Assert.True(value1.Equals(value2));
            }

            [Fact]
            public void ValuesAreNotEqual_returnsFalse() {
                var value1 = new Lib.RSIValue(1M, 2M, 4);
                var value2 = new Lib.RSIValue(1M, 3M, 4);
                Assert.False(value1.Equals(value2));
            }

        }

        #endregion

    }

}