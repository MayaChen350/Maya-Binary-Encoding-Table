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
			const string TEST_RESULT_PATH = "EncodingTestsResults\\MatchingTestResult.txt";
			var encoder = new Encoder();
			if (File.Exists(TEST_RESULT_PATH)) File.Delete(TEST_RESULT_PATH);
			FileStream writer = new FileStream(TEST_RESULT_PATH, FileMode.Append, FileAccess.Write);
			var baseString = new LinkedList<char>();
			foreach (char chara in "yyesA")
			{
				baseString.AddLast(chara);
			}
			var currString = "";
			var lastMatch = "";
			encoder.WriteResultEncoding(ref baseString, ref currString, ref lastMatch, writer, true);

			writer.Close();

			// Assertion
			FileStream reader = new FileStream(TEST_RESULT_PATH, FileMode.Open, FileAccess.Read);

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
			const string TEST_RESULT_PATH = "EncodingTestsResults\\NonMatchingTestResult.txt";
			var encoder = new Encoder();
			if (File.Exists(TEST_RESULT_PATH)) File.Delete(TEST_RESULT_PATH);
			FileStream writer = new FileStream(TEST_RESULT_PATH, FileMode.Append, FileAccess.Write);
			var baseString = new LinkedList<char>();
			foreach (char chara in "yedyed")
			{
				baseString.AddLast(chara);
			}
			var currString = "";
			var lastMatch = "";
			encoder.WriteResultEncoding(ref baseString, ref currString, ref lastMatch, writer, true);

			writer.Close();

			// Assertion
			FileStream reader = new FileStream(TEST_RESULT_PATH, FileMode.Open, FileAccess.Read);

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