using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOne
{
    public class Program
    {
        //Second argument must be 1 for the solution of the first part of the puzzle or 2 for the solution of the second part.

        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                throw new ArgumentException("Expected two arguments");
            }

            char[] charArray = args[0].ToArray();
            int sum = 0;
        
            if (args[1].Equals("1"))
            {
                sum = SumOne(charArray); 
            }
            else if(args[1].Equals("2"))
            {
                sum = SumTwo(charArray);
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        public static int ParseInt(char character)
        {
            int result;

            if(!int.TryParse(character.ToString(), out result))
            {
                throw new ArgumentException("Input string can only contain digits");
            }

            return result;
        }

        public static int SumOne(char[] charArray) 
        {
            int sum = 0;
            int currentNumber = ParseInt(charArray[0]);
            for (int i = 1; i < charArray.Length; ++i)
            {
                int nextNumber;

                nextNumber = ParseInt(charArray[i]);
                if (currentNumber == nextNumber)
                {
                    sum += currentNumber;
                }

                currentNumber = nextNumber;
            }

            if (currentNumber == ParseInt(charArray[0]))
            {
                sum += currentNumber;
            }

            return sum;
        }

        public static int SumTwo(char[] charArray)
        {
            Nullable<int>[] numberArray = new Nullable<int>[charArray.Length];

            int sum = 0;
            int step = charArray.Length / 2;

            for (int i = 0; i < charArray.Length; ++i)
            {
                if (!numberArray[i].HasValue)
                {
                    numberArray[i] = ParseInt(charArray[i]);
                }   

                int secondIndex = i + step;
                if (secondIndex >= numberArray.Length)
                {
                    secondIndex = i + step - numberArray.Length;
                }

                if (!numberArray[secondIndex].HasValue)
                {
                    numberArray[secondIndex] = ParseInt(charArray[secondIndex]);
                }

                if (numberArray[i].Value == numberArray[secondIndex].Value)
                {
                    sum += numberArray[i].Value;
                }
            }

            return sum;
        }
    }
}
