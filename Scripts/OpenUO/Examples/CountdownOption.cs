// **********
// ServUO - CountdownOption.cs
// **********

using System;
using Server.Gumps;

namespace Server.OpenUO.Examples
{
	public class CountdownOption
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8015000,
				Description = 8015001,
				StartDate = new DateTime(2023, 10,2),
				OnTry = RunExample,
				IsEnabled = IsEnabled
			});
		}

		private static void RunExample(Mobile from)
		{
			from.SendGump(new CountdownOptionsGump(from));
		}
		
		public static int IsEnabled()
		{
			return 8000001;
		}
	}

	public class CountdownOptionsGump : Gump
	{
		public void AddBlackAlpha(int x, int y, int width, int height)
		{
			AddImageTiled(x, y, width, height, 2624);
			AddAlphaRegion(x, y, width, height);
		}
		
		public CountdownOptionsGump(Mobile m) : base(350, 350)
		{
			Closable = true;
			Dragable = true;
			AddPage(1);
			
			AddBackground(0, 0, 420, 240, 5054);

			AddBlackAlpha(0, 0, 420, 240);
			
			AddHtmlLocalized(20, 20, 420, 20, 8015002, $"", 0, false, false);
			AddHtmlLocalized(20, 100, 420, 20, 8015003, $"2\t500", 0, false, false);
			AddHtmlLocalized(20, 140, 420, 20, 8015003, $"1\t500", 0, false, false);
			
			AddItem(180,50, 7939);
			AddTooltip(8015003, $"2\t500");
		}
	}
}