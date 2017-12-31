using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayThree
{
    public class Program
    {
        static void Main(string[] args)
        {
            int steps = 0;
            int parameter = ParseInt(args[1]);

            if (args[0].Equals("1"))
            {
                steps = NoStepsOne(parameter);
            }
            else if (args[0].Equals("2"))
            {
                steps = CalculateValue(parameter);
            }

            Console.WriteLine(steps);
            Console.ReadLine();
        }

        public static int NoStepsOne(int parameter)
        {
            int x_axis = 0;
            int y_axis = 0;

            int step = 0;
            int currentValue = 1;
            Direction direction = Direction.RIGHT;

            bool stop = false;
            do
            {
                if(direction == Direction.RIGHT || direction == Direction.LEFT)
                {
                    ++step;
                }

                for(int i = 0; i < step; ++i)
                {
                    if(currentValue == parameter)
                    {
                        stop = true;
                        break;
                    }

                    switch(direction)
                    {
                        case Direction.LEFT:
                            x_axis--;
                            break;

                        case Direction.RIGHT:
                            x_axis++;
                            break;

                        case Direction.UP:
                            y_axis++;
                            break;

                        case Direction.DOWN:
                            y_axis--;
                            break;
                    }

                    ++currentValue;
                }

                switch (direction)
                {
                    case Direction.LEFT:
                        direction = Direction.DOWN;
                        break;

                    case Direction.RIGHT:
                        direction = Direction.UP;
                        break;

                    case Direction.UP:
                        direction = Direction.LEFT;
                        break;

                    case Direction.DOWN:
                        direction = Direction.RIGHT;
                        break;
                }
            } while (!stop);

            return Math.Abs(y_axis) + Math.Abs(x_axis);
        }

        public static int CheckSurrounding(int x_axis, int y_axis, int?[,] table)
        {
            int sum = 0;

            
            for (int j = -1; j <= 1; ++j)
            {
                for (int i = -1; i <= 1; ++i)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (table[x_axis + i, y_axis + j].HasValue)
                    {
                        int value = table[x_axis + i, y_axis + j].Value;
                        sum += value;
                    }
                }
            } 

            if(sum == 0)
            {
                table[x_axis, y_axis] = 1;
            }
            else
            {
                table[x_axis, y_axis] = sum;
            }
            
            return table[x_axis, y_axis].Value;
        }

        public enum Direction
        {
            UP,
            DOWN,
            RIGHT,
            LEFT
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

        public static int CalculateValue(int parameter) {
        
            int dimension = Math.Max(parameter / 50, 10);

            int x_axis = dimension / 2;
            int y_axis = dimension / 2;

            Nullable<int>[,] table = new int?[dimension, dimension];

            int step = 0;
            Direction direction = Direction.RIGHT;

            table[x_axis, y_axis] = 1;
            do
            {
                if (direction == Direction.RIGHT || direction == Direction.LEFT)
                {
                    ++step;
                }

                for (int i = 0; i < step; ++i)
                {
                    int currentValue = CheckSurrounding(x_axis, y_axis, table);
                    int currentField = table[x_axis, y_axis].Value;

                    if (currentValue > parameter)
                    {
                        return currentValue;
                    }

                    switch (direction)
                    {
                        case Direction.LEFT:
                            x_axis--;
                            break;

                        case Direction.RIGHT:
                            x_axis++;
                            break;

                        case Direction.UP:
                            y_axis++;
                            break;

                        case Direction.DOWN:
                            y_axis--;
                            break;
                    }
                }

                switch (direction)
                {
                    case Direction.LEFT:
                        direction = Direction.DOWN;
                        break;

                    case Direction.RIGHT:
                        direction = Direction.UP;
                        break;

                    case Direction.UP:
                        direction = Direction.LEFT;
                        break;

                    case Direction.DOWN:
                        direction = Direction.RIGHT;
                        break;
                }
            } while (true);
        }
    }
}
