#pragma warning disable CS0169 
using System;
using System.Diagnostics;
using System.Threading;

public class FalseSharingExample
{
    private const int NumThreads = 4;
    private const int NumIterations = 10_000_000;

    private static readonly long[] SharedData = new long[NumThreads];

    private static readonly PaddedLong[] PaddedData = new PaddedLong[NumThreads];

    public FalseSharingExample()
    {
        for (int i = 0; i < NumThreads; i++)
        {
            PaddedData[i] = new PaddedLong();
        }

        Console.WriteLine("False Sharing Demostration:");
        RunTest(SharedData, "With False Sharing");

        Console.WriteLine("\n Avoid False Sharing using Padding :");
        RunTest(PaddedData, "Without False Sharing");
    }

    public void RunTest(Array data, string testName)
    {
        var threads = new Thread[NumThreads];
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < NumThreads; i++)
        {
            int threadIndex = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < NumIterations; j++)
                {
                    if (data is long[] shared)
                        shared[threadIndex]++;
                    else if (data is PaddedLong[] padded)
                        padded[threadIndex].Value++;
                }
            });
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        stopwatch.Stop();
        Console.WriteLine($"{testName}: {stopwatch.ElapsedMilliseconds} ms");
    }

    // Clase with padding to avoid false sharing
    private class PaddedLong
    {
        public long Value;
        // Padding to space memory and avoid threads stepping in the toes of each other.
        private long pad1, pad2, pad3, pad4, pad5, pad6, pad7;
    }
}
