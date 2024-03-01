// **********
// ServUO - StoreOption.cs
// **********

using System;

namespace Server.OpenUO.Examples
{
	public class Store
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8017000,
				Description = 8017001,
				StartDate = new DateTime(2023, 10,3),
				OnTry = RunExample,
				IsEnabled = IsEnabled
			});
		}

		private static void RunExample(Mobile from)
		{
			
		}

		public static int IsEnabled()
		{
			return 8000005;
		}
	}
}