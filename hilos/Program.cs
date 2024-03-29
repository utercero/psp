﻿using System;
using System.Diagnostics;
using System.Threading;
namespace hilos
{
    public class Example
    {
        public static void Main()
        {
            /* Thread con parametro. El trabajo a realizar por el thread se define en a traves de una expresion lambda
            var th = new Thread(obj=> {
                int interval;
                try
                {
                    interval = (int)obj;
                }
                catch (InvalidCastException)
                {
                    interval = 5000;
                }
                DateTime start = DateTime.Now;
                var sw = Stopwatch.StartNew();
                Console.WriteLine("Thread {0}: {1}, Priority {2}",
                                  Thread.CurrentThread.ManagedThreadId,
                                  Thread.CurrentThread.ThreadState,
                                  Thread.CurrentThread.Priority);
                do
                {
                    Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                                      Thread.CurrentThread.ManagedThreadId,
                                      sw.ElapsedMilliseconds / 1000.0);
                    Thread.Sleep(500);
                } while (sw.ElapsedMilliseconds <= interval);
                sw.Stop();

            });
            */
            // El trabajo a relizar se define en un funcion
            var th = new Thread(ExecuteInForeground);
            var th2 = new Thread(ExecuteInForeground);
            // el parametro de entrada
            th.Start(4500);
            th2.Start(3000);
            //duermo el hilo actual
            Thread.Sleep(1000);
            Console.WriteLine("Main thread ({0}) exiting...",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private static void ExecuteInForeground(Object obj)
        {
            int interval;
            try
            {
                interval = (int)obj;
            }
            catch (InvalidCastException)
            {
                interval = 5000;
            }
            DateTime start = DateTime.Now;
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Thread {0}: {1}, Priority {2}",
                              Thread.CurrentThread.ManagedThreadId,
                              Thread.CurrentThread.ThreadState,
                              Thread.CurrentThread.Priority);
            do
            {
                Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                                  Thread.CurrentThread.ManagedThreadId,
                                  sw.ElapsedMilliseconds / 1000.0);
                Thread.Sleep(500);
            } while (sw.ElapsedMilliseconds <= interval);
            sw.Stop();
        }
    }
}

// The example displays output like the following:
//       Thread 3: Running, Priority Normal
//       Thread 3: Elapsed 0.00 seconds
//       Thread 3: Elapsed 0.52 seconds
//       Main thread (1) exiting...
//       Thread 3: Elapsed 1.03 seconds
//       Thread 3: Elapsed 1.55 seconds
//       Thread 3: Elapsed 2.06 seconds
//       Thread 3: Elapsed 2.58 seconds
//       Thread 3: Elapsed 3.09 seconds
//       Thread 3: Elapsed 3.61 seconds
//       Thread 3: Elapsed 4.12 seconds