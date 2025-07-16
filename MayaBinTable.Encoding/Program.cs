using MayaBinTable.Common;

namespace MayaBinTable.Encoding;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Write the path of the file you want to use.");
        string? inputPath = Console.ReadLine();

        while (!File.Exists(inputPath))
        {
            Console.WriteLine("This path is invalid. Write another.");
            inputPath = Console.ReadLine();
        }

        Console.WriteLine("Now, write the name of the output file.");
        string? outputPath = Console.ReadLine();

        while (File.Exists(outputPath))
        {
            Console.WriteLine("This name is owned. Please write one which has yet to be.");
            outputPath = Console.ReadLine();
        }

        File.WriteAllText(outputPath!, MayaTable.MAGIC_NUMBER);

        Console.WriteLine("Excellent. Now we shall begin.");
        var reader = new StreamReader(inputPath);
        var writer = new FileStream(outputPath!, FileMode.Append, FileAccess.Write);

        new Encoder().Encode(reader, writer);
        reader.Close();
        writer.Close();
        MayaTable.OffsetStream.Close();
        MayaTable.EntryStream.Close();
    }
}