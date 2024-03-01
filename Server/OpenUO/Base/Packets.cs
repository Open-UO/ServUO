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
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Server.Items;
using Server.Network;
using Server.OpenUO.Enhancements;
using Server.Targeting;

namespace Server.OpenUO
{
	public abstract class OpenUOEnhancedPacket : Packet
	{
		public override bool OpenUOEnhanced => true;

		private int m_OpenUOID;
		private int m_Version;
		public OpenUOEnhancedPacket(int id, int version) : base(0xC3)
		{
			m_OpenUOID = id;
			m_Version = version;
		}

		/*
		 public OpenUOEnhancedPacket(int id, int length) : base(0xC3, length)
		{
			m_OpenUOID = id;
		}*/

		public override void EnsureCapacity(int length)
		{
			base.EnsureCapacity(length);
			m_Stream.Write((ushort)m_OpenUOID);
			m_Stream.Write((ushort)m_Version);
		}
	}
	

	public class ClilocArgs
	{
		public int Number { get; private set; }
		public string Args { get; private set; }

		public ClilocArgs(int num, string args)
		{
			Number = num;
			Args = args;
			if (Args == null) Args = "";
		}
		
	}
	
	public sealed class OpenUOEnhancedSpellbookContent : OpenUOEnhancedPacket
	{
		public OpenUOEnhancedSpellbookContent(Item item, int graphic, int offset, ulong content, ulong extraContent)
			: base(205, 0)
		{
			EnsureCapacity(31);

			//m_Stream.Write((short)0x1B);

			m_Stream.Write(item.Serial);
			m_Stream.Write((short)graphic);
			m_Stream.Write((short)offset);

			for (var i = 0; i < 8; ++i)
			{
				m_Stream.Write((byte)(content >> (i * 8)));
			}
			
			for (var i = 0; i < 8; ++i)
			{
				m_Stream.Write((byte)(extraContent >> (i * 8)));
			}
		}
	}
	
	public sealed class ActiveAbilitiesCompletePacket : OpenUOEnhancedPacket
	{
		public ActiveAbilitiesCompletePacket( Mobile source ) : base(151, 0)
		{
			short rows = (short) source.OUOActiveAbilities.Count;
			this.EnsureCapacity(5 + ((8 + 26) * rows) + source.OUOActiveAbilities.Sum((a) => a.Name.Length));
			m_Stream.Write( rows );
			for (int i = 0; i < rows; i++)
			{
				short slots = (short) source.OUOActiveAbilities[i].Abilities.Count;
				m_Stream.Write((int)source.OUOActiveAbilities[i].Owner.Serial);
				m_Stream.Write((short)source.OUOActiveAbilities[i].Name.Length);
				m_Stream.WriteAsciiFixed(source.OUOActiveAbilities[i].Name, (short)source.OUOActiveAbilities[i].Name.Length);
				m_Stream.Write(slots);
				for (int j = 0; j < slots; j++)
				{
					var a = source.OUOActiveAbilities[i].Abilities[j];
					m_Stream.Write(a.HUDName);
					m_Stream.Write(a.HUDDescription);
					if (a.HUDArguments == null)
					{
						m_Stream.Write((byte)0);
					}
					else
					{
						m_Stream.Write((byte)a.HUDArguments.Length);
						for (int k = 0; k < a.HUDArguments.Length; k++)
						{
							m_Stream.Write(a.HUDArguments[k].Item1);
							m_Stream.Write((int)(a.HUDArguments[k].Item2 * 1000));
						}
					}

					m_Stream.Write(a.HUDIconLarge);
					m_Stream.Write(((int)a.Cooldown.TotalMilliseconds));
					m_Stream.Write(((int)a.CooldownRemaining.TotalMilliseconds));
					m_Stream.Write((short)a.HUDHue);
					m_Stream.Write((short)a.Charge);
					m_Stream.Write((short)(a.UseNextMove ? 0x1: 0x0));
					if (a.InUseUntil >= DateTime.UtcNow)
					{
						m_Stream.Write(((int) a.InUseDuration.TotalMilliseconds));
						var nextUse = (a.InUseUntil - DateTime.UtcNow);
						m_Stream.Write(((int) nextUse.TotalMilliseconds));
					}
					else
					{
						m_Stream.Write(0);
						m_Stream.Write(0);
					}
				}
				
			}
		}
	}

