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

using Server.Network;
using Server.OpenUO;
using Server.Targeting;

namespace Server.OpenUO
{
	public sealed class EnhancedTargetExtra : OpenUOEnhancedPacket
	{
		public EnhancedTargetExtra( Target t ) : base(102, 0)
		{	
			EnsureCapacity(17);
			//m_Stream.Write( (bool) t.AllowGround );
			m_Stream.Write( t.TargetID );
			m_Stream.Write((short)t.AreaOfEffectType );
			m_Stream.Write((short)t.AreaOfEffectRange);
			m_Stream.Write((int)t.PreviewID );
			m_Stream.Write((short)t.PreviewHue);
		}
	}
}

namespace Server.Targeting
{
	public abstract partial class Target
	{
		
		public virtual Packet ExtraPacket( NetState ns )
		{
			return new EnhancedTargetExtra( this );
		}
		
		private int m_AreaOfEffectRange = 0;
		private int m_AreaOfEffectType = 0;
		private int m_PreviewID = 0;
		private int m_PreviewHue = 0;

		public int AreaOfEffectRange { get => m_AreaOfEffectRange; set => m_AreaOfEffectRange = value; }
		public int AreaOfEffectType { get => m_AreaOfEffectType; set => m_AreaOfEffectType = value; }
		public int PreviewID { get => m_PreviewID; set => m_PreviewID = value; }
		public int PreviewHue { get => m_PreviewHue; set => m_PreviewHue = value; }
	}
}