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
            var baseBufferArray = new byte[size];
            var newBufferArray = new byte[size];
            var rngArray = new byte[size];

            for (var i = 0; i < 100; i++)
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
                File.AppendAllText(basePath+"BufferResults.txt", "@" + System.Environment.NewLine); 
                
                


                //rng method
                stopwatch.Start();
                (new RNGCryptoServiceProvider()).GetBytes(rngArray);
                stopwatch.Stop();
                var time2 = stopwatch.Elapsed;
                stopwatch.Reset();


                File.AppendAllLines(basePath+"RNGResults.txt", rngArray.Select(byteValue => byteValue.ToString()).ToArray());
                File.AppendAllText(basePath+"RNGResults.txt", "@" + System.Environment.NewLine);








                //var total = time2.TotalMilliseconds - time1.TotalMilliseconds;
                //Console.WriteLine(total);
                //Thread.Sleep(400); //modify based on web traffic
            }

            


            while (true);
        }

      
        public static void clearFile(string path, string name) //used to clear files
        {
            File.WriteAllText(path + name,string.Empty);
        }

    }
}

//var str = System.Text.Encoding.Default.GetString(modified);
//Console.WriteLine(str);

//string converted = System.Text.Encoding.UTF8.GetString(modified, 0, modified.Length);
//Console.WriteLine(converted);

//Console.WriteLine(System.Convert.ToBase64String(modified, 0, modified.Length));

