using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayEight
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            List<Command> commandList = GetInput(registers);
            Console.WriteLine(ProblemOne(registers, commandList));

            Console.ReadLine();
        }

        public static int ProblemOne(Dictionary<string, int> registers, List<Command> commands)
        {
            int max = 0;
            foreach (Command command in commands)
            {
                int compOperand = registers[command.CompRegister];

                if (command.IsSatisfied(compOperand))
                {
                    registers[command.Register] += command.GetModifier();
                }

                int currentMax = registers.Select(t => t.Value).Max();

                if (currentMax > max)
                {
                    max = currentMax;
                }
             }

            Console.WriteLine("Maxiest max " + max);
            return registers.Select(t => t.Value).Max();
        }

        public static List<Command> GetInput(Dictionary<string, int>  registers)
        {
            List<string> input = System.IO.File.ReadAllLines("../../Input.txt").ToList();
            List<Command> commands = new List<Command>();

            foreach(string line in input)
            {
                string[] splits = line.Split(' ');
                Command command = new Command();
                command.Register = splits[0];

                command.IncOrDec = splits[1].Equals("inc");

                command.IncOperand = DayThree.Program.ParseInt(splits[2]);
                command.CompRegister = splits[4];
                command.CompOperand = DayThree.Program.ParseInt(splits[6]);

                switch(splits[5])
                {
                    case "==":
                        command.Op = Operator.EQUAL;
                        break;
                    case "!=":
                        command.Op = Operator.NOT_EQUAL;
                        break;
                    case ">=":
                        command.Op = Operator.GREATER_EQUAL;
                        break;
                    case ">":
                        command.Op = Operator.GREATER;
                        break;
                    case "<=":
                        command.Op = Operator.LESSER_EQUAL;
                        break;
                    case "<":
                        command.Op = Operator.LESSER;
                        break;
                }

                if(!registers.ContainsKey(command.CompRegister))
                {
                    registers.Add(command.CompRegister, 0);
                }

                if (!registers.ContainsKey(command.Register))
                {
                    registers.Add(command.Register, 0);
                }

                commands.Add(command);
            }

            return commands;
        }

        public class Command
        {
            public bool IncOrDec { get; set; }

            public string Register { get; set; }

            public Operator Op;

            public string CompRegister;

            public int CompOperand;

            public int IncOperand;

            public bool IsSatisfied(int compRegisterValue)
            {
                switch (Op)
                {
                    case Operator.EQUAL:
                        if(compRegisterValue == CompOperand)
                        {
                            return true;
                        }
                        break;
                    case Operator.NOT_EQUAL:
                        if (compRegisterValue != CompOperand)
                        {
                            return true;
                        }
                        break;
                    case Operator.GREATER_EQUAL:
                        if (compRegisterValue >= CompOperand)
                        {
                            return true;
                        }
                        break;
                    case Operator.GREATER:
                        if (compRegisterValue > CompOperand)
                        {
                            return true;
                        }
                        break;
                    case Operator.LESSER_EQUAL:
                        if (compRegisterValue <= CompOperand)
                        {
                            return true;
                        }
                        break;
                    case Operator.LESSER:
                        if (compRegisterValue < CompOperand)
                        {
                            return true;
                        }
                        break;
                }

                return false;
            }

            public int GetModifier()
            {
                if(IncOrDec)
                {
                    return IncOperand;
                } 
                else
                {
                    return -IncOperand;
                }
            }
        }

        public enum Operator
        {
            GREATER,

            GREATER_EQUAL,

            LESSER,

            LESSER_EQUAL,

            EQUAL,

            NOT_EQUAL
        }
    }
}
