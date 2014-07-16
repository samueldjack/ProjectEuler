using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(112, Title="Investigating the density of 'bouncy' numbers.")]
    class Problem112
    {
        public long Solve()
        {
            return (99L).Unfold<long>(x => x + 1)
                .LazilyAggregate(
                new {n = 99L, BouncyNumbers = 0L},
                (n, aggregate) => new { n, BouncyNumbers = aggregate.BouncyNumbers + (n.IsBouncy() ? 1 : 0) }
                )
                .SkipWhile(aggregate => ((double)aggregate.BouncyNumbers)/aggregate.n < 0.99)
                .First()
                .n;
        }
    }

    public static class Problem112Extensions
    {
        public static bool IsBouncy(this long number)
        {
            var analyisResult= number
                .Digits()
                .Aggregate(
                    new {PreviousDigit = default(int?), Increasing = true, Decreasing = true},
                    (aggregate, current) => new
                                           {
                                               PreviousDigit = (int?)current,
                                               Increasing = aggregate.Increasing && (aggregate.PreviousDigit ?? 1) <= current,
                                               Decreasing = aggregate.Decreasing && (aggregate.PreviousDigit ?? 9) >= current
                                           });

            return !analyisResult.Increasing && !analyisResult.Decreasing;
        }
    }
}
