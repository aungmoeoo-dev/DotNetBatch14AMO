//// See https://aka.ms/new-console-template for more information
//using Microsoft.Data.SqlClient;

//SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
//connectionStringBuilder.DataSource = "."; // server name
//connectionStringBuilder.InitialCatalog = "online_wallet"; // db name
//connectionStringBuilder.UserID = "sa"; // username
//connectionStringBuilder.Password = "Aa145156167!"; // password
//connectionStringBuilder.TrustServerCertificate = true;

//string connectionString = connectionStringBuilder.ConnectionString;
//SqlConnection connection = new SqlConnection(connectionString);

//Console.WriteLine($"Connection opening.");
//connection.Open();
//Console.WriteLine($"Connection open.");

//Console.WriteLine($"Connection closing.");
//connection.Close();
//Console.WriteLine($"Connection close.");



//using Microsoft.Data.SqlClient;
//using System.Data;

//SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
//connectionStringBuilder.DataSource = "."; // server name
//connectionStringBuilder.InitialCatalog = "TestDB"; // db name
//connectionStringBuilder.UserID = "sa"; // username
//connectionStringBuilder.Password = "Aa145156167!"; // password
//connectionStringBuilder.TrustServerCertificate = true;

//string connectionString = connectionStringBuilder.ConnectionString;
//SqlConnection connection = new SqlConnection(connectionString);

//connection.Open();

//string query = "select * from TBL_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//DataTable dt = new DataTable();
//adapter.Fill(dt);

//connection.Close();

//// DataTable
//// DataRow
//// DataColumn

//foreach (DataRow dr in dt.Rows)
//{
//	Console.WriteLine(dr["BlogId"]);
//	Console.WriteLine(dr["BlogTitle"]);
//	Console.WriteLine(dr["BlogAuthor"]);
//	Console.WriteLine(dr["BlogContent"]);
//}

using DotNetBarch14AMO.ConsoleApp1.AdoDotNetExamples;
using DotNetBarch14AMO.ConsoleApp1.DapperExamples;
using DotNetBarch14AMO.ConsoleApp1.EFCoreExamples;

AdoDotNetExample adoDotNetExample = new();
//adoDotNetExample.Read();
//adoDotNetExample.Edit("456A4F62-7181-4054-A8CD-160A51F343C9");
//adoDotNetExample.Create("AdoDotNet Create Test title", "AdoDotNet Create Test Author", "AdoDotNet Create Test Content");
//adoDotNetExample.Update("FFFE1C18-22F0-4804-999F-A27377863DCD", "Ado updated title", "Ado updated Author", "Ado updated content");
//adoDotNetExample.Delete("FFFE1C18-22F0-4804-999F-A27377863DCD");

DapperExample dapperExample = new();
//dapperExample.Read();
//dapperExample.Edit("456A4F62-7181-4054-A8CD-160A51F343C8");
//dapperExample.Create("Dapper Create Test title", "Dapper Create Test Author", "Dapper Create Test Content");
//dapperExample.Update("D384129B-6B07-4E59-9B19-1E06EA418A72", "Dapper updated title", "Dapper updated Author", "Dapper updated content");
//dapperExample.Delete("D384129B-6B07-4E59-9B19-1E06EA418A72");

EFCoreExample eFCoreExample = new();
//eFCoreExample.Read();
//eFCoreExample.Edit("456A4F62-7181-4054-A8CD-160A51F343C9");
//eFCoreExample.Create("EFCore create test 1", "EFCore test author", "EFCore test content");
//eFCoreExample.Update("85926186-29bd-4081-ab3a-dd0b496be06b", "Updated EFCore test 1", "Updated EFCore test author", "Updated EFCore test content");
//eFCoreExample.Delete("85926186-29bd-4081-ab3a-dd0b496be06b");