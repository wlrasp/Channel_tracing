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
    public partial class Input : Form
    {
        int flag1 = 0;
        public Input(int flag, string vl, string mm, string kc, int[] Top, int[] Bot)
        {

            InitializeComponent();
            flag1 = flag;
            if (flag == 1)
            {
                
                maskedTextBox2.Text = vl;
                //MessageBox.Show(mm);
                if (Convert.ToInt32(mm) > 0)
                {

                    maskedTextBox1.Enabled = true;
                    checkBox1.Checked = true;
                    maskedTextBox1.Text = mm;
                }
                else
                {
                    maskedTextBox1.Enabled = false;
                    checkBox1.Checked = false;
                }
                maskedTextBox4.Text = kc;

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                for (int i = 1; i <= (Convert.ToInt32(maskedTextBox2.Text)); i++)
                {
                    dataGridView1.Columns.Add("T" + i, "T" + i);
                    dataGridView1.Columns[i - 1].Width = 60;

                }

                for (int i = 1; i <= (Convert.ToInt32(maskedTextBox2.Text)); i++)
                {
                    dataGridView2.Columns.Add("B" + i, "B" + i);
                    dataGridView2.Columns[i - 1].Width = 60;
                }


                this.dataGridView2.Rows.Add();
                this.dataGridView1.Rows.Add();


                for (int i = 0; i < Convert.ToInt32(vl); i++)
                {
                    dataGridView1.Rows[0].Cells[i].Value = Top[i];
                    dataGridView2.Rows[0].Cells[i].Value = Bot[i];
                }



                button3.Enabled = true;
                button1.Enabled = false;

            }
        }//////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;

            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //dataGridView2.AutoResizeColumns();
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;

            if (flag1 == 0)
            {
                maskedTextBox1.Enabled = false;
                button3.Enabled = false;
            }

        }//////////////////////////////////////////////////////

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button1.Enabled = true;
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)////кнопка применить
        {
            string s = "";
            bool z = true;

            try
            {
                int chislo = Convert.ToInt32(maskedTextBox2.Text);
            }
            catch
            {
                s += "\n(1) Неверный формат данных в поле - Число вертикальных линий";

                z = false;
            }

            if (z == true)
            {
                if (Convert.ToInt32(maskedTextBox2.Text) < 2)
                {
                    z = false;
                    s += "\n(2) Число вертикальных линий в канале должно быть не менее двух";
                }
            }




            if (checkBox1.Checked == true && z == true)
            {
                try
                {
                    int chislo = Convert.ToInt32(maskedTextBox1.Text);
                }
                catch
                {
                    s += "\n(3) Неверный формат данных в поле - Максимальное число магистралей ";

                    z = false;
                }
                if (z == true)
                {
                    if (Convert.ToInt32(maskedTextBox1.Text) < 1)
                    {
                        s += "\n(4) Число магистралей должно быть больше одного ";
                        z = false;
                    }

                }
            }



            if (z == true)
            {
                // button1.Enabled = false;
                // maskedTextBox1.Enabled = false;
                // maskedTextBox2.Enabled = false;

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                for (int i = 1; i <= (Convert.ToInt32(maskedTextBox2.Text)); i++)
                {
                    dataGridView1.Columns.Add("T" + i, "T" + i);
                    dataGridView1.Columns[i - 1].Width = 60;
                    dataGridView2.Columns.Add("B" + i, "B" + i);
                    dataGridView2.Columns[i - 1].Width = 60;
                }

                this.dataGridView2.Rows.Add();
                this.dataGridView1.Rows.Add();

                //checkBox1.Enabled = false;
                button3.Enabled = true;
                button1.Enabled = false;

            }
            else MessageBox.Show(s, "Ошибка!");
        }


        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "";
            bool z = true;

            proverka(ref z, ref s);

            if (z == true)
            {
                Hide();
                //maskedTextBox4.Enabled = false;
                //button3.Enabled = false;
                //textBox1.Enabled = false;
                //textBox2.Enabled = false;
                //textBox3.Enabled = false;
                //textBox4.Enabled = false;


                int n = Convert.ToInt32(maskedTextBox4.Text);//число цепей
                int C = Convert.ToInt32(maskedTextBox2.Text);//кол контактов в линейке
                

                int[] B;
                int[] T;
                B = new int[C];
                T = new int[C];
                for (int i = 0; i < C; i++)
                {
                    //верх
                    if (dataGridView1.Rows[0].Cells[i].Value != null) T[i] = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                    else T[i] = 0;
                    //низ
                    if (dataGridView2.Rows[0].Cells[i].Value != null) B[i] = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);
                    else B[i] = 0;
                }
                int ogr = 0;
                bool ogrYN = checkBox1.Checked;
                if (ogrYN == true) ogr = Convert.ToInt32(maskedTextBox1.Text);
                
                

                if (Properties.Settings.Default.alg_trassir == 1) 
                {
                    RA a = new RA();
                    a.Alg(n, C, B, T, ogr);
                    a = null;
                }

                if (Properties.Settings.Default.alg_trassir == 2)
                {

                    //отбор
                    if (Properties.Settings.Default.Otbor == 1) ParamGA[1] = 1;
                    if (Properties.Settings.Default.Otbor == 2) ParamGA[1] = 2;
                    if (Properties.Settings.Default.Otbor == 3) ParamGA[1] = 3;


                    //кросс

                    if (Properties.Settings.Default.Crossing == 1) ParamGA[2] = 1;
                    if (Properties.Settings.Default.Crossing == 2) ParamGA[2] = 2;

                    //мут

                    if (Properties.Settings.Default.Mutac == 1) ParamGA[3] = 1;
                    if (Properties.Settings.Default.Mutac == 2) ParamGA[3] = 2;


                    int RP = Properties.Settings.Default.R_pop;//размер популяции
                    int CG = Properties.Settings.Default.C_gen;//число генераций
                    int PCR = Properties.Settings.Default.P_Crossing;//вер кроссинговера %
                    int PMT = Properties.Settings.Default.P_Mutac;//вер мутации %
                    
                    GA obj = new GA();
                    obj.genetic_alg(n, C, RP, CG, PCR, PMT, B, T, ogr, ogrYN, ParamGA);
                    obj = null;
                }

                
                Close();


            }
            else MessageBox.Show(s, "Ошибка!");

        }///кнопка выполнить трассировку

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false) maskedTextBox1.Enabled = false;
            else maskedTextBox1.Enabled = true;
        }


        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }
        
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            //button3.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {




        }

        private void изФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        int[] ParamGA = new int[4];
        /// 0- 
        /// 1-отбор
        /// 2-кроссинговер
        /// 3-мутация



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton5_Click(object sender, EventArgs e)
        {





        }

        private void radioButton2_Click(object sender, EventArgs e)
        {



        }

        private void radioButton4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton7_Click(object sender, EventArgs e)
        {

        }

        private void radioButton6_Click(object sender, EventArgs e)
        {


        }


        public void proverka (ref bool z, ref string s)
        {

            

            try
            {
                int chislo = Convert.ToInt32(maskedTextBox2.Text);
            }
            catch
            {
                s += "\n(1) Неверный формат данных в поле - Число вертикальных линий";

                z = false;
            }

            if (z == true)
            {
                if (Convert.ToInt32(maskedTextBox2.Text) < 2)
                {
                    z = false;
                    s += "\n(2) Число вертикальных линий в канале должно быть не менее двух";
                }
            }




            if (checkBox1.Checked == true && z == true)
            {
                try
                {
                    int chislo = Convert.ToInt32(maskedTextBox1.Text);
                }
                catch
                {
                    s += "\n(3) Неверный формат данных в поле - Максимальное число магистралей ";

                    z = false;
                }
                if (z == true)
                {
                    if (Convert.ToInt32(maskedTextBox1.Text) < 1)
                    {
                        s += "\n(4) Число магистралей должно быть больше одного ";
                        z = false;
                    }

                }
            }




            try
            {
                int chislo = Convert.ToInt32(maskedTextBox4.Text);


            }
            catch
            {
                z = false;
                s += "\n(5) Неверный формат данных в поле - Количество электрических цепей";
            }



            if (z == true)
            {
                if (Convert.ToInt32(maskedTextBox4.Text) < 2)
                {
                    z = false;
                    s += "\n(10) Количество электрических цепей должно быть не менее двух";
                }
            }


            if (z == true)
            {

                for (int i = 0; i < (Convert.ToInt32(maskedTextBox2.Text)); i++)
                {
                    try
                    {
                        int chislo = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);//верх
                    }
                    catch
                    {
                        z = false;
                        s += "\n(11) Неверный формат данных в поле - B" + (i + 1);
                    }

                    try
                    {
                        int chislo = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);//низ
                    }
                    catch
                    {
                        z = false;
                        s += "\n(12) Неверный формат данных в поле - T" + (i + 1);
                    }
                }
            }




            if (z == true)
            {
                int n1 = Convert.ToInt32(maskedTextBox4.Text);//число цепей
                int C1 = Convert.ToInt32(maskedTextBox2.Text);//кол контактов в линейке
                int[] B1 = new int[C1];
                int[] T1 = new int[C1];

                for (int i = 0; i < C1; i++)
                {
                    //верх
                    if (dataGridView1.Rows[0].Cells[i].Value != null) T1[i] = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                    else T1[i] = 0;
                    //низ
                    if (dataGridView2.Rows[0].Cells[i].Value != null) B1[i] = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);
                    else B1[i] = 0;
                }



                for (int i = 0; i < C1; i++)
                {
                    if ((T1[i] > n1) || (T1[i] < 0))
                    {
                        z = false;
                        s += "\n(14) Неверное значение T" + (i + 1);
                    }

                    if ((B1[i] > n1) || (B1[i] < 0))
                    {
                        z = false;
                        s += "\n(14) Неверное значение B" + (i + 1);
                    }

                }



                if (z == true)
                {

                    int[,] VO1 = new int[n1 + 1, n1 + 1];
                    for (int i = 0; i < C1; i++)
                    {

                        if (T1[i] != 0 && B1[i] != 0)
                            if (T1[i] != B1[i])
                            {
                                VO1[T1[i], B1[i]] = 1;
                            }
                    }

                    for (int i = 1; i <= n1; i++)
                        for (int j = 1; j < n1; j++)
                            if (VO1[i, j] == 1 && VO1[j, i] == 1)
                            {
                                z = false;

                            }
                    if (z == false)
                        s += "\n\n(15) Для трассировки необходимо использовать метод Доглега!\n\n";
                    /* for (int i = 0; i < C1; i++)
                     {
                         if (T1[i] > 0 && B1[i] > 0)
                             if (T1[i] == B1[i]) z = false;
                     }*/
                }
                ///////////////////////////////////////
                int[] TB = new int[2 * C1];
                for (int i = 0; i < C1; i++)
                {
                    TB[i] = T1[i];
                    TB[i + C1] = B1[i];
                }
                int cout = 1;
                Array.Sort(TB);
                for (int i = 1; i < C1 * 2; i++)
                {
                    if (TB[i - 1] != TB[i]) cout++;
                }
                bool co = false;
                for (int i = 0; i < C1 * 2; i++)
                {
                    if (TB[i] == 0) co = true;
                }

                if (co == true) cout--;

                if (cout > n1)
                {
                    z = false;
                    s += "\n(16) Количество введенных электрических цепей больше заданного";
                }
                if (cout < n1)
                {
                    z = false;
                    s += "\n(17) Введены не все электрические цепи";
                }



            }


        }//////////////////////////////////////////////////////



        private void вФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {


            string s = "";
            bool z = true;

            proverka(ref z, ref s);


            if (z == true)
            {
                string f = "";
                f += maskedTextBox2.Text;
                f += "|";
                if (checkBox1.Checked == true) f += maskedTextBox1.Text;
                else f += "0";
                f += "|";
                f += maskedTextBox4.Text;
                f += "|";


                int C1 = Convert.ToInt32(maskedTextBox2.Text);//кол контактов в линейке
                int[] B1 = new int[C1];
                int[] T1 = new int[C1];

                for (int i = 0; i < C1; i++)
                {
                    //верх
                    if (dataGridView1.Rows[0].Cells[i].Value != null) T1[i] = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                    else T1[i] = 0;
                    //низ
                    if (dataGridView2.Rows[0].Cells[i].Value != null) B1[i] = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);
                    else B1[i] = 0;
                }


                for (int i = 0; i < C1; i++)
                {
                    f += Convert.ToString(T1[i]);
                    f += ";";
                }
                f += "|";

                for (int i = 0; i < C1; i++)
                {
                    f += Convert.ToString(B1[i]);
                    f += ";";
                }
                f += "|";



                using (SaveFileDialog dialog = new SaveFileDialog())// создание нового
                {


                    dialog.Filter = "Файлы - канал СБИС(*.ktrk)|*.ktrk";

                    if (dialog.ShowDialog() == DialogResult.Cancel)
                        return;
                    // получаем выбранный файл
                    string filename = dialog.FileName;
                    // сохраняем текст в файл
                    System.IO.File.WriteAllText(filename, f);
                    MessageBox.Show("Файл сохранен");

                }




            }
            else MessageBox.Show(s, "Ошибка!");

        }//////////////////////////////////////////////////////

        private void вБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string s = "";
            bool z = true;
            
            proverka(ref z, ref s);
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (z == true)
            {
                this.каналыTableAdapter1.Fill(this.kanalDataSet1.Каналы);
                this.перечни_электрических_соединенийTableAdapter1.Fill(this.kanalDataSet1.Перечни_электрических_соединений);
                this.сбисTableAdapter1.Fill(this.kanalDataSet1.СБИС);

                int j = this.kanalDataSet1.СБИС.Rows.Count;
                int nom = Convert.ToInt32(this.kanalDataSet1.СБИС.Rows[j - 1]["№ СБИС"]) + 1;
                
                this.kanalDataSet1.СБИС.Rows.Add(nom, nom, nom);


                int temp = 0;
                if (checkBox1.Checked == true) temp = Convert.ToInt32(maskedTextBox1.Text);
                else temp = 0;
                this.kanalDataSet1.Каналы.Rows.Add(nom, temp);
                //DataRow dRow = kanalDataSet1.Tables["СБИС"].NewRow();


                string f = "";
                

                int C1 = Convert.ToInt32(maskedTextBox2.Text);//кол контактов в линейке
                int[] B1 = new int[C1];
                int[] T1 = new int[C1];

                for (int i = 0; i < C1; i++)
                {
                    //верх
                    if (dataGridView1.Rows[0].Cells[i].Value != null) T1[i] = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                    else T1[i] = 0;
                    //низ
                    if (dataGridView2.Rows[0].Cells[i].Value != null) B1[i] = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);
                    else B1[i] = 0;
                }


                for (int i = 0; i < C1; i++)
                {
                    f += Convert.ToString(T1[i]);
                    f += ";";
                }
                

                string f1 = "";
                for (int i = 0; i < C1; i++)
                {
                    f1 += Convert.ToString(B1[i]);
                    f1 += ";";
                }

                this.kanalDataSet1.Перечни_электрических_соединений.Rows.Add(nom, f, f1, C1);


                this.каналыTableAdapter1.Update(this.kanalDataSet1.Каналы);
                this.перечни_электрических_соединенийTableAdapter1.Update(this.kanalDataSet1.Перечни_электрических_соединений);
                this.сбисTableAdapter1.Update(this.kanalDataSet1.СБИС);
                
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                MessageBox.Show("Сохранено в БД под маркировкой: " + nom);
                
            }
            else MessageBox.Show(s, "Ошибка!");

        }//////////////////////////////////////////////////////
    }
}
