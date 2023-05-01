namespace App.Server;

public record BusyThread(Thread? Thread = null, bool IsBusy = false)
{
	public Thread? Thread = Thread;
	public bool IsBusy = IsBusy;
}
