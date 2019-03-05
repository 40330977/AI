using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aicw
{
     
    class cave: IComparable<cave>
    {
        int _name, _xpos, _ypos, _layer, _from;
        double _euc, _length, _score;
        List<int> _cons = new List<int>();

        public cave() { }

        public cave(int name, int xpos, int ypos)//default constructor
        {
            Name = name;
            Xpos = xpos;
            Ypos = ypos;
            
        }

        public int Name//cave name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Xpos
        {
            get
            {
                return _xpos;
            }
            set
            {
                _xpos = value;
            }
        }
        public int Ypos
        {
            get
            {
                return _ypos;
            }
            set
            {
                _ypos = value;
            }
        }
        public double Euc
        {
            get
            {
                return _euc;
            }
            set
            {
                _euc = value;
            }
        }

        public List<int> Cons//list to store connections
        {
            get
            {
                return _cons;
            }
            set
            {
                _cons = value;
            }
        }

        public int Layer//stores iteration the cave was added
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        public double Length//stores length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }
        public double Score//stores score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        void SetName(int namer)
        {
            Name = namer;
        }

        public int CompareTo(cave cave2)//comparatore to sort list to find lowest score
        {
            if(Score > cave2.Score)
            {
                return 1;
            }
            else { return -1; }
        }

        public int From//used to store parent
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }
    }
}
