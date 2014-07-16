using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(19, Title = "How many Sundays fell on the first of the month during the twentieth century?")]
    public class Problem19
    {
        public long Solve()
        {
            DateTime endDate = new DateTime(2000, 12, 31);
            DateTime startDate = new DateTime(1901, 1, 1);

            return startDate
                .Unfold(date => date.AddMonths(1))
                .TakeWhile(date => date <= endDate)
                .Count(date => date.DayOfWeek == DayOfWeek.Sunday);
        }

        //The following method is functionally equivalent to the code above
        public long Solve2()
        {
            DateTime endDate = new DateTime(2000, 12, 31);
            DateTime startDate = new DateTime(1901, 1, 1);
            
            int count = 0;
            for (DateTime date = startDate; date < endDate; date = date.AddMonths(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    count++;
                } 
            }

            return count;
        }
    }
}
