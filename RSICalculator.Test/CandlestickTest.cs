using System;
using Xunit;

namespace RSICalculator.Test 
{
    
    public class CandlestickTest {
        
        #region Test

        public class ConstructorTest : CandlestickTest {

            [Fact]
            public void DataSet_propertiesSet() {
                var stick = new Lib.Candlestick(5M, 10M, 12M, 4M);
                Assert.Equal(5M, stick.StartPrice);
                Assert.Equal(10M, stick.EndPrice);
                Assert.Equal(12M, stick.HighestPrice);
                Assert.Equal(4M, stick.LowestPrice);
            }

            [Fact]
            public void OnlyStartEndPriceSet_propertiesSetHigherAndLowestPricesSetToMinAndMax() {
                var stick = new Lib.Candlestick(5M, 10M);
                Assert.Equal(5M, stick.StartPrice);
                Assert.Equal(10M, stick.EndPrice);
                Assert.Equal(10M, stick.HighestPrice);
                Assert.Equal(5M, stick.LowestPrice);
            }

            [Fact]
            public void DataSet_startPriceLowerThanEndPrice_startEndDifferencePositive() {
                var stick = new Lib.Candlestick(5M, 10M);
                Assert.Equal(5M, stick.StartEndDifference);
            }

            [Fact]
            public void DataSet_startPriceHigherThanEndPrice_startEndDifferenceNegative() {
                var stick = new Lib.Candlestick(10M, 5M);
                Assert.Equal(-5M, stick.StartEndDifference);
            }

        }        
            
        #endregion

    }

}