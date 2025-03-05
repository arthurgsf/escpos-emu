using SkiaSharp;

namespace Printer.Rendering;

public interface IPrintable
{
    public void Render(SKCanvas g, int offsetX, int offsetY);
    public int GetPrintHeight();
}