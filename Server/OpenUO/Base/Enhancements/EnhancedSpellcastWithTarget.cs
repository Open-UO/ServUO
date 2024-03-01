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
using Server.Network;

namespace Server.OpenUO.Enhancements
{
	public class EnhancedSpellcastWithTarget
	{
		public static void TargetedSpell(NetState ns, PacketReader pvSrc, int version)
		{
			switch (version)
			{
				case 0:
				{
					var spellId = (short)(pvSrc.ReadInt16() - 1);    // zero based;
					int target = pvSrc.ReadInt32();
					int targetBeneficial = pvSrc.ReadInt32();

					EventSink.InvokeEnhancedTargetedSpell(
						new EnhancedTargetedSpellEventArgs(
							ns.Mobile, 
							World.FindEntity(target), 
							World.FindEntity(targetBeneficial), 
							spellId));
					break;
				}
			}
		}

	}
}

namespace Server
{
	public delegate void EnhancedTargetedSpellEventHandler(EnhancedTargetedSpellEventArgs e);
	public class EnhancedTargetedSpellEventArgs : EventArgs
	{
		private readonly Mobile m_Mobile;
		private readonly IEntity m_HostileTarget;
		private readonly IEntity m_BeneficialTarget;
		private readonly short m_SpellID;

		public Mobile Mobile => m_Mobile;
		public IEntity HostileHostileTarget => m_HostileTarget;
		public IEntity BeneficialTarget => m_BeneficialTarget;
		public short SpellID => m_SpellID;

		public EnhancedTargetedSpellEventArgs(Mobile m, IEntity hostileTarget, IEntity beneficial, short spellID)
		{
			m_Mobile = m;
			m_HostileTarget = hostileTarget;
			m_BeneficialTarget = beneficial;
			m_SpellID = spellID;
		}
	}

	public partial class EventSink
	{
		public static event EnhancedTargetedSpellEventHandler EnhancedTargetedSpell;
		public static void InvokeEnhancedTargetedSpell(EnhancedTargetedSpellEventArgs e)
		{
			if (EnhancedTargetedSpell != null)
			{
				EnhancedTargetedSpell(e);
			}
		}
	}
}