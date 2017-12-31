using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayFour
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            string[] input = System.IO.File.ReadAllLines("../../Input.txt");
            List<List<string>> listOfLines = DoTheLines(input);

            if (args[0].Equals("1"))
            {
                result = ProblemOne(listOfLines);
            }
            else if (args[0].Equals("2"))
            {
                result = ProblemTwo(listOfLines);
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static List<List<string>> DoTheLines(string[] input)
        {
            List<List<string>> listOfLines = new List<List<string>>();

            for (int i = 0; i < input.Length; ++i)
            {
                string[] splits = input.ElementAt(i).Split(' ');
                List<string> line = new List<string>();
                listOfLines.Add(line);

                foreach(string word in splits)
                {
                    line.Add(word);
                }
            }

            return listOfLines;
        }

        public static int ProblemOne(List<List<string>> listOfLines)
        {
            int goodLines = 0;

            foreach(List<string> line in listOfLines)
            {
                bool badLine = false;
                List<string> fraze = new List<string>();

                foreach(string word in line)
                {
                    if(fraze.Contains(word))
                    {
                        badLine = true;
                        break;
                    }
                    else
                    {
                        fraze.Add(word);
                    }
                }

                if(badLine != true)
                {
                    ++goodLines;
                }
            }

            return goodLines;
        }

        public static int ProblemTwo(List<List<string>> listOfLines)
        {
            int goodLines = 0;

            foreach (List<string> line in listOfLines)
            {
                bool badLine = false;
                List<string> fraze = new List<string>();

                foreach (string word in line)
                {
                    if (CheckAnagram(word, fraze))
                    {
                        badLine = true;
                        break;
                    }
                    else
                    {
                        fraze.Add(word);
                    }
                }

                if (badLine != true)
                {
                    ++goodLines;
                }
            }

            return goodLines;
        }

        public static bool CheckAnagram(string word, List<string> fraze)
        {
            foreach(string word2 in fraze)
            {
                if(IsAnagram(word, word2))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsAnagram(string word1, string word2)
        {
            if(word1.Length != word2.Length)
            {
                return false;
            }

            List<char> chars1 = word1.ToList();
            List<char> chars2 = word2.ToList();

            foreach(char letter1 in chars1)
            {
                foreach(char letter2 in chars2)
                {
                    if(letter1.Equals(letter2))
                    {
                        chars2.Remove(letter2);
                        break;
                    }
                }
            }

            if (chars2.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
