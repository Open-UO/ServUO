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
using Server.Items;
using Server.OpenUO.Enhancements;

namespace Server.OpenUO
{
	public class ExampleActiveAbility : IActiveAbility
	{
		public int Row { get; set; }
        public int Slot { get; set; }
        
        public Mobile Owner { get; set; }

        public TimeSpan InUseDuration { get; set; }
        public DateTime InUseUntil { get; set; }

        public void SetInUse(TimeSpan duration)
        {
            if (Owner?.NetState == null)
                return;
            InUseDuration = duration;
            InUseUntil = DateTime.UtcNow + duration;

            if (Owner.NetState.OpenUOClient && SettingGeneralFlags.EnableEnhancedAbilities)
            {
                Owner.Send(new ActiveAbilityUpdatePacket(this));
            }
        }

        public void SetOnCooldown()
        {
            if (Owner?.NetState == null)
                return;
            
            NextUse = DateTime.UtcNow + Cooldown;
            if (Owner.NetState.OpenUOClient && SettingGeneralFlags.EnableEnhancedAbilities)
            {
                Owner.Send(new ActiveAbilityUpdatePacket(this));
            }
        }
        
        
        public int HUDHue { get; } = 0;
        public TimeSpan Cooldown => TimeSpan.FromSeconds(3);
        
        public int HUDIconLarge => 0x5206;
        public int HUDIconSmall => 0x5206;

        public int HUDName => 8010000;
        public int HUDDescription => 8010001;

        public Tuple<int, float>[] HUDArguments
        {
	        get
	        {
		        return new Tuple<int, float>[] { new Tuple<int, float>(0xAABB00, 1) };
	        }
        }
        
        public int Charge { get; set; } = 0;
        
        public DateTime NextUse { get; set; }


        public bool Draw => true;
        public TimeSpan CooldownRemaining => TimeSpan.FromSeconds(Math.Max(0, (NextUse - DateTime.UtcNow).TotalSeconds));

        public bool UseNextMove { get; set; } = false;
        
        public ExampleActiveAbility(Mobile pm, int row, int slot)
        {
            Owner = pm;
            Row = row;
            Slot = slot;
        }

        public void Toggle(Mobile playerMobile, IEntity owner)
        {
            var now = DateTime.UtcNow;

            if (Charge > 0 && playerMobile.Weapon is BaseWeapon wep && playerMobile.Combatant != null && playerMobile.InRange(playerMobile.Combatant,wep.MaxRange))
            {
	            for (int i = 0; i < Charge; i++)
	            {
		            wep.OnHit(playerMobile, playerMobile.Combatant);   
	            }
	            Charge = 0;
	            SetOnCooldown();
            }
        }
        
	}
}