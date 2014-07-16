using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(16, Title = "What is the sum of the digits of the number 2^1000?")]
    public class Problem16
    {
        public long Solve()
        {  
            return new {Power = 1, Digits = (IEnumerable<int>) new int[] {2}}
                .Unfold((previous) => new { Power = previous.Power + 1, Digits = SumNumbers(previous.Digits, previous.Digits) })
                .SkipWhile(item => item.Power < 1000)
                .First()
                .Digits
                .Sum();
        }

        /// <summary>
        /// Calculate the sum of two numbers represented as a sequence of digits. Numbers should be given
        /// with least significant digits first
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns>The sum of the two numbers as a sequence of digits</returns>
        private IEnumerable<int> SumNumbers(IEnumerable<int> number1, IEnumerable<int> number2)
        {
            // put the numbers together in an array so that we can zip them easily
            var numbers = new[] { number1, number2 };

            // process the entire list of numbers column by column
            // ie, process least significant digits of all numbers first, then the next column of digits, etc.
            return numbers
            .Zip()
                // work through each column, calculating which digit represents the sum for that column,
                // and what the carryover is
            .Aggregate(
                // initialise an annoymous data structure to hold our workings out as we
                // move column-by-column.
                // Digits holds the digits of the sum that we've calculated so far
                // CarryOver holds whatever has been carried over from the previous column
                new { Digits = Stack<int>.Empty, CarryOver = 0 },
                (previousResult, columnDigits) =>
                {
                    var columnSum = columnDigits.Sum() + previousResult.CarryOver;
                    var nextDigit = columnSum % 10;
                    var carryOver = columnSum / 10;
                    return new { Digits = previousResult.Digits.Push(nextDigit), CarryOver = carryOver };
                },
                // to complete the aggregation, we need to add the digits of the final carry-over
                // to the digits of the sum;
                // note that because completeSum.Digits is a stack, when it is enumerated
                // we get the items back in reverse order - ie most significant digit first; hence the call to Reverse()
                // to make the order of the digits in the sum we return consistent with the input digits
                (completeSum) => completeSum.CarryOver == 0 ?
                    completeSum.Digits.Reverse()
                    : completeSum.CarryOver.Digits().Concat(completeSum.Digits).Reverse());
        }
    }
}
