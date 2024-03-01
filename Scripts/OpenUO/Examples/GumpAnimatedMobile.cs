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
using System.Drawing;
using Server.Gumps;
using Server.Mobiles;

namespace Server.OpenUO.Examples
{
	public class GumpAnimatedMobileExample : Gump
    {
	    public static void Initialize()
	    {
		    ExampleGump.ExampleCampaigns.Add(new ExampleCampaign()
		    {
			    Title = 8014000,
			    Description = 8014001,
			    StartDate = new DateTime(2023, 7,3),
			    OnTry = RunExample,
			    IsEnabled = IsEnabled
		    });
	    }

	    public static int IsEnabled()
	    {
		    return 8000001;
	    }

	    public static void RunExample(Mobile m)
	    {
		    m.SendGump(new GumpAnimatedMobileExample(m, 0));
	    }
	    
        private Mobile m_From;
        
        private int m_CurrentHue;
        private int m_BodyValue = 200;
        private int m_HueIndex = 0;
        private int m_BodyIndex = 0;

        private Direction m_Direction;

        private int[] Bodys = new[] {200, 39, 219, 210, 218};
        private int[] Hues = new[] { 32, 0x42 };
        

        public GumpAnimatedMobileExample(Mobile from, int hueIndex) : base(50, 50)
        {
            m_From = from;
            m_HueIndex = hueIndex;
            m_BodyIndex = 0;
            m_Direction = Direction.Left;

            Draw();
        }
        
        public void AddBlackAlpha(int x, int y, int width, int height)
        {
	        AddImageTiled(x, y, width, height, 2624);
	        AddAlphaRegion(x, y, width, height);
        }

        public void Draw()
        {
            Entries.Clear();

            AddPage(0);

            m_BodyValue = Bodys[m_BodyIndex];
            m_CurrentHue = Hues[m_HueIndex];
            

            //AddBackground(0, 0, 420, 440, 5054);

            AddBlackAlpha(0, 0, 380, 300);
            
            
            AddHtmlLocalized(0, 20, 400, 30, 8014002, false, false);

            var animal = new Body(m_BodyValue).IsAnimal;

            if (animal)
            {
                Add(new GumpAnimatedMobile(150, 80, 100, 100, 1f, true,
                    new List<AnimationInfo>()
                    {
                        new AnimationInfo((ushort) m_BodyValue, 1, (byte)m_Direction, (ushort) m_CurrentHue),
                        new AnimationInfo((ushort) m_BodyValue, 2, (byte)m_Direction, (ushort) m_CurrentHue),
                        new AnimationInfo((ushort) m_BodyValue, 3, (byte)m_Direction, (ushort) m_CurrentHue)
                    }
                ));
            }
            else
            {
                Add(new GumpAnimatedMobile(150, 40, 100, 100, 1f, true,
                    new List<AnimationInfo>()
                    {
                        new AnimationInfo((ushort) m_BodyValue, 17, (byte)m_Direction, (ushort) m_CurrentHue),
                        new AnimationInfo((ushort) m_BodyValue, 17, (byte)m_Direction, (ushort) m_CurrentHue),
                        new AnimationInfo((ushort) m_BodyValue, 17, (byte)m_Direction, (ushort) m_CurrentHue),
                        new AnimationInfo((ushort) m_BodyValue, 11, (byte)m_Direction, (ushort) m_CurrentHue),
                        //new AnimationInfo((ushort) m_BodyValue, 15, 2, (ushort) m_CurrentHue),
                        //new AnimationInfo((ushort) m_BodyValue, 16, 2, (ushort) m_CurrentHue),
                        new AnimationInfo((ushort) m_BodyValue, 17, (byte)m_Direction, (ushort) m_CurrentHue),
                        //new AnimationInfo((ushort) m_BodyValue, 2, 2, (ushort) m_CurrentHue),
                    }
                ));
            }

            
            //0x5FB
            
            
            AddButton(310, 100, 0x5FB, 0x5FB, 0x5, GumpButtonType.Reply, 0);


            AddButton(75, 100, 0x9C5A, 0x9C5A, 0x1, GumpButtonType.Reply, 0);
            AddButton(275, 100, 0x9C5B, 0x9C5B, 0x2, GumpButtonType.Reply, 0);
            
	        AddHtmlLocalized(0, 250, 400, 30, 8014003, $"{m_CurrentHue}", 0, false, false);

            AddButton(75, 245, 0x9C5A, 0x9C5A, 0x3, GumpButtonType.Reply, 0);
            AddButton(275, 245, 0x9C5B, 0x9C5B, 0x4, GumpButtonType.Reply, 0);
            
            
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            switch (info.ButtonID)
            {
                case 1:
                {
                    m_BodyIndex--;
                    if (m_BodyIndex < 0)
                        m_BodyIndex = Bodys.Length - 1;
                    break;
                }
                case 2:
                {
                    m_BodyIndex++;
                    if (m_BodyIndex >= Bodys.Length)
                        m_BodyIndex = 0;
                    break;
                }
                case 3:
                {
                    m_HueIndex--;
                    if (m_HueIndex < 0)
                        m_HueIndex = Hues.Length - 1;
                    break;
                }
                case 4:
                {
                    m_HueIndex++;
                    if (m_HueIndex >= Hues.Length)
                        m_HueIndex = 0;
                    break;
                }
                case 5:
                {
                    m_Direction++;
                    if (m_Direction > Direction.Up)
                        m_Direction = 0x0;
                    break;
                }
            }


            if (info.ButtonID < 100 && info.ButtonID != 0)
            {
                Draw();
                m_From.SendGump(this);
            }
        }

        private void SelectionOnCancel(Mobile obj)
        {
            m_From.SendMessage("Cancel");
            Draw();
            m_From.SendGump(this);
        }

        private void CreateItemCancelCallback(GumpButton obj)
        {
            //create tha dye

            Draw();

            m_From.SendGump(this);
        }
    }
}