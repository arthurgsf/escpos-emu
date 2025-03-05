using SkiaSharp;

namespace Printer.Rendering;

public class ReceiptEmptyLine(int height) : IPrintable
{
    public int GetPrintHeight() => height;
    
    public void Render(SKCanvas g, int offsetX, int offsetY) {}
}