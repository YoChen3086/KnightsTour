using System;
using System.Collections;

namespace WarnsDorff
{
    class Program
    {
        private static Random random = new Random();

        private static int cbx = 8;
        private static int cby = 8;
        private static int[,] cb = new int[cbx, cby];
        private static int[] dx = new int[] { -2, -1, 1, 2, -2, -1, 1, 2 };
        private static int[] dy = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

        //Warnsdorff規則指在所有可走且未經過的方格中，
        //馬只可能走這樣一個方格：從該方格出發,馬能跳的方格數最少；
        //如果可跳的方格數相等，則從目前位置看,方格序號小的優先。
        //依照這一規則往往可以找到一條路徑但是並不一定能夠成功。

        static void Main(string[] args)
        {
            do
            {
                PlayGame();
            } while (CheckEndGame() == false);

            PrintResult();

            Console.ReadLine();
        }

        private static void PlayGame()
        {
            int kx = random.Next(cbx);
            int ky = random.Next(cby);

            for (int k = 0; k < cbx * cby; k++)
            {
                cb[ky, kx] = k + 1;
                Stack pk = new Stack();
                for (int i = 0; i < cbx; i++)
                {
                    int nx = kx + dx[i];
                    int ny = ky + dy[i];
                    if (nx >= 0 && nx < cbx && ny >= 0 && ny < cby)
                    {
                        if (cb[ny, nx] == 0)
                        {
                            int ctr = 0;
                            for (int j = 0; j < cbx; j++)
                            {
                                int ex = nx + dx[j];
                                int ey = ny + dy[j];
                                if (ex >= 0 && ex < cbx && ey >= 0 && ey < cby)
                                {
                                    if (cb[ey, ex] == 0)
                                    {
                                        ctr++;
                                    }
                                }
                            }
                            pk.Push(new Step(ctr, i));
                        }
                    }
                }

                if (pk.Count > 0)
                {
                    Step currentStep = new Step(9, 9);
                    while (pk.Count > 0)
                    {
                        Step step = (Step)pk.Pop();
                        if (step.ctr <= currentStep.ctr)
                        {
                            currentStep = step;
                        }
                    }
                    kx += dx[currentStep.i];
                    ky += dy[currentStep.i];
                }
                else
                {
                    break;
                }
            }
        }

        private static bool CheckEndGame()
        {
            bool result = true;
            for (int i = 0; i < cbx; i++)
            {
                for (int j = 0; j < cby; j++)
                {
                    if (cb[i, j] == 0)
                    {
                        cb = new int[cbx, cby];
                        return !result;
                    }
                }
            }

            return result;
        }

        private static void PrintResult()
        {
            for (int i = 0; i < cbx; i++)
            {
                for (int j = 0; j < cby; j++)
                {
                    Console.Write(String.Format("{0:00}", cb[i, j]) + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
