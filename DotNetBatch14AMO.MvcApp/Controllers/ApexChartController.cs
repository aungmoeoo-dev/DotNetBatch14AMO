using DotNetBatch14AMO.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14AMO.MvcApp.Controllers;

public class ApexChartController : Controller
{
	[ActionName("PieChart")]
	public IActionResult PieChart()
	{
		PieChartModel model = new()
		{
			Series = new int[] { 44, 55, 13, 43, 22 },
			Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" }
		};
		return View(model);
	}

	[ActionName("DonutChart")]
	public IActionResult DonutChart()
	{
		PieChartModel model = new()
		{
			Series = new int[] { 44, 55, 13, 43, 22 },
			Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" }
		};
		return View(model);
	}

	[ActionName("RadarChart")]
	public IActionResult RadarChart()
	{
		RadarChartModel model = new()
		{
			Series = new RadarChartSerieModel[]
			{
				new()
				{ Name = "Radar Series 1",
				Data = new int[] { 45, 52, 38, 24, 33, 10 }
				},
				new()
				{ Name = "Radar Series 2",
				Data = new int[] { 26, 21, 20, 6, 8, 15 }
				}
			},
			Labels = new string[] { "April", "May", "June", "July", "August", "September" }
		};
		return View(model);
	}

	[ActionName("SplineLineChart")]
	public IActionResult SplineLineChart()
	{
		LineChartModel model = new()
		{
			Series = new LineChartSerieModel[]
			{
				new()
				{
					Name = "First serie",
					Type = "line",
					Data = new int[] {23,45, 12,67,89, 34, 121 }
				},
				new()
				{
					Name = "Second serie",
					Type = "column",
					Data = new int[] {32,67, 56,23,78, 43, 36 }
				}
			},
			Labels = new string[] { "April", "May", "June", "July", "August", "September", "October" }
		};
		return View(model);
	}

	[ActionName("SplineAreaChart")]
	public IActionResult SplineAreaChart()
	{
		LineChartModel model = new()
		{
			Series = new LineChartSerieModel[]
			{
				new()
				{
					Name = "First serie",
					Type = "area",
					Data = new int[] {23,45, 12,67,89, 34, 121 }
				},
				new()
				{
					Name = "Second serie",
					Type = "area",
					Data = new int[] {32,67, 56,23,78, 43, 36 }
				}
			},
			Labels = new string[] { "April", "May", "June", "July", "August", "September", "October" }
		};
		return View(model);
	}

}
