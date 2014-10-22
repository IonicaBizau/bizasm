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

			if (args.Length < 1) {
				Console.WriteLine ("Missing input file.");
				//Environment.Exit (1);
				return "/home/ionicabizau/Documents/bizasm/Test.asm";
			}

			Uri uri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), args[0]));   
			return Path.GetFullPath(uri.AbsolutePath);
		}

		static string getOutputFilePath (string[] args)
		{
			if (args.Length < 2) {
				Console.WriteLine ("Missing output file.");
				//Environment.Exit (1);
				return "/home/ionicabizau/Documents/bizasm/out";
			}

			Uri uri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), args[1]));   
			return Path.GetFullPath(uri.AbsolutePath);
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
			SourceProgram = input.ReadToEnd();
			input.Close();

			output.Write('B');
			output.Write('I');
			output.Write('Z');
			output.Write(Convert.ToUInt16("0", 16));
			output.Write((ushort)0);
			//Parse(output);
			output.Seek(5, System.IO.SeekOrigin.Begin);
			output.Write(ExecutionAddress);
			output.Close();
			fs.Close();
		}
	}
}
