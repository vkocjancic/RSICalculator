using System;

namespace RSICalculator.Lib
{

    /// <summary>
    /// Class for storing trading period data
    /// </summary>
    public class Candlestick
    {

        #region Properties

        /// <summary>
        /// Get price at the end of trading period
        /// </summary>
        public decimal EndPrice { get; protected set; } = 0M;

        /// <summary>
        /// Get highest price during trading period
        /// </summary>
        public decimal HighestPrice { get; protected set; } = 0M;

        /// <summary>
        /// Get lowest price during trading period
        /// </summary>
        public decimal LowestPrice { get; protected set; } = 0M;

        /// <summary>
        /// Get price at beginning of trading period
        /// </summary>
        public decimal StartPrice { get; protected set; } = 0M;

        /// <summary>
        /// Get difference between end and start price
        /// </summary>
        public decimal StartEndDifference {
            get {
                return EndPrice - StartPrice;
            }
        }

        #endregion

        #region Constructors

        public Candlestick(decimal startPrice, decimal endPrice)
            : this(startPrice, endPrice, Math.Max(startPrice, endPrice), Math.Min(startPrice, endPrice))
        {
        }

        public Candlestick(decimal startPrice, decimal endPrice, decimal highestPrice, decimal lowestPrice)
        {
            EndPrice = endPrice;
            HighestPrice = highestPrice;
            LowestPrice = lowestPrice;
            StartPrice = startPrice;
        }

        #endregion

    }

}