using App.Client;

namespace App.Server;

public sealed class Server
{
	public record CollectedData
	{
		public int RequestsReceived;
		public int RequestsAccepted;
		public int RequestsRejected;
	}

	private readonly BusyThread[] _threads;
	private readonly object _threadLock = new();
	public CollectedData Data { get; }

	public Server(int threadCount)
	{
		_threads = Enumerable
			.Range(0, threadCount)
			.Select(_ => new BusyThread())
			.ToArray();
		Data = new CollectedData();
	}

	public void RequestHandler(object? sender, RequestEventArgs args)
	{
		lock (_threadLock)
		{
			Data.RequestsReceived++;

			for (var i = 0; i < _threads.Length; i++)
			{
				var thread = _threads[i];

				if (thread.IsBusy) continue;

				thread.IsBusy = true;
				thread.Thread = new Thread(() => ResolveRequest(i, args));
				thread.Thread.Start();
				Data.RequestsAccepted++;
				return;
			}

			Data.RequestsRejected++;
		}
	}

	private void ResolveRequest(int threadIdx, RequestEventArgs args)
	{
		Thread.Sleep(args.ResolvingTimespan);
		_threads[threadIdx].IsBusy = false;
	}
}
