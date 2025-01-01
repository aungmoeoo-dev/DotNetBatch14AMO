using System.Collections;

namespace DotNetBatch14AMO.EmojisMinimalApi.Utilities;

public static class IndexedForEachExtension
{
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
	{
		for (var i = 0; i < enumerable.Count(); i++)
		{
			var currentItem = enumerable.ElementAt(i);

			action(currentItem, i);
		}
	}
}