	/*public sealed class OpenUOFeaturePacket : Packet
	{
		public OpenUOFeaturePacket() : base(0x005, 0)
		{
			//this.EnsureCapacity(1);
			//m_Stream.Write(true);
		}
	}*/

	public sealed class ActiveAbilityUpdatePacket : OpenUOEnhancedPacket
	{
		public ActiveAbilityUpdatePacket( IActiveAbility a ) : base(150, 0)
		{
			this.EnsureCapacity(5 + ((8 + 26)));
			m_Stream.Write( (short) a.Row );
			m_Stream.Write( (short) a.Slot );
			m_Stream.Write(((int)a.Cooldown.TotalMilliseconds));
			m_Stream.Write(((int)a.CooldownRemaining.TotalMilliseconds));
			m_Stream.Write((short)a.HUDHue);
			m_Stream.Write((short)a.Charge);
			m_Stream.Write((short)(a.UseNextMove ? 0x1: 0x0));
			if (a.InUseUntil >= DateTime.UtcNow)
			{
				m_Stream.Write(((int) a.InUseDuration.TotalMilliseconds));
				var nextUse = (a.InUseUntil - DateTime.UtcNow);
				m_Stream.Write(((int) nextUse.TotalMilliseconds));
			}
			else
			{
				m_Stream.Write(0);
				m_Stream.Write(0);
			}

		}
	}
	
	
	public sealed class ClientProfileChangeBoolPacket : OpenUOEnhancedPacket
	{
		public ClientProfileChangeBoolPacket( string option, bool val ) : base(103, 0)
		{
			this.EnsureCapacity(12 + option.Length);// 7 + ushort + ushort + length name + bool
			m_Stream.Write( (ushort) 0 ); // cmd
			m_Stream.Write( (ushort) option.Length );
			m_Stream.WriteAsciiFixed( option, (ushort)option.Length );
			m_Stream.Write(val);
		}
	}
	public sealed class ClientProfileChangeIntPacket : OpenUOEnhancedPacket
	{
		public ClientProfileChangeIntPacket( string option, int val ) : base(103, 0)
		{
			this.EnsureCapacity(15 + option.Length);// 7 + ushort + ushort + length name + bool
			m_Stream.Write( (ushort) 1 ); // cmd
			m_Stream.Write( (ushort) option.Length );
			m_Stream.WriteAsciiFixed( option, (ushort)option.Length );
			m_Stream.Write(val);
		}
	}

