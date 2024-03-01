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
using Server.OpenUO.Enhancements;
using Server.Spells.Fourth;
using Server.Targeting;

namespace Server.OpenUO
{
	public class ExampleActiveAbilityTwo : IActiveAbility
	{
		public int Row { get; set; }
		public int Slot { get; set; }

		public Mobile Owner { get; set; }
		private AnimTimer m_AnimTimer;

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
		public TimeSpan Cooldown => TimeSpan.FromSeconds(12);

		public int HUDIconLarge => 0x8F2;
		public int HUDIconSmall => 0x8F2;

		public int HUDName => 8010010;
		public int HUDDescription => 8010011;

		public Tuple<int, float>[] HUDArguments
		{
			get
			{
				return new Tuple<int, float>[] { new Tuple<int, float>(0xAABB00, Charge) };
			}
		}

		public int Charge { get; set; } = 0;

		public DateTime NextUse { get; set; }


		public bool Draw => true;

		public TimeSpan CooldownRemaining =>
			TimeSpan.FromSeconds(Math.Max(0, (NextUse - DateTime.UtcNow).TotalSeconds));

		public bool UseNextMove { get; set; } = false;

		public ExampleActiveAbilityTwo(Mobile pm, int row, int slot)
		{
			Owner = pm;
			Row = row;
			Slot = slot;
		}

		public void Toggle(Mobile playerMobile, IEntity owner)
		{
			var now = DateTime.UtcNow;
			playerMobile.Target = new InternalTarget(this);
			Charge = 0;
		}

		public void Target(IPoint3D iP)
		{
			Point3D p = new Point3D(iP);
			if (!Owner.InLOS(p))
				return;

			Owner.RevealingAction();

			m_AnimTimer = new AnimTimer(this, 4);
			m_AnimTimer.Start();

			SetOnCooldown();
			SetInUse(TimeSpan.FromSeconds(4));
			Owner.Frozen = true;
			Timer.DelayCall(TimeSpan.FromSeconds(4), () => CastSpell(p));
		}

		public void CastSpell(Point3D p)
		{
			Owner.Frozen = false;
			m_AnimTimer?.Stop();
			m_AnimTimer = null;
			for (int x = Math.Max(p.X - 3, 0); x < Math.Min(Owner.Map.Width, p.X + 5); x++)
			{
				for (int y = Math.Max(p.Y - 3, 0); y < Math.Min(Owner.Map.Height, p.Y + 5); y++)
				{
					if (Utility.RandomDouble() < 0.6)
					{
						var point = new Point3D(x, y, p.Z);
						Timer.DelayCall(TimeSpan.FromSeconds(Utility.RandomMinMax(1, 100) / 100d), () =>
						{
							var item = new FireFieldSpell.FireFieldItem(0x3709, point, Owner,
								Owner.Map,
								TimeSpan.FromSeconds(Utility.RandomMinMax(4, 7)), 30);
						});

					}
				}
			}
		}


		private class AnimTimer : Timer
		{
			private readonly ExampleActiveAbilityTwo m_Spell;

			public AnimTimer(ExampleActiveAbilityTwo spell, int count)
				: base(TimeSpan.Zero, TimeSpan.FromSeconds(1), count)
			{
				m_Spell = spell;

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Spell.Owner.Animate(AnimationType.Spell, 0);
			}
		}

		private class InternalTarget : Target
		{
			private readonly ExampleActiveAbilityTwo m_Owner;

			public InternalTarget(ExampleActiveAbilityTwo owner)
				: base(15, true, TargetFlags.Harmful)
			{
				m_Owner = owner;
				AreaOfEffectType = 0x0;
				AreaOfEffectRange = 3;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is IPoint3D point3D)
					m_Owner.Target(point3D);
			}
		}
	}

}