using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(21, Title = "Evaluate the sum of all amicable pairs under 10000.")]
    public class Problem21
    {
        public long Solve()
        {
            Func<int, int> d = n => n.Divisors().Sum() - n;

            return (from a in 1.To(10000)
                    let b = d(a)
                    where a != b && d(b) == a
                    select a)
                    .Sum();
        }
    }
}
