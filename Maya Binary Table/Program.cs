using System;
using System.Collections.Generic;
using System.IO;

namespace Maya_Binary_Table
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Write the path of the file you want to use.");
			string filePath = Console.ReadLine();

			while (!File.Exists(filePath))
			{
				Console.WriteLine("The path is invalid. Write another.");
				filePath = Console.ReadLine();
			}

			var reader = new StreamReader(filePath);
			byte[] encodedString = EncodeToMayaEncoding(reader);
			File.WriteAllBytes("C:\\Users\\Mayachen\\Downloads\\test_result.txt", encodedString);
		}

		static byte[] EncodeToMayaEncoding(StreamReader file)
		{
			List<byte> encodedString = new List<byte>();
			while (!file.EndOfStream)
			{
				if (file.Read() == 'a')
					encodedString.Add(1);
			}

			return encodedString.ToArray();
		}
	}
}
