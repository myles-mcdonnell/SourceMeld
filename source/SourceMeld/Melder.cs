using System;
using System.IO;
using System.Collections.Generic;

namespace SourceMeld
{
	public class Melder
	{
		private readonly string _input;
		private readonly string _output;
		private readonly string _filter;

		public Melder (string input, string output, string filter = "*.cs")
		{
			_input = input;
			_output = output;
			_filter = filter;
		}

		public void Meld()
		{
			var lines = new LinkedList<string> ();

			foreach (var filePath in Directory.GetFiles(_input, _filter)) {
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

			if (File.Exists(_output))
				File.Delete(_output);

			using (var outputWriter = new StreamWriter (new FileStream(_output, FileMode.Create))) {
				foreach (var line in lines) {
					outputWriter.WriteLine (line);
				}
				outputWriter.Flush ();
			}
		}
	}
}

