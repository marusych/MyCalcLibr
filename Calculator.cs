using System;
using System.Text.RegularExpressions;

namespace MyCalcLibr
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            List<int> negativeNum = new List<int>();
            List<string> delimiterOne = new List<string> { ",", "\n" };
            var path = @"//(.+)\n";
            var path1 = @"//\[(.+?)\]\n";
            Regex delimiter = new Regex(path);

            if (new Regex(path1).Match(numbers).Success)
            {
                var delimiterMatch = Regex.Match(numbers, path);
                if (delimiterMatch.Success)
                {
                    var delimiterLine = delimiterMatch.Groups[1].Value;
                    numbers = numbers.Substring(numbers.IndexOf('\n') + 1);

                    var customDelimiters = Regex.Matches(delimiterLine, @"\[(.+?)\]")
                        .Cast<Match>()
                        .Select(match => match.Groups[1].Value)
                        .ToList();

                    delimiterOne.AddRange(customDelimiters);
                }
            }else if (delimiter.Match(numbers).Success)
                {
                    delimiterOne = delimiter.Matches(numbers)
                        .Cast<Match>()
                        .Select(match => Regex.Escape(match.Groups[1].Value))
                        .ToList();
                    numbers = numbers.Substring(numbers.IndexOf('\n') + 1);
                }

            var num = numbers.Split(delimiterOne.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            int sum = 0;

            foreach (string number in num)
            {
                if (!string.IsNullOrEmpty(number))
                {
                    int numOne = int.Parse(number);
                    if (numOne < 0)
                    {
                        negativeNum.Add(numOne);
                    }
                    else if (numOne < 1000)
                    {
                        sum += numOne;
                    }
                }
            }

            if (negativeNum.Count > 0)
            {
                throw new ArgumentException("Negatives not allowed: " + string.Join(", ", negativeNum));
            }
            return sum;
        }
    }
}