using System;
using System.Collections.Generic;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            var values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            //int part1 = Part1(values);
            int part2 = Part2(values);

            Console.WriteLine(part2);
            Console.ReadLine();
        }

        private static int Part1(string[] values)
        {
            IEnumerable<bool> accepted = values.Select(v =>
            {
                var line = v.Split(' ');

                var maxMin = line[0].Split('-');

                int min = Convert.ToInt32(maxMin[0]);
                int max = Convert.ToInt32(maxMin[1]);
                char c = line[1][0];
                string value = line[2];

                int nrOfOccurance = value.Count(s => s == c);

                return (min <= nrOfOccurance && nrOfOccurance <= max);
            });

            return accepted.Count(b => b == true);
        }

        private static int Part2(string[] values)
        {
            IEnumerable<bool> test = values.Select(v =>
            {
                var line = v.Split(' ');

                var firstSecond = line[0].Split('-');

                int first = Convert.ToInt32(firstSecond[0]);
                int second = Convert.ToInt32(firstSecond[1]);
                char c = line[1][0];
                
                string value = line[2];

                return value[first - 1] == c ^ value[second - 1] == c;
            });

            return test.Count(b => b == true);
        }
    }
}
