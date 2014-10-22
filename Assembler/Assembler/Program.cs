using System;
using System.IO;

namespace Assembler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (!Convert.ToBoolean(args.Length)) {
				Console.WriteLine ("Missing input file.");
				Environment.Exit (1);
			}

			Uri uri = new Uri(Path.Combine(Directory.GetCurrentDirectory(), args[0]));   


			string pathToInputFile = Path.GetFullPath(uri.AbsolutePath);
			Console.WriteLine ("Hello World: {0}", pathToInputFile);
		}
	}
}
