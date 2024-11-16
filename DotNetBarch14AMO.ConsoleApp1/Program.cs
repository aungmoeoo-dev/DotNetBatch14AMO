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



using Microsoft.Data.SqlClient;
using System.Data;

SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
connectionStringBuilder.DataSource = "."; // server name
connectionStringBuilder.InitialCatalog = "TestDB"; // db name
connectionStringBuilder.UserID = "sa"; // username
connectionStringBuilder.Password = "Aa145156167!"; // password
connectionStringBuilder.TrustServerCertificate = true;

string connectionString = connectionStringBuilder.ConnectionString;
SqlConnection connection = new SqlConnection(connectionString);

connection.Open();

string query = "select * from TBL_Blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);	

DataTable dt = new DataTable();
adapter.Fill(dt);

connection.Close();

// DataTable
// DataRow
// DataColumn

foreach (DataRow dr in dt.Rows)
{
	Console.WriteLine(dr["BlogId"]);
	Console.WriteLine(dr["BlogTitle"]);
	Console.WriteLine(dr["BlogAuthor"]);
	Console.WriteLine(dr["BlogContent"]);
}