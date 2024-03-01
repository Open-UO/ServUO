#region license

// Copyright (c) 2023, OpenUO
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.
// 3. All advertising materials mentioning features or use of this software
//    must display the following acknowledgement:
//    This product includes software developed by ultima-tony - https://github.com/ultima-tony
// 4. Neither the name of the copyright holder nor the
//    names of its contributors may be used to endorse or promote products
//    derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion
using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Gumps;
using Server.Network;

namespace Server.OpenUO
{
	public class ExampleGump : Gump
	{
		public static List<ExampleCampaign> ExampleCampaigns = new List<ExampleCampaign>();
		
		public static void Initialize()
		{
			CommandSystem.Register("OpenUOExamples", AccessLevel.GameMaster, OpenUOExamples_OnCommand);
			EventSink.Login += EventSinkOnLogin;

			Timer.DelayCall(TimeSpan.FromSeconds(1),
				() => ExampleCampaigns.Sort((a, b) => b.StartDate.CompareTo(a.StartDate)));
		}

		private static void EventSinkOnLogin(LoginEventArgs e)
		{
			Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
			{
				if (e.Mobile.NetState != null && e.Mobile.NetState.OpenUOClient)
					e.Mobile.SendGump(new ExampleGump(e.Mobile));
			});
		}


		private static void OpenUOExamples_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendGump(new ExampleGump(e.Mobile));
		}


		private int m_Page = 0;
		private Mobile m_Mobile;
		
		public ExampleGump(Mobile m) : base(50, 50)
		{
			m_Mobile = m;
			Closable = true;
			Dragable = true;
			AddPage(1);
			BuildGump();
		}

		public void BuildGump()
		{
			Entries.Clear();
			AddBackground(0, 0, 420, 440, 5054);

			AddBlackAlpha(0, 0, 420, 440);
			
			AddHtmlLocalized(20, 60, 200, 20, 8000002, $"{ExampleCampaigns[m_Page].StartDate:d}", 0, false, false);
			
			AddHtmlLocalized(165, 60, 200, 20, 8000003, $"{m_Page + 1}\t{ExampleCampaigns.Count}", 0, false, false);

			AddButton(368, 60, 0x15E3, 0x15E7, 2, GumpButtonType.Reply, 0);
			AddButton(385, 60, 0x15E1, 0x15E5, 3, GumpButtonType.Reply, 0);
			
			AddHtmlLocalized(20, 5, 380, 150, 8000000, false, false);
			AddHtmlLocalized(20, 35, 380, 150, ExampleCampaigns[m_Page].Title, false, false);
			
			AddHtmlLocalized(20, 80, 380, 310, ExampleCampaigns[m_Page].Description, true, true);

			bool isTestableFromGump = ExampleCampaigns[m_Page].OnTry != null; 

			int isAvailable = ExampleCampaigns[m_Page].IsEnabled == null ? 8000006 : ExampleCampaigns[m_Page].IsEnabled.Invoke();
			if (isTestableFromGump)
			{
				AddHtmlLocalized(50, 410, 320, 20, isAvailable, false, false);
				if (isAvailable == 8000001)
					AddButton(372, 408, 4005, 4007, 1, GumpButtonType.Reply, 0);
				else
					AddImage(372, 408, 0xFB1);
			}
			//AddBlackAlpha(190, 10, 220, 100);
			//AddBlackAlpha(10, 120, 400, 260);
			//AddBlackAlpha(10, 390, 400, 40);
			//AddBackground(0,0,420,440,0x238C);
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			switch (info.ButtonID)
			{
				case 1:
				{
					ExampleCampaigns[m_Page].OnTry?.Invoke(m_Mobile);
					BuildGump();
					m_Mobile.SendGump(this);
					break;
				}
				case 2:
				{ 
					m_Page = Math.Max(0, m_Page - 1);
					BuildGump();
					m_Mobile.SendGump(this);
					break;
				}
				case 3:
				{
					m_Page = Math.Min(ExampleCampaigns.Count - 1, m_Page + 1);
					BuildGump();
					m_Mobile.SendGump(this);
					break;
				}
			}
			base.OnResponse(sender, info);
		}

		public void AddBlackAlpha(int x, int y, int width, int height)
		{
			AddImageTiled(x, y, width, height, 2624);
			AddAlphaRegion(x, y, width, height);
		}
	}
}