using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(36, Title = "Find the sum of all numbers less than one million, which are palindromic in base 10 and base 2.")]
    public class Problem36
    {
        public long Solve()
        {
            return 1.To(1000000)
                .Where(number => number.IsPalindromic() && number.IsPalindromicBase2())
                .Sum();
        }
    }
}
