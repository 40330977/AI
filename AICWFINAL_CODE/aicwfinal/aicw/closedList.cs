using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aicw
{
    class closedList:IEnumerable
    {
        private List<cave> closed_list = new List<cave>();


        public void cladd(cave newcave)//add cave
        {
            closed_list.Add(newcave);
        }

        public cave clcavefind(int nam)//find cave
        {
            foreach (cave c in closed_list)
            {
                if (nam == c.Name)
                {
                    return c;
                }
            }
            return null;
        }

        public void clcavedelete(int nam)//delete cave
        {
            cave c = this.clcavefind(nam);
            if (c != null)
            {
                closed_list.Remove(c);
            }
        }

        public int clfindlast()//find last cave used to check if impossible
        {
            int i = 0;
            foreach (cave e in closed_list)
            {
                i++;
            }
           
            return i;
        }

        public IEnumerator<cave> GetEnumerator()//required for foreach
        {
            return closed_list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return closed_list.GetEnumerator();
        }

        public List<cave> Closed_list//allows access to list
        {
            get { return closed_list; }
            set { closed_list = value; }
        }
    }
}
