using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayThree;

namespace DaySix
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] intArray = GetInput();
            int result = 0;

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

        public static int ProblemTwo(int[] intArray)
        {
            List<int[]> iterations = new List<int[]>();
            List<int> sinceLasts = new List<int>(); 

            int[] tempArray = null;
            int index = 0;
            do
            {
                for (int i = 0; i < sinceLasts.Count; ++i)
                {
                    sinceLasts[i] = sinceLasts[i] + 1;
                }
                tempArray = new int[intArray.Length];
                Iteration(intArray);
                intArray.CopyTo(tempArray, 0);

                index = DoesNotRepeat(iterations, tempArray);
                sinceLasts.Add(0);
            } while (index == -1);

            return sinceLasts.ElementAt(index);
        }

        public static int[] GetInput() {
            string input = System.IO.File.ReadAllText("../../Input.txt");
            string[] splits = input.Split('\t');
            int[] intArray = new int[splits.Length];
            int index = 0;

            foreach(string split in splits)
            {
                intArray[index] = DayThree.Program.ParseInt(split);
                ++index;
            }

            return intArray;
        }

        public static int ProblemOne(int[] intArray)
        {
            List<int[]> iterations = new List<int[]>();
            int steps = 0;

            int[] tempArray = null;

            do
            {
                tempArray = new int[intArray.Length];
                Iteration(intArray);
                intArray.CopyTo(tempArray, 0);

                ++steps;

            } while (DoesNotRepeat(iterations, tempArray) == -1);

            return steps;
        }

        public static int DoesNotRepeat(List<int[]> iterations, int[] currentIteration)
        {
            int index = 0;
            foreach(int[] iteration in iterations)
            {
                int matches = 0;
                for (int i = 0; i < iteration.Length; ++i)
                {
                    if(iteration[i] == currentIteration[i])
                    {
                        ++matches;
                    }
                }

                if (matches == iteration.Length)
                {
                    return index;
                }

                ++index;
            }

            iterations.Add(currentIteration);

            return -1;
        }

        public static void Iteration(int[] intArray)
        {
            int index = IndexOfMax(intArray);
            int noSteps = intArray.ElementAt(index);
            intArray[index] = 0;

            for(int i = 0; i < noSteps; ++i)
            {
                ++index;
                if(index == intArray.Length)
                {
                    index = 0;
                }

                intArray[index]++;
            }
        }

        public static int IndexOfMax(int[] intArray)
        {
            int max = intArray[0];
            int index = 0;
            int maxIndex = 0;

            foreach(int number in intArray)
            {
                if(max < number)
                {
                    max = number;
                    maxIndex = index;
                }

                ++index;
            }

            return maxIndex;
        }
    }
}
