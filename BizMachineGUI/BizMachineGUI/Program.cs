using System;
using Gtk;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Interpreter
{
	public static class Interpreter
	{
		public static void ExecuteProgram(string path) {
			BizMachineGUI.MainClass.ExecuteProgram (path);
		}
	}
}


namespace BizMachineGUI
{
	class MainClass
	{
		static MainWindow win;
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
				bmpGraphics.FillRectangle(bgBrush, xLoc, yLoc, 20f, 31f);
				bmpGraphics.DrawString(s, f, fgBrush, pf);
				xLoc += 20;
			}

			string path = "/home/ionicabizau/Documents/bizasm/tmp-" + (++c) + ".png";
			bmp.Save (path, ImageFormat.Png);
			win.SetImageSrc (path);
		}

		private static byte[] BizMemory;
		private static ushort StartAddr;
		private static ushort ExecAddr;
		private static ushort InstructionPointer;
		private static byte Register_A;
		private static byte Register_B;
		private static ushort Register_X;
		private static ushort Register_Y;
		private static ushort Register_D;
		private static void UpdateRegisterStatus()
		{
			string strRegisters = "";
			strRegisters = "Register A = $" + Register_A.ToString("X").PadLeft(2, '0');
			strRegisters += " Register B = $" + Register_B.ToString("X").PadLeft(2, '0');
			strRegisters += " Register D = $" + Register_D.ToString("X").PadLeft(4, '0');
			strRegisters += "\nRegister X = $" + Register_X.ToString("X").PadLeft(4, '0');
			strRegisters += " Register Y = $" + Register_Y.ToString("X").PadLeft(4, '0');
			strRegisters += " Instruction Pointer = $" + InstructionPointer.ToString("X").PadLeft(4, '0');
			win.UpdateRegisterOutput (strRegisters);
		}

		private static void SetRegisterD()
		{
			Register_D = (ushort)(Register_A << 8 + Register_B);
		}

		private static void InterpretProgram(ushort ExecAddr, ushort ProgLength)
		{
			ProgLength = 64000;
			while (ProgLength > 0)
			{
				byte Instruction = BizMemory[InstructionPointer];
				ProgLength--;
				if (Instruction == 0x02) // LDX #<value>
				{
					Register_X = (ushort)((BizMemory[(InstructionPointer +
					                                  2)]) << 8);
					Register_X += BizMemory[(InstructionPointer + 1)];
					ProgLength -= 2;
					InstructionPointer += 3;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x01) // LDA #<value>
				{
					Register_A = BizMemory[(InstructionPointer + 1)];
					SetRegisterD();
					ProgLength -= 1;
					InstructionPointer += 2;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x03) // STA ,X
				{
					BizMemory[Register_X] = Register_A;
					Poke(Register_X, Register_A);
					InstructionPointer++;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x04) // END
				{
					InstructionPointer++;
					UpdateRegisterStatus();
					break;
				}
			}
		}

		public static void ExecuteProgram (string path)
		{

			BizMemory = new byte[65535];
			StartAddr = 0;
			ExecAddr = 0;
			Register_A = 0;
			Register_B = 0;
			Register_D = 0;
			Register_X = 0;
			Register_Y = 0;
			UpdateRegisterStatus();

			ScreenMemoryLocation = 0xA000;
			m_ScreenMemory = new byte[4000];
			for (int i = 0; i < 4000; i += 2)
			{
				m_ScreenMemory[i] = 32;
				m_ScreenMemory[i + 1] = 7;
			}

			BinaryReader br;
			System.IO.FileStream fs = new FileStream(path, System.IO.FileMode.Open);
			br = new System.IO.BinaryReader(fs);
			byte Magic1;
			byte Magic2;
			byte Magic3;

			Magic1 = br.ReadByte();
			Magic2 = br.ReadByte();
			Magic3 = br.ReadByte();

			if (Magic1.ToString() + Magic2.ToString() + Magic3.ToString() != "BIZ")
			{
				MessageBox.Show("This is not a valid biz file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			StartAddr = br.ReadUInt16();
			ExecAddr = br.ReadUInt16();
			ushort Counter = 0;
			while ((br.PeekChar() != -1))
			{
				BizMemory[(StartAddr + Counter)] = br.ReadByte();
				Counter++;
			}
			br.Close();
			fs.Close();

			InstructionPointer = ExecAddr;
			InterpretProgram(ExecAddr, Counter);
		}

		public static void Main (string[] args)
		{
			Gtk.Application.Init ();
			win = new MainWindow ();
			win.Show ();
			Gtk.Application.Run ();
		}
	}
}