	public sealed class RollingTextSimplePacket : OpenUOEnhancedPacket
	{
		public RollingTextSimplePacket( Serial serial, Mobile source, int hue, int cliloc, int val ) : base(101, 0)
		{
			if ( hue == 0 )
				hue = 0x3B2;
			this.EnsureCapacity(23);
			m_Stream.Write( (int) serial );
			m_Stream.Write( (int) (source?.Serial ?? 0) );
			m_Stream.Write( (short) hue );
			m_Stream.Write(cliloc );
			m_Stream.Write(val < 0);
			m_Stream.Write( Math.Abs(val) );
		}
	}

	
	public sealed class RollingTextPacket : OpenUOEnhancedPacket
	{
		public RollingTextPacket( Serial serial, Mobile source, int hue, int font, ClilocArgs[] args ) : base(100, 0)
		{
			if ( hue == 0 )
				hue = 0x3B2;

			int len = 25;
			foreach (var arg in args)
			{
				len += arg.Args.Length * 2;
			}

			this.EnsureCapacity(len);
			m_Stream.Write( (int) serial );
			m_Stream.Write( (int) (source?.Serial ?? 0) );
			m_Stream.Write( (short) hue );
			m_Stream.Write( (byte) font );
			m_Stream.Write( (byte) args.Length);
			for (int i = 0; i < args.Length; i++)
			{
				m_Stream.Write((short) args[i].Args.Length);
				m_Stream.Write( (int) args[i].Number );
				m_Stream.WriteLittleUniNull(args[i].Args);
			}
		}
	}

	
	public sealed class GeneralSettingsPacket : OpenUOEnhancedPacket
	{
		public GeneralSettingsPacket()
			: base(4, 0)
		{
			int extraLength = 0;
			if (GeneralSettings.StoreOverride != null)
				extraLength += 4 + GeneralSettings.StoreOverride.Length;
			EnsureCapacity( 8 + extraLength);// 7 + bool
			if (GeneralSettings.StoreOverride != null)
			{
				m_Stream.Write(GeneralSettings.StoreOverride != null);
				m_Stream.Write((ushort)GeneralSettings.StoreOverride.Length);
				m_Stream.WriteAsciiFixed(GeneralSettings.StoreOverride, GeneralSettings.StoreOverride.Length);
			}
			else
				m_Stream.Write(false);
		}
	}

	
	public sealed class ServerSettingsPacket : OpenUOEnhancedPacket
	{
		public ServerSettingsPacket()
			: base(0x1, 0)
		{
			var clientOptions = GetSettingsBytes(typeof(SettingClientOptionsFlags));
			var generalOptions = GetSettingsBytes(typeof(SettingGeneralFlags));
			var macroOptions = GetSettingsBytes(typeof(SettingsMacrosFlags));
			
			EnsureCapacity(19 + clientOptions.Length + generalOptions.Length + macroOptions.Length); // 7 + size (uint + uint + uint) + length length length
			m_Stream.Write(clientOptions.Length);
			for (int i = 0; i < clientOptions.Length; i++)
				m_Stream.Write(clientOptions[i]);
			
			m_Stream.Write(generalOptions.Length);
			for (int i = 0; i < generalOptions.Length; i++)
				m_Stream.Write(generalOptions[i]);
			
			m_Stream.Write(macroOptions.Length);
			for (int i = 0; i < macroOptions.Length; i++)
				m_Stream.Write(macroOptions[i]);
		}
		private static byte[] GetSettingsBytes(Type typeOfSettings)
		{
			var flags = new SortedDictionary<int, bool>();
			PropertyInfo[] props = typeOfSettings.
				GetProperties(BindingFlags.Static | BindingFlags.Public);

			foreach (var prop in props)
			{
				var id = GetID(prop);
				if (id > -1)
				{
					flags.Add(id, (bool)prop.GetValue(null));
				}
			}

			List<byte> settings = new List<byte>();
			foreach (var kvp in flags)
			{
				var pos = kvp.Key / 8;
				var bit = kvp.Key % 8; 
				while (settings.Count < pos + 1)
				{
					settings.Add(0);
				}
				if (kvp.Value)
				{
					var add = (byte)(1 << bit);
					settings[pos] += add;
				}
			}
			return settings.ToArray();
		}
		
		private static int GetID(PropertyInfo prop)
		{
			object[] attrs = prop.GetCustomAttributes(typeof(OptionIDAttribute), false);

			return attrs.Length > 0 ? (attrs[0] as OptionIDAttribute).ID : -1;
		}
	}

	public sealed class DefaultMoveSpeedPacket : OpenUOEnhancedPacket
	{
		public DefaultMoveSpeedPacket()
			: base(0x2, 0)
		{
			EnsureCapacity(17);// 7 + ushort + ushort + ushort + ushort + ushort
			m_Stream.Write((ushort)MovementSettings.TurnDelay);
			m_Stream.Write((ushort)MovementSettings.MoveSpeedWalkingUnmounted);
			m_Stream.Write((ushort)MovementSettings.MoveSpeedRunningUnmounted);
			m_Stream.Write((ushort)MovementSettings.MoveSpeedWalkingMounted);
			m_Stream.Write((ushort)MovementSettings.MoveSpeedRunningMounted);
		}
	}
	
