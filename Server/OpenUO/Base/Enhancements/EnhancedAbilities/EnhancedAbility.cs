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
using Server;
using Server.Network;

namespace Server.OpenUO.Enhancements
{
	public class ActiveAbility
	{
		public static void UseAbility(NetState ns, PacketReader pvSrc, int version)
		{
			switch (version)
			{
				case 0:
					{
						int slot = pvSrc.ReadInt16();
						int ability = pvSrc.ReadInt16();
						if (ns.Mobile == null)
							return;
						EventSink.InvokeMacroUseActiveBySlot(new MacroUseActiveBySlotEventArgs(ns.Mobile, slot, ability));
						break;
					}
			}
		}
	}
	
	public class ActiveAbilityEventSinks
	{
		public static void Initialize()
		{
			EventSink.MacroUseActiveBySlotMacro += EventSinkOnMacroUseActiveBySlotMacro;
		}

		private static void EventSinkOnMacroUseActiveBySlotMacro(MacroUseActiveBySlotEventArgs e)
		{
			if (e.Mobile is Mobile m)
			{
				if (e.Slot < 0 || e.Slot >= m.OUOActiveAbilities.Count)
				{
					//message slot not found
					m.SendLocalizedMessage(8000030); //"Slot not found");
					return;
				}

				if (e.Ability < 0 || e.Ability >= m.OUOActiveAbilities[e.Slot].Abilities.Count)
				{
					//ability not found
					m.SendLocalizedMessage(8000031); //"Ability not found");
					return;
				}

				if (m.OUOActiveAbilities[e.Slot].Abilities[e.Ability].NextUse > DateTime.UtcNow)
				{
					return;
				}

				m.OUOActiveAbilities[e.Slot].Abilities[e.Ability].Toggle(m, m.OUOActiveAbilities[e.Slot].Owner);
			}
		}
	}
}