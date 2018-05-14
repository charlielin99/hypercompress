# hypercompress

Generates the common unsigned byte array of size 32 used in web apps based on the metrics of:
1. Security/Randomness of values
2. Speed of Generation depending on number of values needed per second

Uses Machine Learning to guess future values based on different generation algo's produced training data to determine the relative randomness.

Next, applies an intuitive algorithm to compress/encrypt the array into a string of <64 characters and reverses string back into target array.
The standard byte to string is using hex, which produces a string of 64 characters when fed a byte array of 32.
