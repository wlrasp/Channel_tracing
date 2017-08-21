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
    public partial class LoadBD : Form
    {
        public LoadBD()
        {
            
            InitializeComponent();
            this.каналыTableAdapter1.Fill(this.kanalDataSet1.Каналы);
            this.перечни_электрических_соединенийTableAdapter1.Fill(this.kanalDataSet1.Перечни_электрических_соединений);
            this.сбисTableAdapter1.Fill(this.kanalDataSet1.СБИС);
            //this.экспонатыTableAdapter.Update(this.bazaDataSet); this.tableAdapterManager.UpdateAll(this.bazaDataSet);
        }

        private void button3_Click(object sender, EventArgs e)//////////////
        {

            int ns = Convert.ToInt32(comboBox1.Text);//сбис
            int nk = 0;
            int npes = 0;

            int i = 0;
            int j = this.kanalDataSet1.СБИС.Rows.Count;

            for (i = 0; i < j; i++)
            {
                if (Convert.ToInt32(this.kanalDataSet1.СБИС.Rows[i]["№ СБИС"]) == ns)
                {
                    nk = Convert.ToInt32(this.kanalDataSet1.СБИС.Rows[i]["№ канала"]);
                    npes = Convert.ToInt32(this.kanalDataSet1.СБИС.Rows[i]["№ перечня электрических соединений"]);
                    i = j;
                }
            }

            string vl = "";
            string mm = "0";
            string kc = "";
            int[] B = new int[1];
            int[] T = new int[1];



            j = this.kanalDataSet1.Каналы.Rows.Count;

            for (i = 0; i < j; i++)
            {
                if (Convert.ToInt32(this.kanalDataSet1.Каналы.Rows[i]["№ канала"]) == nk)
                {
                    mm = Convert.ToString(this.kanalDataSet1.Каналы.Rows[i]["Максимальная ширина канала"]);
                }

            }

            j = this.kanalDataSet1.Перечни_электрических_соединений.Rows.Count;
            for (i = 0; i < j; i++)
            {
                if (Convert.ToInt32(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["№ перечня электрических соединений"]) == npes)
                {
                    vl = Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Количество контактов в блоке"]);
                    B = new int[Convert.ToInt32(vl)];
                    T = new int[Convert.ToInt32(vl)];

                    string s = "";
                    //int l = 0;
                    int l = 0;
                    for (int ii = 0; ii < Convert.ToInt32(vl); ii++)
                    {
                        if (Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Выводы T"])[l] == ';') l++;
                        while (Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Выводы T"])[l] != ';')
                        {

                            s += Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Выводы T"])[l];
                            l++;
                        }

                        T[ii] = Convert.ToInt32(s); s = "";
                    }

                    s = "";
                    l = 0;
                    for (int ii = 0; ii < Convert.ToInt32(vl); ii++)
                    {
                        if (Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Выводы B"])[l] == ';') l++;
                        while (Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Выводы B"])[l] != ';')
                        {

                            s += Convert.ToString(this.kanalDataSet1.Перечни_электрических_соединений.Rows[i]["Выводы B"])[l];
                            l++;
                        }

                        B[ii] = Convert.ToInt32(s); s = "";
                    }
                }
            }



            int[] mas = new int[2 * (Convert.ToInt32(vl))];
            int o = 0;
            for (int y = 0; y < Convert.ToInt32(vl); y++)
            {
                if (T[y] == 0 || B[y] == 0) o = 1;
                mas[y] = T[y];
                mas[y + Convert.ToInt32(vl)] = B[y];
            }
            int x = mas.Distinct().Count();
            if (o == 1) x--;
            kc = Convert.ToString(x);

            Hide();
            Input ввод = new Input(1, vl, mm, kc, T, B);
            ввод.Show();
            Close();

        }

        private void LoadBD_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kanalDataSet.СБИС"
            this.сБИСTableAdapter.Fill(this.kanalDataSet.СБИС);

        }
    }
}
