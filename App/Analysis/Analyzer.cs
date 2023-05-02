using App.Client;
using App.Helpers;

namespace App.Analysis;

public static class Analyzer
{
	public static IEnumerable<string> PerformAnalysis(IReadOnlyList<SystemParameters> parametersSet, TimeSpan duration)
	{
		for (var i = 0; i < parametersSet.Count; i++)
		{
			var parameters = parametersSet[i];

			var nextRequestTimeout = 1 / parameters.ArrivalRate;
			var requestResolvingTimespan = 1 / parameters.ServiceRate;

			var server = new Server.Server(parameters.ThreadCount);
			var client = new Client.Client();
			client.Subscribe(server.RequestHandler);

			TimedCaller.Call(
				() => client.SendRequest(new RequestEventArgs
				{
					Id = Random.Shared.Next(1, 500),
					ResolvingTimespan = TimeSpan.FromSeconds(requestResolvingTimespan)
				}),
				duration,
				TimeSpan.FromSeconds(nextRequestTimeout));

			var actualArrivalRate = server.Data.RequestsReceived / duration.TotalSeconds;
			var actualServiceRate = server.Data.RequestsAccepted / (duration.TotalSeconds * parameters.ThreadCount);

			var theoreticalStats =
				Calculator.CalculateStats(parameters.ArrivalRate, parameters.ServiceRate, parameters.ThreadCount);
			var actualStats =
				Calculator.CalculateStats(actualArrivalRate, actualServiceRate, parameters.ThreadCount);

			yield return Reporter.CreateReport(
				i + 1,
				parameters,
				duration,
				theoreticalStats,
				actualStats,
				server.Data);
		}
	}
}
