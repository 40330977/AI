using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aicw
{
    class cavestore : IEnumerable
    {
        private List<cave> cave_store = new List<cave>();


        public void csadd(cave newcave)//adds cave
        {
            cave_store.Add(newcave);
        }

        public cave cscavefind(int nam)//finds cave
        {
            foreach (cave c in cave_store)
            {
                if (nam == c.Name)
                {
                    return c;
                }
            }
            return null;
        }

        public void cscavedelete(int nam)//deletes cave
        {
            cave c = this.cscavefind(nam);
            if (c != null)
            {
                cave_store.Remove(c);
            }
        }
        public IEnumerator<cave> GetEnumerator()//allows foreach
        {
            return cave_store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cave_store.GetEnumerator();
        }
    }
}
