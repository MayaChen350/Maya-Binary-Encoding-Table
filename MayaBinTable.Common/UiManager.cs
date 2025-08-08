using System.Runtime.CompilerServices;
using MayaBinTable.Common.UI;

namespace MayaBinTable.Common;

public static class UiManager
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CreateMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static unsafe void CreateProgressBar(long* maxValue, long* index)
    {
        uiElement = new ProgressBar(maxValue, index);
    }
    
    static IUiElement? uiElement;

    public static void UpdateUi()
    {
        bool completed = uiElement!.Update();
        
        if (completed) uiElement = null;
    }
}