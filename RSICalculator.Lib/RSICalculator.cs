using System;
using System.Collections.Generic;
using System.Linq;

namespace RSICalculator.Lib {

    /// <summary>
    /// RSICaclulator is a class used to calculate RSI values
    /// </summary>
    public class RSICalculator {
        
        #region Constants
        
        /// <summary>
        /// Minimum number of allowed samples
        /// </summary>
        protected readonly int MIN_NUMBER_OF_SAMPLES = 10;
        
        /// <summary>
        /// Calculation precision
        /// </summary>
        protected readonly int PRECISION = 4;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets number of samples used for calculation 
        /// </summary>
        /// <remarks>For better RSI accuracy, it is recommended you use at least 200 samples.</remarks>
        public int NumberOfSamples { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numberOfSamples">Number of samples used for calculation</param>
        public RSICalculator(int numberOfSamples) {
            if (numberOfSamples < MIN_NUMBER_OF_SAMPLES) {
                throw new ArgumentOutOfRangeException("Parameter numberOfSamples must be grater than 10");
            }
            NumberOfSamples = numberOfSamples;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Calculates initial RSI value
        /// </summary>
        /// <param name="valueDifferences">Array of differences used for calculation. Positive numbers represent gain, while negative represent loss.</param>
        /// <returns>Initial RSI value.</returns>
        public virtual RSIValue CalculateInitialValue(decimal[] valueDifferences)  {
            if (null == valueDifferences) {
                throw new ArgumentNullException("valueDifferences");
            }
            if (valueDifferences.Length < NumberOfSamples) {
                throw new ArgumentException($"valueDifferences contains only {(valueDifferences.Length + 1)} samples. It must contain {NumberOfSamples} samples.");
            }
            var gain = 0M;
            var loss = 0M;
            foreach(var valueDifference in valueDifferences) {
                if (valueDifference < 0) {
                    loss += Math.Abs(valueDifference);
                }
                else {
                    gain += valueDifference;
                }
            }       
            return new RSIValue(Math.Round(gain / NumberOfSamples, PRECISION), Math.Round(loss / NumberOfSamples, PRECISION), PRECISION);
        }

        /// <summary>
        /// Calculates initial RSI value
        /// </summary>
        /// <param name="candlesticks">Enumerable array of candlesticks used for calculation.</param>
        /// <returns>Initial RSI value.</returns>
        public virtual RSIValue CalculateInitialValue(IEnumerable<Candlestick> candlesticks) {
            if (null == candlesticks) {
                throw new ArgumentNullException("candlesticks");
            }
            return CalculateInitialValue(candlesticks.Select(c => c.StartEndDifference).ToArray());
        }

        /// <summary>
        /// Calculates next RSI value from previous
        /// </summary>
        /// <param name="previousRSI">Previous RSI value</param>
        /// <param name="latestDifference">Last difference</param>
        /// <returns>Updated RSI value</returns>
        public virtual RSIValue CalculateNextValue(RSIValue previousRSI, decimal latestDifference)
        {
            if (null == previousRSI) {
                throw new ArgumentNullException("previousRSI");
            }
            return new RSIValue(
                Math.Round(((previousRSI.AverageGain) * (NumberOfSamples - 1) + Math.Max(0, latestDifference)) / NumberOfSamples, PRECISION),
                Math.Round(((previousRSI.AverageLoss) * (NumberOfSamples - 1) - Math.Min(0, latestDifference)) / NumberOfSamples, PRECISION),
                PRECISION
            );
        }

        /// <summary>
        /// Calculates next RSI value from previous
        /// </summary>
        /// <param name="previousRSI">Previous RSI value</param>
        /// <param name="latestCandlestick">Last candlestick</param>
        /// <returns>Updated RSI value</returns>
        public virtual RSIValue CalculateNextValue(RSIValue previousRSI, Lib.Candlestick latestCandlestick) {
            if (null == latestCandlestick) {
                throw new ArgumentNullException("candlestick");
            }
            return CalculateNextValue(previousRSI, latestCandlestick.StartEndDifference);
        }

        #endregion

    }

}