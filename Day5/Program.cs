using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<int> seatIds = values.Select(boardingpass =>
            {
                (int, int, int, int) seat = (0, 127, 0, 7);
                foreach (char c in boardingpass)
                {
                    seat = c switch
                    {
                        'F' => (seat.Item1, seat.Item1 + (seat.Item2 - seat.Item1) / 2, seat.Item3, seat.Item4),
                        'B' => ((seat.Item2 - (seat.Item2 - seat.Item1) / 2), seat.Item2, seat.Item3, seat.Item4),
                        'L' => (seat.Item1, seat.Item2, seat.Item3, seat.Item3 + (seat.Item4 - seat.Item3) / 2),
                        'R' => (seat.Item1, seat.Item2, (seat.Item4 - (seat.Item4 - seat.Item3) / 2), seat.Item4)
                    };
                }
                return seat.Item1 * 8 + seat.Item3;
            });

            int part1 = seatIds.Max();
            int part2 = Enumerable.Range(seatIds.Min(), seatIds.Count()).Except(seatIds).Single();

            Console.WriteLine(part2);
        }
    }
}