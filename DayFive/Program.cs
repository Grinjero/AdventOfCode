using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayThree;

namespace DayFive
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            string[] input = System.IO.File.ReadAllLines("../../Input.txt");
            int[] intArray = ParseArray(input);

            if (args[0].Equals("1"))
            {
                result = ProblemOne(intArray);
            }
            else if (args[0].Equals("2"))
            {
                result = ProblemTwo(intArray);
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static int[] ParseArray(string[] input)
        {
            int[] intArray = new int[input.Length];
            int index = 0;

            foreach(string line in input)
            {
                intArray[index] = DayThree.Program.ParseInt(line);
                ++index;
            }

            return intArray;
        }

        public static int ProblemOne(int[] input)
        {
            int steps = 0;
            int index = 0;
            
            while(index < input.Length)
            {
                input[index]++;
                index = index + input[index] - 1;

                ++steps;
            }

            return steps;
        }

        public static int ProblemTwo(int[] input)
        {
            int steps = 0;
            int index = 0;

            while (index < input.Length)
            {
                if (input[index] >= 3)
                {
                    input[index]--;
                    index = index + input[index] + 1;
                }
                else
                {
                    input[index]++;
                    index = index + input[index] - 1;
                }

                ++steps;
            }

            return steps;
        }
    }
}
