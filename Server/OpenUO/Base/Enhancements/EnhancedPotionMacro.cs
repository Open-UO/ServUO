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
using Server.OpenUO;

namespace Server.OpenUO.Enhancements
{
	public class EnhancedPotionMacro
	{
		public static void UsePotion(NetState ns, PacketReader pvSrc, int version)
		{
			switch (version)
			{
				case 0:
					{
						var potionID = pvSrc.ReadUInt16(); // zero based;
						EventSink.InvokeEnhancedPotionUse(
							new EnhancedPotionEventArgs(
								ns.Mobile,
								potionID));
						break;
					}
			}
		}
	}
}

namespace Server
{
	public delegate void EnhancedPotionEventHandler(EnhancedPotionEventArgs e);
	public class EnhancedPotionEventArgs : EventArgs
	{
		private readonly Mobile m_Mobile;
		private readonly ushort m_PotionID;
		private readonly Type m_Type;

		public Mobile Mobile => m_Mobile;
		public ushort ID => m_PotionID;
		public Type Type => m_Type;

		public EnhancedPotionEventArgs(Mobile m, ushort potionID)
		{
			m_Mobile = m;
			m_PotionID = potionID;	
			if (PotionSettings.IDToType.TryGetValue(m_PotionID, out var value))
				m_Type = value;
		}
	}

	public partial class EventSink
	{
		public static event EnhancedPotionEventHandler EnhancedPotionUseSpecific;
		public static void InvokeEnhancedPotionUse(EnhancedPotionEventArgs e)
		{
			if (EnhancedPotionUseSpecific != null)
			{
				EnhancedPotionUseSpecific(e);
			}
		}
	}
}