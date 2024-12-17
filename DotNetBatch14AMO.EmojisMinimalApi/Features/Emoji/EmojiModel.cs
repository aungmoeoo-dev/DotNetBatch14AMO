using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14AMO.EmojisMinimalApi.Features.Emoji;

public class EmojiModel
{
	public string EmojiId { get; set; }
	public string Emoji { get; set; }
	public string EmojiName { get; set; }
	public string EmojiShortname { get; set; }
	public string EmojiUnicode { get; set; }
	public string EmojiHtml { get; set; }
	public string EmojiCategory { get; set; }
	public string EmojiOrder { get; set; }
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