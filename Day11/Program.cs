using System;

namespace Day11
{
    class Program
    {
        public static string seats;
        public static int rowLength;

        static void Main(string[] args)
        {
            seats = System.IO.File.ReadAllText(@"input-example.txt");
            rowLength = seats.Split('\n')[0].Length;

            string nextRound = "";
            for (int i = 0; i < seats.Length; i++)             
            {
                var c = seats[i];
                if (c == '\n')
                {
                    nextRound += '\n'; 
                    continue;
                }

                if (c == '.')
                {
                    nextRound += '.';
                    continue;
                }

                if (c == 'L' && IsAvailable(i))
                {
                    nextRound += '#';
                }
                else 
                {
                    nextRound += 'L';
                }

                
                
                
            }

            //string inputtext = System.IO.File.ReadAllText(@"input-example2.txt");
            //string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);            

            Console.WriteLine(seats);

            Console.WriteLine();

            Console.WriteLine(nextRound);
        }

        private static bool IsAvailable(int i)
        {            
            int seatNumberAbove = i - rowLength;

            if (

                //Is outside area
                IsOutsideArea(seatNumberAbove) ||

                //Is empty
                seats[i] == 'L' &&

                //Above-Left
                IsFree(i - rowLength - 1) &&

                //Above-Right
                IsFree(i - rowLength + 1) &&

                //Right
                IsFree(i + 1) &&

                //Below
                IsFree(i + rowLength) &&

                //Below-Left
                IsFree(i + rowLength - 1) &&

                //Below-Right
                IsFree(i + rowLength + 1) &&

                //Left
                IsFree(i - 1)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsFree(int position)
        {
            if (seats[position] == 'L' || seats[position] == '\n' || seats[position] == '.')
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private static bool IsOutsideArea(int seatNumber)
        {
            if (seatNumber < 0 || seatNumber >= rowLength)
            {
                return true;
            }            
            else
            {
                return false;
            }
        }       
    }
}
