using System;
using Server.OpenUO;
using Server.OpenUO.Base;
using Server.Targeting;

namespace Server.Spells
{
    public class Initializer
    {
	    private class Reagents
	    {
		    public const int Bloodmoss = 1015004;
		    public const int Nightshade = 1015016;
		    public const int Garlic = 1015021;
		    public const int Ginseng = 1015009;
		    public const int MandrakeRoot = 1015013;
		    public const int SpidersSilk = 1015007;
		    public const int SulfurousAsh = 1044359;
		    public const int BlackPearl = 1015001;
	    }
        public static void Initialize()
        {
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(0, 0x1B58,1, 1015164, 1061290, "Uus Jux", typeof(First.ClumsySpell), TargetFlags.Harmful,
			        Reagents.Bloodmoss, Reagents.Nightshade ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(1, 0x1B59,1, 1015165, 1061291, "In Mani Ylem", typeof(First.CreateFoodSpell), TargetFlags.None,
			        Reagents.Garlic, Reagents.Ginseng, Reagents.MandrakeRoot ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(2, 0x1B5A,1, 1015166, 1061292, "Rel Wis", typeof(First.FeeblemindSpell), TargetFlags.Harmful,
			        Reagents.Nightshade, Reagents.Ginseng ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(3, 0x1B5B,1, 1015011, 1061293, "In Mani", typeof(First.HealSpell), TargetFlags.Beneficial,
			        Reagents.Garlic, Reagents.Ginseng, Reagents.SpidersSilk ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(4, 0x1B5C,1, 1015167, 1061294, "In Por Ylem", typeof(First.MagicArrowSpell), TargetFlags.Harmful,
			        Reagents.SulfurousAsh ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(5, 0x1B5D,1, 1015168, 1061295, "In Lor", typeof(First.NightSightSpell), TargetFlags.Beneficial,
			        Reagents.SpidersSilk, Reagents.SulfurousAsh ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(6, 0x1B5E,1, 1015169, 1061296, "Flam Sanct", typeof(First.ReactiveArmorSpell), TargetFlags.Beneficial,
			        Reagents.Garlic, Reagents.SpidersSilk, Reagents.SulfurousAsh ));
	        SpellSettings.MagerySpells.Add(
		        new MagerySpellSetting(7, 0x1B5F,1, 1015170, 1061297, "Des Mani", typeof(First.WeakenSpell), TargetFlags.Harmful,
			        Reagents.Garlic, Reagents.Nightshade ));
	        
            // First circle
            Register(00, typeof(First.ClumsySpell));
            Register(01, typeof(First.CreateFoodSpell));
            Register(02, typeof(First.FeeblemindSpell));
            Register(03, typeof(First.HealSpell));
            Register(04, typeof(First.MagicArrowSpell));
            Register(05, typeof(First.NightSightSpell));
            Register(06, typeof(First.ReactiveArmorSpell));
            Register(07, typeof(First.WeakenSpell));

			
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(8, 0x1B60,2, 1015005, 1061298, "Ex Uus", typeof(Second.AgilitySpell), TargetFlags.Beneficial,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(9, 0x1B61,2, 1015172, 1061299, "Uus Wis", typeof(Second.CunningSpell), TargetFlags.Beneficial,
		            Reagents.Nightshade, Reagents.MandrakeRoot ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(10, 0x1B62,2, 1015174, 1061300, "An Nox", typeof(Second.CureSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.Ginseng ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(11, 0x1B63,2, 1015173, 1061301, "An Mani", typeof(Second.HarmSpell), TargetFlags.Harmful,
		            Reagents.Nightshade, Reagents.SpidersSilk ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(12, 0x1B64,2, 1015174, 1061302, "In Jux", typeof(Second.MagicTrapSpell), TargetFlags.None,
		            Reagents.Garlic, Reagents.SpidersSilk, Reagents.SulfurousAsh ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(13, 0x1B65,2, 1015175, 1061303, "An Jux", typeof(Second.RemoveTrapSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.SulfurousAsh ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(14, 0x1B66,2, 1015176, 1061304, "Uus Sanct", typeof(Second.ProtectionSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.Ginseng, Reagents.SulfurousAsh ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(15, 0x1B67,2, 1015014, 1061305, "Uus Mani", typeof(Second.StrengthSpell), TargetFlags.Beneficial,
		            Reagents.MandrakeRoot, Reagents.Nightshade ));
            
            // Second circle
            Register(08, typeof(Second.AgilitySpell));
            Register(09, typeof(Second.CunningSpell));
            Register(10, typeof(Second.CureSpell));
            Register(11, typeof(Second.HarmSpell));
            Register(12, typeof(Second.MagicTrapSpell));
            Register(13, typeof(Second.RemoveTrapSpell));
            Register(14, typeof(Second.ProtectionSpell));
            Register(15, typeof(Second.StrengthSpell));

            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(16, 0x1B68,3, 1015178, 1061306, "Rel Sanct", typeof(Third.BlessSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.MandrakeRoot
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(17, 0x1B69,3, 1015179, 1061307, "Vas Flam", typeof(Third.FireballSpell), TargetFlags.Harmful,
		            Reagents.BlackPearl
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(18, 0x1B6A,3, 1015180, 1061308, "An Por", typeof(Third.MagicLockSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.Garlic, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(19, 0x1B6B,3, 1015018, 1061309, "In Nox", typeof(Third.PoisonSpell), TargetFlags.Harmful,
		            Reagents.Nightshade, 1015018
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(20, 0x1B6C,3, 1015181, 1061310, "Ort Por Ylem", typeof(Third.TelekinesisSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(21, 0x1B6D,3, 1015182, 1061311, "Rel Por", typeof(Third.TeleportSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(22, 0x1B6E,3, 1015183, 1061312, "Ex Por", typeof(Third.UnlockSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(23, 0x1B6F,3, 1015184, 1061313, "In Sanct Ylem", typeof(Third.WallOfStoneSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.Garlic
	            ));
            
            // Third circle
            Register(16, typeof(Third.BlessSpell));
            Register(17, typeof(Third.FireballSpell));
            Register(18, typeof(Third.MagicLockSpell));
            Register(19, typeof(Third.PoisonSpell));
            Register(20, typeof(Third.TelekinesisSpell));
            Register(21, typeof(Third.TeleportSpell));
            Register(22, typeof(Third.UnlockSpell));
            Register(23, typeof(Third.WallOfStoneSpell));
            
            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(24, 0x1B70,4, 1015186, 1061314, "Vas An Nox", typeof(Fourth.ArchCureSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.Ginseng, Reagents.MandrakeRoot
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(25, 0x1B71,4, 1015187, 1061315, "Vas Uus Sanct", typeof(Fourth.ArchProtectionSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.Ginseng, Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(26, 0x1B72,4, 1015188, 1061316, "Des Sanct", typeof(Fourth.CurseSpell), TargetFlags.Harmful,
		            Reagents.Garlic, Reagents.Nightshade, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(27, 0x1B73,4, 1015189, 1061317, "In Flam Grav", typeof(Fourth.FireFieldSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(28, 0x1B74,4, 1015012, 1061318, "In Vas Mani", typeof(Fourth.GreaterHealSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.Ginseng, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(29, 0x1B75,4, 1015190, 1061319, "Por Ort Grav", typeof(Fourth.LightningSpell), TargetFlags.Harmful,
		            Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(30, 0x1B76,4, 1015191, 1061320, "Ort Rel", typeof(Fourth.ManaDrainSpell), TargetFlags.Harmful,
		            Reagents.BlackPearl, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(31, 0x1B77,4, 1015192, 1061321, "Kal Ort Por", typeof(Fourth.RecallSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.Bloodmoss, Reagents.MandrakeRoot
	            ));

            // Fourth circle
            Register(24, typeof(Fourth.ArchCureSpell));
            Register(25, typeof(Fourth.ArchProtectionSpell));
            Register(26, typeof(Fourth.CurseSpell));
            Register(27, typeof(Fourth.FireFieldSpell));
            Register(28, typeof(Fourth.GreaterHealSpell));
            Register(29, typeof(Fourth.LightningSpell));
            Register(30, typeof(Fourth.ManaDrainSpell));
            Register(31, typeof(Fourth.RecallSpell));
            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(32, 0x1B78,5, 1015194, 1061322, "In Jux Hur Ylem", typeof(Fifth.BladeSpiritsSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.MandrakeRoot, Reagents.Nightshade
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(33, 0x1B79,5, 1015195, 1061323, "An Grav", typeof(Fifth.DispelFieldSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.Garlic, Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(34, 0x1B7A,5, 1015196, 1061324, "Kal In Ex", typeof(Fifth.IncognitoSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.Garlic, Reagents.Nightshade
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(35, 0x1B7B,5, 1015197, 1061325, "In Jux Sanct", typeof(Fifth.MagicReflectSpell), TargetFlags.Beneficial,
		            Reagents.Garlic, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(36, 0x1B7C,5, 1015198, 1061326, "Por Corp Wis", typeof(Fifth.MindBlastSpell), TargetFlags.Harmful,
		            Reagents.BlackPearl, Reagents.MandrakeRoot, Reagents.Nightshade, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(37, 0x1B7D,5, 1015199, 1061327, "An Ex Por", typeof(Fifth.ParalyzeSpell), TargetFlags.Harmful,
		            Reagents.Garlic, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(38, 0x1B7E,5, 1015200, 1061328, "In Nox Grav", typeof(Fifth.PoisonFieldSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.Nightshade, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(39, 0x1B7F,5, 1015201, 1061329, "Kal Xen", typeof(Fifth.SummonCreatureSpell), TargetFlags.None,
                        Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));

            // Fifth circle
            Register(32, typeof(Fifth.BladeSpiritsSpell));
            Register(33, typeof(Fifth.DispelFieldSpell));
            Register(34, typeof(Fifth.IncognitoSpell));
            Register(35, typeof(Fifth.MagicReflectSpell));
            Register(36, typeof(Fifth.MindBlastSpell));
            Register(37, typeof(Fifth.ParalyzeSpell));
            Register(38, typeof(Fifth.PoisonFieldSpell));
            Register(39, typeof(Fifth.SummonCreatureSpell));
            
            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(40, 0x1B80,6, 1015203, 1061330, "An Ort", typeof(Sixth.DispelSpell), TargetFlags.None,
		            Reagents.Garlic, Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(41, 0x1B81,6, 1015204, 1061331, "Corp Por", typeof(Sixth.EnergyBoltSpell), TargetFlags.Harmful,
		            Reagents.BlackPearl, Reagents.Nightshade
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(42, 0x1B82,6, 1015027, 1061332, "Vas Ort Flam", typeof(Sixth.ExplosionSpell), TargetFlags.Harmful,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(43, 0x1B83,6, 1015205, 1061333, "An Lor Xen", typeof(Sixth.InvisibilitySpell), TargetFlags.Beneficial,
		            Reagents.Bloodmoss, Reagents.Nightshade
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(44, 0x1B84,6, 1015206, 1061334, "Kal Por Ylem", typeof(Sixth.MarkSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(45, 0x1B85,6, 1015207, 1061335, "Vas Des Sanct", typeof(Sixth.MassCurseSpell), TargetFlags.Harmful,
		            Reagents.MandrakeRoot, Reagents.Nightshade, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(46, 0x1B86,6, 1015208, 1061336, "In Ex Grav", typeof(Sixth.ParalyzeFieldSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.Ginseng, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(47, 0x1B87,6, 1015209, 1061337, "Wis Quas", typeof(Sixth.RevealSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.SulfurousAsh
	            ));

            // Sixth circle
            Register(40, typeof(Sixth.DispelSpell));
            Register(41, typeof(Sixth.EnergyBoltSpell));
            Register(42, typeof(Sixth.ExplosionSpell));
            Register(43, typeof(Sixth.InvisibilitySpell));
            Register(44, typeof(Sixth.MarkSpell));
            Register(45, typeof(Sixth.MassCurseSpell));
            Register(46, typeof(Sixth.ParalyzeFieldSpell));
            Register(47, typeof(Sixth.RevealSpell));
            
            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(48, 0x1B88,7, 1015211, 1061338, "Vas Ort Grav", typeof(Seventh.ChainLightningSpell), TargetFlags.Harmful,
		            Reagents.BlackPearl, Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(49, 0x1B89,7, 1015212, 1061339, "In Sanct Grav", typeof(Seventh.EnergyFieldSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.MandrakeRoot, Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(50, 0x1B8A,7, 1015213, 1061340, "Kal Vas Flam", typeof(Seventh.FlameStrikeSpell), TargetFlags.Harmful,
		            Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(51, 0x1B8B,7, 1015214, 1061341, "Vas Rel Por", typeof(Seventh.GateTravelSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(52, 0x1B8C,7, 1015215, 1061342, "Ort Sanct", typeof(Seventh.ManaVampireSpell), TargetFlags.Harmful,
		            Reagents.BlackPearl, Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(53, 0x1B8D,7, 1015216, 1061343, "Vas An Ort", typeof(Seventh.MassDispelSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.Garlic, Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(54, 0x1B8E,7, 1015217, 1061344, "Flam Kal Des Ylem", typeof(Seventh.MeteorSwarmSpell), TargetFlags.Harmful,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(55, 0x1B8F,7, 1015218, 1061345, "Vas Ylem Rel", typeof(Seventh.PolymorphSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));

            // Seventh circle
            Register(48, typeof(Seventh.ChainLightningSpell));
            Register(49, typeof(Seventh.EnergyFieldSpell));
            Register(50, typeof(Seventh.FlameStrikeSpell));
            Register(51, typeof(Seventh.GateTravelSpell));
            Register(52, typeof(Seventh.ManaVampireSpell));
            Register(53, typeof(Seventh.MassDispelSpell));
            Register(54, typeof(Seventh.MeteorSwarmSpell));
            Register(55, typeof(Seventh.PolymorphSpell));
            
            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(56, 0x1B90,8, 1015220, 1061346, "In Vas Por", typeof(Eighth.EarthquakeSpell), TargetFlags.Harmful,
		            Reagents.Bloodmoss, Reagents.Ginseng, Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(57, 0x1B91,8, 1015221, 1061347, "Vas Corp Por", typeof(Eighth.EnergyVortexSpell), TargetFlags.None,
		            Reagents.BlackPearl, Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.Nightshade
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(58, 0x1B92,8, 1015222, 1061348, "An Corp", typeof(Eighth.ResurrectionSpell), TargetFlags.Beneficial,
		            Reagents.Bloodmoss, Reagents.Ginseng, Reagents.Garlic
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(59, 0x1B93,8, 1015223, 1061349, "Kal Vas Xen Hur", typeof(Eighth.AirElementalSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(60, 0x1B94,8, 1015224, 1061350, "Kal Vas Xen Corp", typeof(Eighth.SummonDaemonSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(61, 0x1B95,8, 1015225, 1061351, "Kal Vas Xen Ylem", typeof(Eighth.EarthElementalSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(62, 0x1B96,8, 1015226, 1061352, "Kal Vas Xen Flam", typeof(Eighth.FireElementalSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk, Reagents.SulfurousAsh
	            ));
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(63, 0x1B97,8, 1015227, 1061353, "Kal Vas Xen An Flam", typeof(Eighth.WaterElementalSpell), TargetFlags.None,
		            Reagents.Bloodmoss, Reagents.MandrakeRoot, Reagents.SpidersSilk
	            ));
            
            
            SpellSettings.MagerySpells.Add(
	            new MagerySpellSetting(64, 0x1B75,4, "BIG LIGHTNING", "BIG GO BOOM", "KABOOOOOM!", typeof(Fourth.LightningSpell), TargetFlags.Harmful,
		            Reagents.MandrakeRoot, Reagents.SulfurousAsh
	            ));

            // Eighth circle
            Register(56, typeof(Eighth.EarthquakeSpell));
            Register(57, typeof(Eighth.EnergyVortexSpell));
            Register(58, typeof(Eighth.ResurrectionSpell));
            Register(59, typeof(Eighth.AirElementalSpell));
            Register(60, typeof(Eighth.SummonDaemonSpell));
            Register(61, typeof(Eighth.EarthElementalSpell));
            Register(62, typeof(Eighth.FireElementalSpell));
            Register(63, typeof(Eighth.WaterElementalSpell));
            Register(64, typeof(Fourth.LightningSpell));

            // Necromancy spells
            Register(100, typeof(Necromancy.AnimateDeadSpell));
            Register(101, typeof(Necromancy.BloodOathSpell));
            Register(102, typeof(Necromancy.CorpseSkinSpell));
            Register(103, typeof(Necromancy.CurseWeaponSpell));
            Register(104, typeof(Necromancy.EvilOmenSpell));
            Register(105, typeof(Necromancy.HorrificBeastSpell));
            Register(106, typeof(Necromancy.LichFormSpell));
            Register(107, typeof(Necromancy.MindRotSpell));
            Register(108, typeof(Necromancy.PainSpikeSpell));
            Register(109, typeof(Necromancy.PoisonStrikeSpell));
            Register(110, typeof(Necromancy.StrangleSpell));
            Register(111, typeof(Necromancy.SummonFamiliarSpell));
            Register(112, typeof(Necromancy.VampiricEmbraceSpell));
            Register(113, typeof(Necromancy.VengefulSpiritSpell));
            Register(114, typeof(Necromancy.WitherSpell));
            Register(115, typeof(Necromancy.WraithFormSpell));

            Register(116, typeof(Necromancy.ExorcismSpell));

            // Paladin abilities
            Register(200, typeof(Chivalry.CleanseByFireSpell));
            Register(201, typeof(Chivalry.CloseWoundsSpell));
            Register(202, typeof(Chivalry.ConsecrateWeaponSpell));
            Register(203, typeof(Chivalry.DispelEvilSpell));
            Register(204, typeof(Chivalry.DivineFurySpell));
            Register(205, typeof(Chivalry.EnemyOfOneSpell));
            Register(206, typeof(Chivalry.HolyLightSpell));
            Register(207, typeof(Chivalry.NobleSacrificeSpell));
            Register(208, typeof(Chivalry.RemoveCurseSpell));
            Register(209, typeof(Chivalry.SacredJourneySpell));

            // Samurai abilities
            Register(400, typeof(Bushido.HonorableExecution));
            Register(401, typeof(Bushido.Confidence));
            Register(402, typeof(Bushido.Evasion));
            Register(403, typeof(Bushido.CounterAttack));
            Register(404, typeof(Bushido.LightningStrike));
            Register(405, typeof(Bushido.MomentumStrike));

            // Ninja abilities
            Register(500, typeof(Ninjitsu.FocusAttack));
            Register(501, typeof(Ninjitsu.DeathStrike));
            Register(502, typeof(Ninjitsu.AnimalForm));
            Register(503, typeof(Ninjitsu.KiAttack));
            Register(504, typeof(Ninjitsu.SurpriseAttack));
            Register(505, typeof(Ninjitsu.Backstab));
            Register(506, typeof(Ninjitsu.Shadowjump));
            Register(507, typeof(Ninjitsu.MirrorImage));

            Register(600, typeof(Spellweaving.ArcaneCircleSpell));
            Register(601, typeof(Spellweaving.GiftOfRenewalSpell));
            Register(602, typeof(Spellweaving.ImmolatingWeaponSpell));
            Register(603, typeof(Spellweaving.AttuneWeaponSpell));
            Register(604, typeof(Spellweaving.ThunderstormSpell));
            Register(605, typeof(Spellweaving.NatureFurySpell));
            Register(606, typeof(Spellweaving.SummonFeySpell));
            Register(607, typeof(Spellweaving.SummonFiendSpell));
            Register(608, typeof(Spellweaving.ReaperFormSpell));
            Register(609, typeof(Spellweaving.WildfireSpell));
            Register(610, typeof(Spellweaving.EssenceOfWindSpell));
            Register(611, typeof(Spellweaving.DryadAllureSpell));
            Register(612, typeof(Spellweaving.EtherealVoyageSpell));
            Register(613, typeof(Spellweaving.WordOfDeathSpell));
            Register(614, typeof(Spellweaving.GiftOfLifeSpell));
            Register(615, typeof(Spellweaving.ArcaneEmpowermentSpell));

            Register(677, typeof(Mysticism.NetherBoltSpell));
            Register(678, typeof(Mysticism.HealingStoneSpell));
            Register(679, typeof(Mysticism.PurgeMagicSpell));
            Register(680, typeof(Mysticism.EnchantSpell));
            Register(681, typeof(Mysticism.SleepSpell));
            Register(682, typeof(Mysticism.EagleStrikeSpell));
            Register(683, typeof(Mysticism.AnimatedWeaponSpell));
            Register(684, typeof(Mysticism.StoneFormSpell));
            Register(685, typeof(Mysticism.SpellTriggerSpell));
            Register(686, typeof(Mysticism.MassSleepSpell));
            Register(687, typeof(Mysticism.CleansingWindsSpell));
            Register(688, typeof(Mysticism.BombardSpell));
            Register(689, typeof(Mysticism.SpellPlagueSpell));
            Register(690, typeof(Mysticism.HailStormSpell));
            Register(691, typeof(Mysticism.NetherCycloneSpell));
            Register(692, typeof(Mysticism.RisingColossusSpell));

            Register(700, typeof(SkillMasteries.InspireSpell));
            Register(701, typeof(SkillMasteries.InvigorateSpell));
            Register(702, typeof(SkillMasteries.ResilienceSpell));
            Register(703, typeof(SkillMasteries.PerseveranceSpell));
            Register(704, typeof(SkillMasteries.TribulationSpell));
            Register(705, typeof(SkillMasteries.DespairSpell));

            Register(706, typeof(SkillMasteries.DeathRaySpell));
            Register(707, typeof(SkillMasteries.EtherealBurstSpell));
            Register(708, typeof(SkillMasteries.NetherBlastSpell));
            Register(709, typeof(SkillMasteries.MysticWeaponSpell));
            Register(710, typeof(SkillMasteries.CommandUndeadSpell));
            Register(711, typeof(SkillMasteries.ConduitSpell));
            Register(712, typeof(SkillMasteries.ManaShieldSpell));
            Register(713, typeof(SkillMasteries.SummonReaperSpell));
            Register(714, typeof(SkillMasteries.PassiveMasterySpell));
            Register(715, typeof(SkillMasteries.PassiveMasterySpell));
            Register(716, typeof(SkillMasteries.WarcrySpell));
            Register(717, typeof(SkillMasteries.PassiveMasterySpell));
            Register(718, typeof(SkillMasteries.RejuvinateSpell));
            Register(719, typeof(SkillMasteries.HolyFistSpell));
            Register(720, typeof(SkillMasteries.ShadowSpell));
            Register(721, typeof(SkillMasteries.WhiteTigerFormSpell));
            Register(722, typeof(SkillMasteries.FlamingShotSpell));
            Register(723, typeof(SkillMasteries.PlayingTheOddsSpell));
            Register(724, typeof(SkillMasteries.ThrustSpell));
            Register(725, typeof(SkillMasteries.PierceSpell));
            Register(726, typeof(SkillMasteries.StaggerSpell));
            Register(727, typeof(SkillMasteries.ToughnessSpell));
            Register(728, typeof(SkillMasteries.OnslaughtSpell));
            Register(729, typeof(SkillMasteries.FocusedEyeSpell));
            Register(730, typeof(SkillMasteries.ElementalFurySpell));
            Register(731, typeof(SkillMasteries.CalledShotSpell));
            Register(732, typeof(SkillMasteries.PassiveMasterySpell));
            Register(733, typeof(SkillMasteries.ShieldBashSpell));
            Register(734, typeof(SkillMasteries.BodyGuardSpell));
            Register(735, typeof(SkillMasteries.HeightenedSensesSpell));
            Register(736, typeof(SkillMasteries.ToleranceSpell));
            Register(737, typeof(SkillMasteries.InjectedStrikeSpell));
            Register(738, typeof(SkillMasteries.PassiveMasterySpell));
            Register(739, typeof(SkillMasteries.RampageSpell));
            Register(740, typeof(SkillMasteries.FistsOfFurySpell));
            Register(741, typeof(SkillMasteries.PassiveMasterySpell));
            Register(742, typeof(SkillMasteries.WhisperingSpell));
            Register(743, typeof(SkillMasteries.CombatTrainingSpell));
        }

        public static void Register(int spellID, Type type)
        {
	        SpellSettings.IDToType.Add(spellID, type);
            SpellRegistry.Register(spellID, type);
        }
    }
}
