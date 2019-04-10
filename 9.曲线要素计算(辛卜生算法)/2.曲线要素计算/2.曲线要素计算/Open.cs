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
    class Open
    {
        public Open(Form1 form)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "文本文件|*.txt|Excel旧版本文件|*.xls|Excel新版本文件|*.xlsx";
            open.Title = "打开文件";
            if (open.ShowDialog() == DialogResult.OK)
            {
                    #region txt文档
                if (open.FilterIndex == 1)
                {
                    using (StreamReader sr = new StreamReader(open.FileName, Encoding.Default))
                    {
                        string[] StrArray;
                        sr.ReadLine();
                        sr.ReadLine();
                        StrArray = sr.ReadLine().Split(',');
                        form.txt_JDZ.Text = StrArray[0];
                        form.txt_JDX.Text = StrArray[1];
                        form.txt_JDY.Text = StrArray[2];

                        sr.ReadLine();
                        sr.ReadLine();
                        StrArray = sr.ReadLine().Split(',');
                        form.txt_HZZ.Text = StrArray[0];
                        form.txt_HZX.Text = StrArray[1];
                        form.txt_HZY.Text = StrArray[2];

                        sr.ReadLine();
                        sr.ReadLine();
                        StrArray = sr.ReadLine().Split(',');
                        form.txt_R.Text = StrArray[0];
                        form.txt_A.Text = StrArray[1];
                        form.txt_ZJ.Text = StrArray[2];
                        form.txt_Ls1.Text = StrArray[3];
                        form.txt_Ls2.Text = StrArray[4];

                        sr.ReadLine();
                        sr.ReadLine();
                        StrArray = sr.ReadLine().Split(',');
                        form.txt_leftZJ.Text = StrArray[0];
                        form.txt_rightZJ.Text = StrArray[1];
                    }
                }
                    #endregion
                #region xls或xlsx文档
                else
                {
                    Excel.Application excel1 = new Excel.Application();//创建一个Excel文件
                    excel1.Visible = false;//以只读方式打开Excel文件
                    Excel.Workbook workbook1 = excel1.Application.Workbooks.Open(open.FileName);//获取要打开的工作簿
                    Excel.Worksheet worksheet1 = excel1.Workbooks[1].Worksheets[1];//获取工作簿中的第一个工作表
                    //int rows1 = worksheet1.UsedRange.Rows.Count;//得到行数
                    //MessageBox.Show(rows1.ToString());
                    //int columns1 = worksheet1.UsedRange.Columns.Count;//得到列数
                    //MessageBox.Show(columns1.ToString());
                    form.txt_JDZ.Text = worksheet1.Cells[3, 1].Value.ToString();
                    form.txt_JDX.Text = worksheet1.Cells[3, 2].Value.ToString();
                    form.txt_JDY.Text = worksheet1.Cells[3, 3].Value.ToString();
                    form.txt_HZZ.Text = worksheet1.Cells[6, 1].Value.ToString();
                    form.txt_HZX.Text = worksheet1.Cells[6, 2].Value.ToString();
                    form.txt_HZY.Text = worksheet1.Cells[6, 3].Value.ToString();
                    form.txt_R.Text = worksheet1.Cells[9, 1].Value.ToString();
                    form.txt_A.Text = worksheet1.Cells[9, 2].Value.ToString();
                    form.txt_ZJ.Text = worksheet1.Cells[9, 3].Value.ToString();
                    form.txt_Ls1.Text = worksheet1.Cells[9, 4].Value.ToString();
                    form.txt_Ls2.Text = worksheet1.Cells[9, 5].Value.ToString();
                    form.txt_leftZJ.Text = worksheet1.Cells[12, 1].Value.ToString();
                    form.txt_rightZJ.Text = worksheet1.Cells[12, 2].Value.ToString();
                    workbook1.Close();
                }
                #endregion
                
            }
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
    }
}
