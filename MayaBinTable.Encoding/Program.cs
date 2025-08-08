using MayaBinTable.Common;

namespace MayaBinTable.Encoding;

internal class Program
{
    const string APPLICATION_NAME = "MayaEncode";

    public static void Main(string[] args)
    {
        (string inputFile, string outputFile) files;

        if (args.Length == 0)
            files = Interactive();
        else
        {
            if (args[0] == "--help")
            {
                Console.WriteLine($"Usage: {APPLICATION_NAME} [inputFile] [outputFile]" +
                                  $"\n{APPLICATION_NAME} (leave empty for interactive mode)");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File not found: {args[0]}");
                return;
            }

            if (File.Exists(args[1]))
            {
                Console.WriteLine($"File already existing: {args[1]}");
                return;
            }

            files.inputFile = args[0];
            files.outputFile = args[1];
        }
        
        File.WriteAllText(files.outputFile, MayaTable.MAGIC_NUMBER);

        var reader = new StreamReader(files.inputFile);
        var writer = new FileStream(files.outputFile, FileMode.Append, FileAccess.Write);
        
        new Encoder().Encode(reader, writer);
        reader.Close();
        writer.Close();
        MayaTable.OffsetStream.Close();
        MayaTable.EntryStream.Close();
    }

    public static (string inputFile, string outputFile) Interactive()
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

        Console.WriteLine("Excellent. Truly excellent. Now we shall begin.");
        
        return (inputPath, outputPath!);
    }
}