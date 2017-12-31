using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySeven
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tower> towerList = GetInput();

            if (args[0].Equals("1"))
            {
                Console.WriteLine(ProblemOne(towerList));
            }
            else if (args[0].Equals("2"))
            {
                Console.WriteLine(ProblemTwo(towerList));
            }

            Console.ReadLine();
        }

        public static int ProblemTwo(List<Tower> towerList)
        {
            Tower rootTower = ProblemOne(towerList);

            do
            {
                int[] weights = new int[rootTower.Children.Count];

                for(int i = 0; i < weights.Length; ++i) 
                {
                    Tower tempTower = FindByName(towerList, rootTower.Children[i]);
                    if(tempTower == null)
                    {
                        continue;
                    }
                    weights[i] = tempTower.GetTotalWeight();
                }

                Tower differentOne = null;

                for(int first = 0; first < weights.Length; ++first)
                {
                    int differents = 0;
                    for(int second = 0; second < weights.Length; ++second)
                    {
                        if(first == second)
                        {
                            continue;
                        }

                        if(weights[first] != weights[second])
                        {
                            FindByName(towerList, rootTower.Children[first]).MyDifference = weights[first] - weights[second];
                            differents++;
                        }
                    }

                    if(differents == weights.Length - 1)
                    {
                        differentOne = FindByName(towerList, rootTower.Children[first]);
                        break;
                    }
                }

                if(differentOne == null)
                {
                    Console.WriteLine(rootTower);
                    return rootTower.Weight - rootTower.MyDifference;
                }


                rootTower = differentOne;
            } while (true);
        }

        public static Tower FindByName(List<Tower> towerList, string name)
        {
            foreach(Tower tower in towerList)
            {
                if(tower.Name.Equals(name))
                {
                    return tower;
                }
            }

            return null;
        }

        public static Tower ProblemOne(List<Tower> towerList)
        {
            List<Tower> underlings = new List<Tower>();

            foreach(Tower tower in towerList)
            {
                if(tower.Children.Count() == 0)
                {
                    underlings.Add(tower);
                }
            }

            do
            {
                List<Tower> carriers = new List<Tower>();

                foreach(Tower carrierTower in towerList)
                {
                    foreach(Tower underling in underlings)
                    {
                        if(carrierTower.Children.Contains(underling.Name))
                        {
                            carriers.Add(carrierTower);
                            break;
                        }
                    }
                }

                if(carriers.Count == 0)
                {
                    return underlings.First();
                }

                underlings = carriers;
            } while (true);
        }

        public static List<Tower> GetInput()
        {
            List<string> input = System.IO.File.ReadAllLines("../../Input.txt").ToList();
            List<Tower> towerList = new List<Tower>();

            foreach(string line in input)
            {
                string[] splits = line.Split(' ');

                string name = splits[0];
                int weight = DayThree.Program.ParseInt(splits[1].Substring(1, splits[1].Length - 2));

                Tower tower = new Tower(name, weight, towerList);
                towerList.Add(tower);

                if(splits.Length > 2)
                {
                    for(int i = 3; i < splits.Length - 1; ++i)
                    {
                        tower.Children.Add(splits[i].Substring(0, splits[i].Length - 1));
                    }

                    tower.Children.Add(splits[splits.Length - 1].Substring(0, splits[splits.Length - 1].Length));
                }
            }

            return towerList;
        }

        public class Tower
        {
            public string Name { get; set; }

            public int Weight { get; set; }

            public List<string> Children { get; }

            public List<Tower> TowerList;

            private int TotalWeight;

            public int MyDifference { set; get; }

            public Tower(string name, int weight, List<Tower> towerList)
            {
                Name = name;
                Weight = weight;
                Children = new List<string>();
                TowerList = towerList;
            }

            public int GetTotalWeight()
            {
                if(TotalWeight == 0)
                {
                    TotalWeight = CalculateTotalWeight(TowerList);
                }

                return TotalWeight;
            }

            public override bool Equals(object obj)
            {
                if(!(obj is Tower))
                {
                    return false;
                }

                Tower other = (Tower) obj;

                return Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override string ToString()
            {
                return Name;
            }

            public int CalculateTotalWeight(List<Tower> towerList)
            {
                int sum = Weight;

                foreach(string towerName in Children)
                {
                    sum += FindByName(towerList, towerName).GetTotalWeight();
                }

                return sum;
            }
        }
    } 
}
