// See https://aka.ms/new-console-template for more information

using DotNetBatch14AMO.JsonplaceholderApiConsoleApp;

JsonplaceholderService jsonplaceholderService = new ();

var postsList = await jsonplaceholderService.GetPosts();
foreach (var postModel in postsList)
{
	Console.WriteLine(postModel.Title);
}
Console.ReadLine ();

var post = await jsonplaceholderService.GetPost(5);
Console.WriteLine(post.Title);
Console.ReadLine();

PostModel createPostModel = new()
{
	UserId = 1,
	Title = "Refit Test Title",
	Body = "Refit Test Body"
};
var createResponse = await jsonplaceholderService.CreatePost(createPostModel);
Console.WriteLine(createResponse.Title);
Console.ReadLine();

PostModel updatePostModel = new()
{
	UserId = 1,
	Title = "Updated Refit Test Title",
	Body = "Updated Refit Test Body"
};
var updateResponse = await jsonplaceholderService.UpdatePost(5, updatePostModel);
Console.WriteLine(updateResponse.Title);
Console.ReadLine();

var deleteResponse = await jsonplaceholderService.DeletePost(5);
Console.WriteLine(deleteResponse.Title);
Console.ReadLine();