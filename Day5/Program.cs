using System;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var seatIds = values.Select(boardingpass =>
            {
                string bytes = boardingpass
                    .Replace('F', '0')
                    .Replace('B', '1')
                    .Replace('L', '0')
                    .Replace('R', '1');

                return Convert.ToInt32(bytes, 2);

                //(int, int, int, int) seat = (0, 127, 0, 7);
                //foreach (char c in boardingpass)
                //{
                //    seat = c switch
                //    {
                //        'F' => (seat.Item1, seat.Item1 + (seat.Item2 - seat.Item1) / 2, seat.Item3, seat.Item4),
                //        'B' => ((seat.Item2 - (seat.Item2 - seat.Item1) / 2), seat.Item2, seat.Item3, seat.Item4),
                //        'L' => (seat.Item1, seat.Item2, seat.Item3, seat.Item3 + (seat.Item4 - seat.Item3) / 2),
                //        'R' => (seat.Item1, seat.Item2, (seat.Item4 - (seat.Item4 - seat.Item3) / 2), seat.Item4)
                //    };
                //}
                //return (boardingpass, seat.Item1 * 8 + seat.Item3);
            });

            int part1 = seatIds.Max();
            int part2 = Enumerable.Range(seatIds.Min(), seatIds.Count()).Except(seatIds).Single();

            Console.WriteLine($"Part 1: {part1}, Part 2: {part2}");
            Console.ReadLine();
        }
    }
}