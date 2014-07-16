using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectEuler
{
    [EulerProblem(89, Title = "Develop a method to express Roman numerals in minimal form.")]
    class Problem89
    {
        public long Solve()
        {
            var lines = File.ReadAllLines("ProblemData\\Problem89Data.txt");

            var numberOfCharacters = lines.Select(line => line.Length).Sum();

            var minimisedNumberOfCharacters = lines
                .Select(line => line.ConvertToDecimal())
                .Select(number => number.ConvertToNumerals())
                .Select(numerals => numerals.Length)
                .Sum();

            return numberOfCharacters - minimisedNumberOfCharacters;
        }
    }
}
