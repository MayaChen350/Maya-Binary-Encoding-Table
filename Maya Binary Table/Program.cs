using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MayaBinaryTable
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

			byte[] encodedString = new Encoder(new MayaTable()).Encode(reader);
			File.WriteAllBytes("C:\\Users\\Mayachen\\Downloads\\test_result.txt", encodedString);
		}
	}
}
