# hypercompress

<h4>* WORK IN PROGRESS *</h4>

Generates the common unsigned byte array of size 32 used in web apps based on the metrics of:

1. Security/Randomness of values
2. Speed of Generation depending on number of values needed per second (Web Traffic Dependent)

Uses Machine Learning (Support Vector Machine) to guess future values based on different generation algo's produced training data to determine the relative randomness while taking into account the speed.

Next, applies an intuitive algorithm to compress/encrypt the array into a string of <64 characters and reverses string back into target array.

The standard industry byte to string conversion is hex, which produces a string of 64 characters when fed a byte array of 32.

Reducing the size of the string will increase server speed depending on the application/# of requests fed on average. 
