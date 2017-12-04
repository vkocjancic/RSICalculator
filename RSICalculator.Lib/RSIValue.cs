using System;

namespace RSICalculator.Lib
{

    /// <summary>
    /// Class for storing RSI value
    /// </summary>
    public class RSIValue
    {

        #region Declarations

        /// <summary>
        /// Maximum allowed calculation precision
        /// </summary>
        protected readonly int MAX_PRECISION = 28;

        #endregion

        #region Properties

        /// <summary>
        /// Gets RSI value
        /// </summary>
        public decimal RSI { get; protected set; } = 0M;
        
        /// <summary>
        /// Gets RS value
        /// </summary>
        public decimal RS { get; protected set; } = 0M;
        
        /// <summary>
        /// Gets average gain
        /// </summary>
        public decimal AverageGain { get; protected set; } = 0M;
        
        /// <summary>
        /// Gets average loss
        /// </summary>
        public decimal AverageLoss { get; protected set; } = 0M;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="averageGain">Average gain</param>
        /// <param name="averageLoss">Average loss</param>
        /// <param name="precision">Calculation precission</param>
        public RSIValue(decimal averageGain, decimal averageLoss, int precision = 0)
        {
            precision = (0 == precision) ? MAX_PRECISION : precision;
            AverageGain = averageGain;
            AverageLoss = (0 == averageLoss) ? (decimal)Math.Pow(10, ((-1) * precision)) : averageLoss;
            RS = Math.Round(AverageGain / AverageLoss, precision);
            RSI = Math.Round(100 - (100 / (1 + RS)), precision - 1);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Overridden operator ==
        /// </summary>
        /// <param name="leftValue">Left RSI value</param>
        /// <param name="rightValue">Right RSI value</param>
        /// <returns>True, if values are equal, false othewrise.</returns>
        public static bool operator ==(RSIValue leftValue, RSIValue rightValue)
        {
            if ((null == (object)leftValue) && (null == (object)rightValue)) return true;
            if (null == (object)leftValue) return false;
            if (null == (object)rightValue) return false;
            return (leftValue.RSI == rightValue.RSI)
                && (leftValue.RS == rightValue.RS)
                && (leftValue.AverageGain == rightValue.AverageGain)
                && (leftValue.AverageLoss == rightValue.AverageLoss);
        }

        /// <summary>
        /// Overridden operator !=
        /// </summary>
        /// <param name="leftValue">Left RSI value</param>
        /// <param name="rightValue">Right RSI value</param>
        /// <returns>True, if values are not equal, false othewrise.</returns>
        public static bool operator !=(RSIValue leftValue, RSIValue rightValue) => !(leftValue == rightValue);

        #endregion

        #region Overrides

        /// <summary>
        /// Overridden method Equals, used to compare two objects
        /// </summary>
        /// <param name="obj">RSI value to compare</param>
        /// <returns>True, if values are equal, false othewrise.</returns></returns>
        public override bool Equals(object obj) => this == (RSIValue)obj;

        /// <summary>
        /// Overridden method GetHashCode
        /// </summary>
        /// <returns>Object hash code</returns>
        public override int GetHashCode() => base.GetHashCode();

        #endregion

    }

}