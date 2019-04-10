using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2.曲线要素计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();//初始化窗体控件
        }
        #region 时间控件
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
            timer1.Enabled = true;
            timer1.Interval = 1000;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }
        #endregion
        #region 打开
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open open = new Open(this);
        }
        #endregion
        #region 计算
        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jisuan Jisuan = new jisuan(this);
        }
        #endregion
        #region 其他
        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\t本软件由吉林建筑大学团队开发\n\t使用过程中有任何疑问\n\t请联系QQ：1209001368");
        }
        private void 帮助文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }
        #endregion
        #region 保存刷新
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save save = new Save(this);
            MessageBox.Show("保存成功");
        }
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();//清空窗体中的所有控件
            this.InitializeComponent();
        }
        #endregion
        #region 绘图
        private void 绘图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.pictureBox1.Refresh();//刷新picturebox
            huitu Huitu = new huitu(this,form2);
        }
        #endregion
        #region dxf保存
        private void dxfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "dxf图形保存";
            saveFileDialog1.Filter = "dxf图形(*.dxf)|*.dxf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            { 
                
            }
        }
        #endregion
        #region bmp保存
        private void bmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save1 = new SaveFileDialog();
                save1.Title = "bmp文件保存";
                save1.Filter = "图像文件(*.bmp)|*.bmp";
                if (save1.ShowDialog() == DialogResult.OK)
                {
                    huitu.image.Save(save1.FileName);
                }
                MessageBox.Show("保存成功！");
            }
            catch
            {
                MessageBox.Show("请先进行绘图再保存！");
                return;
            }
        }
        #endregion

        private void rdb_left_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
