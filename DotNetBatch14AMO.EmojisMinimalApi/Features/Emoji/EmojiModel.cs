using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14AMO.EmojisMinimalApi.Features.Emoji;

public class EmojiModel
{
	public string Emoji { get; set; }
	public string Name { get; set; }
	public string Shortname { get; set; }
	public string Unicode { get; set; }
	public string Html { get; set; }
	public string Category { get; set; }
	public string Order { get; set; }
}

public class EmojiRequestModel
{
	public List<EmojiModel> Emojis { get; set; }
}

public class EmojiListResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public List<EmojiModel> Data { get; set; }
}

public class EmojiResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public EmojiModel Data { get; set; }
}