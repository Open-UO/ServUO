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
	public class MultipointEffect
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8020000,
				Description = 8020001,
				StartDate = new DateTime(2023, 10,4),
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
			m.Send(new MovingEffectMultiPointTimed(m.NetState, m, 0xEEF, 30, 80, -1, false, 0, 0, 0, 0, 0,
				EffectLayer.Head, 0, 360, new List<Tuple<TimeSpan, Point3D>>()
				{
					new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(0.5), new Point3D(m.Location.X, m.Location.Y, m.Location.Z + 20)),
					new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(0.5), new Point3D(m.Location.X, m.Location.Y, m.Location.Z + 40)),
					new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(0.5), new Point3D(m.Location.X, m.Location.Y, m.Location.Z + 20)),
					new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(0.5), new Point3D(m.Location.X, m.Location.Y, m.Location.Z)),
				}));
			for (int i = 0; i < 50; i++)
			{
				
				m.Send(new MovingEffectMultiPointTimed(m.NetState, m, 0x36F4, 30, 80, -1, false, 0, 0, 0, 0, 0,
					EffectLayer.Head, 0, (short)Utility.RandomMinMax(50,300), new List<Tuple<TimeSpan, Point3D>>()
					{
						new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(Utility.RandomMinMax(100,300)/200d), new Point3D(m.Location.X - 5 + (i/5), m.Location.Y + 5 - (i/5), m.Location.Z + 20 + Utility.RandomMinMax(10,30) + i % 5)),
						new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(Utility.RandomMinMax(100,300)/100d), new Point3D(m.Location.X - 5 + (i/5), m.Location.Y + 5 - (i/5), m.Location.Z + 40 + Utility.RandomMinMax(10,30) + i % 5)),
						new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(Utility.RandomMinMax(100,300)/200d), new Point3D(m.Location.X - 5 + (i/5), m.Location.Y + 5 - (i/5), m.Location.Z + 20 + Utility.RandomMinMax(10,30) + i % 5)),
						new Tuple<TimeSpan, Point3D>(TimeSpan.FromSeconds(Utility.RandomMinMax(100,300)/200d), new Point3D(m.Location.X - 5 + (i/5), m.Location.Y + 5 - (i/5), m.Location.Z )),
					}));
			}
			
		}
	}
}