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
using System.CodeDom;
using System.Collections.Generic;
using Server.Network;
using Server.OpenUO.Base;

namespace Server.OpenUO
{
	public static class SettingClientOptionsFlags
	{
		/// <summary>
		/// Rolling Text Options showing up in the option menu, with this disabled you will still be able to send rolling text
		/// Clients just won't be able to change rolling text options.
		/// </summary>
		[OptionID(0)]
		public static bool ShowRollingTextOptions => true;
		
		/// <summary>
		/// Friend manager which will prevent targets from being selected as hostile targets
		/// </summary>
		[OptionID(1)]
		public static bool FriendManagerOptions => true;

		/// <summary>
		/// Split Hostile and Beneficial Last Targets Options
		/// </summary>
		[OptionID(2)]
		public static bool AllowSplitTargetsOptions => true;

		/// <summary>
		/// Option for client to enable a pointer showing where their selected mobile (last target / last beneficial target is)
		/// </summary>
		[OptionID(3)]
		public static bool AllowSelectedMobileDisplay => true;

		/// <summary>
		/// Allow Area of Effect Target Options
		/// </summary>
		[OptionID(4)]
		public static bool AreaOfEffectTargetOptions => true;
		
		/// <summary>
		/// Allow offscreen targeting 
		/// </summary>
		[OptionID(5)]
		public static bool AllowOffscreenOptions => true;

		/// <summary>
		/// Allow Players the option to disable Walking Animations
		/// </summary>
		[OptionID(6)]
		public static bool NoWalkAnimationOption => true;
		
		
		/// <summary>
		/// Allow Players to toggle whether they are using fast turning or not.
		/// If this option is disabled players will always turn at the global limit.
		/// </summary>
		[OptionID(7)]
		public static bool FastTurnOption => false;
	}

	public static class SettingsMacrosFlags
	{
		/// <summary>
		/// Sallos Style Targeting
		/// </summary>
		[OptionID(0)]
		public static bool AllowSallosTargeting => true;
		
		/// <summary>
		/// Allow for macros to be set to use potion type on the server side so the server will search for potions.
		/// No longer restricted by having to have a container open, and faster than any string command like [usetype
		/// </summary>
		[OptionID(1)]
		public static bool EnhancedPotionMacros => true;

		/// <summary>
		/// Allow for spell targets to be sent in one packet, bypassing the targeting process
		/// </summary>
		[OptionID(2)]
		public static bool EnhancedSpellMacros => true;
		
		/// <summary>
		/// Allows setting macros for enhanced abilities
		/// </summary>
		[OptionID(3)]
		public static bool EnhancedAbilities => true;
	}

	public static class SettingGeneralFlags
	{
		/// <summary>
		/// Skip the City Select menu when creating a new character
		/// </summary>
		[OptionID(0)]
		public static bool DisableCityOption => true;

		/// <summary>
		/// Disable all race options when creating a new character
		/// </summary>
		[OptionID(1)]
		public static bool DisableRaceOptions => true;
		
		/// <summary>
		/// Cooldowns can be sent from the server to display remaining cooldowns
		/// </summary>
		[OptionID(2)]
		public static bool CooldownGumpEnabled => true;

		/// <summary>
		/// Enhanced buff system which gives serial numbers to buffs rather than using the gumpid to categorize them.
		/// </summary>
		[OptionID(3)]
		public static bool EnhancedBuffInformation => true;

		/// <summary>
		/// Remove the info button from the menu bar.
		/// </summary>
		[OptionID(4)]
		public static bool RemoveInfoFromMenuBar => true;

		/// <summary>
		/// Remove the chat button from the menu bar.
		/// </summary>
		[OptionID(5)]
		public static bool RemoveChatFromMenuBar => true;
		
		/// <summary>
		/// Enables/Disables enhanced abilities from showing up. (Also Enables the reset position in options)
		/// </summary>
		[OptionID(6)]
		public static bool EnableEnhancedAbilities => true;
	}

	public static class MovementSettings
	{
		public static ushort MoveSpeedWalkingUnmounted { get; set; } = (ushort) TimeSpan.FromSeconds(0.4).TotalMilliseconds;
		public static ushort MoveSpeedRunningUnmounted { get; set; } = (ushort) TimeSpan.FromSeconds(0.2).TotalMilliseconds;

		//Default move speed with a mount
		public static ushort MoveSpeedWalkingMounted { get; set; } = (ushort) TimeSpan.FromSeconds( 0.2 ).TotalMilliseconds;
		public static ushort MoveSpeedRunningMounted { get; set; } = (ushort) TimeSpan.FromSeconds( 0.1 ).TotalMilliseconds;

		//Turning delay
		public static ushort TurnDelay => (ushort) TimeSpan.FromMilliseconds(80).TotalMilliseconds;
	}

	public static class PotionSettings
	{
		public static List<PotionSetting> Potions = new List<PotionSetting>();
		public static Dictionary<int, Type> IDToType = new Dictionary<int, Type>();
	}
	
	public static class SpellSettings
	{
		public static List<MagerySpellSetting> MagerySpells = new List<MagerySpellSetting>();
		
		public static Dictionary<int, Type> IDToType = new Dictionary<int, Type>();
	}

	public static class GeneralSettings
	{
		/// <summary>
		/// Store button is overwritten with this URL.
		/// </summary>
		public static string StoreOverride => "https://www.openuo.io/support.php";
	}
	
	public static class Settings
	{
		private static Packet _SettingFlags;
		private static Packet _MovementSpeed;
		private static Packet _PotionPacket;
		private static Packet _GeneralSettings;
		private static Packet _SpellsPacket;
		public static void Initialize()
		{
			_GeneralSettings = new GeneralSettingsPacket();
			_GeneralSettings.SetStatic();
			_SettingFlags = new ServerSettingsPacket();
			_SettingFlags.SetStatic();
			_MovementSpeed = new DefaultMoveSpeedPacket();
			_MovementSpeed.SetStatic();
			
			

			Timer.DelayCall(TimeSpan.Zero, () =>
			{
				_PotionPacket = new PotionSettingsPacket();
				_PotionPacket.SetStatic();
				_SpellsPacket = new MagerySpellSettingsPacket();
				_SpellsPacket.SetStatic();
			});
			
			EventSink.Login += EventSinkOnLogin;
		}

		private static void EventSinkOnLogin(LoginEventArgs e)
		{
			if (e.Mobile.NetState != null && e.Mobile.NetState.OpenUOClient)
			{
				e.Mobile.Send(_SettingFlags);
				e.Mobile.Send(_MovementSpeed);
				e.Mobile.Send(_PotionPacket);
				e.Mobile.Send(_SpellsPacket);
				e.Mobile.Send(_GeneralSettings);
			}

		}
	}
}