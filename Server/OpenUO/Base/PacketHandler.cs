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
using System.Net;
using Server.Network;
using Server.OpenUO.Enhancements;

namespace Server.OpenUO
{
	public class PacketHandlers
	{
		
		public static void Initialize()
		{
			Network.PacketHandlers.Register(0xC3, 0, false, OnReceiveOpenUO);
			PacketHandlers.Register(0x0, 0, false, OpenUOHello);
			PacketHandlers.Register(0x1, 0, true, EnhancedSpellcastWithTarget.TargetedSpell);
			PacketHandlers.Register(0x2, 0, true, EnhancedPotionMacro.UsePotion);
			PacketHandlers.Register(0x3, 0, true, ActiveAbility.UseAbility);
			EventSink.ServerList += EventSinkOnServerList;
			EventSink.ServerSelected += EventSinkOnServerSelected;
			
		}

		private static void EventSinkOnServerSelected(SelectServerEventArgs e)
		{
			if (Equals(e.Selected.Address.Address, IPAddress.Parse("1.1.1.1")))
			{
				//OpenUO Selected
				e.State.LaunchBrowser("https://www.openuo.io");
				e.State.Send(new AccountLoginRej(ALRReason.BadComm));
				e.State.Dispose();
			}
			else
			{
				e.State.AuthID = Network.PacketHandlers.GenerateAuthID(e.State);
				e.State.SentFirstPacket = false;
				e.State.Send(new PlayServerAck(e.Selected, e.State.AuthID));
			}
		}

		private static void EventSinkOnServerList(ServerListEventArgs e)
		{
			
			e.Servers.Insert(0, new ServerInfo("RecommendOpenUO", 100, TimeZone.CurrentTimeZone, new IPEndPoint(IPAddress.Parse("1.1.1.1"), 80)));
			//e.State.Send(new OpenUOFeaturePacket());
		}
		
		private static void OpenUOHello(NetState state, PacketReader pvSrc, int version)
		{
			bool isDebug = pvSrc.ReadBoolean();
			short major = pvSrc.ReadInt16();
			short minor = pvSrc.ReadInt16();
			short build = pvSrc.ReadInt16();
			state.OpenUOClient = true;
			Console.WriteLine($"Recieved a connection from OpenUO {(isDebug ? "DEBUG" : "Release")} {major}.{minor}.{build}");
		}

		
		private static void OnReceiveOpenUO(NetState state, PacketReader pvSrc)
		{
			var packetID = pvSrc.ReadUInt16();
			var version = pvSrc.ReadUInt16();
			if (!m_Handlers.ContainsKey(packetID))
			{
				Utility.PushColor(ConsoleColor.Red);
				Console.WriteLine("Client: {0}: Unknown Packet (0x{1:X2}) v{2}", state, packetID, version);
				Utility.PopColor();
				return;
			}
			var handler = m_Handlers[packetID];
			if (handler.Ingame && (state.Mobile == null || state.Mobile.Deleted))
			{
				Utility.PushColor(ConsoleColor.Red);
				Console.WriteLine("Client: {0}: Packet (0x{1:X2}) Requires State Mobile", state, packetID);
				Utility.PopColor();
				state.Dispose();
				return;
			}
			handler.OnReceive(state, pvSrc, version);
		}
		
		public delegate void OnPacketReceiveOpenUO(NetState state, PacketReader pvSrc, int version);
		private static readonly Dictionary<ushort, PacketHandlerOpenUO> m_Handlers = new Dictionary<ushort, PacketHandlerOpenUO>();
		
		public static void Register(ushort packetID, int length, bool ingame, OnPacketReceiveOpenUO onReceive)
		{
			m_Handlers.Add(packetID, new PacketHandlerOpenUO(packetID, length, ingame, onReceive));
		}
		
		public class PacketHandlerOpenUO
		{
			private readonly int m_PacketID;
			private readonly int m_Length;
			private readonly bool m_Ingame;
			private readonly OnPacketReceiveOpenUO m_OnReceive;

			public PacketHandlerOpenUO(int packetID, int length, bool ingame, OnPacketReceiveOpenUO onReceive)
			{
				m_PacketID = packetID;
				m_Length = length;
				m_Ingame = ingame;
				m_OnReceive = onReceive;
			}

			public int PacketID => m_PacketID;

			public int Length => m_Length;

			public OnPacketReceiveOpenUO OnReceive => m_OnReceive;

			public ThrottlePacketCallback ThrottleCallback { get; set; }

			public bool Ingame => m_Ingame;
		}
	}
}