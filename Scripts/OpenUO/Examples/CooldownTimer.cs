// **********
// ServUO - CountdownOption.cs
// **********

using System;
using Server.Gumps;

namespace Server.OpenUO.Examples
{
	public class CoolDownTimer
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8016000,
				Description = 8016001,
				StartDate = new DateTime(2023, 10,3),
				OnTry = RunExample,
				IsEnabled = IsEnabled
			});
		}

		private static void RunExample(Mobile from)
		{
			from.NetState.Send(
				new AddCooldownTimer(
					0xf0c, 0, (float) 25f, 5, 10, null, (short)32, 
					(short)82, (short)118)
				);
			from.NetState.Send(
				new AddCooldownTimer(
					3617, 85, (float) 8f, 0, 10, null, (short)214, 
					(short)812, (short)410)
			);
			from.NetState.Send(
				new AddCooldownTimer(
					4148, 0, (float) 1.8f, 8, 10, null, (short)414, 
					(short)612, (short)210)
			);
		}

		
		public static int IsEnabled()
		{
			return SettingGeneralFlags.CooldownGumpEnabled ? 8000001 : 8000004;
		}
	}
}