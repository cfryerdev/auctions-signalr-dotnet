using Auctioneer.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace Auctioneer.Logic.Database
{
	public class AuctionContext : DbContext
	{
		public DbSet<Auction> Auctions { get; set; }
		public DbSet<AuctionItem> AuctionItems { get; set; }

		public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
		{
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
				var auction1 = Guid.NewGuid().ToString();
				Auctions.Add(new Auction { Id = auction1, Name = "First Auction", TypeId = 2, StartDateTime = DateTime.Now.AddHours(-2), EndDateTime = DateTime.Now.AddHours(6) });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction1, Index = 1 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction1, Index = 2 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 3", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction1, Index = 3 });

				var auction2 = Guid.NewGuid().ToString();
				Auctions.Add(new Auction { Id = auction2, Name = "Second Auction", TypeId = 1, StartDateTime = DateTime.Now.AddHours(-1), EndDateTime = DateTime.Now.AddHours(4).AddMinutes(12).AddSeconds(22) });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction2 });

				var auction3 = Guid.NewGuid().ToString();
				Auctions.Add(new Auction { Id = auction3, Name = "Third Auction", TypeId = 2, StartDateTime = DateTime.Now.AddDays(-4), EndDateTime = DateTime.Now.AddDays(-3) });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction3, Index = 1 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction3, Index = 2 });

				var auction4 = Guid.NewGuid().ToString();
				Auctions.Add(new Auction { Id = auction4, Name = "Fourth Auction", TypeId = 3, StartDateTime = DateTime.Now.AddDays(3), EndDateTime = DateTime.Now.AddDays(4) });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 3", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 4", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 5", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });

				var auction5 = Guid.NewGuid().ToString();
				Auctions.Add(new Auction { Id = auction5, Name = "Fifth Auction", TypeId = 3, StartDateTime = DateTime.Now.AddHours(3).AddSeconds(17), EndDateTime = DateTime.Now.AddHours(4).AddMinutes(12).AddSeconds(22) });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 3", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 4", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 5", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 6", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
				AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid().ToString(), Name = "Item 7", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });

				SaveChanges();
			}
		}
	}
}