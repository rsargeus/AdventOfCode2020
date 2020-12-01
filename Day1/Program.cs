using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            int[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToArray();

            //int part1 = GetPart1(values);
            int part2 = GetPart2(values);

            Console.WriteLine(part2);
        }

        private static int GetPart1(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {                

                for (int j = ints.Length - 1; j >= 0; j--)
                {
                    int sum = ints[i] + ints[j];
                    if (sum == 2020)
                    {
                        return (ints[i] * ints[j]);
                    }
                }
            }

            throw new InvalidOperationException("Could not find product equals 2020");
        }

        private static int GetPart2(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {                
                for (int j = ints.Length - 1; j >= 0; j--)
                {
                    for (int k = 0; k < ints.Length; k++)
                    {
                        if(ints[k] != ints[i] && ints[k] != ints[j])
                        {
                            int sum = ints[i] + ints[j] + ints[k];
                            if (sum == 2020)
                            {
                                return (ints[i] * ints[j] * ints[k]);
                            }
                        }
   
                    }
                }
            }

            throw new InvalidOperationException("Could not find product equals 2020");
        }
    }
}
