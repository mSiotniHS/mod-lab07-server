namespace App;

public static class Calculator
{
	public static Statistics CalculateStats(double arrivalRate, double serviceRate, int channelCount)
	{
		var presentArrivalRate = arrivalRate / serviceRate;

		var systemIdlenessProbability =
			1 / Enumerable
				.Range(0, channelCount + 1)
				.Select(i => Math.Pow(presentArrivalRate, i) / Factorial(i))
				.Sum();

		var systemFailureProbability =
			Math.Pow(presentArrivalRate, channelCount) * systemIdlenessProbability / Factorial(channelCount);

		var relativeThroughput = 1 - systemFailureProbability;
		var absoluteThroughput = arrivalRate * relativeThroughput;

		var avgNumberOfOccupiedChannels = absoluteThroughput / serviceRate;

		return new Statistics
		{
			SystemIdlenessProbability = systemIdlenessProbability,
			SystemFailureProbability = systemFailureProbability,
			RelativeThroughput = relativeThroughput,
			AbsoluteThroughput = absoluteThroughput,
			AvgNumberOfOccupiedChannels = avgNumberOfOccupiedChannels
		};
	}

	private static int Factorial(int n) =>
		n switch
		{
			0 => 1,
			1 => 1,
			_ => Enumerable
				.Range(1, n)
				.Aggregate((acc, x) => acc * x)
		};
}

public struct Statistics
{
	public double SystemIdlenessProbability;
	public double SystemFailureProbability;
	public double RelativeThroughput;
	public double AbsoluteThroughput;
	public double AvgNumberOfOccupiedChannels;
}
