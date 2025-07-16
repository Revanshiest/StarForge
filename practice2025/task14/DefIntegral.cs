using System;
using System.Threading;

namespace task14
{
    public class DefiniteIntegral
    {
        //
        // a, b - границы отрезка, на котором происходит вычисление определенного интеграла
        // function - функция, для которой вычисляется определенный интеграл
        // step - размер одного шага разбиения
        // threadsNumber - число потоков, которые используются для вычислений
        //
        public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
        {
            double result = 0.0;
            object locker = new object();
            Thread[] threads = new Thread[threadsNumber];
            Barrier barrier = new Barrier(threadsNumber + 1);

            double range = b - a;
            double part = range / threadsNumber;

            for (int t = 0; t < threadsNumber; t++)
            {
                int threadIndex = t;
                threads[t] = new Thread(() =>
                {
                    double localA = a + threadIndex * part;
                    double localB = (threadIndex == threadsNumber - 1) ? b : localA + part;
                    double localResult = 0.0;

                    for (double x = localA; x < localB; x += step)
                    {
                        double xNext = Math.Min(x + step, localB);
                        localResult += (function(x) + function(xNext)) * (xNext - x) / 2.0;
                    }

                    lock (locker)
                    {
                        result += localResult;
                    }

                    barrier.SignalAndWait();
                });
                threads[t].Start();
            }

            barrier.SignalAndWait();
            return result;
        }
    }
}