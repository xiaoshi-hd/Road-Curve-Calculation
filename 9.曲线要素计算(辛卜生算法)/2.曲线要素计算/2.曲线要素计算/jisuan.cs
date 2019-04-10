using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;//类加窗体引用才能调用窗体对象,同时要把所有窗体控件的modifiers属性设置为public

namespace _2.曲线要素计算
{
    public class jisuan
    {
        //声明为public变量，可以在该类的外部调用，但必须实例化类，再声明为static静态变量，不用实例化，只需加上类型即可调用
        int k,n;//转向，保留小数位数
        public static double R, Ls1, Ls2, A;//已知曲线要素
        public static double q1, q2, p1, p2, T1, T2, LY, LH, E, DH;//计算曲线要素
        public static double KQHZ, KJD, KZH, KHY, KQZ, KYH, KHZ, ZJ;//主点里程桩号
        public static double XZH, YZH, XHY, YHY, XQZ, YQZ, XYH, YYH, XHZ, YHZ,HYT,QZT,YHT,HZT;//主点坐标,切线方位角
        public static double JDX,JDY,QHZX,QHZY;//交点，前缓圆点X,Y坐标
        public static double fangwei;//起点方位角
        public double leftZJ, rightZJ;//左右桩距
        public static List<double> zhuanghao;
        public static List<double> fangweijiao;
        public static List<double> Xzuobiao;
        public static List<double> Yzuobiao;
        public static List<double> leftX;
        public static List<double> leftY;
        public static List<double> rightX;
        public static List<double> rightY;
        public static string str;
        public static string str1;
        public jisuan(Form1 form)
        {
            #region 初始化
            zhuanghao = new List<double>();
            fangweijiao =new List<double>();
            Xzuobiao = new List<double>();
            Yzuobiao = new List<double>();
            leftX = new List<double>();
            leftY = new List<double>();
            rightX = new List<double>();
            rightY = new List<double>();
            form.richTextBox1.Text = "";
            form.richTextBox2.Text = "";
            #endregion
            #region 曲线转向
            if (form.rdb_left.Checked)
            {
                k = -1;
            }
            else
            {
                k = 1;
            }
            #endregion
            #region 已知数据导入
            try
            {
                Ls1 = Convert.ToDouble(form.txt_Ls1.Text.Replace(" ",""));
                Ls2 = Convert.ToDouble(form.txt_Ls2.Text.Replace(" ", ""));
                R = Convert.ToDouble(form.txt_R.Text.Replace(" ", ""));
                A = Caculate.dmstohudu(Convert.ToDouble(form.txt_A.Text.Replace(" ", "")));
                KJD = Convert.ToDouble(form.txt_JDZ.Text.Replace(" ", ""));
                KQHZ = Convert.ToDouble(form.txt_HZZ.Text.Replace(" ", ""));
                ZJ = Convert.ToInt16(form.txt_ZJ.Text.Replace(" ", ""));
                JDX = Convert.ToDouble(form.txt_JDX.Text.Replace(" ", ""));
                JDY = Convert.ToDouble(form.txt_JDY.Text.Replace(" ", ""));
                QHZX = Convert.ToDouble(form.txt_HZX.Text.Replace(" ", ""));
                QHZY = Convert.ToDouble(form.txt_HZY.Text.Replace(" ", ""));
                leftZJ = Convert.ToDouble(form.txt_leftZJ.Text.Replace(" ", ""));
                rightZJ = Convert.ToDouble(form.txt_rightZJ.Text.Replace(" ", ""));
            }
            catch
            {
                MessageBox.Show("请输入正确的已知数据！");
                return;
            }
            #endregion
            #region 保留小数位数
            //其实没有太大意义，一般保留三位小数即可
            string[] s = form.txt_JDX.Text.Split('.');//按小数点分隔字符串
            if (s.Length == 2)//s[2]存在
            {
                n = s[1].Replace(" ", "").Length;
            }
            else
            {
                n = 3;
            }
            //MessageBox.Show(n.ToString());
            #endregion
            #region 曲线要素
            p1 = Math.Round(Ls1 * Ls1 / 24 / R, n);
            p2 = Math.Round(Ls2 * Ls2 / 24 / R, n);
            q1 = Math.Round(Ls1 / 2 - Math.Pow(Ls1, 3) / 240 / Math.Pow(R, 2), n);
            q2 = Math.Round(Ls2 / 2 - Math.Pow(Ls2, 3) / 240 / Math.Pow(R, 2), n);
            T1 = Math.Round((R + p1) * Math.Tan(A / 2) + q1 - (p1 - p2) / Math.Sin(A), n);
            T2 = Math.Round((R + p2) * Math.Tan(A / 2) + q2 - (p1 - p2) / Math.Sin(A), n);
            LY = Math.Round(R * A - (Ls1 + Ls2) / 2, n);
            LH = Math.Round(LY + Ls1 + Ls2, n);
            E = Math.Round((R + (p1 + p2) / 2) / Math.Cos(A / 2) - R, n);
            DH = Math.Round(T1 + T2 - LH, n);
            #endregion
            #region 主点里程桩号
            KZH = Math.Round(KJD - T1, n);
            KHY = Math.Round(KZH + Ls1, n);
            KQZ = Math.Round(KZH + LH / 2, n);
            KYH = Math.Round(KZH + LY + Ls1, n);
            KHZ = Math.Round(KZH + LH, n);
            #endregion
            #region 计算桩号
            double zhuanghao1;//作为中间变量存储桩号
            zhuanghao1 = KQHZ - KQHZ % ZJ;
            zhuanghao.Add(KQHZ);
            while (true)
            {
                zhuanghao1 += ZJ;
                zhuanghao1 = Math.Round(zhuanghao1,n);
                if (zhuanghao1 > KHZ)
                {
                    zhuanghao.Add(KHZ);
                    break;
                }
                zhuanghao.Add(zhuanghao1);
                if (zhuanghao1 == Math.Floor(KZH / ZJ) * ZJ)
                {
                    zhuanghao.Add(KZH);
                    continue;
                }
                if (zhuanghao1 == Math.Floor(KHY / ZJ) * ZJ)
                {
                    zhuanghao.Add(KHY);
                    continue;
                } 
                if (zhuanghao1 == Math.Floor(KQZ / ZJ) * ZJ)
                {
                    zhuanghao.Add(KQZ);
                    continue;
                } 
                if (zhuanghao1 == Math.Floor(KYH / ZJ) * ZJ)
                {
                    zhuanghao.Add(KYH);
                    continue;
                }
            }
            #endregion
            #region 切线方位角计算
            fangwei = Caculate.fangweijiao(QHZX,QHZY,JDX,JDY);//返回弧度值
            #endregion
            #region 4等分复化辛普生公式计算曲线中桩坐标
            //首先需要定义曲线段主点的曲率，通过曲率计算切线方位角，进而求出坐标
            List<double> fenduanfangwei = new List<double>();
            double QA, QT, ZA, iA, iT,iX,iY;//起终点曲率和切线方位角
            double H;
            for (int i = 0; i < zhuanghao.Count; i++)//遍历所有里程桩号
            {
                 #region 直线段
                if (zhuanghao[i] <= KZH)//当KHZ=QKZH时，也就是直线段为0时，HZ的坐标等于QHZ的坐标
                {
                    Xzuobiao.Add(Math.Round(QHZX + (zhuanghao[i] - zhuanghao[0]) * Math.Cos(fangwei), n));
                    Yzuobiao.Add(Math.Round(QHZY + (zhuanghao[i] - zhuanghao[0]) * Math.Sin(fangwei), n));
                    fangweijiao.Add(fangwei);//存储弧度值
                    XZH = Xzuobiao[i];
                    YZH = Yzuobiao[i];
                }
                #endregion
                #region ZH--HY段
                if (form.txt_Ls1.Text == "0" )//Ls1 = 0的情况
                {
                    HYT = fangwei;
                    XHY = XZH;
                    YHY = YZH;
                }
                if (zhuanghao[i] > KZH && zhuanghao[i] <= KHY)//当Ls1=0时，KZH=KHY，以下程序不会被执行,即Ls1！=0的情况
                {
                    QA = 0;
                    QT = fangwei;
                    ZA = k * 1.0 / R;
                    iA = QA + (ZA - QA) * (zhuanghao[i] - KZH) / (KHY - KZH);
                    iT = QT + (iA + QA) * (zhuanghao[i] - KZH) / 2.0;
                    fangweijiao.Add(iT);
                    HYT = iT;

                    fenduanfangwei.Clear();
                    fenduanfangwei.Add(iT);

                    H = (zhuanghao[i] - KZH) / 4;
                    for (double j = KZH + H / 2.0; j < zhuanghao[i]; j += H / 2.0)
                    {
                        if (fenduanfangwei.Count < 8)//KZH,KZH+H/2,KZH+H,KZH+3H/2,KZH+2H,KZH+5H/2,KZH+3H,KZH+7H/2-----共8个元素
                        {
                            iA = QA + (ZA - QA) * (j - KZH) / (KHY - KZH);
                            iT = QT + (iA + QA) * (j - KZH) / 2.0;
                            fenduanfangwei.Add(iT);
                        }
                    }
                    double sinA = 0,cosA = 0,sinB = 0,cosB = 0;//A为2K等分点的切线方位角，B为K等分点的切线方位角
                    for (int j = 0; j < fenduanfangwei.Count; j++)
                    {
                        if (j % 2 == 1)//循环4次
                        {
                            sinA += Math.Sin(fenduanfangwei[j]);
                            cosA += Math.Cos(fenduanfangwei[j]);
                        }
                        else
                        {
                            if (j > 1)//循环3次
                            {
                                sinB += Math.Sin(fenduanfangwei[j]);
                                cosB += Math.Cos(fenduanfangwei[j]);
                            }
                        }
                    }
                    //用辛普生公式计算坐标
                    iX = XZH + H * (Math.Cos(QT) + 4 * cosA + 2 * cosB + Math.Cos(HYT)) / 6;
                    iY = YZH + H * (Math.Sin(QT) + 4 * sinA + 2 * sinB + Math.Sin(HYT)) / 6;
                    Xzuobiao.Add(Math.Round(iX, 3));
                    Yzuobiao.Add(Math.Round(iY, 3));
                    XHY = Math.Round(iX, 3);
                    YHY = Math.Round(iY, 3);
                }
                #endregion
                #region HY--YH
                if (zhuanghao[i] > KHY && zhuanghao[i] <= KYH)
                {
                    QA = k * 1.0 / R;
                    QT = HYT;
                    ZA = k * 1.0 / R;
                    iA = QA + (ZA - QA) * (zhuanghao[i] - KHY) / (KYH - KHY);
                    iT = QT + (iA + QA) * (zhuanghao[i] - KHY) / 2.0;
                    fangweijiao.Add(iT);
                    YHT = iT;

                    fenduanfangwei.Clear();
                    fenduanfangwei.Add(iT);

                    H = (zhuanghao[i] - KHY) / 4;
                    for (double j = KHY + H / 2.0; j < zhuanghao[i]; j += H / 2.0)
                    {
                        if (fenduanfangwei.Count < 8)//KZH,KZH+H/2,KZH+H,KZH+3H/2,KZH+2H,KZH+5H/2,KZH+3H,KZH+7H/2-----共8个元素
                        {
                            iA = QA + (ZA - QA) * (j - KHY) / (KYH - KHY);
                            iT = QT + (iA + QA) * (j - KHY) / 2.0;
                            fenduanfangwei.Add(iT);
                        }
                    }
                    double sinA = 0, cosA = 0, sinB = 0, cosB = 0;//A为2K等分点的切线方位角，B为K等分点的切线方位角
                    for (int j = 0; j < fenduanfangwei.Count; j++)
                    {
                        if (j % 2 == 1)//循环4次
                        {
                            sinA += Math.Sin(fenduanfangwei[j]);
                            cosA += Math.Cos(fenduanfangwei[j]);
                        }
                        else
                        {
                            if (j > 1)//循环3次
                            {
                                sinB += Math.Sin(fenduanfangwei[j]);
                                cosB += Math.Cos(fenduanfangwei[j]);
                            }
                        }
                    }
                    //用辛普生公式计算坐标
                    iX = XHY + H * (Math.Cos(QT) + 4 * cosA + 2 * cosB + Math.Cos(YHT)) / 6;
                    iY = YHY + H * (Math.Sin(QT) + 4 * sinA + 2 * sinB + Math.Sin(YHT)) / 6;
                    Xzuobiao.Add(Math.Round(iX, 3));
                    Yzuobiao.Add(Math.Round(iY, 3));
                    XYH = Math.Round(iX, 3);
                    YYH = Math.Round(iY, 3);
                }
                #endregion
                #region YH--HZ段
                if (form.txt_Ls2.Text == "0")//Ls2 = 0的情况
                {
                    HZT = YHT;
                    XHZ = XYH;
                    YHZ = YYH;
                }
                if (zhuanghao[i] > KYH && zhuanghao[i] <= KHZ)
                {
                    QA = k * 1.0 / R;
                    QT = YHT;
                    ZA = 0;
                    iA = QA + (ZA - QA) * (zhuanghao[i] - KYH) / (KHZ - KYH);
                    iT = QT + (iA + QA) * (zhuanghao[i] - KYH) / 2.0;
                    fangweijiao.Add(iT);
                    HZT = iT;

                    fenduanfangwei.Clear();
                    fenduanfangwei.Add(iT);

                    H = (zhuanghao[i] - KYH) / 4;
                    for (double j = KYH + H / 2.0; j < zhuanghao[i]; j += H / 2.0)
                    {
                        if (fenduanfangwei.Count < 8)//KZH,KZH+H/2,KZH+H,KZH+3H/2,KZH+2H,KZH+5H/2,KZH+3H,KZH+7H/2-----共8个元素
                        {
                            iA = QA + (ZA - QA) * (j - KYH) / (KHZ - KYH);
                            iT = QT + (iA + QA) * (j - KYH) / 2.0;
                            fenduanfangwei.Add(iT);
                        }
                    }
                    double sinA = 0, cosA = 0, sinB = 0, cosB = 0;//A为2K等分点的切线方位角，B为K等分点的切线方位角
                    for (int j = 0; j < fenduanfangwei.Count; j++)
                    {
                        if (j % 2 == 1)//循环4次
                        {
                            sinA += Math.Sin(fenduanfangwei[j]);
                            cosA += Math.Cos(fenduanfangwei[j]);
                        }
                        else
                        {
                            if (j > 1)//循环3次
                            {
                                sinB += Math.Sin(fenduanfangwei[j]);
                                cosB += Math.Cos(fenduanfangwei[j]);
                            }
                        }
                    }
                    //用辛普生公式计算坐标
                    iX = XYH + H * (Math.Cos(QT) + 4 * cosA + 2 * cosB + Math.Cos(HZT)) / 6;
                    iY = YYH + H * (Math.Sin(QT) + 4 * sinA + 2 * sinB + Math.Sin(HZT)) / 6;
                    Xzuobiao.Add(Math.Round(iX, 3));
                    Yzuobiao.Add(Math.Round(iY, 3));
                    XHZ = Math.Round(iX, 3);
                    YHZ = Math.Round(iY, 3);
                }
                #endregion
            }
            #endregion
            #region 边桩计算
            for (int i = 0; i < Xzuobiao.Count; i++)
            {
                leftX.Add(Math.Round(Xzuobiao[i] + leftZJ * Math.Cos(fangweijiao[i] - Math.PI / 2),3));
                leftY.Add(Math.Round(Yzuobiao[i] + leftZJ * Math.Sin(fangweijiao[i] - Math.PI / 2),3));
                rightX.Add(Math.Round(Xzuobiao[i] + rightZJ * Math.Cos(fangweijiao[i] + Math.PI / 2), 3));
                rightY.Add(Math.Round(Yzuobiao[i] + rightZJ * Math.Sin(fangweijiao[i] + Math.PI / 2), 3));
            }
            #endregion
            #region 数据导出
            str = "主点要素:" + "\n" + "p1=" + p1 + "\t" + "p2=" + p2 + "\t" + "q1=" + q1 + "\t" + "q2=" + q2 + "\t" + "T1=" + T1 + "\t" + "T2=" + T2 + "\n";
            str += "圆曲线长Ly=" + LY + "\t" + "平曲线 L=" + LH + "\t" + " 外矢距 E=" + E + "\t" + "切曲差D=" + DH + "\n";
            str += "主点里程桩号：\n" + "KZH=" + KZH + "\t" + "KHY=" + KHY + "\t" + "KQZ=" + KQZ + "\t" + "KYH=" + KYH + "\t" + "KHZ=" + KHZ + "\n";
            str += "主点坐标：\n" + "ZH(x,y) " + XZH + "," + YZH + "\t" + "HY(x,y) " + XHY + "," + YHY + "\n" + "YH(x,y) " + XYH + "," + YYH + "\t" + "HZ(x,y) " + XHZ + "," + YHZ;
            form.richTextBox1.Text = str;
            
            str1 = null;
            str1 = string.Format("{0,-8}", "桩号") + string.Format("{0,-12}", "X坐标") + string.Format("{0,-13}", "Y坐标") + string.Format("{0,-12}", "切线方位角(d.ms)") 
                + string.Format("{0,-10}", "左边桩X") + string.Format("{0,-10}", "左边桩Y") + string.Format("{0,-10}", "右边桩X") + string.Format("{0,-10}", "右边桩Y") + "\n";
            for (int i = 0; i < zhuanghao.Count; i++)
            {
                str1 += string.Format("{0,-10}", zhuanghao[i]) + string.Format("{0,-15}", Xzuobiao[i]) + string.Format("{0,-15}", Yzuobiao[i]) + string.Format("{0,-15}", Caculate.hudutodms(fangweijiao[i]));
                str1 += string.Format("{0,-15}", leftX[i]) + string.Format("{0,-15}", leftY[i]) + string.Format("{0,-15}", rightX[i]) + string.Format("{0,-15}",rightY[i]) + "\n";
            }
            form.richTextBox2.Text = str1;
            #endregion
        }
    }
}
