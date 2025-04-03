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

			const string PATH = "C:\\Users\\Mayachen\\Downloads\\test_result.txt";
			var reader = new StreamReader(filePath);
			if (File.Exists(PATH)) File.Delete(PATH);
			var writer = new FileStream(PATH, FileMode.Append, FileAccess.Write);

			new Encoder().Encode(reader, writer);
			reader.Close();
			writer.Close();
		}
	}
}
