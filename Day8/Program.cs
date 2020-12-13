using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    class Program
    {        
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            //string inputtext = System.IO.File.ReadAllText(@"input-example.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);


            Dictionary<int, (string operation, int number)> instructions = new Dictionary<int, (string operation, int number)>();

            for (int i = 0; i < values.Length; i++)
            {
                var v = values[i];

                string[] split = v.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                instructions.Add(i, (split[0], int.Parse(split[1])));
            }

            GetPart1(instructions, out int part1);            
            
            int part2 = 0;
            bool foundPart2 = false;
            for (int i=0; i<instructions.Count(); i++) 
            {                
                if (instructions[i].operation == "jmp") 
                {                    
                    foundPart2 = GetPart1(Modify(i, instructions), out part2);

                    if (foundPart2) 
                    {
                        break;
                    }
                }
            }                       

            Console.WriteLine($"Part1: {part1}. Part2: {part2}:{foundPart2}");
        }        

        private static bool GetPart1(Dictionary<int, (string command, int number)> instructions, out int accumulator)
        {
            Dictionary<int, int> performedInstructions = new Dictionary<int, int>();

            int index = 0;
            accumulator = 0;

            for (int i = 0; index < instructions.Count(); i++)
            {                
                index = RunCommand(index, instructions[index], performedInstructions, out accumulator);

                if (performedInstructions.ContainsKey(index))
                {
                    return false;
                }

                performedInstructions.Add(index, accumulator);
            }

            return true;
        }

        private static int RunCommand(int index, (string operation, int number) instruction, Dictionary<int, int> performedInstructions, out int accumulator)
        {

            if (!performedInstructions.Any())
            {
                accumulator = 0;
            }
            else 
            {
                accumulator = performedInstructions.Last().Value;
            }
   
            switch (instruction.operation)
            {
                case "nop":
                    return index+1;
                case "acc":
                    accumulator += instruction.number;
                    return index + 1;
                case "jmp":                    
                    return index + instruction.number;

                default:
                    throw new InvalidOperationException(instruction.operation);
            }
        }

        private static Dictionary<int, (string command, int number)> Modify(int i, Dictionary<int, (string command, int number)> instructions)
        {
            var clone = new Dictionary<int, (string command, int number)>(instructions);

            var instr = clone[i];
            instr.command = "nop";
            clone[i] = instr;

            return clone;
        }
    }
}