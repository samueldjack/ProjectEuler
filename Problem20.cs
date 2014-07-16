using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(20, Title = "Find the sum of digits in 100!")]
    public class Problem20
    {
        public long Solve()
        {
            IEnumerable<int> factorial = Functional.Unfold(
                new
                    {
                        Term = 1,
                        Number = new []{1} as IEnumerable<int>
                    },
                previous => new
                                {
                                    Term = previous.Term + 1,
                                    Number = Enumerable.Repeat(previous.Number, previous.Term + 1).SumNumbers()
                                }
                )
                .SkipWhile(term => term.Term < 100)
                .First()
                .Number;

            return factorial.Sum();
        }
    }
}
