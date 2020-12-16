using System;
using System.Linq;

namespace Day11
{
    class Program
    {        
        public static int rowLength;

        static void Main(string[] args)
        {
            string seats = System.IO.File.ReadAllText(@"input-example.txt");
            rowLength = seats.Split('\n')[0].Length+1;
            Console.WriteLine(seats);            

            string previousRound = "";
            for (int i = 1; seats != previousRound; i++) 
            {
                Console.WriteLine($"----------Round {i}------------------------");

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
            /*int aboveLeft = i - rowLength - 1;
            int above = i - rowLength;
            int aboveRight = i - rowLength + 1;
            int right = i + 1;
            int belowLeft = i + rowLength - 1;
            int below = i + rowLength;
            int belowRight = i + rowLength + 1;
            int left = i - 1;*/
            
            var bits = new bool[9];

            //Is empty
            bits[0] = seats[i] == '#';

            //Above-Left
            bits[1] = !IsFree(i, seats, -1, -1, sightDistance);

            //Above
            bits[2] = !IsFree(i, seats, 0, -1, sightDistance);

            //Above-Right
            bits[3] = !IsFree(i, seats, 1, -1, sightDistance);

            //Right
            //var asd = !IsFree(right, seats); 
            bits[4] = !IsFree(i, seats, 1, 0, sightDistance);

            

            //Below-Left
            bits[5] = !IsFree(i, seats, -1, 1, sightDistance);

            //Below
            bits[6] = !IsFree(i, seats, 0, 1, sightDistance);

            //Below-Right
            bits[7] = !IsFree(i, seats, 1, 1, sightDistance);

            //Left
            bits[8] = !IsFree(i, seats, -1, 0, sightDistance);
            //bits[8] = !IsFree(left, seats);

            return bits;
        }

        private static bool IsFree(int position, string seats, int horizontalAdjustment, int verticalAdjustment, int distance) 
        {
            bool outOfRange = false;
            
            for(int i=1; !outOfRange && i <= distance; i++)
            {

                if (i == 2) 
                {
                    string s = "";
                }



                int x = horizontalAdjustment * i;
                int y = position + (rowLength * i * verticalAdjustment);
                
                bool isFree = IsFree(y + x, seats, out outOfRange);

                if (isFree && !outOfRange) 
                {
                    return true;
                }
                
                if (outOfRange) 
                {
                    return true;
                }
                
                /*
                if (!isFree) 
                {
                    return true;
                }*/
            }

            return false;            
        }       
        

        private static bool IsFree(int position, string seats, out bool outOfRange)
        {
            outOfRange = position < 0 || position >= seats.Length;

            return outOfRange || seats[position] == 'L' || seats[position] == '\n' || seats[position] == '.';
        }
    }
}
