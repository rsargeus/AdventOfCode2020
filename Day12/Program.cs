using System;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText(@"input.txt");
            string[] instructions = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            (int north, int east, int degrees) boatPosition = CreateBoatPosition(0, 0, 90);

            //(int north, int east, int degrees) wayPoint = CreatePosition(0, 0, 90);



            foreach (string instr in instructions)
            {
                string command = instr.Substring(0, 1);
                int value = Convert.ToInt32(instr.Substring(1, instr.Length - 1));


                if (command == "F") 
                {
                    command = ConvertCommand(boatPosition.degrees);                    
                }
                
                boatPosition = MoveBoat(boatPosition, command, value);
                Console.WriteLine(boatPosition);
            }

            Console.WriteLine($"Part1: { Math.Abs(boatPosition.north) + Math.Abs(boatPosition.east) }");
        }

        private static string ConvertCommand(int degrees)
        {
            degrees %= 360;
            degrees += 360;
            degrees %= 360;

            return degrees switch
            {
                0 => "N",
                90 => "E",
                180 => "S",
                270 => "W",
                _ => throw new InvalidOperationException(),
            };
        }

        private static (int north, int east, int degrees) CreateBoatPosition(int north, int east, int degrees)
        {
            return (north, east, degrees);
        }

        private static (int north, int east, int degrees) MoveBoat((int north, int east, int degrees) position, string command, int value)
        {
            return command switch
            {
                "N" => CreateBoatPosition(position.north + value, position.east, position.degrees),
                "E" => CreateBoatPosition(position.north, position.east + value, position.degrees),
                "S" => CreateBoatPosition(position.north - value, position.east, position.degrees),
                "W" => CreateBoatPosition(position.north, position.east-value, position.degrees),
                "R" => CreateBoatPosition(position.north, position.east, position.degrees + value),
                "L" => CreateBoatPosition(position.north, position.east, position.degrees - value),                                
                _ => throw new InvalidOperationException(),
            };
        }

    }
}
