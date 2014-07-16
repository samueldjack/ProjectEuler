using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(31, Title = "Investigating combinations of English currency denominations.")]
    public class Problem31
    {
        public long Solve()
        {
            var coins = new[] {1, 2, 5, 10, 20, 50, 100, 200};
            coins.OrderByDescending(x => x).ToList();

            return CountCombinations(200, coins);
        }

        private long CountCombinations(int amount, IList<int> orderedCoinList)
        {
            // if there is only one type of coin left, then
            // there is either one combination, or zero, 
            // depending on whether the amount is divisible by the value of the coin.
            if (orderedCoinList.Count == 1)
            {
                return (amount % orderedCoinList[0]) == 0 ? 1 : 0;
            }

            // pick the largest coin in the list
            var largestCoin = orderedCoinList.First();
            var reducedCoinList = orderedCoinList.Skip(1).ToList();

            var timesCoinCanBeUsed = amount / largestCoin;

            return (
                    from usages in 0.To(timesCoinCanBeUsed)
                    select 
                        CountCombinations(amount - (largestCoin * usages), reducedCoinList)
                    )
                    .Sum();

        }
    }
}
