using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string values = System.IO.File.ReadAllText(@"input.txt");
            string[] passportData = values.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);


            IEnumerable<Dictionary<string, string>> passports = passportData.Select(p =>
            {
                var fields = p.Split(new[] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> passportFields = fields.Select(f => 
                {
                    var split = f.Split(':');
                    return (split[0], split[1]);

                }).ToDictionary(t=> t.Item1, t=> t.Item2);
                return passportFields;
            });

            int part1 = passports.Where(p => p.Count() == 8 || p.Count() == 7 && !p.Keys.Contains("cid")).Count();

            int part2 = passports.Where(p => 
                    IsValidByr(p) && 
                    IsValidIyr(p) &&
                    IsValidEyr(p) &&
                    IsValidHgt(p) &&
                    IsValidHcl(p) &&
                    IsValidEcl(p) &&
                    IsValidPid(p)
                ).Count();
            
            Console.WriteLine(part2);
        }        

        private static bool IsValidByr(Dictionary<string, string> passport)
        {
            passport.TryGetValue("byr", out string value);
            return IsValidYear(value, 1920, 2002);
        }

        private static bool IsValidIyr(Dictionary<string, string> passport)
        {
            passport.TryGetValue("iyr", out string value);
            return IsValidYear(value, 2010, 2020);                        
        }

        private static bool IsValidEyr(Dictionary<string, string> passport)
        {
            passport.TryGetValue("eyr", out string value);
            return IsValidYear(value, 2020, 2030);
        }

        private static bool IsValidHgt(Dictionary<string, string> p)
        {
            p.TryGetValue("hgt", out string value);
            if (value is null) 
            {
                return false;
            }

            int number = int.Parse(value.Substring(0, value.Length - 2));
            string unit = value.Substring(value.Length - 2, 2);

            return unit switch
            {
                "cm" => 150 <= number && number <= 193,
                "in" => 59 <= number && number <= 76,
                _ => false
            };
        }

        private static bool IsValidHcl(Dictionary<string, string> p)
        {
            p.TryGetValue("hcl", out string value);
            if (value is null)
            {
                return false;
            }
            string pattern = @"^#(?:[0-9a-fA-F]{3}){1,2}$";
            Match m = Regex.Match(value, pattern, RegexOptions.IgnoreCase);

            return m.Success;
        }

        private static bool IsValidEcl(Dictionary<string, string> p)
        {
            p.TryGetValue("ecl", out string value);
            if (value is null)
            {
                return false;
            }

            return value switch
            {
                "amb" => true,
                "blu" => true,
                "brn" => true,
                "gry" => true,
                "grn" => true,
                "hzl" => true,
                "oth" => true,
                _ => false
            };
        }

        private static bool IsValidPid(Dictionary<string, string> p)
        {
            p.TryGetValue("pid", out string value);
            if (value is null)
            {
                return false;
            }

            if (value.Length != 9)
            {
                return false;
            }

            return int.TryParse(value, out _);

        }

    private static bool IsValidYear(string value, int min, int max)
        {
            int.TryParse(value, out int year);
            return min <= year && year <= max;
        }       
    }
}
