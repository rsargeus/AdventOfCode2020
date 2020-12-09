using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    internal class Bag
    {
        public string Id 
        { 
            get
            {
                return Tone + Color;
            }
        }

        public List<(int?, Bag)> Bags { get; } = new List<(int?, Bag)>();
        internal string Tone { get; set; }
        internal string Color { get; set; }

        internal static Bag Parse(string s, out int number)
        {
            string[] bagData = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            bool isChild = int.TryParse(bagData[0], out number);

            if (isChild)
            {
                return new Bag { Tone = bagData[1], Color = bagData[2] };
            }
            else 
            {
                return null;
            }            
        }

        internal static Bag Parse(string s)
        {
            string[] bagData = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Bag bag = new Bag { Tone = bagData[0], Color = bagData[1] };
            return bag;
        }

        internal void Add(int number, Bag bag)
        {            
            Bags.Add((number, bag));
        }

        internal string ToString(int indent)
        {
            string s = "";
            for (int i=0; i<indent;i++)
            {
                s += " ";
            }

            return $"{s}{Tone} {Color}";
        }
    }
}
