using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace _2.曲线要素计算
{
    class Save
    {
        public Save(Form1 form)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "保存文件";
            save.Filter = "文本文件|*.txt|Excel旧版本文件|*.xls|Excel新版本文件|*.xlsx";
            if (save.ShowDialog() == DialogResult.OK)
            {
                #region txt文档
                if (save.FilterIndex == 1)
                {
                    string[] strarray = jisuan.str.Split('\n');//文件流不能识别字符串里的\n，所以只能用writeline换行
                    StreamWriter sw = new StreamWriter(save.FileName);
                    sw.WriteLine(form.textBox1.Text +  form.textBox2.Text);
                    for (int i = 0; i < strarray.Length; i++)
                    {
                        sw.WriteLine(strarray[i]);
                    }
                    sw.WriteLine();

                    sw.WriteLine("逐桩坐标");
                    string[] strarray1 = jisuan.str1.Split('\n');
                    for (int i = 0; i < strarray1.Length; i++)
                    {
                        sw.WriteLine(strarray1[i]);
                    }
                    sw.Close();
                    MessageBox.Show("保存成功！");
                }
                #endregion
                #region xls或xlsx文件
                else
                {
                    Excel.Application excel1 = new Excel.Application();//创建一个excel对象
                    Excel.Workbook workbook1 = excel1.Workbooks.Add(true);//为该excel对象添加一个工作簿
                    Excel.Worksheet worksheet1 = excel1.Workbooks[1].Worksheets[1];//获取工作簿中的第一个工作表
                    //Excel.Range range = worksheet1.get_Range(worksheet1.Cells[1, 1], worksheet1.Cells[1, 12]);//获取部分单元格
                    //range.Merge(0);//合并单元格
                    //为工作表填充数据，以单元格为操作对象
                    #region 曲线要素赋值
                    worksheet1.Cells[1, 1].Value = form.textBox1.Text + form.textBox2.Text;
                    worksheet1.Cells[2, 1].Value = "主点要素：";
                    worksheet1.Cells[3, 1].Value = "p1 = " + jisuan.p1;
                    worksheet1.Cells[3, 2].Value = "p2 = " + jisuan.p2;
                    worksheet1.Cells[3, 3].Value = "q1 = " + jisuan.q1;
                    worksheet1.Cells[3, 4].Value = "q2 = " + jisuan.q2;
                    worksheet1.Cells[3, 5].Value = "T1 = " + jisuan.T1;
                    worksheet1.Cells[3, 6].Value = "T2 = " + jisuan.T2;
                    worksheet1.Cells[4, 1].Value = "圆曲线长LY = " + jisuan.LY;
                    worksheet1.Cells[4, 2].Value = "平曲线长LH = " + jisuan.LH;
                    worksheet1.Cells[4, 3].Value = "外矢距E = " + jisuan.E;
                    worksheet1.Cells[4, 4].Value = "切曲差DH = " + jisuan.DH;
                    worksheet1.Cells[5, 1].Value = "主点里程桩号：";
                    worksheet1.Cells[6, 1].Value = "KZH = " + jisuan.KZH;
                    worksheet1.Cells[6, 2].Value = "KHY = " + jisuan.KHY;
                    worksheet1.Cells[6, 3].Value = "KQZ = " + jisuan.KQZ;
                    worksheet1.Cells[6, 4].Value = "KYH = " + jisuan.KYH;
                    worksheet1.Cells[6, 5].Value = "KHZ = " + jisuan.KHZ;
                    worksheet1.Cells[7, 1].Value = "主点坐标：";
                    worksheet1.Cells[8, 1].Value = "ZH(x,y) = " + jisuan.XZH + " , " + jisuan.YZH;
                    worksheet1.Cells[8, 2].Value = "HY(x,y) = " + jisuan.XHY + " , " + jisuan.YHY;
                    worksheet1.Cells[8, 3].Value = "YH(x,y) = " + jisuan.XYH + " , " + jisuan.YYH;
                    worksheet1.Cells[8, 4].Value = "HZ(x,y) = " + jisuan.XHZ + " , " + jisuan.YHZ;
                    #endregion
                    #region 逐桩坐标赋值
                    worksheet1.Cells[10, 1].Value = "逐桩坐标：";
                    worksheet1.Cells[11, 1].Value = "桩号";
                    worksheet1.Cells[11, 2].Value = "X坐标";
                    worksheet1.Cells[11, 3].Value = "Y坐标";
                    worksheet1.Cells[11, 4].Value = "切线方位角(d.ms)";
                    worksheet1.Cells[11, 5].Value = "左边桩X";
                    worksheet1.Cells[11, 6].Value = "左边桩Y";
                    worksheet1.Cells[11, 7].Value = "右边桩X";
                    worksheet1.Cells[11, 8].Value = "右边桩Y";
                    for (int i = 0; i < jisuan.zhuanghao.Count; i++)
                    {
                        worksheet1.Cells[i + 12, 1].Value = jisuan.zhuanghao[i];
                        worksheet1.Cells[i + 12, 2].Value = jisuan.Xzuobiao[i];
                        worksheet1.Cells[i + 12, 3].Value = jisuan.Yzuobiao[i];
                        worksheet1.Cells[i + 12, 4].Value = Caculate.hudutodms(jisuan.fangweijiao[i]);
                        worksheet1.Cells[i + 12, 5].Value = jisuan.leftX[i];
                        worksheet1.Cells[i + 12, 6].Value = jisuan.leftY[i];
                        worksheet1.Cells[i + 12, 7].Value = jisuan.rightX[i];
                        worksheet1.Cells[i + 12, 8].Value = jisuan.rightY[i];
                    }
                    #endregion
                    worksheet1.Columns.AutoFit();//自动调整列宽
                    worksheet1.SaveAs(save.FileName);//保存工作表
                    workbook1.Close();
                    MessageBox.Show("保存成功！");
                }
                #endregion
            }
        }
    }
}
