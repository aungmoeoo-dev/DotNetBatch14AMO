using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14AMO.RestApi.Features.Blog;

public class AppDbContext : DbContext
{
	private SqlConnectionStringBuilder _sqlConnectionStringBuilder;

	public AppDbContext()
	{
		_sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
		{
			DataSource = ".",
			InitialCatalog = "TestDB",
			UserID = "sa",
			Password = "Aa145156167!",
			TrustServerCertificate = true
		};
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
	}

	public DbSet<BlogModel> Blogs { get; set; }
}
