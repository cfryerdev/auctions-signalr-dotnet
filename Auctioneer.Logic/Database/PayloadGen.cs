using Auctioneer.Logic.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Database
{
	public static class PayloadGen
	{
		private static int MinYear = 2000;

		private static int MaxYear = 2024;

		private static readonly Dictionary<string, string[]> PossibleModelsByMake = new Dictionary<string, string[]> {
			{ "Audi", new [] { "A4", "A6", "A8", "Q3", "Q5" }},
			{ "BMW", new [] { "3 Series", "5 Series", "7 Series", "X3", "X5" }},
			{ "Chevrolet", new [] { "Impala", "Malibu", "Cruz", "Volt", "Equinox" }},
			{ "Ford", new [] { "Fiesta", "Focus", "Mustang", "Explorer", "F-150" }},
			{ "Honda", new [] { "Civic", "Accord", "Insight", "Passport", "Pilot" }},
			{ "Hyundai", new [] { "Elantra", "Sonata", "Tucson", "Santa Fe", "Kona" }},
			{ "Mercedes-Benz", new [] { "A-Class", "C-Class", "E-Class", "S-Class", "GLC" }},
			{ "Toyota", new [] { "Corolla", "Camry", "Prius", "Rav4", "Highlander" }},
			{ "Volkswagen", new [] { "Golf", "Passat", "Tiguan", "Jetta", "Atlas" }},
			{ "Volvo", new [] { "S60", "S90", "V60", "XC60", "XC90" }}
		};

		private static string[] PossibleTrims = { "Base", "Sport", "Luxury", "Offroad", "Performance", "Premium" };

		private static string GenerateStockNumber()
		{
			var rand = new Random();
			return rand.Next(25000, 85000).ToString();
		}

		public static string GenerateRandomVehicle()
		{
			var rnd = new Random();
			var randomMakeIndex = rnd.Next(PossibleModelsByMake.Count);
			var randomMake = PossibleModelsByMake.ElementAt(randomMakeIndex);
			var randomVehicle = new Vehicle
			{
				Id = GenerateStockNumber(),
				Year = rnd.Next(MinYear, MaxYear),
				Make = randomMake.Key,
				Model = randomMake.Value[rnd.Next(randomMake.Value.Length)],
				Trim = PossibleTrims[rnd.Next(PossibleTrims.Length)]
			};
			return JsonConvert.SerializeObject(randomVehicle);
		}
	}
}
