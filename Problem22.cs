using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(22, Title = "What is the total of all the name scores in the file of first names?")]
    public class Problem22
    {
        public long Solve()
        {
            const string fileName = @"ProblemData\Problem22.txt";

            return File.ReadAllText(fileName)
                .Split(new[] {','})
                .Select(name => name.Trim(new[] {'\"'}))
                .OrderBy(name => name)
                .Select((name, index) => (index + 1) * name.GetAlphabeticalValue())
                .Sum();
        }
    }

    public static class Problem22Extensions
    {
        public static int GetAlphabeticalValue(this string value)
        {
            return value.ToCharArray().Select(letter => (letter - 'A') +1).Sum();
        }
    }
}
