using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rendez_vous
{
    class Program
    {
        static int[] V = new int[1000];
        static int[] W = new int[1000];
        static int min = int.MaxValue;
        static double med = 0;

        static SemaphoreSlim s1 = new SemaphoreSlim(0);
        static SemaphoreSlim s2 = new SemaphoreSlim(0);

        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Metodo1());
            t1.Start();
            Thread t2 = new Thread(() => Metodo2());
            t2.Start();

            while (t1.IsAlive) { }
            while (t2.IsAlive) { }

            Console.WriteLine($"minimo: {min}");
            Console.WriteLine($"media: {med}");

            Console.ReadLine();
        }

        private static void Metodo1()
        {
            Random r = new Random();

            for (int i = 0; i < V.Length; i++)
            {
                V[i] = r.Next(0, 1000);
                if (V[i] < min)
                    min = V[i];
            }
            s2.Release();

            s1.Wait();
            for (int i = 0; i < W.Length; i++)
            {
                if (W[i] < min)
                    min = W[i];
            }
        }

        private static void Metodo2()
        {
            Random r = new Random();

            for (int i = 0; i < W.Length; i++)
            {
                W[i] = r.Next(0, 1000);
                med += W[i];
            }
            s1.Release();

            s2.Wait();
            for (int i = 0; i < V.Length; i++)
                med += V[i];
            med = med / 2000;
        }
    }
}
