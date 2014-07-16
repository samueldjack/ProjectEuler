using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public static class MaximumSumThroughTriangleSolver
    {
        public static int SolveFromFile(string dataFilePath)
        {
            string problemData = File.ReadAllText(dataFilePath);

            return Solve(problemData);
        }

        public static int Solve(string problemData)
        {
            var rows = problemData
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(line =>
                        line.Split(' ')
                            .Select(token => int.Parse(token))
                            .ToList()
                )
                .ToList();

            return rows.Aggregate(
                new List<int> {0},
                (currentMaxima, nextRow) =>
                    {
                        var rowLength = nextRow.Count();
                        return nextRow
                            .Select((cell, index) =>
                                    index == 0 ? cell + currentMaxima[index]
                                        : index == rowLength - 1 ? cell + currentMaxima[index - 1]
                                              : Math.Max(cell + currentMaxima[index - 1], cell + currentMaxima[index]))
                            .ToList();
                    }
                )
                .Max();
        }
    }
}
