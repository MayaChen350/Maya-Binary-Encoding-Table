using Microsoft.VisualStudio.TestTools.UnitTesting;
using MayaBinaryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MayaBinaryTable.Tests
{
	[TestClass()]
	public class EncoderTests
	{
		[TestMethod()]
		public void EncodeTest()
		{
			MatchingStringTest();
			NonMatchingStringTest();
		}

		private void MatchingStringTest()
		{
			// Preparation
			var encoder = new Encoder();
			FileStream writer = new FileStream("EncodingTestsResults\\MatchingTestResult.txt", FileMode.Append, FileAccess.Write);
			var baseString = new LinkedList<Char>();
			foreach (char chara in "yyesA")
			{
				baseString.AddLast(chara);
			}
			var currString = "";
			var lastMatch = "";
			encoder.WriteResultEncoding(ref baseString, ref currString, ref lastMatch, writer, true);

			writer.Close();

			// Assertion
			FileStream reader = new FileStream("EncodingTestsResults\\MatchingTestResult.txt", FileMode.Open, FileAccess.Read);

			foreach (var expected in new byte[] { 49, 98, 128, 149 })
			{
				byte actual = (byte)reader.ReadByte();
				if (actual != expected)
					Assert.Fail($"{expected} was expected but {actual} was found.");
			}

			var remaininingByte = reader.ReadByte();
			if (remaininingByte != -1)
			{
				Console.WriteLine(remaininingByte);
				Assert.Fail("Some characters were remaining in the reader.");
			}

			reader.Close();
		}

		private void NonMatchingStringTest()
		{
			// Preparation
			var encoder = new Encoder();
			FileStream writer = new FileStream("EncodingTestsResults\\NonMatchingTestResult.txt", FileMode.Append, FileAccess.Write);
			var baseString = new LinkedList<Char>();
			foreach (char chara in "yedyed")
			{
				baseString.AddLast(chara);
			}
			var currString = "";
			var lastMatch = "";
			encoder.WriteResultEncoding(ref baseString, ref currString, ref lastMatch, writer);

			writer.Close();

			// Assertion
			FileStream reader = new FileStream("EncodingTestsResults\\NonMatchingTestResult.txt", FileMode.Open, FileAccess.Read);

			foreach (var expected in new byte[] { 49, 9, 7, 49, 9, 7 })
			{
				byte actual = (byte)reader.ReadByte();
				if (actual != expected)
					Assert.Fail($"{expected} was expected but {actual} was found.");
			}

			if (reader.ReadByte() != -1)
				Assert.Fail("Some characters were remaining in the reader.");

			reader.Close();
		}

		private void WithFileTest()
		{

		}

		private void FileNowEmptyTest()
		{

		}
	}
}