using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Канальная_трассировка_СБИС
{
    class GA
    {
        public int[] ParamGA = new int[4];
        /// 0- 
        /// 1-отбор
        /// 2-кроссинговер
        /// 3-мутация

        public void genetic_alg(int n, int C, int RP, int CG, int PCR, int PMT, int[] B, int[] T, int ogr, bool ogrYN, int[] ParamGA1)
        {
            int[,] E = new int[1, 1];
            if (Main.SelfRef != null)
            {
                Main.SelfRef.Paintt(T, B, 0, 0, 0, E, 1);
            }


            for (int i = 0; i < 4; i++)
            {
                ParamGA[i] = ParamGA1[i];
            }



            int[,] RVO;
            int[,] HO;

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

            RVO = new int[n + 1, n + 1];

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    RVO[i, j] = VO[i, j];


            ///
            //for (int i = 1; i <= n; i++)
            //for (int j = 1; j <= n; j++)
            //   if (RVO[i, j] == 1) RVO[j, i] = 200000000;

            ///

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
            ////////////

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    if (VO[i, j] == 1)
                    {
                        RVO[i, j] = 1;
                        RVO[j, i] = 0;
                    }

            /////////////////////////
            //рассчитаем NRVO
            int NRVO = 0;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (RVO[i, j] == 1) NRVO++;
                }

            ///////////////////////////////////////
            //Граф горизонтальных ограничений
            HO = new int[n + 1, n + 1];
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
            }

            ////////////////////////////////////////////////////////////////

            //int L = NHO - NRVO;//колво генов в хромосоме популяции


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
            int L = 0;//определяем минимальную длину
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



            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //формирование начальной популяции

            Random rand = new Random();
            int[,] Genes = new int[RP, (L + 1)];//для хранения генов и критерия качества
                                                ////случайно заполняем начальную популяцию размером RP

            int[,] Genes1 = new int[1, (L + 1)];


            for (int i = 0; i < RP; i++)
            {

                for (int j = 0; j < L; j++)
                {
                    Genes[i, j] = rand.Next(0, 2);
                    Genes1[0, j] = Genes[i, j];

                }

                CF(Genes1, 1, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C);



                if (Genes1[0, L] >= 2147483645)
                {
                    int e = 0;
                    while (e < RP)
                    {
                        for (int j = 0; j < L; j++)
                        {
                            Genes[i, j] = rand.Next(0, 2);
                            Genes1[0, j] = Genes[i, j];
                        }
                        CF(Genes1, 1, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C);
                        if (Genes1[0, L] < 2147483645) e = RP;
                        e++;
                    }

                }


                Genes[i, L] = Genes1[0, L];

            }




            ///оценим популяцию
            // CF(Genes, RP, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///int CG = Convert.ToInt32(textBox2.Text);//число генераций
            /// 
            //int[] CF_grafik = new int[CG + 1];


            int bestHr = 0;

            for (int i = 0; i < RP; i++)
            {
                if (Genes[i, L] < Genes[bestHr, L])
                    bestHr = i;
            }
            // CF_grafik[0] = Genes[bestHr, L];

            // int[,] CF_tabl = new int[RP, CG + 1];
            // for (int i = 0; i < RP; i++)
            // {
            //   CF_tabl[i, 0] = Genes[i, L];
            // }

            //////////////////////////////////////////////////////////////////////////////////////////

            for (int i = 0; i < CG; i++)
            {
                CrossM(Genes, RP, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C, PCR, PMT);


                //  for (int j = 0; j < RP; j++)
                // {
                //     CF_tabl[j, i + 1] = Genes[j, L];
                //  }



                int bestHrg = 0;
                for (int j = 0; j < RP; j++)
                {
                    if (Genes[j, L] < Genes[bestHrg, L])
                        bestHrg = j;
                }
                //CF_grafik[i + 1] = Genes[bestHrg, L];
            }



            ///выберу лучшую хромосому
            bestHr = 0;

            for (int i = 0; i < RP; i++)
            {
                if (Genes[i, L] < Genes[bestHr, L])
                    bestHr = i;
            }



            int shirina_Kanala = 0;
            int[,] Eskiz_rezult = new int[n, n];


            Eskiz_K(Genes, RP, L, MHrom, Hromosoma, dhr, bestHr, ref shirina_Kanala, Eskiz_rezult, RVO, HO, n, C);


            /*int n = Convert.ToInt32(maskedTextBox4.Text);//число цепей
            int C = Convert.ToInt32(maskedTextBox2.Text);//кол контактов в линейке
            B = new int[C];
            T = new int[C];*/
            if (Genes[bestHr, L] < 2147483645)
            {
                if (ogrYN == true)
                {
                    if (shirina_Kanala > ogr)
                    {
                        System.Windows.Forms.MessageBox.Show("Слишком узкий канал! Для трассировки требуется больше магистралей!", "Ошибка");

                    }
                    else
                    {
                        if (Main.SelfRef != null)
                        {
                            Main.SelfRef.Paintt(T, B, n, C, shirina_Kanala, Eskiz_rezult, 1);
                        }

                    }
                }
                else
                {
                    if (Main.SelfRef != null)
                    {
                        Main.SelfRef.Paintt(T, B, n, C, shirina_Kanala, Eskiz_rezult, 1);
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Не удалось выполнить трассировку, необходим метод Доглега", "Ошибка");
            }
        }

        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void CrossM(int[,] Genes, int RP, int L, string[,] MHrom, string[,] Hromosoma, int dhr, int[,] RVO, int[,] HO, int[] B, int[] T, int n, int C, int P_CR, int P_MT)
        {
            if (L >= 1)
            {
                int l_cf = 0;
                int Kol_pot = 0;
                //int P_CR = Convert.ToInt32(textBox3.Text);//вер кроссинговера %
                //int P_MT = Convert.ToInt32(textBox4.Text);//вер мутации %  
                int[,] GenesPotomki = new int[RP, (L + 1)];//для хранения потомков и критерия качества
                Random rand = new Random();


                int luch_h = 0;///номер лучшей хр
                //ищу лучшую хромосому
                for (int i = 0; i < RP; i++)
                {
                    if (Genes[i, L] < Genes[luch_h, L]) luch_h = i;
                }


                //// MessageBox.Show("ляляляляляляля", "О программе");
                while (Kol_pot < RP)
                {
                    int cr2 = 0;
                    int cr1 = 0;
                    Random rand3 = new Random();
                    cr1 = rand3.Next(0, RP);//первый родитель

                    //панмиксия
                    if (ParamGA[1] == 1) cr2 = rand3.Next(0, RP);//второй родитель






                    if (ParamGA[1] == 2 || ParamGA[1] == 3)
                    {/////найдем отличия хромосом со случайной
                        int[] Otlich = new int[RP];

                        for (int i = 0; i < RP; i++)
                        {
                            for (int j = 0; j < L; j++)
                            {
                                if (Genes[i, j] != Genes[cr1, j]) Otlich[i]++;
                            }
                        }



                        //аутбридинг далекие
                        if (ParamGA[1] == 2)
                        {
                            cr2 = 0;

                            for (int i = 0; i < RP; i++)
                            {
                                if (Otlich[i] > Otlich[cr2]) cr2 = i;
                            }

                        }

                        //имбридинг близкие
                        if (ParamGA[1] == 3)
                        {
                            cr2 = 0;

                            for (int i = 0; i < RP; i++)
                            {
                                if (Otlich[i] > 0)
                                    if (Otlich[i] < Otlich[cr2]) cr2 = i;
                            }
                        }



                    }




                    int t_cr = 0;
                    Random rand4 = new Random();
                    t_cr = rand4.Next(0, L);
                    int P = 0;
                    P = rand.Next(0, 100);///генерируем вероятность кроссинговера
                    if (P <= P_CR)
                    {

                        if (ParamGA[2] == 1)//классический одноточечный кросс
                        {

                            int P1 = 0;
                            P1 = rand.Next(0, 100);

                            if (P1 < 50)
                            {
                                ///кросс
                                for (int i = 0; i < t_cr; i++)
                                {
                                    GenesPotomki[Kol_pot, i] = Genes[cr1, i];//luch_h
                                }
                                for (int i = t_cr; i < L; i++)
                                {
                                    GenesPotomki[Kol_pot, i] = Genes[cr2, i];
                                }
                            }
                            else
                            {
                                ///кросс
                                for (int i = 0; i < t_cr; i++)
                                {
                                    GenesPotomki[Kol_pot, i] = Genes[cr2, i];
                                }
                                for (int i = t_cr; i < L; i++)
                                {
                                    GenesPotomki[Kol_pot, i] = Genes[cr1, i];//luch_h
                                }

                            }

                        }

                        if (ParamGA[2] == 2)//унифицированный однородный
                        {
                            for (int i = 0; i < L; i++)
                            {
                                int P_un = 0;
                                P_un = rand.Next(0, 100);

                                if (P_un < 50)
                                    GenesPotomki[Kol_pot, i] = Genes[cr2, i];

                                else
                                {
                                    GenesPotomki[Kol_pot, i] = Genes[cr1, i];
                                }

                            }

                        }



                        Kol_pot++;

                    }
                }//while

                Kol_pot = 0;
                while (Kol_pot < RP)
                {
                    //ParamGA[3] = 1;//одноточ мут
                    //ParamGA[3] = 2;//многоточ мут


                    if (ParamGA[3] == 1)//одноточ мут
                    {
                        //мут
                        int Pm = 0;
                        Random rand5 = new Random();
                        Pm = rand5.Next(0, 100);
                        if (Pm <= P_MT)
                        {
                            int T_m = 0;
                            T_m = rand.Next(0, L);
                            if (GenesPotomki[Kol_pot, T_m] == 1) GenesPotomki[Kol_pot, T_m] = 0;
                            else GenesPotomki[Kol_pot, T_m] = 1;
                        }

                        if (Kol_pot != luch_h)
                        {

                            Random rand44 = new Random();
                            Pm = rand44.Next(0, 100);
                            if (Pm <= P_MT)
                            {
                                int T_m = 0;
                                T_m = rand.Next(0, L);
                                if (Genes[Kol_pot, T_m] == 1) Genes[Kol_pot, T_m] = 0;
                                else Genes[Kol_pot, T_m] = 1;
                            }
                        }
                    }

                    if (ParamGA[3] == 2)//многоточ мут
                    {
                        int Pm = 0;

                        Random rand44 = new Random();
                        Pm = rand44.Next(0, 100);
                        if (Pm <= P_MT)
                        {
                            int ktm = 1;
                            Random ran56 = new Random();
                            ktm = ran56.Next(1, L);

                            for (int i = 1; i < ktm; i++)
                            {
                                int T_m = 0;
                                T_m = rand.Next(0, L);
                                if (GenesPotomki[Kol_pot, T_m] == 1) GenesPotomki[Kol_pot, T_m] = 0;
                                else GenesPotomki[Kol_pot, T_m] = 1;


                            }
                        }


                        if (Kol_pot != luch_h)
                        {
                            Random rand449 = new Random();
                            Pm = rand449.Next(0, 100);
                            if (Pm <= P_MT)
                            {
                                int ktm = 1;
                                Random ran56 = new Random();
                                ktm = ran56.Next(1, L);

                                for (int i = 1; i < ktm; i++)
                                {
                                    int T_m = 0;
                                    T_m = rand.Next(0, L);
                                    if (Genes[Kol_pot, T_m] == 1) Genes[Kol_pot, T_m] = 0;
                                    else Genes[Kol_pot, T_m] = 1;


                                }
                            }
                        }

                    }






                    Kol_pot++;
                }



                // MessageBox.Show("лял", "О программе");
                ///оценим популяцию потомков
                //MessageBox.Show("хочу оценить", "О программе");





                CF(GenesPotomki, RP, L, MHrom, Hromosoma, dhr, RVO, HO, B, T, n, C);
                //MessageBox.Show("оценил", "О программе");



                ///// нужно выбрать RP лучших
                int[] v_l = new int[2 * RP];
                for (int i = 0; i < RP; i++)
                {
                    v_l[i] = Genes[i, L];
                    v_l[i + RP] = GenesPotomki[i, L];
                }

                Array.Sort(v_l, 0, (2 * RP));
                l_cf = v_l[RP - 1];//наибольшее что можно взять
                                   ///нужно удалить все хромосомы с CF>l_cf
                ////////////////////////////////////////////////////////////////////////////////////////
                int[,] GenesTemp = new int[RP, (L + 1)];//для временного хранения лучших потомков и критерия качества



                int temp = 0;
                for (int i = 0; i < RP; i++)
                {
                    if (Genes[i, L] < l_cf)
                    {
                        for (int j = 0; j <= L; j++)
                            GenesTemp[temp, j] = (Genes[i, j]);
                        temp++;
                    }
                }

                if (temp < RP)
                {
                    for (int i = 0; i < RP; i++)
                    {
                        if (GenesPotomki[i, L] < l_cf)
                        {
                            for (int j = 0; j <= L; j++)
                                GenesTemp[temp, j] = (GenesPotomki[i, j]);
                            temp++;
                        }
                    }
                }

                while (temp < RP)
                {
                    for (int i = 0; i < RP; i++)
                    {
                        if (temp < RP)
                            if (Genes[i, L] == l_cf)
                            {
                                for (int j = 0; j <= L; j++)
                                    GenesTemp[temp, j] = (Genes[i, j]);
                                temp++;
                            }

                        if (temp < RP)
                        {
                            if (GenesPotomki[i, L] == l_cf)
                            {
                                for (int j = 0; j <= L; j++)
                                    GenesTemp[temp, j] = (GenesPotomki[i, j]);
                                temp++;
                            }
                        }


                    }
                }


                for (int i = 0; i < RP; i++)
                    for (int j = 0; j <= L; j++)
                    {
                        (Genes[i, j]) = GenesTemp[i, j];
                    }

            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Eskiz_K(int[,] Genes, int RP, int L, string[,] MHrom, string[,] Hromosoma, int dhr, int bestHr, ref int shirina_Kanala, int[,] Eskiz, int[,] RVO, int[,] HO, int n, int C)
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
            int p = 0;
            //MessageBox.Show("4", "О программе");
            int cikl = 0;
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


                        //MessageBox.Show("42", "О программе");//////////////////////////////////////////////////////////////////////////
                        if ((vhod == 0 && ishod > 0))// || cikl > 100000)//заносим вершину в эскиз и удаляем из графа топологии
                        {
                            for (int i = 1; i <= n; i++)
                            {
                                GTopology[nomC, i] = 0;
                                ///GTopology[i, nomC] = 0;

                            }
                            udalenoVersin++;////вершина удалена и ГТ


                            ////занесем вершину в эскиз
                            ///////////////////////////////////////////////////////////////////////////////////
                            ////можно ли остаться на этой магистрали
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
                        //////////////////////////////////////////////////////////////////////////////////
                        else cikl++;
                    }

                    else
                    {

                        if (p == 0)
                        {
                            p++;
                        }
                        bool h = false;
                        for (int i = 0; i < n; i++)
                            for (int j = 0; j < n; j++)
                            {
                                if (Eskiz[i, j] == nomC) h = true;
                            }
                        if (h == false)
                        {
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////


        public void CF(int[,] Genes, int RP, int L, string[,] MHrom, string[,] Hromosoma, int dhr, int[,] RVO, int[,] HO, int[] B, int[] T, int n, int C)
        {

            ///RP размер популяции
            for (int ng = 0; ng < RP; ng++) //номер хромосомы гена
            {

                int nomMagistrali = 0;
                int[,] Eskiz = new int[n, n];


                Eskiz_K(Genes, RP, L, MHrom, Hromosoma, dhr, ng, ref nomMagistrali, Eskiz, RVO, HO, n, C);

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



    }
}