	public sealed class MagerySpellSettingsPacket : OpenUOEnhancedPacket
	{
		private int CalculateSize()
		{
			int ret = 4;
			foreach (var spell in SpellSettings.MagerySpells)
			{
				ret += 2; // ID
				ret += 1; //byte(circle)
				ret += 2; // gumpiconid
				ret += 3; // cliloc type + tooltip type + powerwords
				if (spell.NameCliloc > 0)
					ret += 4;
				else
					ret += spell.Name.Length + 2;
				
				if (spell.TooltipCliloc > 0)
					ret += 4;
				else
					ret += spell.Tooltip.Length + 2;
				
				if (spell.PowerwordsCliloc > 0)
					ret += 4;
				else
					ret += spell.Powerwords.Length + 2;
				ret += 1;
				ret += spell.Reagents.Length * 4;
				ret += 1; // targetflags
			}

			return ret;
		}
		public MagerySpellSettingsPacket() : base(0x5, 0)
		{
			EnsureCapacity(CalculateSize());
			m_Stream.Write(SpellSettings.MagerySpells.Count);
			foreach (var spell in SpellSettings.MagerySpells)
			{
				m_Stream.Write((ushort)(spell.ID + 1));
				m_Stream.Write((byte)spell.Circle);
				m_Stream.Write((ushort)spell.GumpIconID);
				bool nameIsCliloc = (spell.NameCliloc > 0);
				m_Stream.Write(nameIsCliloc);
				if (nameIsCliloc)
				{
					m_Stream.Write(spell.NameCliloc);
				}
				else
				{
					m_Stream.Write((ushort)spell.Name.Length);
					m_Stream.WriteAsciiFixed(spell.Name, spell.Name.Length);
				}

				bool tooltipIsCliloc = (spell.NameCliloc > 0);
				m_Stream.Write(tooltipIsCliloc);
				if (tooltipIsCliloc)
				{
					m_Stream.Write(spell.TooltipCliloc);
				}
				else
				{
					m_Stream.Write((ushort)spell.Tooltip.Length);
					m_Stream.WriteAsciiFixed(spell.Tooltip, spell.Tooltip.Length);
				}

				bool PowerwordsIsCliloc = (spell.PowerwordsCliloc > 0);
				m_Stream.Write(PowerwordsIsCliloc);
				if (PowerwordsIsCliloc)
				{
					m_Stream.Write(spell.PowerwordsCliloc);
				}
				else
				{
					m_Stream.Write((ushort)spell.Powerwords.Length);
					m_Stream.WriteAsciiFixed(spell.Powerwords, spell.Powerwords.Length);
				}
				m_Stream.Write((byte)spell.Reagents.Length);
				for (int i = 0; i < spell.Reagents.Length; i++)
					m_Stream.Write(spell.Reagents[i]);
				m_Stream.Write((byte)spell.TargetFlags);
			}
		}
	}
	
	public sealed class PotionSettingsPacket : OpenUOEnhancedPacket
	{
		public PotionSettingsPacket() : base(0x3, 0)
		{

			var clilocs = PotionSettings.Potions.Where((p) => p.Cliloc > 0).ToList();
			var strings = PotionSettings.Potions.Where((p) => p.Name != null).ToList();
			
			EnsureCapacity(15 + (clilocs.Count * 6) + (strings.Count * 4) + strings.Sum((p) => p.Name.Length));// 7 + ushort + ushort + ushort + ushort + ushort
			m_Stream.Write((ushort)clilocs.Count);
			foreach (var pot in clilocs)
			{
				m_Stream.Write((ushort)pot.ID);
				m_Stream.Write(pot.Cliloc);
			}
			m_Stream.Write((ushort)strings.Count);
			foreach (var pot in strings)
			{
				m_Stream.Write((ushort)pot.ID);
				m_Stream.Write((ushort)pot.Name.Length);
				m_Stream.WriteAsciiFixed(pot.Name, (ushort)pot.Name.Length);
			}
		}
	}
	
	public sealed class CloseContainerPacket : OpenUOEnhancedPacket
	{
		public CloseContainerPacket( Container container) : base(206, 0)
		{
			this.EnsureCapacity( 5 + 4);
			m_Stream.Write( container.Serial.Value );
		}
	}
	
