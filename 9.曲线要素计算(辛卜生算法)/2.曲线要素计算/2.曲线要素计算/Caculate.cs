using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.曲线要素计算
{
    class Caculate
    {
        public static double dmstohudu(double dms)
        {
            double d, m, s;
            d = Math.Floor(dms);
            m = Math.Floor(100*(dms-d));
            s = 100 * (100 * (dms - d) - m);
            return (d + m / 60+s / 3600)*Math.PI /180;
        }
        public static double hudutodms(double hudu)
        {
            double du, d, m, s;//因为是方位角的计算，要考虑小于0时的情况
            if (hudu > 2 * Math.PI)
            {
                hudu -= 2 * Math.PI;
            }
            if(hudu < 0)
            {
                hudu += 2 * Math.PI; 
            }
            du = hudu * 180 / Math.PI;
            d = Math.Floor(du);
            m = Math.Floor(60 * (du - d));
            s = 60 * (60 * (du - d) - m);
            return Math.Round((d + m / 100 + s / 10000),6);
        }
        public static double fangweijiao(double x1, double y1, double x2, double y2)
        {
            double a = 180 - 90 * Math.Abs(y2 - y1 + Math.Pow(10, -10)) / (y2 - y1 + Math.Pow(10, -10)) - Math.Atan((x2 - x1) / (y2 - y1 + Math.Pow(10, -10))) * 180 / Math.PI;
            //加上10^-10可以保证y2 = y1时不会报错
            return Math.Round(a * Math.PI / 180,8);//保留八位小数精度可以满足要求,返回弧度值
        }
    }

    
}
