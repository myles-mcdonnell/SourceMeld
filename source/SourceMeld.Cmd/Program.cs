using System;
using System.Collections.Generic;
using System.IO;

namespace SourceMeld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length<2)
				throw new Exception("Incorrect number of args.  source meld --help for usage");

			if (args [0].ToUpper()=="--HELP") {
				ShowUsage ();
				return;
			}

			new SourceMeld.Melder (args [0], args [1], args.Length > 2 ? args [2] : "*.cs").Meld ();
		}

		public static void ShowUsage()
		{
			Console.Out.WriteLine("sourcemeld {inputDir:required} {outputFile:required} {filter:optional:default *.cs}"); 
		}
	}
}
