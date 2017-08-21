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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            alg = Properties.Settings.Default.alg_trassir;
            if (alg == 1)
            {
                radioButton10.Checked = true;
                radioButton11.Checked = false;
            }
            if (alg == 2)
            {
                radioButton11.Checked = true;
                radioButton10.Checked = false;
            }





            //Properties.Settings.Default.
            textBox1.Text = Convert.ToString(Properties.Settings.Default.R_pop);
            textBox2.Text = Convert.ToString(Properties.Settings.Default.C_gen); ;
            textBox3.Text = Convert.ToString(Properties.Settings.Default.P_Crossing);
            textBox4.Text = Convert.ToString(Properties.Settings.Default.P_Mutac);
            //отбор
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton5.Checked = false;

            if (Properties.Settings.Default.Otbor == 1)
            {
                ParamGA[1] = 1;
                radioButton1.Checked = true;
            }
            if (Properties.Settings.Default.Otbor == 2)
            {
                ParamGA[1] = 2;
                radioButton2.Checked = true;
            }
            if (Properties.Settings.Default.Otbor == 3)
            {
                ParamGA[1] = 3;
                radioButton5.Checked = true;
            }



            //кросс
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            if (Properties.Settings.Default.Crossing == 1)
            {
                ParamGA[2] = 1;
                radioButton4.Checked = true;
            }
            if (Properties.Settings.Default.Crossing == 2)
            {
                ParamGA[2] = 2;
                radioButton3.Checked = true;
            }




            //мут
            radioButton6.Checked = false;
            radioButton7.Checked = false;

            if (Properties.Settings.Default.Mutac == 1)
            {
                ParamGA[3] = 1;
                radioButton7.Checked = true;
            }
            if (Properties.Settings.Default.Mutac == 2)
            {
                ParamGA[3] = 2;
                radioButton6.Checked = true;
            }



            /* Properties.Settings.Default.Otbor = ParamGA[1];
             Properties.Settings.Default.Crossing = ParamGA[2];
             Properties.Settings.Default.Mutac = ParamGA[3];*/




        }//////////////

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        int alg;
        int[] ParamGA = new int[4];
        /// 0- 
        /// 1-отбор
        /// 2-кроссинговер
        /// 3-мутация
        

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;

            radioButton2.Checked = false;
            radioButton5.Checked = false;


            ParamGA[1] = 1; //вкл панмиксию
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;

            radioButton1.Checked = false;
            radioButton5.Checked = false;

            ParamGA[1] = 2;//аутбридинг
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            radioButton5.Checked = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;


            ParamGA[1] = 3;//инбридинг
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            radioButton3.Checked = true;

            radioButton4.Checked = false;
            ParamGA[2] = 2;//унифиц кросс
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            radioButton4.Checked = true;

            radioButton3.Checked = false;
            ParamGA[2] = 1;//классич кросс
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            radioButton6.Checked = true;

            radioButton7.Checked = false;
            ParamGA[3] = 2;//многоточ мут
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            radioButton7.Checked = true;

            radioButton6.Checked = false;
            ParamGA[3] = 1;//одноточ мут
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool z = true;
            string s = "";

            try
            {
                int chislo = Convert.ToInt32(textBox1.Text);

            }
            catch
            {
                z = false;
                s += "\n(6) Неверный формат данных в поле - Размер популяции";
            }
            if (z == true)
            {
                if (Convert.ToInt32(textBox1.Text) < 1)
                {
                    z = false;
                    s += "\n(6) Размер популяции должен быть не менее одного";
                }
            }


            try
            {
                int chislo = Convert.ToInt32(textBox2.Text);

            }
            catch
            {
                z = false;
                s += "\n(7) Неверный формат данных в поле - Число генераций";
            }
            if (z == true)
            {
                if (Convert.ToInt32(textBox2.Text) < 1)
                {
                    z = false;
                    s += "\n(7) Число генераций должно быть не менее одного";
                }
            }


            try
            {
                int chislo = Convert.ToInt32(textBox3.Text);

            }
            catch
            {
                z = false;
                s += "\n(8) Неверный формат данных в поле - Вероятность кроссинговера";
            }




            try
            {
                int chislo = Convert.ToInt32(textBox4.Text);

            }
            catch
            {
                z = false;
                s += "\n(9) Неверный формат данных в поле - Вероятность мутации";
            }


            if (z == true)
                if (Convert.ToInt32(textBox3.Text) < 0 || Convert.ToInt32(textBox3.Text) > 100 || Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 100)
                {
                    z = false;
                    s += "(13) Вероятность должна находиться в пределах от 0% до 100%";
                }




            if (z == true)
            {
                Properties.Settings.Default.alg_trassir = alg;
                Properties.Settings.Default.Otbor = ParamGA[1];
                Properties.Settings.Default.Crossing = ParamGA[2];
                Properties.Settings.Default.Mutac = ParamGA[3];
                /// 1-отбор
                /// 2-кроссинговер
                /// 3-мутация
                /// 
                Properties.Settings.Default.R_pop = Convert.ToInt32(textBox1.Text);
                Properties.Settings.Default.C_gen = Convert.ToInt32(textBox2.Text);
                Properties.Settings.Default.P_Crossing = Convert.ToInt32(textBox3.Text);
                Properties.Settings.Default.P_Mutac = Convert.ToInt32(textBox4.Text);
                Properties.Settings.Default.Save();
                Close();
            }
            else MessageBox.Show(s, "Ошибка!");
        }//////////////проверка и сохранение

        private void radioButton10_Click(object sender, EventArgs e)
        {
            radioButton10.Checked = true;
            radioButton11.Checked = false;
            alg = 1;///рой
        }

        private void radioButton11_Click(object sender, EventArgs e)
        {
            radioButton11.Checked = true;
            radioButton10.Checked = false;
            alg = 2;//ГА
        }
    
    }
}

