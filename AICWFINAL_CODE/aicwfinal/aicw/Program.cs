using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Roderick Ewles
//40330977
//24/11/2018
//AI Coursework
//Main program

namespace aicw
{
    class Program
    {
        

        static void Main(string[] args)
        {
            cavestore cstore = new cavestore();//create lists
            closedList cllist = new closedList();
            openList oplist = new openList();
            openList operl = new openList();
            
            string chars;
            if (args.Length == 0){
                 chars = System.IO.File.ReadAllText(@"C:\Users\Roddy\Desktop\aicwbetter5\aicwbetter3\generated500-2.cav");//if used to debug
            }
            else
            {
                 chars = System.IO.File.ReadAllText(args[0]+".cav");//read in file
            }
           
            string s = args[0];

            args = chars.Split(new char[] { ',' });//remove commas
           
            int cavenum = int.Parse(args[0]);//find number of caves
            int[] caveloc = null;
            Array.Resize(ref caveloc, cavenum * 2 + 1);
            for (int i = 1; i < (cavenum * 2 + 1); i++)
            {
                int passer = int.Parse(args[i]);
                caveloc[i] = passer;//store cave coordinates
            }
            int k = 1;
            for (int i = 1; i < (cavenum * 2 + 1);)//create, name and assign locations to caves
            {
                cave namer = new cave(k, caveloc[i], caveloc[i + 1]);
                cstore.csadd(namer);
                i++;
                i++;
                k++;
            }
            int[] connection = null;
            Array.Resize(ref connection, (cavenum * cavenum ));
            int p = 0;
            for (int i = (cavenum * 2 + 1); i < (cavenum * cavenum + (cavenum * 2 +1)); i++)//separate out connections
            {

                connection[p] = int.Parse(args[i]);
                p++;
            }
            
            foreach(cave c in cstore)
            {
                connecttester(c);//works out what caves are connected
            }
            
            bool impossible = false;//boolean to track if system impossible to navigate
            bool found = false;//boolean to track if a solution is found
            int looper = 1;
            List<int> imper = new List<int>();
            List<int> tracker = new List<int>();
            List<int> trackerop = new List<int>();
            List<int> trackert = new List<int>();

            cave com = new cave();
            cave conner = new cave();
            cave start = cstore.cscavefind(1);//set start cave
            cave target = cstore.cscavefind(cavenum);//set target cave

            cllist.cladd(start);
            tracker.Add(start.Name);
            foreach (int i in start.Cons)//find caves connected to start and add to open list
            {
                conner = cstore.cscavefind(i);
                conner.Layer = looper;
                oplist.oladd(conner);
            }
            foreach(cave c in oplist)//score those caves
            {
                euclid(c, start);
                
                lengths(c, start);
                
            }
            
            while (found != true && looper <= cavenum)
            {
                
                looper++;

                if (oplist.opfindlast() == 0)//checks if impossible
                {
                    if (operl.opfindlast() == 0)
                    {
                        impossible = true;
                        break;
                    }
                    else
                    {
                        operl.sortme();
                        oplist.oladd(operl.Open_list.First());
                    }

                }

                oplist.sortme();//sort the list by score
                com = oplist.Open_list.First();//set com to the lowest scoring cave

                cllist.cladd(com);//add com to closed list
                oplist.cavedelete(com.Name);//remove it from open list
                tracker.Add(com.Name);//add com name to tracker
               
                foreach (int i in com.Cons)//for each to score connections from com and add them to open list
                {
                    cave v = cllist.clcavefind(i);//to check if in closed list
                    cave e = oplist.cavefind(i);//to check if in open list
                    conner = cstore.cscavefind(i);//get connection from cave store
                    if (v == null && e == null)//if not in both lists 
                    {
                        conner.From = com.Name;//store parent
                        conner.Layer = looper;//store layer added
                        euclid(conner, com);
                        lengths(conner, com);//score cave
                        oplist.oladd(conner);//add to open list
                        
                    }
                    else if (v == null && e != null && e.Score <= conner.Score)
                    {
                        continue;//if already in open list continue
                       
                    }

                }
               
                trackerop.Clear();
                if (com.Name == target.Name)//set found true if destination reached
                {
                    found = true;
                    break;
                }

            }
            int o = 0;
            List<cave> outputter = new List<cave>();//list to output result
            outputter.Add(cllist.Closed_list.Last());//add target

            while(outputter.Last().From != 0)//while not start
            {
                outputter.Add(cllist.clcavefind(outputter.Last().From));//add parent to list creating reverse path
            }
            outputter.Add(start);//add start as not added in loop as ==
            outputter.Reverse();//reverse to get path

            if (found)//if solved
            {
                string[] output = new string[cavenum];

                foreach (cave c in outputter)//creates output
                {
                    output[o] = c.Name.ToString();
                    o = o + 1;
                }

                Console.WriteLine(string.Join(" ", output));
                string out1 = string.Join(" ", output);//puts spaces in between cave names
                System.IO.File.WriteAllText(@".\" + s +".csn", out1);//creates output file
            }
            if (impossible)//if impossible
            {
                Console.WriteLine("0");
                string out2 = "0";
                System.IO.File.WriteAllText(@".\" + s + ".csn", out2);//creates output file containing a 0
            }

            void euclid(cave to, cave from)//calculates euclidian distance
            {
                double eucy = Math.Sqrt(((to.Xpos - from.Xpos) * (to.Xpos - from.Xpos)) + ((to.Ypos - from.Ypos) * (to.Ypos - from.Ypos)));
                to.Euc = eucy;
            }

            void lengths(cave neighbor, cave current)//scores cave
            {
                double euc = Math.Sqrt(((target.Xpos - neighbor.Xpos) * (target.Xpos - neighbor.Xpos)) + ((target.Ypos - neighbor.Ypos) * (target.Ypos - neighbor.Ypos)));
                double lengther = current.Length + neighbor.Euc;
                neighbor.Length = lengther;//stores length for debugging
                neighbor.Score = neighbor.Length + euc;//stores score
            }

            void connecttester(cave c)//tests what caves are connected
            {
                int post = c.Name-1;//cave being examined
                int multis = 1;//multiplier to set offset to check for cave connections

                while (multis < cavenum)
                {
                    
                    if (connection[post + cavenum * multis] == 1)//if a 1 at position then there is a connection
                    {
                        c.Cons.Add(multis+1);//store connection
                        multis++;//increment multiplier
                    }
                    else
                    {
                        multis++;//increment multiplier
                    }
                }

            }

        }
    }
}
