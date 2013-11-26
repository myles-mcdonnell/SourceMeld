using System;
using System.Collections.Generic;
using System.IO;

namespace SourceMeld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length<2){
				throw new Exception("Incorrect number of args.  source meld --help for usage");
			}

			if (args [0].ToUpper()=="--HELP") {
				ShowUsage ();
				return;
			}

			var input = args [0];
			var output = args [1];
			var filter = args.Length > 2 ? args [2] : "*.cs";

			var lines = new LinkedList<string>();

			foreach (var filePath in Directory.GetFiles(input, filter)) {
				using (var reader = new StreamReader( new FileStream (filePath, FileMode.Open))) {
					while (!reader.EndOfStream) {
						var line = reader.ReadLine ();
						if (line.IndexOf ("using") == 0) {
							if (!lines.Contains (line))
								lines.AddFirst (line);
						} else {
							lines.AddLast (line);
						}
					}
				}
			}

			if (File.Exists (output))
				File.Delete (output);

			using (var outputWriter = new StreamWriter (new FileStream(output, FileMode.Create))){
				foreach (var line in lines) {
					outputWriter.WriteLine (line);
				}
				outputWriter.Flush ();
			}
		}

		public static void ShowUsage()
		{
			Console.Out.WriteLine("sourcemeld {inputDir:required} {outputFile:required} {filter:optional:default *.cs}"); 
		}
	}
}
