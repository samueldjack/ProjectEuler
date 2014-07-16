using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public static class RomanNumeralExtensions
    {
        static Dictionary<int,string> _numberToNumeralLookup = new Dictionary<int,string>(){
            {1, "I"}, 
            {4, "IV"}, 
            {5, "V"}, 
            {9, "IX"}, 
            {10, "X"},  
            {40, "XL"}, 
            {50, "L"}, 
            {90, "XC"}, 
            {100, "C"}, 
            {400, "CD"},
            {500,"D"}, 
            {900, "CM"},
            {1000, "M"}};

        static Dictionary<string, int> _numeralToNumberLookup;
        static IStack<int> _availableNumerals;

        static RomanNumeralExtensions()
        {
            _numeralToNumberLookup = _numberToNumeralLookup.ToDictionary(pair => pair.Value, pair => pair.Key);
            _availableNumerals = _numberToNumeralLookup.Keys.OrderBy(x => x).ToStack();
        }

        public static int ConvertToDecimal(this string numerals)
        {
            var result = numerals
                .ToCharArray()
                .Reverse()
                .Select(c => _numeralToNumberLookup[c.ToString()])
                .Aggregate(
                    new { MaxValue = 0, RunningTotal = 0 },
                    (state, item) => new
                    {
                        MaxValue = Math.Max(state.MaxValue, item),
                        RunningTotal = item >= state.MaxValue ? state.RunningTotal + item 
                                                                       : state.RunningTotal - item
                    }, 
                    aggregate => aggregate.RunningTotal);

            return result;
        }

        public static string ConvertToNumerals(this int number)
        {
            Func<int, IStack<int>, IEnumerable<string>> numeralsEnumerator = Trampoline.MakeLazyTrampoline((int numberToConvert, IStack<int> availableNumerals) =>
            {
                if (numberToConvert == 0)
                {
                    return Trampoline.YieldBreak<int, IStack<int>, string>();
                }

                int currentNumeral = availableNumerals.Peek();

                int quotient = numberToConvert / currentNumeral;
                int remainder = numberToConvert % currentNumeral;

                string numberAsNumerals = _numberToNumeralLookup[currentNumeral].Repeat(quotient);

                return Trampoline.YieldAndRecurse<int, IStack<int>, string>(
                    numberAsNumerals, 
                    remainder, availableNumerals.Pop());
            }
            );

            var result = numeralsEnumerator(number, _availableNumerals)
                     .Aggregate(
                        new StringBuilder(),
                        (sb, numerals) => sb.Append(numerals),
                        sb => sb.ToString());

            return result;
        }
    }
}
