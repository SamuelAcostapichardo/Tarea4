using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Ejemplo1
{
    class Program
    {
        public static int N = 10_000_000;
        static void Main(string[] args)
        {
            DisplayTimerProperties();

            Console.WriteLine();
            Console.WriteLine("Press the Enter key to begin:");
            Console.ReadLine();
            Console.WriteLine();

            //TimeOperations();
            int[] vec = VectorOrdenado();

            //busquedaSecuencial(vec, vec[N - 1]);
            busquedaSecuencial(vec,vec[N-1]);
        }

        private static void busquedaSecuencial(int[] vec, int buscar)
        {          
            bool encontrado = false;
            int posicion = -1;
            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();
            for (int i = 0; i < N & !encontrado; i++)
            {               
                if (vec[i] == buscar)
                {                   
                    encontrado = true;
                    posicion = i;                  
                }
            }
            timeMeasure.Stop();
            Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine(timeMeasure.Elapsed.TotalMilliseconds / 1000);
            Console.WriteLine($"Precisión:{1.0 / Stopwatch.Frequency:E}segundos");
            Console.WriteLine(posicion);
        }

        public static void Busquedabinaria(int[]vec,int buscar) 
        {
            //const long numIterations = 10000000;
            bool encontrado = false;          
            int average = 0;         
            int min = 0;
            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();
            while (min <= N && encontrado == false)
            {
                average = (N + min) / 2; 
                if (vec[average] == buscar)
                {
                    encontrado = true;
                    break;
                }
                else if (vec[average] > buscar)
                {
                    N = average - 1;
                }
                else
                {
                    min = average + 1;
                }
                if (encontrado == true)
                {
                    Console.WriteLine("Posicion" ,(average +1));
                }
                else
                {
                    Console.WriteLine("El elemento no se econtro",buscar,average+1);
                }
            }
            timeMeasure.Stop();
            Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"Precisión:{1.0/Stopwatch.Frequency:E}segundos");
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Alta precision");
            }
            else
            {
                Console.WriteLine("Baja precision");
            }
            //timerparse.Stop();
            //ticksThisTime = timerparse.ElapsedTicks;
            //Time10kOperations.Stop();
            //milliSec = Time10kOperations.ElapsedMilliseconds;
            //Console.WriteLine("Total time {0} operations: {1} millisecond", numIterations, milliSec);

        }


        public static void Buscar(int[]vec, int buscar) 
        {
            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();
            bool encontrado = false;
            int izquierda = 0, derecha = N - 1;
            while (izquierda <= derecha && !encontrado)
            {
                int medio = izquierda + (derecha-izquierda)/2;
                if (vec[medio] ==buscar)
                {
                    encontrado = true;
                }
                else if (vec[medio] <buscar)
                {
                    izquierda = medio + 1;
                }
                else
                {
                    derecha = medio - 1;
                }
                if (encontrado)
                {
                    Console.WriteLine("La posicion "+ medio);
                }
            }
            timeMeasure.Stop();
           Console.WriteLine(timeMeasure.Elapsed.TotalMilliseconds / 1000);
            Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalMilliseconds}ms");
           // Console.WriteLine($"Precisión:{1.0 / Stopwatch.Frequency:E}segundos");
        }

        private static int[] VectorOrdenado()
        {
            int[] vec = new int[N];
            for (int i = 0; i < N; i++)
            {
                vec[i] = i;
            }
            return vec;
        }




        public static void DisplayTimerProperties()
        {
            // Display the timer frequency and resolution.
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Operations timed using the system's high-resolution performance counter.");
            }
            else
            {
                Console.WriteLine("Operations timed using the DateTime class.");
            }

            long frequency = Stopwatch.Frequency;
            Console.WriteLine("  Timer frequency in ticks per second = {0}",
                frequency);
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            Console.WriteLine("  Timer is accurate within {0} nanoseconds",
                nanosecPerTick);
        }


        private static void TimeOperations()
        {
            long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            const long numIterations = 10000;

            // Define the operation title names.
            String[] operationNames = {"Operation: Int32.Parse(\"0\")",
                                           "Operation: Int32.TryParse(\"0\")",
                                           "Operation: Int32.Parse(\"a\")",
                                           "Operation: Int32.TryParse(\"a\")"};

            // Time four different implementations for parsing
            // an integer from a string.

            for (int operation = 0; operation <= 3; operation++)
            {
                // Define variables for operation statistics.
                long numTicks = 0;
                long numRollovers = 0;
                long maxTicks = 0;
                long minTicks = Int64.MaxValue;
                int indexFastest = -1;
                int indexSlowest = -1;
                long milliSec = 0;

                Stopwatch time10kOperations = Stopwatch.StartNew();

                // Run the current operation 10001 times.
                // The first execution time will be tossed
                // out, since it can skew the average time.

                for (int i = 0; i <= numIterations; i++)
                {
                    long ticksThisTime = 0;
                    int inputNum;
                    Stopwatch timePerParse;

                    switch (operation)
                    {
                        case 0:
                            // Parse a valid integer using
                            // a try-catch statement.

                            // Start a new stopwatch timer.
                            timePerParse = Stopwatch.StartNew();

                            try
                            {
                                inputNum = Int32.Parse("0");
                            }
                            catch (FormatException)
                            {
                                inputNum = 0;
                            }

                            // Stop the timer, and save the
                            // elapsed ticks for the operation.

                            timePerParse.Stop();
                            ticksThisTime = timePerParse.ElapsedTicks;
                            break;
                        case 1:
                            // Parse a valid integer using
                            // the TryParse statement.

                            // Start a new stopwatch timer.
                            timePerParse = Stopwatch.StartNew();

                            if (!Int32.TryParse("0", out inputNum))
                            {
                                inputNum = 0;
                            }

                            // Stop the timer, and save the
                            // elapsed ticks for the operation.
                            timePerParse.Stop();
                            ticksThisTime = timePerParse.ElapsedTicks;
                            break;
                        case 2:
                            // Parse an invalid value using
                            // a try-catch statement.

                            // Start a new stopwatch timer.
                            timePerParse = Stopwatch.StartNew();

                            try
                            {
                                inputNum = Int32.Parse("a");
                            }
                            catch (FormatException)
                            {
                                inputNum = 0;
                            }

                            // Stop the timer, and save the
                            // elapsed ticks for the operation.
                            timePerParse.Stop();
                            ticksThisTime = timePerParse.ElapsedTicks;
                            break;
                        case 3:
                            // Parse an invalid value using
                            // the TryParse statement.

                            // Start a new stopwatch timer.
                            timePerParse = Stopwatch.StartNew();

                            if (!Int32.TryParse("a", out inputNum))
                            {
                                inputNum = 0;
                            }

                            // Stop the timer, and save the
                            // elapsed ticks for the operation.
                            timePerParse.Stop();
                            ticksThisTime = timePerParse.ElapsedTicks;
                            break;

                        default:
                            break;
                    }

                    // Skip over the time for the first operation,
                    // just in case it caused a one-time
                    // performance hit.
                    if (i == 0)
                    {
                        time10kOperations.Reset();
                        time10kOperations.Start();
                    }
                    else
                    {

                        // Update operation statistics
                        // for iterations 1-10000.
                        if (maxTicks < ticksThisTime)
                        {
                            indexSlowest = i;
                            maxTicks = ticksThisTime;
                        }
                        if (minTicks > ticksThisTime)
                        {
                            indexFastest = i;
                            minTicks = ticksThisTime;
                        }
                        numTicks += ticksThisTime;
                        if (numTicks < ticksThisTime)
                        {
                            // Keep track of rollovers.
                            numRollovers++;
                        }
                    }
                }

                // Display the statistics for 10000 iterations.

                time10kOperations.Stop();
                milliSec = time10kOperations.ElapsedMilliseconds;

                Console.WriteLine();
                Console.WriteLine("{0} Summary:", operationNames[operation]);
                Console.WriteLine("  Slowest time:  #{0}/{1} = {2} ticks",
                    indexSlowest, numIterations, maxTicks);
                Console.WriteLine("  Fastest time:  #{0}/{1} = {2} ticks",
                    indexFastest, numIterations, minTicks);
                Console.WriteLine("  Average time:  {0} ticks = {1} nanoseconds",
                    numTicks / numIterations,
                    (numTicks * nanosecPerTick) / numIterations);
                Console.WriteLine("  Total time looping through {0} operations: {1} milliseconds",
                    numIterations, milliSec);
            }
        }
    }









  
}
