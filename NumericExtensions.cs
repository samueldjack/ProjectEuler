using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEuler
{
    public static class NumericExtensions
    {
        public static IEnumerable<long> Range(long first, long last)
        {
            if (first == last)
            {
                yield return first;
            }
            else if (first < last)
            {
                for (long l = first; l <= last; l++)
                {
                    yield return l;
                }
            }
            else
            {
                for (long l = first; l >= last; l--)
                {
                    yield return l;
                }
            }
        }

        /// <summary>
        /// Calculate the sum of two numbers represented as a sequence of digits. Numbers should be given
        /// with least significant digits first
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns>The sum of the two numbers as a sequence of digits</returns>
        public static IEnumerable<int> SumNumbers(this IEnumerable<int> number1, IEnumerable<int> number2)
        {
            // put the numbers together in an array so that we can zip them easily
            var numbers = new[] { number1, number2 };

            return SumNumbers(numbers);
        }

        /// <summary>
        /// Calculate the sum of two numbers represented as a sequence of digits. Numbers should be given
        /// with least significant digits first
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns>The sum of the two numbers as a sequence of digits</returns>
        public static IEnumerable<int> SumNumbers(this IEnumerable<IEnumerable<int>> numbers)
        {
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

       

        public static bool IsDivisibleBy(this int number, long factor)
        {
            return number % factor == 0;
        }

        public static IEnumerable<int> Divisors(this int number)
        {
            return (from factor in 1.To((int)Math.Sqrt(number))
                    where number.IsDivisibleBy(factor)
                    select new [] { factor, number / factor }).Concat();    
        }

        public static IList<PrimeFactor> PrimeFactors(this int number)
        {
            return ContinueFactorisation(new List<PrimeFactor> { PrimeFactor.Unity }, number);
        }

        public static IList<PrimeFactor> PrimeFactors(this long number)
        {
            return ContinueFactorisation(new List<PrimeFactor> { PrimeFactor.Unity }, number);
        }

        private static IList<PrimeFactor> ContinueFactorisation(IList<PrimeFactor> partialFactorisation, long remainder)
        {
            var factorGreaterThan = partialFactorisation.Last().Prime;

            // find next factor of number
            long nextFactor = Range(factorGreaterThan + 1, remainder)
                .SkipWhile(x => remainder % x > 0).FirstOrDefault();

            if (nextFactor == remainder)
            {
                return ExtendList<PrimeFactor>(partialFactorisation, new PrimeFactor { Prime = remainder, Multiplicity = 1 });
            }
            else
            {
                // find its multiplicity
                long multiplicity = Enumerable.Range(1, Int32.MaxValue).TakeWhile(x => remainder % (long)Math.Pow(nextFactor, x) == 0).Last();
                long quotient = remainder / (long)Math.Pow(nextFactor, multiplicity);

                PrimeFactor nextPrimeFactor = new PrimeFactor { Prime = nextFactor, Multiplicity = multiplicity };
                var extendedList = ExtendList<PrimeFactor>(partialFactorisation, nextPrimeFactor);
                
                if (quotient == 1)
                {
                    return  extendedList;
                }
                else
                {
                    return ContinueFactorisation(extendedList, quotient);
                }
            }
        }

        private static IList<T> ExtendList<T>(IList<T> list, T additionalItem)
        {
            List<T> extendedList = new List<T>(list);
            extendedList.Add(additionalItem);

            return extendedList;
        }
    }
}
