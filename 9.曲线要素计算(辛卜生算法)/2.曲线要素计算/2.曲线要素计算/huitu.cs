using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _2.曲线要素计算
{
    class huitu
    {
        public static Bitmap image;
        public huitu(Form1 form,Form2 form2)
        {
            try
            {
                Pen pen1 = new Pen(Color.Green, 2.5f);//虚线
                pen1.DashStyle = DashStyle.Custom;
                pen1.DashPattern = new float[] { 5, 5 };
                Pen pen2 = new Pen(Color.Black, 3);//直线曲线段
                Pen pen3 = new Pen(Color.Red, 2);//文字注记
                image = new Bitmap((int)(jisuan.Yzuobiao.Max() - jisuan.Yzuobiao.Min()) + 500, (int)(jisuan.Xzuobiao.Max() - jisuan.Xzuobiao.Min()) + 500);//显示图形范围
                Graphics g = Graphics.FromImage(image);
                g.RotateTransform(-90);//旋转为测量坐标系
                g.TranslateTransform(-(int)(jisuan.Xzuobiao.Max() + 100), -(int)jisuan.Yzuobiao.Min() + 100);//划定原点位置
                PointF[] pf = new PointF[jisuan.Xzuobiao.Count];

                //通过直线绘制缓和曲线
                for (int i = 0; i < jisuan.Xzuobiao.Count; i++)
                {
                    pf[i].X = (float)jisuan.Xzuobiao[i];
                    pf[i].Y = (float)jisuan.Yzuobiao[i];
                }
                g.DrawLines(pen2, pf);

                //绘制虚线
                g.DrawLine(pen1, (float)jisuan.XZH, (float)jisuan.YZH, (float)jisuan.JDX + 300 * (float)Math.Cos(jisuan.fangwei), (float)jisuan.JDY + 300 * (float)Math.Sin(jisuan.fangwei));
                g.DrawLine(pen1, (float)jisuan.JDX, (float)jisuan.JDY, (float)jisuan.XHZ, (float)jisuan.YHZ);

                //绘制圆弧
                g.DrawArc(pen3, (float)jisuan.JDX - 50, (float)jisuan.JDY - 50, 100, 100, (float)(jisuan.fangwei * 180 / Math.PI), (float)(jisuan.A * 180 / Math.PI));

                //绘制箭头
                AdjustableArrowCap cap = new AdjustableArrowCap(5, 10);
                pen2.CustomEndCap = cap;
                g.DrawLine(pen2, pf[pf.Length - 1].X - 200, pf[pf.Length - 1].Y, pf[pf.Length - 1].X - 100, pf[pf.Length - 1].Y);
                ziti(g, new PointF(pf[pf.Length - 1].X - 200 + 25, pf[pf.Length - 1].Y - 50 + 25), "X");

                //绘制字体
                PointF pf1 = new PointF();
                pf1.X = (float)jisuan.XZH;
                pf1.Y = (float)jisuan.YZH;
                ziti(g, pf1, "ZH点" + '(' + Convert.ToString(jisuan.XZH) + ',' + Convert.ToString(jisuan.YZH) + ')');
                pf1.X = (float)jisuan.XHY;
                pf1.Y = (float)jisuan.YHY;
                ziti(g, pf1, "HY点" + '(' + Convert.ToString(jisuan.XHY) + ',' + Convert.ToString(jisuan.YHY) + ')');
                pf1.X = (float)jisuan.XYH;
                pf1.Y = (float)jisuan.YYH;
                ziti(g, pf1, "YH点" + '(' + Convert.ToString(jisuan.XYH) + ',' + Convert.ToString(jisuan.YYH) + ')');
                pf1.X = (float)jisuan.XHZ;
                pf1.Y = (float)jisuan.YHZ;
                ziti(g, pf1, "Hz点" + '(' + Convert.ToString(jisuan.XHZ) + ',' + Convert.ToString(jisuan.YHZ) + ')');
                pf1.X = (float)jisuan.JDX;
                pf1.Y = (float)jisuan.JDY;
                ziti(g, pf1, "JD点" + '(' + Convert.ToString(jisuan.JDX) + ',' + Convert.ToString(jisuan.JDY) + ')');
                pf1.X = (float)jisuan.JDX + 100 * (float)Math.Cos(jisuan.fangwei);
                pf1.Y = (float)jisuan.JDY + 100 * (float)Math.Sin(jisuan.fangwei);
                ziti(g, pf1, "转角α" + Convert.ToString(Caculate.hudutodms(jisuan.A)));
                form2.pictureBox1.Image = (Image)image;
            }
            catch
            {
                MessageBox.Show("请先进行计算再绘图！");
                return;
            }
            form2.Show();//没有绘图就不会显示窗体
        }
        public void ziti(Graphics g, PointF pf, string STR)
        {
            Bitmap bt2 = new Bitmap(200,800);
            Graphics g2 = Graphics.FromImage(bt2);
            g2.RotateTransform(90);
            g2.TranslateTransform(0, -30);
            g2.DrawString(STR, new Font("宋体", 20), Brushes.Blue, new Point(5, 5));
            g.DrawImage((Image)bt2, pf.X - 25, pf.Y - 25);
        }
    }
}
