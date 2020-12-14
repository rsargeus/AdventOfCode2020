using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static int validCombinations = 0;        

        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            //string inputtext = System.IO.File.ReadAllText(@"input-example2.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = values.Select(v => int.Parse(v)).OrderBy(x => x).ToList();

            //Insert charging outlet
            numbers.Insert(0, 0);

            //Insert device built-in joltage adapter
            int builtInAdapter = numbers.Last() + 3;
            numbers.Add(builtInAdapter);



            Group(numbers);
            
            int part1 = GetPart1(numbers);

            //FindValidCombinations(numbers.First(), numbers, builtInAdapter);

            double part2 = Group(numbers);

            Console.WriteLine($"Part1: {part1}. Part2: {part2}");
        }

        

        //To slow!
        private static void FindValidCombinations(int adapter, IEnumerable<int> numbers, int builtInAdapter)
        {            
            //Console.WriteLine(adapter);

            if (adapter == builtInAdapter)
            {
                validCombinations++;
                return;
            }
            

            var validAdapters = numbers.Where(n => (n - adapter) <= 3 && n > adapter);
            
            foreach(var a in validAdapters)
            //for(int i=0; i<validAdapters.Count();i++)
            {
                //Console.WriteLine(validAdapters[i]);
                FindValidCombinations(a, numbers, builtInAdapter);                                
            }

            //Console.WriteLine($"--{adapter}---{validCombinations}-----");            
        }

        private static int GetPart1(List<int> numbers)
        {
            int diff1 = 0;
            int diff3 = 0;

            for (int i = 1; i < numbers.Count; i++)
            {
                var diff = numbers[i] - numbers[i - 1];
                if (diff == 1)
                {
                    diff1++;
                }
                else if (diff == 3)
                {
                    diff3++;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            //Console.WriteLine($"Diff1: {diff1}. Diff3:{diff3}");
            int part1 = diff1 * diff3;
            return part1;
        }

        private static double Group(List<int> numbers)
        {
            int group = 0;            

            double product = 1;


            List<int> group2 = new List<int>();

            for (int i = 1; i < numbers.Count; i++)
            {

                int diffToNext = numbers[i] - numbers[i - 1];
                //int diffToPrevious = numbers[i-1] - numbers[i - 2];

                if (diffToNext == 1 && group < 4)
                {
                    //Console.Write(numbers[i-1] + " ");
                    group++;

                    group2.Add(numbers[i - 1]);
                }
                else 
                {
                    //Console.WriteLine($"({group-1}   {Math.Pow(2, group - 1)})");

                    string[] s = group2.Select(c => c.ToString()).Skip(1).ToArray();

                    group2 = new List<int>();

                    double value = Math.Pow(2, s.Length);

                    value = value == 8 ? 7 : value;

                    product *= value;

                    //Console.WriteLine($"{value}  {string.Join(' ', s)} :{product}");
                    group = 0;
                }
            }

            return product;
                        
        }
    }
}
