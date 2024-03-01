using System;
using System.Collections.Generic;
using System.Linq;
using Server.Network;

namespace Server.Gumps
{
	public class AnimationInfo
	{
		public readonly ushort BodyValue;
		public readonly byte AnimationGroup;
		public readonly byte Direction;
		public readonly ushort Hue;

		public AnimationInfo(ushort bodyValue, byte animationGroup, byte direction, ushort hue)
		{
			BodyValue = bodyValue;
			AnimationGroup = animationGroup;
			Direction = direction;
			Hue = hue;
		}
	}

	
	public class GumpAnimatedMobile : GumpEntry
	{
		private int m_X, m_Y;
		private int m_Width, m_Height;
		private int m_Hue;

		private float m_Scale;

		private bool m_Centered;
		private List<AnimationInfo> m_Animations;

		//This is a horse running left - Add(new GumpAnimatedMobile(450, 180, 100, 100, true, 1f, new AnimationInfo(200, 1, 2, (ushort)hueList[i])));

		
		public GumpAnimatedMobile( int x, int y, int width, int height, float scale, bool centered, List<AnimationInfo> animations )
		{
			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
			m_Scale = scale;
			m_Centered = centered;
			m_Animations = animations.ToList();

		}

		public int X
		{
			get
			{
				return m_X;
			}
			set
			{
				Delta( ref m_X, value );
			}
		}

		public int Y
		{
			get
			{
				return m_Y;
			}
			set
			{
				Delta( ref m_Y, value );
			}
		}

		public int Hue
		{
			get
			{
				return m_Hue;
			}
			set
			{
				Delta( ref m_Hue, value );
			}
		}

		public override string Compile()
		{

			string ret = "{{ gumpanimatedmobile ";

			ret += $"{m_X} {m_Y} {m_Width} {m_Height} {m_Scale} {m_Centered} ";

			for (int i = 0; i < m_Animations.Count; i++)
			{
				ret +=
					$"{m_Animations[i].BodyValue} {m_Animations[i].AnimationGroup} {m_Animations[i].Direction} {m_Animations[i].Hue}";
			}
			

			ret += "}}";
			return ret;
		}

		private static byte[] m_LayoutName = Gump.StringToBuffer( "gumpanimatedmobile" );

		public override void AppendTo( IGumpWriter disp )
		{
			disp.AppendLayout( m_LayoutName );
			disp.AppendLayout( m_X );
			disp.AppendLayout( m_Y );
			disp.AppendLayout( m_Width );
			disp.AppendLayout( m_Height );
			disp.AppendLayout( m_Scale.ToString() );
			disp.AppendLayout( m_Centered.ToString() );

			for (int i = 0; i < m_Animations.Count; i++)
			{
				disp.AppendLayout(m_Animations[i].BodyValue);
				disp.AppendLayout(m_Animations[i].AnimationGroup);
				disp.AppendLayout(m_Animations[i].Direction);
				disp.AppendLayout(m_Animations[i].Hue);
			}
		}
	}
}