using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            //string inputtext = System.IO.File.ReadAllText(@"input-example.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<long> numbers = values.Select(v => long.Parse(v)).ToList();

            long part1 = Find(25, numbers);
            var foundPart2 = TryGetSumRange(numbers, part1, out IEnumerable<long> range);            
            long part2 = range.Min() + range.Max();

            Console.WriteLine($"Part1: {part1} Part2: {part2} : {foundPart2}");
        }

        static long Find(int preamble, List<long> numbers)
        {
            for (int i = preamble; i < numbers.Count(); i++)
            {
                IEnumerable<long> range = numbers.Skip(i - preamble).Take(preamble);
                var sums = GetCombination(range.ToList());                

                if (!sums.Contains(numbers[i]))
                {
                    return numbers[i];
                }
            }
            throw new InvalidOperationException();
        }

        static bool TryGetSumRange(List<long> list, long number, out IEnumerable<long> range)
        {
            range = null;

            for (int i = 0; i < list.Count(); i++)
            {
                long sum = list[i];
                for (int j = i+1; j < list.Count(); j++)
                {
                    sum += list[j];
                    if (sum == number)
                    {
                        range = list.Skip(i).Take(j-i+1);
                        return true;
                    }
                    else if(sum > number)
                    {
                        break;
                    }
                }
            }

            return false;
        }

        static IEnumerable<long> GetCombination(List<long> list)
        {
            List<long> sums = new List<long>();
            for (int i = 0; i < list.Count(); i++)
            {
                for (int j = 0; j < list.Count(); j++)
                {
                    if (i != j) 
                    {
                        sums.Add(list[i] + list[j]);
                    }                                        
                }
            }

            return sums.Distinct();
        }
    }
}
