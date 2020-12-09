using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        static Dictionary<string, Bag> topBags = new Dictionary<string, Bag>();

        static void Main(string[] args)
        {
            string inputtext = System.IO.File.ReadAllText(@"input.txt");
            //string inputtext = System.IO.File.ReadAllText(@"input-example.txt");
            string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string bagRow in values)
            {
                string[] bagItem = bagRow.Split(new[] { "contain", "," }, StringSplitOptions.RemoveEmptyEntries);

                Bag topBag = Bag.Parse(bagItem[0]);

                for (int i = 1; i < bagItem.Length; i++)
                {
                    Bag bag = Bag.Parse(bagItem[i], out int number);
                    if (bag != null)
                    {
                        topBag.Add(number, bag);
                    }
                }
                topBags.Add(topBag.Id, topBag);
            }

            UpdateTree();
            

            //foreach (var b in topBags.Values) 
            //{
            //    Write((0, b), 0);
            //}

            //PART 1: 238
            int part1 = GetPartOne();

            //82930
            int part2 = GetPartTwo(topBags["shinygold"]);

            Console.WriteLine($"Part 1: {part1}, Part 2: {part2}");
        }

        private static int GetPartTwo(Bag bag)
        {
            //Write((0, bag), 0);

            IEnumerable<int> c = bag.Bags.Select(b => Count(b.Item1.Value, b.Item2));

            return c.Sum(i=> i);
        }                
        
        private static int Count(int number , Bag b)
        {
            int childCount = 1;            
            foreach ((int, Bag) x in b.Bags)
            {                
                childCount += Count(x.Item1, x.Item2);                                
            }

            return childCount * number;
            
        }

        private static int GetPartOne()
        {
            int part1 = 0;
            foreach (var b in topBags.Values)
            {
                if (Contains(b, "shinygold"))
                {
                    part1++;
                }
            }

            return part1;
        }

        private static void UpdateTree()
        {
            foreach (var bag in topBags.Values)
            {
                for (int i = 0; i < bag.Bags.Count; i++)
                {
                    string bagId = bag.Bags[i].Item2.Id;
                    bag.Bags[i] = (bag.Bags[i].Item1, topBags[bagId]);
                }
            }
        }

        private static void Write((int, Bag) bag, int indent)
        {
            if (bag.Item2.Id == "shinygold") 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }            
            
            Console.WriteLine(bag.Item1 + " " +  bag.Item2.ToString(indent));

            Console.ResetColor();
            foreach ((int, Bag) b in bag.Item2.Bags) 
            {
                Write(b, indent+2);
            }
        }

        private static bool Contains(Bag bag, string v)
        {
            if (topBags[bag.Id].Bags.Any(b => b.Item2.Id == v))
            {                
                return true;
            }

            foreach ((int, Bag) c in bag.Bags) 
            {
                if (Contains(c.Item2, v)) 
                {
                    return true;
                }
            }

            return false;
        }        
    }
}
