using System;
using System.IO;

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
			var writer = new FileStream("C:\\Users\\Mayachen\\Downloads\\test_result.txt", FileMode.Append, FileAccess.Write);

			new Encoder().Encode(reader, writer);
			reader.Close();
			writer.Close();
		}
	}
}