	public sealed class AddCooldownTimer : OpenUOEnhancedPacket
	{
		public AddCooldownTimer( int itemID, short itemHue, float timeInSeconds, int offsetX, int offsetY, string text,
			short circleHue, short textHue, short countdownHue) : base(110, 0)
		{
			this.EnsureCapacity( 29 +  (text == null ? 0 : text.Length));
			// 7 + int + short + int + short + short + short + short + short + short + length
			// 29
			m_Stream.Write( itemID );
			m_Stream.Write( (short) itemHue);
			m_Stream.Write( (int) (timeInSeconds * 100));
			m_Stream.Write( (short) offsetX);
			m_Stream.Write( (short) offsetY);
			m_Stream.Write( (short) (text == null ? 0 : text.Length));
			if (text != null && text.Length != 0)
				m_Stream.WriteAsciiFixed(text, text.Length);
			m_Stream.Write( circleHue );
			m_Stream.Write(textHue);
			m_Stream.Write(countdownHue);
		}
	}
	public sealed class PlayableAreaClear : OpenUOEnhancedPacket
	{
		public PlayableAreaClear( ) : base( 120, 0 )
		{
			EnsureCapacity(9);
			// 7 + short
			m_Stream.Write((short)0);
		}
	}
	[Flags]
	public enum HighlightType : ushort
	{
		Item = 0x1,
		Land = 0x2,
		Mobile = 0x4,
		Multi = 0x8,
		Static = 0x10,
	}
	
	public sealed class HighlightAreaPacket : OpenUOEnhancedPacket
	{
		public HighlightAreaPacket( Rectangle2D rect, HighlightType type, short hue, byte priority ) : base( 121, 0 )
		{
			EnsureCapacity(22);
			// 7 + short(7) + byte
			m_Stream.Write((short)1);
			m_Stream.Write((short)rect.X);
			m_Stream.Write((short)rect.Y);
			m_Stream.Write((short)rect.Width);
			m_Stream.Write((short)rect.Height);
			m_Stream.Write((short)hue);
			m_Stream.Write((ushort)type);
			m_Stream.Write(priority);

		}
	}
	public sealed class HighlightAreaRemovePacket : OpenUOEnhancedPacket
	{
		public HighlightAreaRemovePacket( Rectangle2D rect, HighlightType type, short hue, byte priority ) : base( 121, 0 )
		{
			EnsureCapacity(22);
			// 7 + short(7) + byte
			m_Stream.Write((short)2);
			m_Stream.Write((short)rect.X);
			m_Stream.Write((short)rect.Y);
			m_Stream.Write((short)rect.Width);
			m_Stream.Write((short)rect.Height);
			m_Stream.Write((short)hue);
			m_Stream.Write((ushort)type);
			m_Stream.Write(priority);

		}
	}
	
	public sealed class HighlightAreaClearAllPacket : OpenUOEnhancedPacket
	{
		public HighlightAreaClearAllPacket( ) : base( 121, 0 )
		{
			EnsureCapacity(9);
			m_Stream.Write((short)3);
		}
	}
	
	public sealed class PlayableAreaSet : OpenUOEnhancedPacket
	{
		public PlayableAreaSet( bool blocking, int hue, List<Rectangle2D> areas) : base( 120, 0 )
		{
			EnsureCapacity(18 + (8 * areas.Count));
			//7 + int, short, bool, short, short + areas
			m_Stream.Write((short)1);
			m_Stream.Write(blocking);
			m_Stream.Write((short)hue);
			m_Stream.Write((short)areas.Count);
			for (int i = 0; i < areas.Count; i++)
			{
				m_Stream.Write((short)areas[i].X);
				m_Stream.Write((short)areas[i].Y);
				m_Stream.Write((short)areas[i].Width);
				m_Stream.Write((short)areas[i].Height);
			}
		}
	}
	
	public enum EffectType
	{
		Moving    = 0x00,
		Lightning = 0x01,
		FixedXYZ  = 0x02,
		FixedFrom = 0x03,
		FlashEffect = 0x04,
		MovingTimed = 0x40,
		MovingMultiPointTimed = 0x41,
	}
	
	public sealed class MovingEffectTimed : OpenUOEnhancedPacket
	{
		public MovingEffectTimed( NetState state, IEntity from, IEntity to, int itemID, int speed, int duration, short fixedDirection, 
			bool explodes, int hue, int renderMode, int effect, int explodeEffect, int explodeSound, EffectLayer layer, 
			int unknown, TimeSpan durationTimeSpan, short spinning ) : 
			this( from, to, itemID, speed, duration, fixedDirection, 
			explodes, hue, renderMode, effect, explodeEffect, explodeSound, layer, unknown, durationTimeSpan, spinning)
		{
		}
		
