using Auctioneer.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class AuctionContext : DbContext
{
	public DbSet<Auction> Auctions { get; set; }
	public DbSet<AuctionItem> AuctionItems { get; set; }

	public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
	{
		//ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		SeedData();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase("AuctionDb");
		optionsBuilder.UseLazyLoadingProxies(false);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Auction>()
			.HasMany(a => a.Items)
			.WithOne(i => i.Auction)
			.HasForeignKey(i => i.AuctionId);
	}

	public void SeedData()
	{
		if (!Auctions.Any())
		{
			Auctions.Add(new Auction { Id = 1, Name = "First Auction", TypeId = 2, StartDateTime = DateTime.Now.AddHours(-2), EndDateTime = DateTime.Now.AddHours(6) });
			AuctionItems.Add(new AuctionItem { Id = 1, Name = "Item 1", Payload = "", AuctionId = 1 });
			AuctionItems.Add(new AuctionItem { Id = 2, Name = "Item 2", Payload = "", AuctionId = 1 });
			AuctionItems.Add(new AuctionItem { Id = 3, Name = "Item 3", Payload = "", AuctionId = 1 });

			Auctions.Add(new Auction { Id = 2, Name = "Second Auction", TypeId = 1, StartDateTime = DateTime.Now.AddHours(-1), EndDateTime = DateTime.Now.AddHours(4).AddMinutes(12).AddSeconds(22) });
			AuctionItems.Add(new AuctionItem { Id = 4, Name = "Item 1", Payload = "", AuctionId = 2 });

			Auctions.Add(new Auction { Id = 3, Name = "Third Auction", TypeId = 2, StartDateTime = DateTime.Now.AddDays(-4), EndDateTime = DateTime.Now.AddDays(-3) });
			AuctionItems.Add(new AuctionItem { Id = 5, Name = "Item 1", Payload = "", AuctionId = 3 });
			AuctionItems.Add(new AuctionItem { Id = 6, Name = "Item 2", Payload = "", AuctionId = 3 });

			Auctions.Add(new Auction { Id = 4, Name = "Fourth Auction", TypeId = 3, StartDateTime = DateTime.Now.AddDays(3), EndDateTime = DateTime.Now.AddDays(4) });
			AuctionItems.Add(new AuctionItem { Id = 7, Name = "Item 1", Payload = "", AuctionId = 4 });
			AuctionItems.Add(new AuctionItem { Id = 8, Name = "Item 2", Payload = "", AuctionId = 4 });
			AuctionItems.Add(new AuctionItem { Id = 9, Name = "Item 3", Payload = "", AuctionId = 4 });
			AuctionItems.Add(new AuctionItem { Id = 10, Name = "Item 4", Payload = "", AuctionId = 4 });
			AuctionItems.Add(new AuctionItem { Id = 11, Name = "Item 5", Payload = "", AuctionId = 4 });

			Auctions.Add(new Auction { Id = 5, Name = "Fifth Auction", TypeId = 3, StartDateTime = DateTime.Now.AddHours(3).AddSeconds(17), EndDateTime = DateTime.Now.AddHours(4).AddMinutes(12).AddSeconds(22) });
			AuctionItems.Add(new AuctionItem { Id = 12, Name = "Item 1", Payload = "", AuctionId = 5 });
			AuctionItems.Add(new AuctionItem { Id = 13, Name = "Item 2", Payload = "", AuctionId = 5 });
			AuctionItems.Add(new AuctionItem { Id = 14, Name = "Item 3", Payload = "", AuctionId = 5 });
			AuctionItems.Add(new AuctionItem { Id = 15, Name = "Item 4", Payload = "", AuctionId = 5 });
			AuctionItems.Add(new AuctionItem { Id = 16, Name = "Item 5", Payload = "", AuctionId = 5 });
			AuctionItems.Add(new AuctionItem { Id = 17, Name = "Item 6", Payload = "", AuctionId = 5 });
			AuctionItems.Add(new AuctionItem { Id = 18, Name = "Item 7", Payload = "", AuctionId = 5 });

			SaveChanges();
		}
	}
}