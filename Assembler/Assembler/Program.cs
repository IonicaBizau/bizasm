using System;
using System.IO;

namespace Assembler
{
	class MainClass
	{
		static string SourceProgram;
		static System.Collections.Hashtable LabelTable;
		static int CurrentNdx;
		static ushort AsLength;
		static bool IsEnd;
		static ushort ExecutionAddress;
		private enum Registers
		{
			Unknown = 0,
			A = 4,
			B = 2,
			D = 1,
			X = 16,
			Y = 8
		}

		static string getInputFilePath (string[] args)
		{

			if (args.Length < 1)
			{
				Console.WriteLine ("Missing input file.");
				//Environment.Exit (1);
				// TODO
				return "/home/ionicabizau/Documents/bizasm/Test.asm";
			}

			Uri uri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), args[0]));   
			return Path.GetFullPath(uri.AbsolutePath);
		}

		static string getOutputFilePath (string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine ("Missing output file.");
				//Environment.Exit (1);
				// TODO
				return "/home/ionicabizau/Documents/bizasm/out";
			}

			Uri uri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), args[1]));   
			return Path.GetFullPath(uri.AbsolutePath);
		}

		private static void DoEnd(System.IO.BinaryWriter OutputFile, bool
		                   IsLabelScan)
		{
			++AsLength;
			if (!IsLabelScan)
			{
				OutputFile.Write((byte)0x04);
			}
		}

		private static void EatWhiteSpaces()
		{
			while (char.IsWhiteSpace(SourceProgram[CurrentNdx]))
			{
				++CurrentNdx;
			}
		}

		private static void InterpretSTA(System.IO.BinaryWriter OutputFile, bool IsLabelScan)
		{
			EatWhiteSpaces();
			if (SourceProgram[CurrentNdx] == ',')
			{
				Registers r;
				byte opcode = 0x00;
				++CurrentNdx;
				EatWhiteSpaces();
				r = ReadRegister();
				switch (r)
				{
					case Registers.X:
					opcode = 0x03;
					break;
				}
				AsLength += 1;
				if (!IsLabelScan)
				{
					OutputFile.Write(opcode);
				}
			}
		}

		private static ushort ReadWordValue()
		{
			ushort val = 0;
			bool IsHex = false;
			string sval = "";
			if (SourceProgram[CurrentNdx] == '$')
			{
				++CurrentNdx;
				IsHex = true;
			}
			while (char.IsLetterOrDigit(SourceProgram[CurrentNdx]))
			{
				sval = sval + SourceProgram[CurrentNdx];
				++CurrentNdx;
			}
			if (IsHex)
			{
				val = Convert.ToUInt16(sval, 16);
			}
			else
			{
				val = ushort.Parse(sval);
			}
			return val;
		}

		private static byte ReadByteValue()
		{
			byte val = 0;
			bool IsHex = false;
			string sval = "";
			if (SourceProgram[CurrentNdx] == '$')
			{
				++CurrentNdx;
				IsHex = true;
			}
			while (char.IsLetterOrDigit(SourceProgram[CurrentNdx]))
			{
				sval = sval + SourceProgram[CurrentNdx];
				++CurrentNdx;
			}
			if (IsHex)
			{
				val = Convert.ToByte(sval, 16);
			}
			else
			{
				val = byte.Parse(sval);
			}
			return val;
		}

		private static string GetLabelName()
		{
			string lblname = "";
			while (char.IsLetterOrDigit(SourceProgram[CurrentNdx]))
			{
				if (SourceProgram[CurrentNdx] == ':')
				{
					++CurrentNdx;
					break;
				}
				lblname = lblname + SourceProgram[CurrentNdx];
				++CurrentNdx;
			}
			return lblname.ToUpper();
		}

		private static Registers ReadRegister()
		{
			Registers r = Registers.Unknown;
			if (SourceProgram [CurrentNdx].ToString ().ToUpper () == "X")
			{
				r = Registers.X;
			} else if (SourceProgram [CurrentNdx].ToString ().ToUpper () == "Y")
			{
				r = Registers.Y;
			} else if (SourceProgram [CurrentNdx].ToString ().ToUpper () == "D")
			{
				r = Registers.D;
			} else if (SourceProgram [CurrentNdx].ToString ().ToUpper () == "A")
			{
				r = Registers.A;
			} else if (SourceProgram [CurrentNdx].ToString ().ToUpper () == "B")
			{
				r = Registers.B;
			}

			++CurrentNdx;
			return r;
		}

		private static void InterpretLDX(System.IO.BinaryWriter OutputFile, bool
		                          IsLabelScan)
		{
			EatWhiteSpaces();
			if (SourceProgram[CurrentNdx] == '#')
			{
				++CurrentNdx;
				ushort val = ReadWordValue();
				AsLength += 3;
				if (!IsLabelScan)
				{
					OutputFile.Write((byte)0x02);
					OutputFile.Write(val);
				}
			}
		}

		private static void InterpretLDA(System.IO.BinaryWriter OutputFile, bool
		                          IsLabelScan)
		{
			EatWhiteSpaces();
			if (SourceProgram[CurrentNdx] == '#')
			{
				++CurrentNdx;
				byte val = ReadByteValue();
				AsLength += 2;
				if (!IsLabelScan)
				{
					OutputFile.Write((byte)0x01);
					OutputFile.Write(val);
				}
			}
		}

		private static void ReadMneumonic(System.IO.BinaryWriter OutputFile, bool
		                           IsLabelScan)
		{
			string Mneumonic = "";
			while (!(char.IsWhiteSpace(SourceProgram[CurrentNdx])))
			{
				Mneumonic = Mneumonic + SourceProgram[CurrentNdx];
				++CurrentNdx;
			}
			if (Mneumonic.ToUpper() == "LDX") InterpretLDX(OutputFile,
			                                               IsLabelScan);
			if (Mneumonic.ToUpper() == "LDA") InterpretLDA(OutputFile,
			                                               IsLabelScan);
			if (Mneumonic.ToUpper() == "STA") InterpretSTA(OutputFile,
			                                               IsLabelScan);
			if (Mneumonic.ToUpper() == "END")
			{
				IsEnd = true;
				DoEnd(OutputFile,IsLabelScan); EatWhiteSpaces(); ExecutionAddress =
					(ushort)LabelTable[(GetLabelName())]; return;
			}
			while (SourceProgram[CurrentNdx] != '\n')
			{
				++CurrentNdx;
			}
			++CurrentNdx;
		}

		private static void LabelScan(System.IO.BinaryWriter OutputFile, bool IsLabelScan)
		{
			if (char.IsLetter(SourceProgram[CurrentNdx]))
			{
				// Must be a label
				if (IsLabelScan) LabelTable.Add(GetLabelName(), AsLength);
				while (SourceProgram[CurrentNdx] != '\n')
					++CurrentNdx;
				++CurrentNdx;
				return;
			}
			EatWhiteSpaces();
			ReadMneumonic(OutputFile, IsLabelScan);
		}

		private static void Parse(System.IO.BinaryWriter OutputFile)
		{
			CurrentNdx = 0;
			while (IsEnd == false)
				LabelScan(OutputFile, true);
			IsEnd = false;
			CurrentNdx = 0;
			AsLength = Convert.ToUInt16("0", 16);
			while (IsEnd == false)
				LabelScan(OutputFile, false);
		}

		public static void Main (string[] args)
		{

			// Read input file
			string inputFilePath = getInputFilePath (args);
			string outputFilePath = getOutputFilePath (args);

			Console.WriteLine ("Input:\n{0}", inputFilePath);

			// Initialize values
			LabelTable = new System.Collections.Hashtable(50);
			CurrentNdx = 0;
			AsLength = 0;
			ExecutionAddress = 0;
			IsEnd = false;
			SourceProgram = "";

			AsLength = Convert.ToUInt16("0", 16);

			System.IO.BinaryWriter output;
			System.IO.TextReader input;
			System.IO.FileStream fs = new System.IO.FileStream(outputFilePath, System.IO.FileMode.Create);
			output = new System.IO.BinaryWriter(fs);
			input = System.IO.File.OpenText(inputFilePath);

			string[] lines = input.ReadToEnd ().Split('\n');
			input.Close();

			for (int i = 0; i < lines.Length; ++i)
			{
				string cLine = lines [i];
				if (cLine.StartsWith("#"))
				{
					continue;
				}
				SourceProgram += cLine + "\n";
			}



			output.Write('B');
			output.Write('I');
			output.Write('Z');
			output.Write(Convert.ToUInt16("0", 16));
			output.Write((ushort)0);
			Parse(output);
			output.Seek(5, System.IO.SeekOrigin.Begin);
			output.Write(ExecutionAddress);
			output.Close();
			fs.Close();
		}
	}
}