using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTwo
{
    class Program
    {
        //First argument must be 1 for the solution of the first part of the puzzle or 2 for the solution of the second part.
        static void Main(string[] args)
        {
            int sum = 0;

            if (args[0].Equals("1"))
            {
                sum = CheckSumOne(args);
            }
            else if (args[0].Equals("2"))
            {
                sum = CheckSumTwo(args);
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        public static int ParseInt(string word)
        {
            int result;

            if (!int.TryParse(word, out result))
            {
                throw new ArgumentException("Input string can only contain digits");
            }

            return result;
        }

        public static int CheckSumOne(string[] args)
        {
            bool first = true;
            int min = 0;
            int max = 0;
            int checkSum = 0;

            for (int i = 1; i < args.Length; ++i)
            {
                string word = args[i];
                if (first)
                {
                    min = ParseInt(word);
                    max = min;
                    first = false;
                }

                if (word.Contains('\n'))
                {
                    String[] splits = word.Split('\n');
                    int value = ParseInt(splits[0]);

                    if (value > max)
                    {
                        max = value;
                    }

                    if (value < min)
                    {
                        min = value;
                    }

                    checkSum += max - min;

                    min = ParseInt(splits[1]);
                    max = min;
                }
                else
                {
                    int value = ParseInt(word);

                    if (value > max)
                    {
                        max = value;
                    }

                    if (value < min)
                    {
                        min = value;
                    }
                }
            }

            checkSum += max - min;

            return checkSum;
        }

        public static int CheckSumTwo(string[] args)
        {
            List<List<int>> table = new List<List<int>>();
            List<int> currentLine = new List<int>();
            table.Add(currentLine);

            int sum = 0;

            for (int i = 1; i < args.Length; ++i)
            {
                string word = args[i];

                if(word.Contains('\n'))
                {
                    string[] splits = word.Split('\n');
                    int value = ParseInt(splits[0]);
                    currentLine.Add(value);

                    currentLine = new List<int>();
                    table.Add(currentLine);
                    value = ParseInt(splits[1]);
                    currentLine.Add(value);
                }
                else
                {
                    int value = ParseInt(word);
                    currentLine.Add(value);
                }
            }

            foreach(List<int> line in table)
            {
                bool stop = false;
                for(int i = 0;  i < line.Count(); ++i) 
                {
                    for(int j = 0; j < line.Count(); ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        int numFirst = line.ElementAt(i);
                        int numSecond = line.ElementAt(j);

                        if(numFirst % numSecond == 0)
                        {
                            stop = true;
                            sum += numFirst / numSecond;
                            break;
                        }
                    }

                    if(stop == true)
                    {
                        break;
                    }
                }
            }

            return sum;
        }
    }
}
