using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            var values = inputtext.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int part1 = values.Select(v => v.Distinct()
                            .Where(c => c != '\n')
                            .Count()
                        ).Sum(i => i);

            int part2 = values.Select(v =>
            {
                var result = v.Where(char.IsLetter).GroupBy(c => c).Select(g => (
                    Letter: g.Key,
                    Count: g.Count()
                ));

                int persons = v.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length;

                return result.Where(a => a.Count == persons).Count();

            }).Sum(i => i);


            Console.WriteLine($"Part 1: {part1}, Part 2: {part2}");
        }
    }
}
