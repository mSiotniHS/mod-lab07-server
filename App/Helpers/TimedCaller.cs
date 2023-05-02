namespace App.Helpers;

public static class TimedCaller
{
	private static bool _shouldWork;

	public static void Call(Action action, TimeSpan duration, TimeSpan timeout)
	{
		_shouldWork = true;
		var thread = new Thread(() =>
		{
			while (_shouldWork)
			{
				action();
				Thread.Sleep(timeout);
			}
		});
		thread.Start();
		Thread.Sleep(duration);
		_shouldWork = false;
	}
}
