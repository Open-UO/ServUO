// **********
// ServUO - PacketHandlers.cs
// **********

using System;
using Server.Network;

namespace Server
{
	public class SelectServerEventArgs : EventArgs
	{
		private readonly ServerInfo m_Selected;
		private readonly NetState m_State;

		public ServerInfo Selected => m_Selected;
		public NetState State => m_State;

		public SelectServerEventArgs(NetState state, ServerInfo server)
		{
			m_Selected = server;
			m_State = state;
		}
	}
	public partial class EventSink
	{
		public delegate void SelectServerEventHandler(SelectServerEventArgs e);

		public static event SelectServerEventHandler ServerSelected;

		public static void InvokeServerSelect(SelectServerEventArgs e)
		{
			if (ServerSelected != null)
			{
				ServerSelected(e);
			}
		}
	}
}

namespace Server.Network
{
	public abstract partial class Packet
	{
		public virtual bool OpenUOEnhanced => false;
	}
	public static partial class PacketHandlers
	{
		
		/*public static uint GenerateAuthID(NetState state)
		{
			if (m_AuthIDWindow.Count == m_AuthIDWindowSize)
			{
				uint oldestID = 0;
				var oldest = DateTime.MaxValue;

				foreach (var kvp in m_AuthIDWindow)
				{
					if (kvp.Value.Age < oldest)
					{
						oldestID = kvp.Key;
						oldest = kvp.Value.Age;
					}
				}

				m_AuthIDWindow.Remove(oldestID);
			}

			uint authID;

			do
			{
				authID = (uint)Utility.RandomMinMax(1, UInt32.MaxValue - 1);

				if (Utility.RandomBool())
				{
					authID |= 1U << 31;
				}
			}
			while (m_AuthIDWindow.ContainsKey(authID));

			m_AuthIDWindow[authID] = new AuthIDPersistence(state.Version, state.OpenUOClient);

			return authID;
		}*/
		
		/*public static void PlayServer(NetState state, PacketReader pvSrc)
		{
			int index = pvSrc.ReadInt16();
			var info = state.ServerInfo;
			var a = state.Account;

			if (info == null || a == null || index < 0 || index >= info.Length)
			{
				Utility.PushColor(ConsoleColor.Red);
				Console.WriteLine("Client: {0}: Invalid Server ({1})", state, index);
				Utility.PopColor();

				state.Dispose();
			}
			else
			{
				ServerInfo si = info[index];
				EventSink.InvokeServerSelect(new SelectServerEventArgs(state, si));
				//state.Send(new PlayServerAck(info[index], state.AuthID));
			}
		}*/
	}
}