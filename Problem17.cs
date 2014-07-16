using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
[EulerProblem(17, Title="How many letters would be needed to write all the numbers in words from 1 to 1000?")]
public class Problem17
{
    public long Solve()
    {
        return 
            1.To(1000)
                .Select(
                    number => number
                                .ConvertToWords()
                                .ToCharArray()
                                .Count(character => char.IsLetter(character))
                )
                .Sum();
    }

    
}
}
