using System;
using System.Linq;

namespace Day11
{
    class Program
    {        
        public static int rowLength;
        public static int round = 0;

        static void Main(string[] args)
        {
            string seats = System.IO.File.ReadAllText(@"input.txt");
            rowLength = seats.Split('\n')[0].Length+1;
            Console.WriteLine(seats);            

            string previousRound = "";
            for (round = 1; seats != previousRound; round++) 
            {
                Console.WriteLine($"----------Round {round}------------------------");

                previousRound = seats;
                seats = NewRound(seats, 5, int.MaxValue);
                Console.WriteLine(seats);
                
            }

            int part1 = seats.Count(c => c == '#');
            Console.WriteLine($"Part1: {part1}");   
        }

        private static string NewRound(string seats, int maximumOccupiedSeats, int sightDistance)
        {
            string nextRound = "";
            for (int i = 0; i < seats.Length; i++)
            {
                var c = seats[i];
                switch (c)
                {
                    case '\n':
                        nextRound += c;
                        continue;
                    case '.':
                        nextRound += c;
                        continue;

                    case 'L':
                        if (IsAvailable(i, seats, sightDistance))
                        {
                            nextRound += '#';
                        }
                        else 
                        {
                            nextRound += c;
                        }
                        continue;
                    case '#':
                        if (ToCrowded(i, seats, maximumOccupiedSeats, sightDistance))
                        {
                            nextRound += 'L';
                        }
                        else
                        {
                            nextRound += c;
                        }

                        continue;
                }                
            }

            return nextRound;
        }

        private static bool ToCrowded(int i, string seats, int maximumOccupiedSeats, int sightDistance)
        {
            bool[] a = GetOccupiedSeats(i, seats, sightDistance);
            int b = a.Where(b => b).Count();

            return a[0] && b > maximumOccupiedSeats;
        }

        private static bool IsAvailable(int i, string seats, int sightDistance)
        {            

            bool[] a = GetOccupiedSeats(i, seats, sightDistance);
            int b = a.Where(b => b).Count();

            return b == 0;
        }

        private static bool[] GetOccupiedSeats(int i, string seats, int sightDistance)
        {                        
            var bits = new bool[9];
            
            //Is empty
            bits[0] = seats[i] == '#';

            //Above-Left
            bits[1] = IsInSight(i, seats, -1, -1, sightDistance);

            //Above
            bits[2] = IsInSight(i, seats, 0, -1, sightDistance);

            //Above-Right
            bits[3] = IsInSight(i, seats, 1, -1, sightDistance);

            //Right            
            bits[4] = IsInSight(i, seats, 1, 0, sightDistance);            

            //Below-Left
            bits[5] = IsInSight(i, seats, -1, 1, sightDistance);

            //Below
            bits[6] = IsInSight(i, seats, 0, 1, sightDistance);

            //Below-Right
            bits[7] = IsInSight(i, seats, 1, 1, sightDistance);

            //Left
            bits[8] = IsInSight(i, seats, -1, 0, sightDistance);            

            return bits;
        }

        private static bool IsInSight(int position, string seats, int horizontalAdjustment, int verticalAdjustment, int distance) 
        {
            bool outOfRange = false;
            
            for(int i=1; !outOfRange && i <= distance; i++)
            {

                int x = horizontalAdjustment * i;
                int y = position + (rowLength * i * verticalAdjustment);

                int seenPosition = x + y;

                if (i == 2 && round == 2) 
                {
                    string s = "";
                }



                
                
                char? seat = TryGetSeat(seenPosition, seats, out outOfRange);

                if (outOfRange)
                    return false;

                switch (seat.Value) 
                {
                    case 'L':
                        return false;
                    case '#':
                        return true;
                    case '.':
                        continue;
                    case '\n':
                        return false;
                        
                }                                
            }

            return false;            
        }


        private static char? TryGetSeat(int position, string seats, out bool outOfRange)
        {
            outOfRange = position < 0 || position >= seats.Length;            

            if (outOfRange)
                return null;
            else
                return seats[position];                                    
        }
    }
}
