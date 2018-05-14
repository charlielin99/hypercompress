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

            //change to local machine base path
            var basePath = @"C:\projects\hypercompress\results\";

            //initialize vars outside of method calls to improve runtime
            const int size = 32;
            var stopwatch = Stopwatch.StartNew();
            var myRandom = new Random();
            var lcgRandom = new LCGGenerator();
            var baseBufferArray = new byte[size];
            var newBufferArray = new byte[size];
            var rngArray = new byte[size];
            var lcgArray = new byte[size];

            double totaltime1 = 0;
            double totaltime2 = 0;
            double totaltime3 = 0;


            for (var i = 0; i <100; i++)
            {

                //buffer method
                stopwatch.Start();
                myRandom.NextBytes(baseBufferArray);
                var randomWindow = myRandom.Next(0, size);
                Buffer.BlockCopy(baseBufferArray, randomWindow, newBufferArray, 0, size - randomWindow);
                Buffer.BlockCopy(baseBufferArray, 0, newBufferArray, size - randomWindow, randomWindow);
                stopwatch.Stop();
                var time1 = stopwatch.Elapsed;
                stopwatch.Reset();

                
                File.AppendAllLines(basePath+"BufferResults.txt", newBufferArray.Select(byteValue => byteValue.ToString()).ToArray());
                File.AppendAllText(basePath+"BufferResults.txt", "@" + System.Environment.NewLine); //makes it easier to clean data in file with python split method


             

                //rng method
                stopwatch.Start();
                (new RNGCryptoServiceProvider()).GetBytes(rngArray);
                stopwatch.Stop();
                var time2 = stopwatch.Elapsed;
                stopwatch.Reset();


                File.AppendAllLines(basePath+"RNGResults.txt", rngArray.Select(byteValue => byteValue.ToString()).ToArray());
                File.AppendAllText(basePath+"RNGResults.txt", "@" + System.Environment.NewLine);



             

                //lcg method
                stopwatch.Start();
                for (var j = 0; j < size; j++)
                {
                    var value = lcgRandom.Next(255);
                    lcgArray[j] = (BitConverter.GetBytes(value))[0];
                }
                stopwatch.Stop();
                var time3 = stopwatch.Elapsed;
                stopwatch.Reset();


                File.AppendAllLines(basePath + "LCGResults.txt", lcgArray.Select(byteValue => byteValue.ToString()).ToArray());
                File.AppendAllText(basePath + "LCGResults.txt", "@" + System.Environment.NewLine);


                totaltime1 += time1.TotalMilliseconds;
                totaltime2 += time2.TotalMilliseconds;
                totaltime3 += time3.TotalMilliseconds;


                Thread.Sleep(1000); //modify based on web traffic
            }

            Console.WriteLine("Time 1: " + totaltime1/100);
            Console.WriteLine("Time 2: " + totaltime2/100);
            Console.WriteLine("Time 3: " + totaltime3/100);



            //clearFile(basePath,"BufferResults.txt");
            //clearFile(basePath, "RNGResults.txt");
            //clearFile(basePath, "LCGResults.txt");

            Console.ReadLine();


        }

      
        public static void clearFile(string path, string name) //used to clear files
        {
            File.WriteAllText(path + name,string.Empty);
        }

    }
}


















// a few system converts, may be handy
/*
var str = System.Text.Encoding.Default.GetString(modified);
Console.WriteLine(str);

string converted = System.Text.Encoding.UTF8.GetString(modified, 0, modified.Length);
Console.WriteLine(converted);

Console.WriteLine(System.Convert.ToBase64String(modified, 0, modified.Length));
*/
