namespace App.Client;

public sealed class RequestEventArgs : EventArgs
{
	public int Id { get; init; }
	public TimeSpan ResolvingTimespan { get; init; }
}
