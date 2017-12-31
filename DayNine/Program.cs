using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayNine
{
    class Program
    {
        public static int garbage; 
        static void Main(string[] args)
        {
            garbage = 0;
            string input = System.IO.File.ReadAllText("../../Input.txt");

            Console.WriteLine(CalculateGroup(1, input, 0));
            Console.WriteLine("Garbage " + garbage);
            Console.ReadLine();
        }

        public static int CalculateGroup(int myScore, string text, int index)
        {
            bool trashMode = false;
            int score = myScore;

            for(int i = index; i < text.Length; ++i)
            {
                bool enteredGarbage = false;
                if (text[i] == '>' && trashMode == true)
                {
                    trashMode = false;
                }
                else if (text[i] == '!' && trashMode == true)
                {
                    ++i;
                    enteredGarbage = true;
                }
                else if (text[i] == '<' && trashMode == false)
                {
                    enteredGarbage = true;
                    trashMode = true;
                }
                else if (text[i] == '{' && trashMode == false && index != i)
                {
                    score += CalculateGroup(myScore + 1, text, i);
                    i = PassGroup(i, text);
                }
                else if (text[i] == '}' && trashMode == false)
                {
                    return score;
                }

                if(trashMode == true && enteredGarbage == false)
                {
                    ++garbage;
                }
            }

            return score;
        }

        public static int PassGroup(int index, string text)
        {
            int noBrackets = 1;
            bool trashMode = false;
            for(int i = index + 1; i < text.Length; ++i)
            {
                if(text[i] == '>' && trashMode == true)
                {
                    trashMode = false;
                    continue;
                }
                if(text[i] == '!' && trashMode == true)
                {
                    ++i;
                    continue;
                }
                else if(text[i] == '<' && trashMode == false)
                {
                    trashMode = true;
                }
                else if(text[i] == '{' && trashMode == false)
                {
                    noBrackets++;
                }
                else if(text[i] == '}' && trashMode == false)
                {
                    noBrackets--;
                }

                if(noBrackets == 0)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
