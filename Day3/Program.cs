using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);            

            int right1Down1 = FindTrees(1, 1, values);
            int right3Down1 = FindTrees(3, 1, values);
            int right5Down1 = FindTrees(5, 1, values);
            int right7Down1 = FindTrees(7, 1, values);
            int right1Down2 = FindTrees(1, 2, values);

            //int part1 = right3Down1;
            
            int part2 = right1Down1 * right3Down1 * right5Down1 * right7Down1 * right1Down2;

            Console.WriteLine(part2);
            Console.ReadLine();
        }

        private static int FindTrees(int skipX, int skipY, string[] values)
        {
            int treeCounter = 0;
            int x = 0;
            for (int y = 0; y < values.Length; y+=skipY)
            {
                char[] road = values[y].ToCharArray();
                char c = road[x];

                if (c == '#')
                {
                    treeCounter++;
                }

                x += skipX;
                if (x >= road.Length)
                {
                    x -= road.Length;
                }
            }
            return treeCounter;
        }
    }
}