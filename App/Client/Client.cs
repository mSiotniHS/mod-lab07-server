namespace App.Client;

public sealed class Client
{
	public event EventHandler<RequestEventArgs>? Request;

	public void SendRequest(RequestEventArgs args) =>
		Request?.Invoke(this, args);

	public void Subscribe(EventHandler<RequestEventArgs> handler) =>
		Request += handler;
}
