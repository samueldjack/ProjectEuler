using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(25, Title = "What is the first term in the Fibonacci sequence to contain 1000 digits?")]
    public class Problem25
    {
        public long Solve()
        {
            return Functional.Unfold(
                new
                    {
                        Term = (IEnumerable<int>) new[] {2},
                        Previous = (IEnumerable<int>) new[] {1},
                        TermNumber = 3
                    },
                previous => new
                                {
                                    Term = previous.Term.SumNumbers(previous.Previous),
                                    Previous = previous.Term,
                                    TermNumber = previous.TermNumber + 1
                                }
                )
                .SkipWhile(term => term.Term.Count() < 1000)
                .First()
                .TermNumber;
        }
    }
}
