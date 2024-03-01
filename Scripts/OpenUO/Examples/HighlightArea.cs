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

namespace Server.OpenUO.Examples
{
	public class HighlightArea
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8019000,
				Description = 8019001,
				StartDate = new DateTime(2023, 10,3),
				OnTry = RunExample,
				IsEnabled = IsEnabled
			});
		}

		public static int IsEnabled()
		{
			return 8000001;
		}

		public static void RunExample(Mobile m)
		{
			m.Send(new HighlightAreaPacket(new Rectangle2D(m.Location.X-10,m.Location.Y-10,10,10), HighlightType.Item | HighlightType.Land | HighlightType.Mobile | HighlightType.Multi | HighlightType.Static, 32, 1));
			m.Send(new HighlightAreaPacket(new Rectangle2D(m.Location.X,m.Location.Y-10,10,10), HighlightType.Item | HighlightType.Land | HighlightType.Mobile | HighlightType.Multi | HighlightType.Static, 84, 1));
			m.Send(new HighlightAreaPacket(new Rectangle2D(m.Location.X + 10,m.Location.Y-10,10,10), HighlightType.Item | HighlightType.Land | HighlightType.Mobile | HighlightType.Multi | HighlightType.Static, 843, 1));
			
			m.Send(new HighlightAreaPacket(new Rectangle2D(m.Location.X-10,m.Location.Y,10,10), HighlightType.Item | HighlightType.Land | HighlightType.Mobile | HighlightType.Multi | HighlightType.Static, 333, 1));
			m.Send(new HighlightAreaPacket(new Rectangle2D(m.Location.X,m.Location.Y,10,10), HighlightType.Item | HighlightType.Mobile | HighlightType.Multi | HighlightType.Static, 444, 1));
			m.Send(new HighlightAreaPacket(new Rectangle2D(m.Location.X + 10,m.Location.Y,10,10), HighlightType.Item | HighlightType.Land | HighlightType.Multi | HighlightType.Static, 843, 1));
			Timer.DelayCall(TimeSpan.FromSeconds(30), () =>
			{
				m.Send(new HighlightAreaClearAllPacket());
			});
		}
	}
}