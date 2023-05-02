namespace App.Analysis;

public static class Reporter
{
	public static string CreateReport(
		int id,
		SystemParameters parameters,
		TimeSpan duration,
		Statistics theoreticalStats,
		Statistics actualStats,
		Server.Server.CollectedData serverData)
	{
		return
			$$"""
			Результаты моделирования №{{id}}
			
			Параметры системы:
			- Интенсивность потока заявок: {{parameters.ArrivalRate}}
			- Интенсивность потока обслуживания: {{parameters.ServiceRate}}
			- Число потоков: {{parameters.ThreadCount}}
			
			Продолжительность анализа: {{duration.TotalSeconds}} с

			Данные с сервера:
			- Общее число заявок: {{serverData.RequestsReceived}}
			- Число принятых заявок: {{serverData.RequestsAccepted}}
			- Число отклонённых заявок: {{serverData.RequestsRejected}}

			Теоретические подсчёты:
			- Вероятность простоя системы: {{theoreticalStats.SystemIdlenessProbability}}
			- Вероятность отказа системы: {{theoreticalStats.SystemFailureProbability}}
			- Относительная пропускная способность: {{theoreticalStats.RelativeThroughput}}
			- Абсолютная пропускная способность: {{theoreticalStats.AbsoluteThroughput}}
			- Среднее число занятых каналов: {{theoreticalStats.AvgNumberOfOccupiedChannels}}

			Реальные результаты:
			- Вероятность простоя системы: {{actualStats.SystemIdlenessProbability}}
			- Вероятность отказа системы: {{actualStats.SystemFailureProbability}}
			- Относительная пропускная способность: {{actualStats.RelativeThroughput}}
			- Абсолютная пропускная способность: {{actualStats.AbsoluteThroughput}}
			- Среднее число занятых каналов: {{actualStats.AvgNumberOfOccupiedChannels}}

			Абсолютные разницы:
			- Вероятность простоя системы: {{Math.Abs(theoreticalStats.SystemIdlenessProbability - actualStats.SystemIdlenessProbability)}}
			- Вероятность отказа системы: {{Math.Abs(theoreticalStats.SystemFailureProbability - actualStats.SystemFailureProbability)}}
			- Относительная пропускная способность: {{Math.Abs(theoreticalStats.RelativeThroughput - actualStats.RelativeThroughput)}}
			- Абсолютная пропускная способность: {{Math.Abs(theoreticalStats.AbsoluteThroughput - actualStats.AbsoluteThroughput)}}
			- Среднее число занятых каналов: {{Math.Abs(theoreticalStats.AvgNumberOfOccupiedChannels - actualStats.AvgNumberOfOccupiedChannels)}}
			""";
	}
}
