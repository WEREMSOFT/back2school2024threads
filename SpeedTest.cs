using System;
using System.Threading;
using System.Diagnostics;


public class SpeedTest
{
    public SpeedTest()
	{

		const int LARGE_NUMBER = 20000;

		int[,] Elements = new int[LARGE_NUMBER, LARGE_NUMBER];

		var stopWatch = new Stopwatch();

		stopWatch.Start();

			for(int j = 0; j < LARGE_NUMBER; j++)
		for(int i = 0; i < LARGE_NUMBER; i++)
				Elements[i, j] = i * j;

		stopWatch.Stop();

		long microseconds = stopWatch.ElapsedTicks / (Stopwatch.Frequency / 1000000);

		Console.WriteLine("Elapsed Time:  " + microseconds + " ms");
	}
}