		public MovingEffectTimed( IEntity from, IEntity to, int itemID, int speed, int duration, short fixedDirection, 
			bool explodes, int hue, int renderMode, int effect, int explodeEffect, int explodeSound, EffectLayer layer, 
			int unknown, TimeSpan durationTimeSpan, short spinning ) : base(200, 0)
		{
			this.EnsureCapacity( 60 );
			// 7 + byte + int(2) + short(3) + byte + short(2) + byte(3) + short + bool + int(2) + short(3) + int + byte + short + int + short
			
			m_Stream.Write((byte) EffectType.MovingTimed);
			
			var fromPoint = from.Location;
			var toPoint = to.Location;
			m_Stream.Write( (int) from.Serial );
			m_Stream.Write( (int) to.Serial );
			m_Stream.Write( (short) itemID );
			m_Stream.Write( (short) fromPoint.m_X );
			m_Stream.Write( (short) fromPoint.m_Y );
			m_Stream.Write( (sbyte) fromPoint.m_Z );
			m_Stream.Write( (short) toPoint.m_X );
			m_Stream.Write( (short) toPoint.m_Y );
			m_Stream.Write( (sbyte) toPoint.m_Z );
			m_Stream.Write( (byte) speed );
			m_Stream.Write( (byte) duration );
			m_Stream.Write((short) fixedDirection);
			

			m_Stream.Write( (bool) explodes );
			m_Stream.Write( (int) hue );
			m_Stream.Write( (int) renderMode );
			m_Stream.Write( (short) effect );
			m_Stream.Write( (short) explodeEffect );
			m_Stream.Write( (short) explodeSound );
			m_Stream.Write( (int) Serial.Zero );
			m_Stream.Write( (byte) layer );
			m_Stream.Write( (short) unknown );
			m_Stream.Write((int) (durationTimeSpan.TotalMilliseconds));
			m_Stream.Write((short) spinning);
		
		}
	}
	
	public sealed class MovingEffectMultiPointTimed : OpenUOEnhancedPacket
	{
		public MovingEffectMultiPointTimed( NetState state, IEntity from, int itemID, int speed, int duration, short direction, 
			bool explodes, int hue, int renderMode, int effect, int explodeEffect, int explodeSound, EffectLayer layer, 
			int unknown, short spinning, List<Tuple<TimeSpan, Point3D>> points) : base(201, 0)
		{
			this.EnsureCapacity( 49 + (points.Count * 9) );
			// 7 + byte + int + short(3) + byte(3) + short + byte + int(2) + short(3) + int + byte + short(3) + 9 * count
			// 7 + 1 + 4 + 6 + 3 + 2 + 1 + 8 + 6 + 4 + 1 + 6
			m_Stream.Write((byte) EffectType.MovingMultiPointTimed);
			
			var fromPoint = from.Location;
			m_Stream.Write( (int) from.Serial );
			m_Stream.Write( (short) itemID );
			m_Stream.Write( (short) fromPoint.m_X );
			m_Stream.Write( (short) fromPoint.m_Y );
			m_Stream.Write( (sbyte) fromPoint.m_Z );
			m_Stream.Write( (byte) speed );
			m_Stream.Write( (byte) duration );
			m_Stream.Write( (short) direction );
			m_Stream.Write( (bool) explodes );
			m_Stream.Write( (int) hue );
			m_Stream.Write( (int) renderMode );
			m_Stream.Write( (short) effect );
			m_Stream.Write( (short) explodeEffect );
			m_Stream.Write( (short) explodeSound );
			m_Stream.Write( (int) Serial.Zero );
			m_Stream.Write( (byte) layer );
			m_Stream.Write( (short) unknown );
			m_Stream.Write((short) spinning);
			m_Stream.Write((short)points.Count);
			for (int i = 0; i < points.Count; i++)
			{
				m_Stream.Write((int)points[i].Item1.TotalMilliseconds);
				m_Stream.Write((short)points[i].Item2.X);
				m_Stream.Write((short)points[i].Item2.Y);
				m_Stream.Write((sbyte)points[i].Item2.Z);
			}
		}
	}

}