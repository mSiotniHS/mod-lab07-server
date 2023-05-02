using System.Text;
using App.Analysis;

namespace App;

internal static class Program
{
	public static void Main()
	{
		var cases = new SystemParameters[]
		{
			new() {ArrivalRate = 5, ServiceRate = 1, ThreadCount = 5},
			new() {ArrivalRate = 10, ServiceRate = 1, ThreadCount = 5},
			new() {ArrivalRate = 2, ServiceRate = 0.5, ThreadCount = 1}
		};

		var reports = Analyzer.PerformAnalysis(cases, TimeSpan.FromSeconds(30));

		using var writer = new StreamWriter("results.txt", false, Encoding.UTF8);

		foreach (var report in reports)
		{
			writer.WriteLine(report);
			writer.WriteLine("\n-----\n");
		}
	}
}
