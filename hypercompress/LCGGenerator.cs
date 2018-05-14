using System;

namespace hypercompress
{
    class LCGGenerator //https://en.wikipedia.org/wiki/Linear_congruential_generator
    {
        private const long a = 4294967296; // 2^32
        private const long b = 1664525;
        private const long c = 1013904223;
        private long value;

        public LCGGenerator()
        {
            value = DateTime.Now.Ticks % a;
        }

        public LCGGenerator(long seed) //optional constructor 
        {
            value = seed;
        }

        public long Next()
        {
            value = ((b * value) + c) % a;

            return value;
        }

        public long Next(long maxValue)
        {
            return Next() % maxValue;
        }
    }
}