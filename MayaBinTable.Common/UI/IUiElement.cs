namespace MayaBinTable.Common.UI;

public interface IUiElement
{
    /// <summary>
    /// Update the current Ui Element.
    /// </summary>
    /// <returns>Return true if completed.</returns>
    public bool Update();
}