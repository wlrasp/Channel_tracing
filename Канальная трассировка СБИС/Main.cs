using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Канальная_трассировка_СБИС
{
    public partial class Main : Form
    {

        int m = 50;//масштаб эскиза




        public Main()
        {
            InitializeComponent();
            SelfRef = this;
            Main.SelfRef.AutoScroll = true;
            panel1.AutoScroll = true;
        }//////////////

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void загрузитьИзБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)//////настройки
        {
            Settings rv = new Settings();
            rv.Show();
        }

        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)//////////////
        {

            int[] e1 = new int[1];
            Input rv = new Input(0, "", "", "", e1, e1);
            rv.Show();
        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fOpen a = new fOpen();
            a.fileopen();
            a = null;
        }//////////////

        private void загрузитьИзБазыДанныхToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadBD rv = new LoadBD();
            rv.Show();
        }//////////////

        public static Main SelfRef
        {
            get; set;
        }//////////////


        int[] B;
        int[] T;
        int[,] Eskiz_rezult;
        int n=0;
        int C;
        int shirina_Kanala;


        public void Paintt (int[] T1, int[] B1, int n1, int C1, int shirina_Kanala1, int[,] Eskiz_rezult1, int g)
        {
            if (g == 1)
            {
                n = n1;
                C = C1;
                shirina_Kanala = shirina_Kanala1;

                B = new int[C];
                T = new int[C];
                Eskiz_rezult = new int[n, n];


                for (int i = 0; i < C; i++)
                {
                    B[i] = B1[i];
                    T[i] = T1[i];
                }

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        Eskiz_rezult[i, j] = Eskiz_rezult1[i, j];


                pictureBox1.Refresh();
            }
            else
            {
                pictureBox1.Width = 1;
                pictureBox1.Height = 1;
            }
        }//////////////


        public void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (n > 0)
            {
                pictureBox1.Width = m * (C + 3);
                pictureBox1.Height = m * (shirina_Kanala + 5);

                SolidBrush Brush = new SolidBrush(Color.Black);
                int x = 0;
                int y = 0;
                int width = m / 7;
                int height = m / 7;

                Pen lin = new Pen(Color.Black, 1);
                lin.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                float[] dashValues = { m / 10, m / 10 };
                Pen strih = new Pen(Color.Black, 1);
                strih.DashPattern = dashValues;

                //String drawString = "5";
                // Create font and brush.
                Font drawFont = new Font("Times New Roman", m / 3);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.
                //int x7 = 150;
                //int y7 = 150;


                int x1, y1, x2, y2;

                x1 = m;
                y1 = 2 * m;
                x2 = m * (C + 2);
                y2 = 2 * m;
                e.Graphics.DrawLine(lin, x1, y1, x2, y2);
                e.Graphics.DrawLine(lin, x1, y1, x1, y1 - (m / 2));
                e.Graphics.DrawLine(lin, x2, y2 - (m / 2), x2, y2);
                x = x1 + m; y = y1;
                for (int i = 0; i < C; i++)
                {
                    e.Graphics.FillEllipse(Brush, x - m / 14, y - m / 14, width, height);
                    if (T[i] > 0)
                        e.Graphics.DrawString(T[i].ToString(), drawFont, drawBrush, x - m / 5, y - m / 2 - m / 10);
                    x += m;
                }

                x1 = m;
                y1 = m * (shirina_Kanala + 3);
                x2 = m * (C + 2);
                y2 = m * (shirina_Kanala + 3);
                e.Graphics.DrawLine(lin, x1, y1, x2, y2);
                e.Graphics.DrawLine(lin, x1, y1, x1, y1 + (m / 2));
                e.Graphics.DrawLine(lin, x2, y2 + (m / 2), x2, y2);
                x = x1 + m; y = y1;
                for (int i = 0; i < C; i++)
                {
                    e.Graphics.FillEllipse(Brush, x - m / 14, y - m / 14, width, height);
                    if (B[i] > 0)
                        e.Graphics.DrawString(B[i].ToString(), drawFont, drawBrush, x - m / 5, y + m / 10);
                    x += m;
                }


                width = m / 8;
                height = m / 8;
                x = 2 * m;
                y = 3 * m;




                for (int i = 0; i < shirina_Kanala; i++)//магистраль
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (Eskiz_rezult[i, j] > 0)
                        {
                            int nach = m * (C + 3);
                            int kon = 0;

                            for (int k = 0; k < C; k++)
                            {
                                if (T[k] == Eskiz_rezult[i, j])
                                {
                                    int x3 = 2 * m + m * k;//
                                    int y3 = 2 * m;//
                                    int x4 = 2 * m + m * k;
                                    int y4 = 2 * m + m * (i + 1);
                                    e.Graphics.DrawLine(strih, x3, y3, x4, y4);
                                    e.Graphics.FillRectangle(Brush, x4 - m / 16, y4 - m / 16, width, height);
                                    if (x3 < nach) nach = x3;
                                    if (x3 > kon) kon = x3;
                                }

                                if (B[k] == Eskiz_rezult[i, j])
                                {
                                    int x3 = 2 * m + m * k;//
                                    int y3 = 2 * m + m * (shirina_Kanala + 1);//
                                    int x4 = 2 * m + m * k;
                                    int y4 = 2 * m + m * (i + 1);
                                    e.Graphics.DrawLine(strih, x3, y3, x4, y4);
                                    e.Graphics.FillRectangle(Brush, x4 - m / 16, y4 - m / 16, width, height);
                                    if (x3 < nach) nach = x3;
                                    if (x3 > kon) kon = x3;
                                }
                            }
                            e.Graphics.DrawLine(lin, nach, 2 * m + m * (i + 1), kon, 2 * m + m * (i + 1));


                        }
                    }
                }


            }
        }//////////////

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
