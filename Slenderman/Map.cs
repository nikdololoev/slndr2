﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slenderman
{
    class Map
    {
        public List<int> map = new List<int> {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                                              1,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,
                                              1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,1,0,0,1,1,1,0,1,1,1,1,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,1,0,0,1,
                                              1,0,0,0,0,2,0,0,0,0,0,0,0,1,0,1,0,1,1,1,1,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,1,1,1,1,0,1,0,1,0,0,0,0,0,1,
                                              1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,1,
                                              1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
        };

        int MapHeight;
        int MapWidht;

        public Map()
        {
            MapWidht = 24;
            MapHeight = map.Count / MapWidht;
        }

        public void FindWave(int startX, int startY, int targetX, int targetY)
        {
            int[,] Map=new int[MapHeight,MapWidht];
            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidht; j++)
                {
                    Map[i, j] = map[i * 24 + j];
                }
            }
            bool add = true;
            int[,] cMap = new int[MapHeight, MapWidht];
            int x, y, step = 0;
            for (y = 0; y < MapHeight; y++)
                for (x = 0; x < MapWidht; x++)
                {
                    if (Map[y, x] == 1)
                        cMap[y, x] = -2;//индикатор стены
                    else
                        cMap[y, x] = -1;//индикатор еще не ступали сюда
                }
            cMap[targetY, targetX] = 0;//Начинаем с финиша
            while (add == true)
            {
                add = false;
                for (y = 0; y < MapWidht; y++)
                    for (x = 0; x < MapHeight; x++)
                    {
                        if (cMap[x, y] == step)
                        {
                            //Ставим значение шага+1 в соседние ячейки (если они проходимы)
                            if (y - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;
                            if (x - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (y + 1 < MapWidht && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                cMap[x + 1, y] = step + 1;
                            if (x + 1 < MapHeight && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                cMap[x, y + 1] = step + 1;
                        }
                    }
                step++;
                add = true;
                if (cMap[startY, startX] != -1)//решение найдено
                    add = false;
                if (step > MapWidht * MapHeight)//решение не найдено
                    add = false;
            }
            //Отрисовываем карты
            for (y = 0; y < MapHeight; y++)
            {
                Console.WriteLine();
                for (x = 0; x < MapWidht; x++)
                    if (cMap[y, x] == -1)
                        Console.Write(" ");
                    else
                    if (cMap[y, x] == -2)
                        Console.Write("#");
                    else
                    if (y == startY && x == startX)
                        Console.Write("S");
                    else
                    if (y == targetY && x == targetX)
                        Console.Write("F");
                    else
                    if (cMap[y, x] > -1)
                        Console.Write("{0}", cMap[y, x]);

            }
            Console.ReadKey();
        }
    }
}
