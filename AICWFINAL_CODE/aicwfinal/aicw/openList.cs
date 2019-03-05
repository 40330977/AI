using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aicw
{
    class openList:IEnumerable
    {
        private List<cave> open_list = new List<cave>();


        public void oladd(cave newcave)//add cave
        {
            open_list.Add(newcave);
        }

        public cave cavefind(int nam)//find cave
        {
            foreach (cave c in open_list)
            {
                if (nam == c.Name)
                {
                    return c;
                }
            }
            return null;
        }

        public void cavedelete(int nam)//delete cave
        {
            cave c = this.cavefind(nam);
            if (c != null)
            {
                open_list.Remove(c);
            }
        }

        public IEnumerator<cave> GetEnumerator()//allows foreach
        {
            return open_list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return open_list.GetEnumerator();
        }

        public int opfindlast()//find last
        {
            int i = 0;
            foreach (cave e in open_list)
            {
                i++;
            }

            return i;
        }

        public void sortme()//allows sorting
        {
            open_list.Sort();
        }

        public List<cave> Open_list//allows list access
        {
            get { return open_list; }
            set { open_list = value; }
        }
    }
}
