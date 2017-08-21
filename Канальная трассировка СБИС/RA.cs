using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Канальная_трассировка_СБИС
{
    class RA
    {

        public void ogran(int n, int C, int[] B, int[] T, int[,] RVO, int[,] HO, ref int[,] TB)
        {
            //n число цепей
            ///C число контактов



            ///////////////////////////////////////////////////
            //Граф вертикальных ограничений[начало,конец]
            int[,] VO = new int[n + 1, n + 1];
            for (int i = 0; i < C; i++)
            {
                if (T[i] > 0 && B[i] > 0)
                    if (T[i] != B[i])
                    {
                        VO[T[i], B[i]] = 1;
                    }
            }














            //////Получение РГВО[начало,конец]

            // RVO = new int[n + 1, n + 1];

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    RVO[i, j] = VO[i, j];


            ///
            //for (int i = 1; i <= n; i++)
            //for (int j = 1; j <= n; j++)
            //   if (RVO[i, j] == 1) RVO[j, i] = 200000000;

            //

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    if (RVO[i, j] < 1) RVO[i, j] = 200000000;


            for (int k = 1; k <= n; k++)
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                        if (RVO[i, j] > RVO[i, k] + RVO[k, j])
                            RVO[i, j] = RVO[i, k] + RVO[k, j];

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (RVO[i, j] >= 200000000) RVO[i, j] = 0;
                    if (RVO[i, j] > 1) RVO[i, j] = 1;
                }
            ///////////

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    if (VO[i, j] == 1)
                    {
                        RVO[i, j] = 1;
                        RVO[j, i] = 0;
                    }

            ////////////////////////
            //рассчитаем NRVO
            int NRVO = 0;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (RVO[i, j] == 1) NRVO++;
                }

            ///////////////////////////////////////
            //Граф горизонтальных ограничений
            // HO = new int[n + 1, n + 1];
            //определения линейного расположения гориз участков
            int[,] HOsegments = new int[2, n + 1];
            for (int j = 1; j <= n; j++)
            {
                int nach = 0;
                for (int i = 0; i < C; i++)
                {
                    if (T[i] == j || B[i] == j)
                    {
                        if (nach == 0)
                        {
                            HOsegments[0, j] = i; nach = 1;
                        }
                        else HOsegments[1, j] = i;
                    }
                }
            }
            //определение линейных пересечений гориз участков
            //за одно рассчитаем NHO
            int NHO = 0;
            for (int i = 1; i <= n; i++)
            {
                int k = i + 1;
                while (k <= n)
                {
                    if ((HOsegments[0, i] <= HOsegments[1, k]) && (HOsegments[1, i] >= HOsegments[0, k]))
                    {
                        HO[i, k] = 1;
                        HO[k, i] = 1;
                        NHO++;
                    }
                    k++;
                }
            }//


            TB = new int[2, n + 1];
            for (int j = 1; j <= n; j++)
            {
                for (int i = 0; i < C; i++)
                {
                    if (T[i] == j) TB[0, j]++;
                    if (B[i] == j) TB[1, j]++;
                }

            }

            for (int j = 1; j <= n; j++)
            {
                if (TB[1, j] > TB[0, j])
                {
                    for (int i = 1; i <= n; i++)
                    {
                        if (j != i && HO[i, j] == 1)
                        {
                            if (TB[1, i] == TB[0, i])
                            {
                                RVO[i, j] = 1;

                            }


                        }
                    }
                }

            }




            for (int j = 1; j <= n; j++)
            {
                if (TB[1, j] < TB[0, j])
                {
                    for (int i = 1; i <= n; i++)
                    {
                        if (j != i && HO[i, j] == 1)
                        {
                            if (TB[1, i] == TB[0, i])
                            {
                                RVO[j, i] = 1;

                            }


                        }
                    }
                }

            }







        }







        ///////////////////////////////////////////

        public void makeF(int[,] HO, int[,] F, int n, int[,] RVO1, ref int err)
        {
            int[,] RVO = new int[n + 1, n + 1];

            for (int q = 0; q <= n; q++)
                for (int w = 0; w <= n; w++)
                {
                    RVO[q, w] = RVO1[q, w];
                }




            int cikl = 0;
            int nf = 0;
            int i = 1;
            int razm = 0;
            while (razm < n)
            {



                bool h1 = false;
                for (int i2 = 0; i2 < n; i2++)
                    for (int j = 0; j < n; j++)
                    {
                        if (F[i2, j] == i) h1 = true;
                    }


                if (h1 == false)
                {
                    bool Ogr = false;
                    for (int u = 1; u <= n; u++)
                    {
                        if (RVO[u, i] == 1) Ogr = true;
                        if (Ogr == true) cikl++;
                    }

                    if (Ogr == false)
                    {
                        cikl = 0;

                        bool Ogran = false;
                        for (int r = 0; r < n; r++)///обход списка цепей в магистрали
                        {
                            if (F[nf, r] > 0)
                            {
                                ////проверим есть ли ограничения
                                if (F[nf, r] != i)
                                {
                                    if (HO[i, F[nf, r]] == 1) Ogran = true;
                                }
                            }
                        }
                        if (Ogran == true) nf++;
                        else
                        {
                            razm++;
                            int j = 0;
                            while (F[nf, j] > 0) j++;
                            F[nf, j] = i;
                            //nf = 0;
                            /////////

                            for (int u = 1; u <= n; u++) RVO[i, u] = 0;


                        }
                    }
                }
                if (i == n) i = 1;
                else i++;


                if (cikl > (n * n))
                {
                    razm = n + 1;
                    err = 1;
                }
            }



        }
        public void sdvig(int[,] D, int a, int kp)
        {
            /*  s = "";
             for (int i = 0; i < a; i++)
             {
                 for (int j = 0; j < k_p; j++)
                 {
                     s += Convert.ToString(D[i, j]) + " ";
                 }
                 s += "\n";
             }

             System.Windows.Forms.MessageBox.Show(s, "Ошибка");
             */


            for (int i = 0; i < kp; i++)//обход строки
            {
                bool sd = true;

                while (sd == true)
                {
                    for (int j = 0; j < a; j++)//обх столб
                    {
                        if (j > 0)
                        {
                            if (D[j, i] > 0 && D[j - 1, i] == 0)
                            {
                                D[j - 1, i] = D[j, i];
                                D[j, i] = 0;
                            }
                        }

                    }

                    bool sd1 = false;
                    for (int j = 0; j < a; j++)//обх столб
                    {
                        if (j > 0)
                        {
                            if (D[j, i] > 0 && D[j - 1, i] == 0) sd1 = true;
                        }

                    }
                    sd = sd1;
                }
            }



        }

        public void makeEskiz(ref int[,] Eskiz, int k_p, int a, int[,] D, int n, int[,] RVO, int[,] HO, ref int nomMagistrali, ref int cfk, int[] B, int[] T, int C)
        {
            int nomC = 1;

            for (int t = 0; t < k_p; t++)
                for (int g = 0; g < a; g++)
                    if (D[g, t] > 0)
                    {
                        nomC = D[g, t];

                        bool Ogran = false;
                        for (int r = 0; r < n; r++)///обход списка цепей в магистрали
                        {
                            if (Eskiz[nomMagistrali, r] > 0)
                            {
                                ////проверим есть ли ограничения
                                if (Eskiz[nomMagistrali, r] != nomC)
                                {
                                    if (RVO[Eskiz[nomMagistrali, r], nomC] == 1) Ogran = true;
                                    if (HO[nomC, Eskiz[nomMagistrali, r]] == 1) Ogran = true;
                                }
                            }
                        }

                        if (Ogran == true) nomMagistrali++;

                        int ieee = 0;
                        while (Eskiz[nomMagistrali, ieee] > 0)
                        {
                            ieee++;
                        }
                        Eskiz[nomMagistrali, ieee] = nomC;
                    }
            nomMagistrali++;

            // for (int t = 0; t < n; t++) if (Eskiz[t, 0] > 0) shirina_Kanala++;
            CF(Eskiz, n, nomMagistrali, B, T, C, ref cfk);

        }

        public void CF(int[,] Eskiz, int n, int shirina_Kanala, int[] B, int[] T, int C, ref int cfk)
        {

            int dvl = 0;
            for (int i = 0; i < n; i++)//номер текущей магистрали
                for (int j = 0; j < n; j++)
                {
                    if (Eskiz[i, j] > 0)
                    {
                        int dv = i + 1;
                        int dn = shirina_Kanala - i;
                        for (int z = 0; z < C; z++)
                        {
                            if (T[z] == Eskiz[i, j]) dvl = dvl + dv;
                            if (B[z] == Eskiz[i, j]) dvl = dvl + dn;
                        }
                    }
                }
            cfk = (shirina_Kanala * 10) * C + dvl;
        }

        //dlvl(Eskiz2, n, shirina_Kanala, B, T, C, ref e2);
        public void dlvl(int[,] Eskiz, int n, int shirina_Kanala, int[] B, int[] T, int C, ref int d)
        {

            int dvl = 0;
            for (int i = 0; i < n; i++)//номер текущей магистрали
                for (int j = 0; j < n; j++)
                {
                    if (Eskiz[i, j] > 0)
                    {
                        int dv = i + 1;
                        int dn = shirina_Kanala - i;
                        for (int z = 0; z < C; z++)
                        {
                            if (T[z] == Eskiz[i, j]) dvl = dvl + dv;
                            if (B[z] == Eskiz[i, j]) dvl = dvl + dn;
                        }
                    }
                }
            d = dvl;
        }

        public void CF2(ref int[,] Genes, int RP, int L, string[,] MHrom, string[,] Hromosoma, int dhr, int[,] RVO, int[,] HO, int[] B, int[] T, int n, int C, int[,] TB)
        {

            ///RP размер популяции
            for (int ng = 0; ng < RP; ng++) //номер хромосомы гена
            {

                int nomMagistrali = 0;
                int[,] Eskiz = new int[n, n];


                Eskiz_K(Genes, RP, L, MHrom, Hromosoma, dhr, ng, ref nomMagistrali, ref Eskiz, RVO, HO, n, C, TB);

                ///////////////////////////////////////////////////////////////////////////////////
                if (Genes[ng, L] != 2147483645)
                {

                    ////////////////////////////////////////////////////////////////////////////////  

                    ////Расчет качества = (ширина канала)*число контактов + длина вертикальный цепей
                    //нахожу длину вертикальных линий
                    int dvl = 0;
                    for (int i = 0; i < n; i++)//номер текущей магистрали
                        for (int j = 0; j < n; j++)
                        {
                            if (Eskiz[i, j] > 0)
                            {
                                int dv = i + 1;
                                int dn = nomMagistrali - i;
                                for (int z = 0; z < C; z++)
                                {
                                    if (T[z] == Eskiz[i, j]) dvl = dvl + dv;
                                    if (B[z] == Eskiz[i, j]) dvl = dvl + dn;
                                }
                            }
                        }
                    Genes[ng, L] = (nomMagistrali * 10) * C + dvl;
                }

            }

        } ////// CF

        ///////////////////////////////////////////
        public void Eskiz_K(int[,] Genes, int RP, int L, string[,] MHrom, string[,] Hromosoma, int dhr, int bestHr, ref int shirina_Kanala, ref int[,] Eskiz, int[,] RVO, int[,] HO, int n, int C, int[,] TB)
        {


            ///RP размер популяции
            // int n = Convert.ToInt32(maskedTextBox4.Text);//число цепей
            // int C = Convert.ToInt32(maskedTextBox2.Text);//кол контактов в линейке
            /////Нужна целевая функция оценки хромосомы = (ширина канала+2)*число контактов + длина вертикальный цепей
            //строю граф топологии


            //for (int ng = 0; ng < RP; ng++) //номер хромосомы гена
            //{

            int ng = bestHr;
            ////MessageBox.Show("1 L=" + L, "О программе");
            int[,] GTopology = new int[n + 1, n + 1];
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    GTopology[i, j] = 0;

            /////string[,] Hromosoma = new string[3, dhr];
            //из большой хромосомы
            for (int i = 0; i < dhr; i++)
            {
                if (Hromosoma[2, i] != null)
                    if (Hromosoma[2, i] != "*")
                    {
                        if (Convert.ToInt32(Hromosoma[2, i]) == 0) GTopology[Convert.ToInt32(Hromosoma[0, i]), Convert.ToInt32(Hromosoma[1, i])] = 1;
                        if (Convert.ToInt32(Hromosoma[2, i]) == 1) GTopology[Convert.ToInt32(Hromosoma[1, i]), Convert.ToInt32(Hromosoma[0, i])] = 1;
                    }
            }
            /// MessageBox.Show("2", "О программе");
            ////добавим из генерир хромосомы
            for (int il = 0; il < L; il++)// обход цепей
            {
                if (Genes[ng, il] == 0) GTopology[Convert.ToInt32(MHrom[0, il]), Convert.ToInt32(MHrom[1, il])] = 1;
                if (Genes[ng, il] == 1) GTopology[Convert.ToInt32(MHrom[1, il]), Convert.ToInt32(MHrom[0, il])] = 1;
            }



            ////////////
            ////////////
            //добавим дуги если есть пути
            //for (int i = 1; i <= n; i++)
            // for (int j = 1; j <= n; j++)
            //  if (GTopology[i, j] == 1) GTopology[j, i] = 20000000;


            /*/ int[,] GTopo = new int[n + 1, n + 1];
             for (int i = 1; i <= n; i++)
                 for (int j = 1; j <= n; j++)
                     GTopo[i, j] = GTopology[i, j];

             */



            //добавим дуги если есть пути
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    if (GTopology[i, j] < 1) GTopology[i, j] = 20000000;
            for (int k = 1; k <= n; k++)
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                        if (GTopology[i, j] > GTopology[i, k] + GTopology[k, j])
                            GTopology[i, j] = GTopology[i, k] + GTopology[k, j];
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (GTopology[i, j] >= 20000000) GTopology[i, j] = 0;
                    if (GTopology[i, j] > 1) GTopology[i, j] = 1;
                }
            /////////// 
            /*



                        for (int i = 1; i <= n; i++)
                            for (int j = 1; j <= n; j++)
                                if (GTopo[i, j] == 1)
                                {
                                    GTopology[i, j] = 1;
                                    GTopology[j, i] = 0;
                                }



                        */ //MessageBox.Show("3", "О программе");
                           //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                           //восстановл из гт эскиза
                           //int[,] Eskiz = new int[n, n]; //номер вершины [магистраль, список номеров цепей]

            int nomMagistrali = 0;
            //ищем вершины у которых только исходящие дуги
            int udalenoVersin = 0;
            //for (int nomC=1; nomC< n; nomC++)
            int nomC = 1;
            //int p = 0;
            //MessageBox.Show("4", "О программе");
            int cikl = 0;
            int[] UDV = new int[n + 1];
            while (udalenoVersin < n)//удалено вершин
            {
                ///проверям есть ли ребра
                int provpust = 0;///количество ребер в графе топологии
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                    {
                        if (GTopology[i, j] == 1) provpust++;
                    }


                bool h1 = false;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (Eskiz[i, j] == nomC) h1 = true;
                    }
                if (h1 == false)
                {

                    //MessageBox.Show("41 provpust=" + provpust, "О программе");/////////////////////////////////////////////////////////////////////
                    if (provpust > 0)/////проверка ребра вершины
                    {
                        int vhod = 0;
                        int ishod = 0;
                        for (int i = 1; i <= n; i++)
                        {
                            if (GTopology[i, nomC] == 1) vhod++;
                            if (GTopology[nomC, i] == 1) ishod++;
                        }

                        int ogrRVO = 0;

                        for (int i = 1; i <= n; i++)
                        {
                            if (UDV[i] == 0)
                                if (RVO[i, nomC] == 1)
                                    ogrRVO = 1;

                        }

                        //MessageBox.Show("42", "О программе");//////////////////////////////////////////////////////////////////////////
                        if ((vhod == 0 && ishod > 0) && ogrRVO == 0)// || cikl > 100000)//заносим вершину в эскиз и удаляем из графа топологии
                        {
                            for (int i = 1; i <= n; i++)
                            {
                                GTopology[nomC, i] = 0;
                                ///GTopology[i, nomC] = 0;

                            }
                            udalenoVersin++;////вершина удалена и ГТ
                            UDV[nomC] = 1;

                            ////занесем вершину в эскиз
                            ///////////////////////////////////////////////////////////////////////////////////
                            ////можно ли остаться на этой магистрали
                            nomMagistrali = 0;
                            for (int iy = 1; iy <= n; iy++)
                            {
                                if (UDV[iy] == 1)
                                    if (RVO[iy, nomC] == 1)
                                    {
                                        for (int i9 = 0; i9 < n; i9++)
                                            for (int j9 = 0; j9 < n; j9++)
                                            {
                                                if (Eskiz[i9, j9] == iy) { nomMagistrali = i9 + 1; i9 = n; j9 = n; }
                                            }

                                    }


                            }
                            bool Ogran = true;
                            while (Ogran == true)
                            {

                                Ogran = false;
                                for (int r = 0; r < n; r++)///обход списка цепей в магистрали
                                {
                                    if (Eskiz[nomMagistrali, r] > 0)
                                    {
                                        ////проверим есть ли ограничения
                                        if (Eskiz[nomMagistrali, r] != nomC)
                                        {


                                            if (RVO[Eskiz[nomMagistrali, r], nomC] == 1) Ogran = true;
                                            if (HO[nomC, Eskiz[nomMagistrali, r]] == 1) Ogran = true;
                                        }

                                    }
                                }

                                if (Ogran == true) nomMagistrali++;
                                else
                                {
                                    int ieee = 0;
                                    while (Eskiz[nomMagistrali, ieee] > 0)
                                    {
                                        ieee++;
                                    }
                                    Eskiz[nomMagistrali, ieee] = nomC;
                                    Ogran = false;
                                }
                            }

                        }
                        //////////////////////////////////////////////////////////////////////////////////
                        else cikl++;
                    }

                    else
                    {

                        //if (p == 0)
                        //{
                        //    p++;
                        //}



                        bool h = false;
                        for (int i = 0; i < n; i++)
                            for (int j = 0; j < n; j++)
                            {
                                if (Eskiz[i, j] == nomC) h = true;
                            }
                        if (h == false)
                        {
                            for (int i = 0; i < n; i++)
                                if (Eskiz[i, 0] > 0) nomMagistrali = i;


                            //вершины нет в эскизе, добавим её
                            udalenoVersin++;////вершина удалена и ГТ
                            bool Ogran = false;

                            for (int r = 0; r < n; r++)///обход списка цепей в магистрали
                            {
                                if (Eskiz[nomMagistrali, r] > 0)
                                {
                                    ////проверим есть ли ограничения
                                    if (RVO[Eskiz[nomMagistrali, r], nomC] == 1) Ogran = true;
                                    if (HO[nomC, Eskiz[nomMagistrali, r]] == 1) Ogran = true;
                                }
                            }
                            if (Ogran == true) nomMagistrali++;

                            for (int i = 0; i < n; i++)
                            {
                                if (Eskiz[nomMagistrali, i] < 1)
                                {
                                    Eskiz[nomMagistrali, i] = nomC;
                                    i = n;
                                }

                            }


                        }
                    }
                }
                ///
                if (nomC == n) nomC = 1;
                else nomC++;

                if (cikl > 3 * n)
                {
                    Genes[ng, L] = 2147483645;
                    udalenoVersin = n + 1;
                }
            }



            ////ширина канала
            shirina_Kanala = (nomMagistrali + 1);

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        public void optimizeN(int n, int[,] RVO, int[,] HO, ref int[,] Eskiz_OPT, int C, int[] B, int[] T, ref int shirina_Kanala, int[,] TB)
        {
            ///makeEskiz(ref Eskiz, k_p, a, D, n, RVO, HO, ref shirina_Kanala, ref cfk, B, T, C);

            int L = 0;
            int CG = Properties.Settings.Default.C_gen;



            //создадим хромосому по ограничениям 
            //Найти цепи расположение которых можно менять, чтобы включить в хромосому
            int dhr = 0;//длина хромосомы
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    dhr++;
                }

            string[,] Hromosoma = new string[3, dhr];
            //заполним хромосому номерами цепей

            int khr = 0;
            for (int i = 1; i <= n; i++)
                for (int j = i + 1; j <= n; j++)
                {
                    Hromosoma[0, khr] = Convert.ToString(i);
                    Hromosoma[1, khr] = Convert.ToString(j);
                    khr++;
                }
            /////добавим значения из ГГО
            for (int i = 0; i < dhr; i++)
            {
                if ((HO[(Convert.ToInt32(Hromosoma[0, i])), (Convert.ToInt32(Hromosoma[1, i]))]) == 0)
                {
                    Hromosoma[2, i] = "*";
                }
            }

            //добавим значения из РГВО
            for (int i = 0; i < dhr; i++)
            {
                if ((RVO[(Convert.ToInt32(Hromosoma[0, i])), (Convert.ToInt32(Hromosoma[1, i]))]) == 1)
                {
                    Hromosoma[2, i] = "0";
                }
                if ((RVO[(Convert.ToInt32(Hromosoma[1, i])), (Convert.ToInt32(Hromosoma[0, i]))]) == 1)
                {
                    Hromosoma[2, i] = "1";
                }
            }



            ////////////////////////////////////////////////
            //Уменьшаем длину хромосомы
            //int L = 0;//определяем минимальную длину
            for (int i = 0; i < dhr; i++)
                if (Hromosoma[2, i] == null) L++;
            //
            //создадим место для популяции и потомков
            //цепи хромосомы
            string[,] MHrom = new string[2, L];
            for (int i = 0, j = 0; i < L; i++)
            {
                while (Hromosoma[2, j] != null) j++;

                MHrom[0, i] = Hromosoma[0, j];
                MHrom[1, i] = Hromosoma[1, j];
                j++;

            }

            //////будем пытаться менять местами цепи из МHrom
            // CG раз
            //Eskiz_OPT
            

            int[,] Genes = new int[1, (L + 1)];
            ///заполним ген топологией первого эскиза

            //1
            //4
            //0//1 выше 4


            for (int i = 0; i < L; i++)
            {
                int v1 = 0;
                int v2 = 0;

                for (int i1 = 0; i1 < n; i1++)
                {
                    for (int j1 = 0; j1 < n; j1++)
                    {
                        if (Eskiz_OPT[i1, j1] == Convert.ToInt32(MHrom[0, i])) { v1 = i1; }
                    }
                }
                for (int i1 = 0; i1 < n; i1++)
                {
                    for (int j1 = 0; j1 < n; j1++)
                    {
                        if (Eskiz_OPT[i1, j1] == Convert.ToInt32(MHrom[1, i])) { v2 = i1; }
                    }
                }

                if (v1 < v2) Genes[0, i] = 0;
                else Genes[0, i] = 1;
            }



            CF2(ref Genes, 1, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C, TB);
            //System.Windows.Forms.MessageBox.Show("1", "Ошибка");
            // System.Windows.Forms.MessageBox.Show(Convert.ToString(Genes[0, L]), " g1");

            if (L > 1)
                for (int g = 0; g < CG * n; g++)
            {
                //int[,] Eskiz = new int[n, n];

                int[,] Genes2 = new int[1, (L + 1)];
                for (int i = 0; i < L; i++)
                {
                    Genes2[0, i] = Genes[0, i];
                }

                /////////////////
                // int Pm = 0;
                // int P_MT = 100;

                //Random rand44 = new Random();
                // Pm = rand44.Next(0, 100);
                // if (Pm <= P_MT)
                // {
                int ktm = 1;
                Random ran56 = new Random();
                
                ktm = ran56.Next(1, L);

                for (int i = 1; i < ktm; i++)
                {
                    int T_m = 0;
                    Random rand = new Random();
                    T_m = rand.Next(0, L);
                    if (Genes2[0, T_m] == 1) Genes2[0, T_m] = 0;
                    else Genes2[0, T_m] = 1;
                }
                // }

                CF2(ref Genes2, 1, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C, TB);
                // System.Windows.Forms.MessageBox.Show(Convert.ToString(Genes2[0, L]), "Ошибка");
                if (Genes2[0, L] < Genes[0, L])
                    for (int i = 0; i <= L; i++)
                    {
                        Genes[0, i] = Genes2[0, i];
                    }



                ////////////////////////
            }


            for (int i1 = 0; i1 < n; i1++)
            {
                for (int j1 = 0; j1 < n; j1++)
                {
                    Eskiz_OPT[i1, j1] = 0;
                }
            }

            shirina_Kanala = 0;
            Eskiz_K(Genes, 1, L, MHrom, Hromosoma, dhr, 0, ref shirina_Kanala, ref Eskiz_OPT, RVO, HO, n, C, TB);
            //Eskiz_K(int[,] Genes, int RP, int L, string[,] MHrom, string[,] Hromosoma, int dhr, int bestHr, ref int shirina_Kanala, int[,] Eskiz, int[,] RVO, int[,] HO, int n, int C)


            /*
            for (int i1 = 0; i1 < n; i1++)
            {
                for (int j1 = 0; j1 < n; j1++)
                {
                    Eskiz_OPT[i1, j1] = 0;
                }
            }


            Eskiz_K(Genes, 1, L, MHrom, Hromosoma, dhr, 0, ref shirina_Kanala, Eskiz, RVO, HO, n, C);

            if (Genes[0, L)<cfk)
            */

        }

        public void Alg(int n, int C, int[] B, int[] T, int maxmag)
        {
            int[,] RVO = new int[n + 1, n + 1];
            int[,] HO = new int[n + 1, n + 1];
            int[,] TB = new int[2, n + 1];
            ogran(n, C, B, T, RVO, HO, ref TB);
            //System.Windows.Forms.MessageBox.Show("1", "Ошибка");
            int[,] F = new int[n, n];



            int err = 0;

            makeF(HO, F, n, RVO, ref err);

            if (err == 1) System.Windows.Forms.MessageBox.Show("Не удалось выполнить трассировку, необходим метод Доглега", "Ошибка");
            else

            {


                int k_p = 0;
                int a = 0;
                for (int i = 0; i < n; i++)
                {
                    if (F[i, 0] > 0) k_p++;
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (F[i, j] > 0) if (a < j + 1) a = j + 1;
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////
                int[,] D = new int[a, k_p];
                Random rand = new Random();

                for (int i = 0; i < k_p; i++)
                {
                    for (int j = 0; j < a; j++)
                    {
                        if (F[i, j] > 0)
                        {

                            int y = rand.Next(0, a);
                            while (D[y, i] > 0) y = rand.Next(0, a);

                            D[y, i] = F[i, j];
                        }
                    }
                }

                sdvig(D, a, k_p);

                int shirina_Kanala = 0;
                int[,] Eskiz = new int[n, n];
                int cfk = 0;
                makeEskiz(ref Eskiz, k_p, a, D, n, RVO, HO, ref shirina_Kanala, ref cfk, B, T, C);

                if (cfk >= 2147483645) System.Windows.Forms.MessageBox.Show("Не удалось выполнить трассировку, необходим метод Доглега", "Ошибка");
                else
                {

                    shirina_Kanala = 0;
                    optimizeN(n, RVO, HO, ref Eskiz, C, B, T, ref shirina_Kanala, TB);



                    if (maxmag > 0)
                    {
                        if (shirina_Kanala > maxmag)
                        {
                            System.Windows.Forms.MessageBox.Show("Слишком узкий канал! Для трассировки требуется больше магистралей!", "Ошибка");

                        }
                        else
                        {
                            if (Main.SelfRef != null)
                            {
                                Main.SelfRef.Paintt(T, B, n, C, shirina_Kanala, Eskiz, 1);
                            }
                        }
                    }
                    else
                    {
                        if (Main.SelfRef != null)
                        {
                            Main.SelfRef.Paintt(T, B, n, C, shirina_Kanala, Eskiz, 1);
                        }
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////

    }

}
