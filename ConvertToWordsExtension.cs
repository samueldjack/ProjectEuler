using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEuler
{
    public static class ConvertToWordsExtension
    {
        private static Dictionary<long, string> _wordDictionary;
        private const int OneThousand = 1000;
        private const long OneMillion = 1000000;
        private const long OneBillion = 1000000000;
        private const long OneTrillion = 1000000000000;
        private const long OneQuadrillion = 1000000000000000;
        private const long OneQuintillion = 1000000000000000000;

        /// <summary>
        /// Converts a number to its English representation in words.
        /// </summary>
        /// <remarks>Uses the Short Scale for large numbers. See http://en.wikipedia.org/wiki/Long_and_short_scales for more details</remarks>
        public static string ConvertToWords(this long number)
        {
            EnsureWordDictionaryInitialised();

            if (number == long.MinValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return ConvertToWordsCore(number);
        }

        /// <summary>
        /// Converts a number to its English representation in words
        /// </summary>
        public static string ConvertToWords(this int number)
        {
            return ConvertToWords((long)number);
        }

        private static Dictionary<long, string> CreateWordDictionary()
        {
            return new Dictionary<long, string>
                       {
                           {0, "zero"},
                           {1, "one"},
                           {2, "two"},
                           {3, "three"},
                           {4, "four"},
                           {5, "five"},
                           {6, "six"},
                           {7, "seven"},
                           {8, "eight"},
                           {9, "nine"},
                           {10, "ten"},
                           {11, "eleven"},
                           {12, "twelve"},
                           {13, "thirteen"},
                           {14, "fourteen"},
                           {15, "fifteen"},
                           {16, "sixteen"},
                           {17, "seventeen"},
                           {18, "eighteen"},
                           {19, "nineteen"},
                           {20, "twenty"},
                           {30, "thirty"},
                           {40, "forty"},
                           {50, "fifty"},
                           {60, "sixty"},
                           {70, "seventy"},
                           {80, "eighty"},
                           {90, "ninety"},
                           {100, "hundred"},
                           {OneThousand, "thousand"},
                           {OneMillion, "million"},
                           {OneBillion, "billion"},
                           {OneTrillion, "trillion"},
                           {OneQuadrillion, "quadrillion"},
                           {OneQuintillion, "quintillion"}
                       };
        }

        private static void EnsureWordDictionaryInitialised()
        {
            // ensure thread-safety when caching our word dictionary
            // note: this doesn't prevent two copies of the word dictionary
            // being initialised - but that doesn't matter; only one would be
            // cached, the other garbage collected.
            if (_wordDictionary == null)
            {
                var dictionary = CreateWordDictionary();
                Interlocked.CompareExchange(ref _wordDictionary, dictionary, null);
            }    
        }

        private static string ConvertToWordsCore(long number)
        {
            return
                number < 0 ? "negative " + ConvertToWordsCore(Math.Abs(number))
                    : 0 <= number && number < 20 ? _wordDictionary[number]
                    : 20 <= number && number < 100 ? ProcessTens(number, _wordDictionary)
                    : 100 <= number && number < OneThousand ? ProcessHundreds(number, _wordDictionary)
                    : OneThousand <= number && number < OneMillion ? ProcessLargeNumber(number, OneThousand, _wordDictionary)
                    : OneMillion <= number && number < OneBillion ? ProcessLargeNumber(number, OneMillion, _wordDictionary)
                    : OneBillion <= number && number < OneTrillion ? ProcessLargeNumber(number, OneBillion, _wordDictionary)
                    : OneTrillion <= number && number < OneQuadrillion ? ProcessLargeNumber(number, OneTrillion, _wordDictionary)
                    : OneQuadrillion <= number && number < OneQuintillion ? ProcessLargeNumber(number, OneQuadrillion, _wordDictionary)
                    : ProcessLargeNumber(number, OneQuintillion, _wordDictionary); // long.Max value is just over nine quintillion
        }

        private static string ProcessLargeNumber(long number, long baseUnit, Dictionary<long, string> wordDictionary)
        {
            // split the number into number of baseUnits (thousands, millions, etc.)
            // and the remainder
            var numberOfBaseUnits = number / baseUnit;
            var remainder = number % baseUnit;
            // apply ConvertToWordsCore to represent the number of baseUnits as words
            string conversion = ConvertToWordsCore(numberOfBaseUnits) + " " + wordDictionary[baseUnit];
            // recurse for any remainder
            conversion += remainder > 0 ? ", " + ConvertToWordsCore(remainder) : "";
            return conversion;
        }

        private static string ProcessHundreds(long number, Dictionary<long, string> wordDictionary)
        {
            var hundreds = number / 100;
            var remainder = number % 100;
            string conversion = wordDictionary[hundreds] + " " + wordDictionary[100];
            conversion += remainder > 0 ? " and " + ConvertToWordsCore(remainder) : "";
            return conversion;
        }

        private static string ProcessTens(long number, Dictionary<long, string> wordDictionary)
        {
            Debug.Assert(0 <= number && number < 100);

            // split the number into the number of tens and the number of units,
            // so that words for both can be looked up independantly
            var tens = (number / 10) * 10;
            var units = number % 10;
            string conversion = wordDictionary[tens];
            conversion += units > 0 ? "-" + wordDictionary[units] : "";
            return conversion;
        }
    }
}
