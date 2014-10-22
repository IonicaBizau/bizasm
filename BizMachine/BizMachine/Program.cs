using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace BizMachine
{
	class MainClass
	{
		private static ushort m_ScreenMemoryLocation;
		private static byte[] m_ScreenMemory;
		public static ushort ScreenMemoryLocation
		{
			get
			{
				return m_ScreenMemoryLocation;
			}
			set
			{
				m_ScreenMemoryLocation = value;
			}
		}

		public static void Poke(ushort Address, byte Value)
		{
			ushort MemLoc;
			try
			{
				MemLoc = (ushort)(Address - m_ScreenMemoryLocation);
			}
			catch (Exception)
			{
				return;
			}
			if (MemLoc < 0 || MemLoc > 3999)
				return;
			m_ScreenMemory[MemLoc] = Value;
			B32Screen_Paint ();
		}

		public static byte Peek(ushort Address)
		{
			ushort MemLoc;
			try
			{
				MemLoc = (ushort)(Address - m_ScreenMemoryLocation);
			}
			catch (Exception)
			{
				return (byte)0;
			}
			if (MemLoc < 0 || MemLoc > 3999)
				return (byte)0;
			return m_ScreenMemory[MemLoc];
		}

		static int c = -1;
		private static void B32Screen_Paint()
		{
			Bitmap bmp = new Bitmap(600, 200);
			Graphics bmpGraphics = Graphics.FromImage(bmp);
			Font f = new Font("Monaco", 20f, FontStyle.Bold);
			int xLoc = 0;
			int yLoc = 0;
			for (int i = 0; i < 4000; i += 2)
			{
				SolidBrush bgBrush = null;
				SolidBrush fgBrush = null;
				if ((m_ScreenMemory[i + 1] & 112) == 112)
				{
					bgBrush = new SolidBrush(Color.Gray);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 96)
				{
					bgBrush = new SolidBrush(Color.Brown);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 80)
				{
					bgBrush = new SolidBrush(Color.Magenta);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 64)
				{
					bgBrush = new SolidBrush(Color.Red);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 48)
				{
					bgBrush = new SolidBrush(Color.Cyan);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 32)
				{
					bgBrush = new SolidBrush(Color.Green);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 16)
				{
					bgBrush = new SolidBrush(Color.Blue);
				}
				if ((m_ScreenMemory[i + 1] & 112) == 0)
				{
					bgBrush = new SolidBrush(Color.Black);
				}
				if ((m_ScreenMemory[i + 1] & 7) == 0)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.Gray);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Black);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 1)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.LightBlue);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Blue);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 2)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.LightGreen);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Green);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 3)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.LightCyan);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Cyan);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 4)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.Pink);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Red);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 5)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.Fuchsia);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Magenta);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 6)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.Yellow);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Brown);
					}
				}
				if ((m_ScreenMemory[i + 1] & 7) == 7)
				{
					if ((m_ScreenMemory[i + 1] & 8) == 8)
					{
						fgBrush = new SolidBrush(Color.White);
					}
					else
					{
						fgBrush = new SolidBrush(Color.Gray);
					}
				}
				if (bgBrush == null)
					bgBrush = new SolidBrush(Color.Black);
				if (fgBrush == null)
					fgBrush = new SolidBrush(Color.Gray);
				if (((xLoc % 640) == 0) && (xLoc != 0))
				{
					yLoc += 31;
					xLoc = 0;
				}
				string s = System.Text.Encoding.ASCII.GetString(m_ScreenMemory, i, 1);
				PointF pf = new PointF(xLoc, yLoc);
				bmpGraphics.FillRectangle(bgBrush, xLoc+20, yLoc+20, 8f, 11f);
				bmpGraphics.DrawString(s, f, fgBrush, pf);
				xLoc += 20;
			}

			bmp.Save ("/home/ionicabizau/Documents/bizasm/tmp-" + (++c) + ".png", ImageFormat.Png);
		}

		public static void Main (string[] args)
		{

			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine ("Hello World!");
			ScreenMemoryLocation = 0xA000;
			m_ScreenMemory = new byte[4000];
			for (int i = 0; i < 4000; i += 2)
			{
				m_ScreenMemory[i] = 32;
				m_ScreenMemory[i + 1] = 7;
			}
			Poke(0xa000, 73);
			Poke(0xa002, 111);
			Poke(0xa004, 110);
			Poke(0xa006, 105);
			Poke(0xa008, 99);
			Poke(0xa001, Convert.ToByte("00011111", 2));
			Poke(0xa010, 97);
		}
	}
}
