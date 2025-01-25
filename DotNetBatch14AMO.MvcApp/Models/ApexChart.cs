namespace DotNetBatch14AMO.MvcApp.Models
{
	public class ApexChart
	{
	}

	public class PieChartModel
	{
		public int[] Series { get; set; }
		public string[] Labels { get; set; }
	}

	public class RadarChartModel
	{
		public RadarChartSerieModel[] Series { get; set; }
		public string[] Labels { get; set; }	
	}
	public class RadarChartSerieModel
	{
		public string Name { get; set; }
		public int[] Data { get; set; }
	}

	public class LineChartModel
	{
		public LineChartSerieModel[] Series { get; set; }

		public string[] Labels { get; set; }
	}

	public class LineChartSerieModel
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public int[] Data { get; set; }
	}

}
