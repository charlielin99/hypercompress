using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace hypercompress
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize var outside of method calls to improve runtime
            const int size = 32;
            var stopwatch = Stopwatch.StartNew();
            var myRandom = new Random();
            var baseBuffer = new byte[size];
            var bufferArray = new byte[size];
            var rngArray = new byte[size];

            for (var i = 0; i < 1; i++)
            {

                //buffer method
                stopwatch.Start();
                myRandom.NextBytes(baseBuffer);
                var randomWindow = myRandom.Next(0, size);
                Buffer.BlockCopy(baseBuffer, randomWindow, bufferArray, 0, size - randomWindow);
                Buffer.BlockCopy(baseBuffer, 0, bufferArray, size - randomWindow, randomWindow);
                stopwatch.Stop();
                var time1 = stopwatch.Elapsed;
                stopwatch.Reset();

                //Work Desktop Path
                System.IO.File.WriteAllLines(@"C:\projects\hypercompress\results\BufferResults.txt", bufferArray.Select(byteValue => byteValue.ToString()).ToArray());





                //rng method
                stopwatch.Start();
                (new RNGCryptoServiceProvider()).GetBytes(rngArray);
                stopwatch.Stop();
                var time2 = stopwatch.Elapsed;
                stopwatch.Reset();

                //Work Desktop Path
                System.IO.File.WriteAllLines(@"C:\projects\hypercompress\results\RNGResults.txt", rngArray.Select(byteValue => byteValue.ToString()).ToArray());








                //var total = time2.TotalMilliseconds - time1.TotalMilliseconds;
                //Console.WriteLine(total);
                Thread.Sleep(400); //modify based on web traffic
            }







            while (true) ;
        }

        //var str = System.Text.Encoding.Default.GetString(modified);
        //Console.WriteLine(str);

        //string converted = System.Text.Encoding.UTF8.GetString(modified, 0, modified.Length);
        //Console.WriteLine(converted);

        //Console.WriteLine(System.Convert.ToBase64String(modified, 0, modified.Length));

    }
}

