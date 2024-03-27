using Auctioneer.Logic.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Database
{
	public class Seeder
	{
		private IDbConnection _database;

		public Seeder(IDbConnection dbConnection)
		{
			_database = dbConnection;
		}
		
		public void ClearSeedData()
		{
			_database.Query("DELETE FROM [AuctionItem] WHERE [Name] LIKE 'Seed: %'");
			_database.Query("DELETE FROM [Auction] WHERE [Name] LIKE 'Seed: %'");
		}

		public void SeedData()
		{
			var Auctions = new List<Auction>();
			var AuctionItems = new List<AuctionItem>();

			var auction1 = Guid.NewGuid();
			Auctions.Add(new Auction { Id = auction1, Name = "Seed: First Auction", VisibilityId = (int)AuctionVisibility.Blind, TypeId = (int)AuctionType.MultipleSequential, StartDateTime = DateTime.Now.AddHours(-2), EndDateTime = DateTime.Now.AddHours(6) });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction1, Index = 1 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction1, Index = 2 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 3", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction1, Index = 3 });

			var auction2 = Guid.NewGuid();
			Auctions.Add(new Auction { Id = auction2, Name = "Seed: Second Auction", VisibilityId = (int)AuctionVisibility.Public, TypeId = (int)AuctionType.Single, StartDateTime = DateTime.Now.AddHours(-1), EndDateTime = DateTime.Now.AddMinutes(10).AddSeconds(22) });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction2 });

			var auction3 = Guid.NewGuid();
			Auctions.Add(new Auction { Id = auction3, Name = "Seed: Third Auction", VisibilityId = (int)AuctionVisibility.Public, TypeId = (int)AuctionType.MultipleSequential, StartDateTime = DateTime.Now.AddDays(-4), EndDateTime = DateTime.Now.AddDays(-3) });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction3, Index = 1 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction3, Index = 2 });

			var auction4 = Guid.NewGuid();
			Auctions.Add(new Auction { Id = auction4, Name = "Seed: Fourth Auction", VisibilityId = (int)AuctionVisibility.Public, TypeId = (int)AuctionType.MultipleOpenHouse, StartDateTime = DateTime.Now.AddDays(3), EndDateTime = DateTime.Now.AddDays(4) });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 3", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 4", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 5", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction4 });

			var auction5 = Guid.NewGuid();
			Auctions.Add(new Auction { Id = auction5, Name = "Seed: Fifth Auction", VisibilityId = (int)AuctionVisibility.Public, TypeId = (int)AuctionType.MultipleOpenHouse, StartDateTime = DateTime.Now.AddHours(3).AddSeconds(17), EndDateTime = DateTime.Now.AddHours(4).AddMinutes(12).AddSeconds(22) });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 2", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 3", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 4", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 5", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 6", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 7", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction5 });

			var auction6 = Guid.NewGuid();
			Auctions.Add(new Auction { Id = auction6, Name = "Seed: Sixth Auction", VisibilityId = (int)AuctionVisibility.Public, TypeId = (int)AuctionType.Single, StartDateTime = DateTime.Now.AddHours(-1), EndDateTime = DateTime.Now.AddHours(2).AddMinutes(10).AddSeconds(22) });
			AuctionItems.Add(new AuctionItem { Id = Guid.NewGuid(), Name = "Seed: Item 1", Payload = PayloadGen.GenerateRandomVehicle(), AuctionId = auction6 });

			Auctions.ForEach(auction =>
			{
				_database.Insert<Auction>(auction);
			});

			AuctionItems.ForEach(auctionItem =>
			{
				_database.Insert<AuctionItem>(auctionItem);
			});
		}
	}
}
