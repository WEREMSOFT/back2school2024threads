using System;
using System.Threading;
using System.Diagnostics;

class RunTest(int instanceNumber, object LockObject)
{
	public void Run()
	{
		lock(LockObject)
		{
			Console.WriteLine($"##################[{instanceNumber}]#################");
			Console.WriteLine($"Starting process on {instanceNumber}");
			Console.WriteLine($"Hello World {instanceNumber}");
			Thread.Sleep(instanceNumber * 1000);
		}
		Console.WriteLine($"Finishing process on {instanceNumber}");
	}
}

public class ThreadTest
{
	const int THREAD_COUNT = 10;
 	private static object lockObject = new object();

    public void Run()
    {
		RunTest[] processes = new RunTest[THREAD_COUNT];

		for(int i = 0; i < THREAD_COUNT; i++)
		{
			processes[i] = new RunTest(i, lockObject);
		}

		Thread[] threads = new Thread[THREAD_COUNT];
		{
			int i = 0;

			foreach(var process in processes)
			{
				threads[i] = new Thread(process.Run);
				i++;
			}
		}

		foreach(var thread in threads)
		{
			thread.Start();
		}

		foreach(var thread in threads)
		{
			thread.Join();
		}
    }
}
