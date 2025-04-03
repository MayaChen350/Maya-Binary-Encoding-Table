using Microsoft.VisualStudio.TestTools.UnitTesting;
using MayaBinaryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable.Tests
{
	[TestClass()]
	public class EncoderTests
	{
		[TestMethod()]
		public void EncodeTest()
		{
			MatchingStringTest();
		}

		private void MatchingStringTest()
		{
			var encoder = new Encoder();
		}

		private void NonMatchingStringTest()
		{

		}

		private void WithFileTest()
		{

		}

		private void FileNowEmptyTest()
		{

		}
	}
}