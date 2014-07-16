using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public class PascalsTriangle
    {
        public static long GetEntry(int row, int column)
        {
            // the L suffix on "Entry = 1L" is to force Entry to have a long type
            return Functional.Unfold(new {Entry = 1L, Column = 1},
                                     previous =>
                                     new
                                         {
                                             Entry = (previous.Entry*(row + 1 - previous.Column))/previous.Column,
                                             Column = previous.Column + 1
                                         })
                .SkipWhile(item => item.Column <= column)
                .First()
                .Entry;
        }
    }
}
