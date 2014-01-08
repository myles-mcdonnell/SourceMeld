using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using FluentAssertions;

namespace SourceMeld.UnitTests
{
	[TestFixture]
	public class MelderTests
	{
		[Test]
		public void Meld()
		{
			new SourceMeld.Melder (".", "Melded.rst", "*.tst").Meld();

			string line;
			List<string> lines = new List<string> ();

			using (var reader = File.OpenText ("Melded.rst")) {
				while (!string.IsNullOrEmpty(line = reader.ReadLine()))
					lines.Add (line);
			}
		
			lines.Count.Should ().Be (2);
			lines [0].Should().Be ("one");
			lines [1].Should().Be ("two");
		}
	}
}

