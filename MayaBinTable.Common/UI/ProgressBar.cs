namespace MayaBinTable.Common.UI;

// TODO: Fix this
public unsafe class ProgressBar : IUiElement
{
    private double value = 0;
    private short pourcentValueLength = 1;
    private double step;
    private short indexProgress = 0;

    private long* maxValue;
    private long* index;

    public ProgressBar(long* maxValue, long* index)
    {
        step = Math.Floor(*maxValue / 100d);

        this.maxValue = maxValue;
        this.index = index;

        Console.WriteLine(value + "%");
    }

    // TODO: Fix this
    public bool Update()
    {
        int originalRow = Console.CursorTop;
        Console.CursorTop = originalRow - 1;
        Console.CursorLeft = pourcentValueLength;

        if (*index >= *maxValue)
            value = 100;
        else
        {
            value += step;

            if (value % 10 == 0)
            {
                Console.Write("\b");
                if (pourcentValueLength == 1)
                    pourcentValueLength++;
            }
        }

        Console.Write($"\b{Math.Floor(value)}%");
        Console.CursorTop = originalRow;
        Console.CursorLeft = indexProgress;
        indexProgress++;
        Console.Write("=");

        return value == 100;
    }

    // Old Update method (it took many *cpu cycles* (in my mind))

    //  public bool Update()
    // {
    //     int originalRow = Console.CursorTop;
    //     Console.CursorTop = originalRow - 1;
    //     Console.CursorLeft += pourcentValueLength;
    //
    //     double newValue = Math.Ceiling(value + step * 1d);
    //     if (newValue > 100)
    //         newValue = 100;
    //     else
    //     {
    //         double valueTens = Math.Floor(value / 10f);
    //
    //         if (newValue > 10)
    //             if (Math.Floor(newValue / 10f) == valueTens) // Same 10s
    //                 newValue -= valueTens * 10;
    //             else
    //                 Console.Write("\b");
    //     }
    //
    //     Console.Write($"\b\b{newValue}%");
    //     Console.CursorTop = originalRow;
    //     Console.Write("=");
    //
    //     return newValue == 100;
    // }
}