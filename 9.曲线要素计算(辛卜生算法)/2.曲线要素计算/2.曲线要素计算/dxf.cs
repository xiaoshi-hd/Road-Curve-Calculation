using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2.曲线要素计算
{
    class dxf
    {
        public static dxf(StreamWriter sw)
        {
            sw.Write("0\nSECTION\n2\nTABLES\n0\nTABLE\n2\nLAYER\n");
            sw.Write("0\nLAYER\n70\n0\n2\nshiti\n62\n");
        }
    }
}
