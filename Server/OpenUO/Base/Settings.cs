// **********
// ServUO - Settings.cs
// **********

using System;
using Server.Targeting;

namespace Server.OpenUO.Base
{
	public class PotionSetting
	{
		public int ID { get; }
		public int Cliloc { get; } = 0;
		public string Name { get; } = null;
		public Type Type { get; }

		public PotionSetting(int id, string name, Type type)
		{
			Type = type;
			ID = id;
			Name = name;
			PotionSettings.IDToType.Add(ID, type);
		}

		public PotionSetting(int id, int cliloc, Type type)
		{
			Type = type;
			ID = id;
			Cliloc = cliloc;
			PotionSettings.IDToType.Add(ID, type);
		}
	}
	
	public class MagerySpellSetting
	{
		public int ID { get; set; }
		public int GumpIconID { get; set; }
		public int NameCliloc { get; set; } = 0;
		public int TooltipCliloc { get; set; } = 0;
		public int PowerwordsCliloc { get; set; } = 0;
		public int Circle { get; set; }
		public string Name { get; set; } = null;
		public string Powerwords { get; set; } = null;
		public string Tooltip { get; set; }
		public Type Type { get; }
		public int[] Reagents { get; set; }
		public TargetFlags TargetFlags { get; set; }
		
		public MagerySpellSetting(int id, int gumpIconID, int circle, string name, string tooltip, string powerwords, Type type, TargetFlags targetFlags, params int[] regs)
		{
			Tooltip = tooltip;
			GumpIconID = gumpIconID;
			ID = id;
			Circle = circle;
			Name = name;
			Type = type;
			Reagents = regs;
			TargetFlags = targetFlags;
			Powerwords = powerwords;
		}
		public MagerySpellSetting(int id, int gumpIconID, int circle, string name, int tooltip, string powerwords, Type type, TargetFlags targetFlags, params int[] regs)
		{
			TooltipCliloc = tooltip;
			GumpIconID = gumpIconID;
			ID = id;
			Circle = circle;
			Name = name;
			Type = type;
			Reagents = regs;
			TargetFlags = targetFlags;
			Powerwords = powerwords;
		}
		
		public MagerySpellSetting(int id, int gumpIconID, int circle, int name, int tooltip, string powerwords, Type type, TargetFlags targetFlags, params int[] regs)
		{
			TooltipCliloc = tooltip;
			GumpIconID = gumpIconID;
			ID = id;
			Circle = circle;
			NameCliloc = name;
			Type = type;
			Reagents = regs;
			TargetFlags = targetFlags;
			Powerwords = powerwords;
		}

		public MagerySpellSetting(int id, int gumpIconID, int circle, int nameCliloc, int tooltipCliloc, int powerwordsCliloc, Type type, TargetFlags targetFlags, params int[] regs)
		{
			GumpIconID = gumpIconID;
			ID = id;
			Circle = circle;
			NameCliloc = nameCliloc;
			TooltipCliloc = tooltipCliloc;
			Type = type;
			Reagents = regs;
			TargetFlags = targetFlags;
			PowerwordsCliloc = powerwordsCliloc;
		}
	}
}