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
using Server.OpenUO.Enhancements;

namespace Server.OpenUO
{
	public class ExampleAbilityGroup
	{
		public static void Initialize()
		{
			ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
			{
				Title = 8000500,
				Description = 8000501,
				StartDate = new DateTime(2023, 7,2),
				OnTry = RunExample,
				IsEnabled = IsEnabled
			});
		}

		public static int IsEnabled()
		{
			return SettingGeneralFlags.EnableEnhancedAbilities ? 8000001 : 8000004;
		}

		public static void RunExample(Mobile m)
		{
			var group = new List<ActiveAbilityGroup>()
			{
				new ActiveAbilityGroup(m)
				{
					Abilities = new List<IActiveAbility>()
					{
						new ExampleActiveAbility(m, 0, 0),
						new ExampleActiveAbilityTwo(m, 0, 1)
					},
					Name = "An Example Ability",
					Owner = m
				}
			};

			m.OUOActiveAbilities = group;
				
			if (m.NetState != null && m.NetState.OpenUOClient && SettingGeneralFlags.EnableEnhancedAbilities)
				m.Send(new ActiveAbilitiesCompletePacket(m));
			ChargesTimer(group);
		}

		/// <summary>
		/// Example for increasing the charges. 
		/// </summary>
		/// <param name="group"></param>
		private static void ChargesTimer(List<ActiveAbilityGroup> group)
		{
			if (group[0].Owner is Mobile m && (m.NetState == null || !m.NetState.OpenUOClient || !SettingGeneralFlags.EnableEnhancedAbilities || group != m.OUOActiveAbilities))
				return;
				
			var ability = group[0].Abilities[0];
			if (ability.Charge < 20)
			{
				ability.Charge++;
				ability.Owner.Send(new ActiveAbilityUpdatePacket(ability));
			}

			Timer.DelayCall(TimeSpan.FromSeconds(2), () => ChargesTimer(group));
		}
	}
}