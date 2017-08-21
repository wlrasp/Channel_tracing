using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Канальная_трассировка_СБИС
{
    class fOpen
    {
        public void fileopen()
        {

            using (System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog())// создание нового OpenFileDialog
            {


                dialog.Filter = "Файлы - канал СБИС(*.ktrk)|*.ktrk";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) // если выбираем файл и нажимаем кнопку ОК
                {

                    string filename = dialog.FileName;
                    string fileText = System.IO.File.ReadAllText(filename);

                    int lengthf = fileText.Length;


                    int j = 0;
                    string s = "";

                    string vl = "";
                    string mm = "";
                    string kc = "";



                    for (int i = 0; i < lengthf; i++)
                    {
                        if (fileText[i] == '|') j++;
                        if (j == 0) vl += fileText[i];

                    }

                    j = 0;

                    int[] B = new int[Convert.ToInt32(vl)];
                    int[] T = new int[Convert.ToInt32(vl)];
                    vl = "";
                    for (int i = 0; i < lengthf; i++)
                    {
                        if (fileText[i] == '|') { j++; i++; }
                        //////
                        if (j == 0) vl += fileText[i];
                        if (j == 1) mm += fileText[i];
                        if (j == 2) kc += fileText[i];

                        if (j == 3)
                        {
                            for (int ii = 0; ii < Convert.ToInt32(vl); ii++)
                            {
                                if (fileText[i] == ';') i++;
                                while (fileText[i] != ';')
                                {

                                    s += fileText[i];
                                    i++;
                                }

                                T[ii] = Convert.ToInt32(s); s = "";
                            }

                        }

                        if (j == 4)
                        {
                            for (int ii = 0; ii < Convert.ToInt32(vl); ii++)
                            {
                                if (fileText[i] == ';') i++;
                                while (fileText[i] != ';')
                                {

                                    s += fileText[i];
                                    i++;
                                }

                                B[ii] = Convert.ToInt32(s); s = "";
                            }
                        }






                    }



                    /* string a = "";
                     a += vl;
                     a += "\n";
                     a += mm;
                     a += "\n";
                     a += kc;
                     a += "\n";

                     for (int i = 0; i < Convert.ToInt32(vl); i++)
                     {
                         a += Convert.ToString(T[i]);
                         a += " ";
                     }
                     a += "\n";
                     for (int i = 0; i < Convert.ToInt32(vl); i++)
                     {
                         a += Convert.ToString(B[i]);
                         a += " ";
                     }
                     a += "\n";




                     MessageBox.Show(a);
                     */



                    Input ввод = new Input(1, vl, mm, kc, T, B);
                    ввод.Show();



                }
            }
            //////////////






        }
    }
}
