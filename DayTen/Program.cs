using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTen
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = GenerateNumbers(256);
            

            if (args[0].Equals("1"))
            {
                List<int> lenghts = GetInput();
                Console.WriteLine(Knotting(lenghts, numbers));
            }
            else
            {
                
                List<int> lenghts = GetInputComplex();
                lenghts.Add(17);
                lenghts.Add(31);
                lenghts.Add(73);
                lenghts.Add(47);
                lenghts.Add(23);
                Console.WriteLine(KnottingComplex(lenghts, numbers));
            }
            Console.ReadLine();
        }

        public static int Knotting(List<int> lenghts, List<int> numbers)
        {
            int index = 0;
            int step = 0;

            foreach(int length in lenghts)
            {
                if(index >= numbers.Count)
                {
                    index = index - numbers.Count;
                }
                ReversePart(index, length, numbers);

                index =  index + step + length;
                ++step;
            }

            return numbers[0] * numbers[1];
        }

        public static string KnottingComplex(List<int> lengths, List<int> numbers)
        {
            int index = 0;
            int step = 0;

            for (int i = 0; i < 64; ++i)
            {
                foreach (int length in lengths)
                {
                    if (index >= numbers.Count)
                    {
                        index = index - (index/numbers.Count) * numbers.Count;
                    }
                    ReversePart(index, length, numbers);

                    index = index + step + length;
                    ++step;
                }
            }

            return XOR(16, numbers);
        }

        public static string XOR(int length, List<int> numbers)
        {
            List<int> xorList = new List<int>();
            int groupCounter = 0;

            for(int group = 0; group < numbers.Count / length; ++group)
            {
                int start = groupCounter * length;
                int xor = 0;
                for(int i = 0; i < length; ++i)
                {
                    xor = xor ^ numbers[i + start];
                }

                xorList.Add(xor);
                ++groupCounter;
            }

            StringBuilder sb = new StringBuilder();

            foreach(int number in xorList)
            {
                sb.Append(number.ToString("X2"));
            }

            return sb.ToString();
        }

        public static void ReversePart(int start, int length, List<int> numbers)
        {
            List<int> tempList = new List<int>();

            int index = start;
            for(int i = 0; i < length; ++i)
            {
                if(index >= numbers.Count)
                {
                    index = 0;
                }

                tempList.Add(numbers[index]);
                index++;
            }

            tempList.Reverse();

            index = start;
            for (int i = 0; i < length; ++i)
            {
                if (index == numbers.Count)
                {
                    index = 0;
                }

                numbers[index] = tempList[i];
                index++;
            }
        }

        public static List<int> GetInput()
        {
            string input = System.IO.File.ReadAllText("../../Input.txt");
            List<int> lenghts = new List<int>();
            string[] splits = input.Split(',');

            foreach(string split in splits)
            {
                lenghts.Add(DayThree.Program.ParseInt(split));
            }

            return lenghts;
        }

        public static List<int> GetInputComplex()
        {
            string input = System.IO.File.ReadAllText("../../Input.txt");
            List<int> lenghts = new List<int>();

            byte[] bytes = Encoding.ASCII.GetBytes(input);

            foreach(byte byt in bytes)
            {
                lenghts.Add(byt);
            }

            return lenghts;
        }
        public static List<int> GenerateNumbers(int size)
        {
            List<int> numbers = new List<int>();
            for(int i = 0; i < size; ++i)
            {
                numbers.Add(i);
            }

            return numbers;
        }
    }
}
