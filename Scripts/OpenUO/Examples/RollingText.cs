﻿#region license

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

namespace Server.OpenUO.Examples
{
	public class RollingText
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8011000,
				Description = 8011001,
				StartDate = new DateTime(2023, 7,2),
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
			m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011100, -50));
			Timer.DelayCall(TimeSpan.FromSeconds(0.4), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011101, -5));
			});
			
			Timer.DelayCall(TimeSpan.FromSeconds(0.9), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011102, -5));
			});
			Timer.DelayCall(TimeSpan.FromSeconds(1.2), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 0x42, 8011102, 5));
			});
			
			Timer.DelayCall(TimeSpan.FromSeconds(1.6), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 0x42, 8011101, 5));
			});
			
			Timer.DelayCall(TimeSpan.FromSeconds(2.3), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011103, -5));
			});
			
			Timer.DelayCall(TimeSpan.FromSeconds(2.8), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011104, -5));
			});
			Timer.DelayCall(TimeSpan.FromSeconds(3.2), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011105, -5));
			});
			
			
			Timer.DelayCall(TimeSpan.FromSeconds(3.8), () =>
			{
				m.Send(new RollingTextSimplePacket(m.Serial, m, 32, 8011106, -5));
			});
			Timer.DelayCall(TimeSpan.FromSeconds(4.3), () =>
			{
				m.Send(new OpenUO.RollingTextPacket(m.Serial, m, 0x42, 3, new ClilocArgs[] {
					new ClilocArgs(8011107, "5\tTesting")
					}));
			});
			
			
			Timer.DelayCall(TimeSpan.FromSeconds(4.8), () =>
			{
				m.Send(new OpenUO.RollingTextPacket(m.Serial, m, 0x72, 0, new ClilocArgs[] {
					new ClilocArgs(8011108, "5\tTesting")
				}));
			});
		}
	}
}