using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3in1Game
{
    [Serializable()]
    public class Player
    {

        public Player() {
        }
        public string Ime { set; get; }
        public int Poeni { set; get; }
        public string Mode { set; get; }



        public override string ToString()
        {
            return string.Format(Ime + "  -poeni:" + Poeni + "\n");
        }
    }
}
