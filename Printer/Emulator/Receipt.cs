using Printer.Rendering;
using SkiaSharp;

namespace Printer.Emulator;

public class Receipt
{
    private readonly PaperConfiguration _paperConfiguration;

    public readonly string Guid;
    
    private int PaperWidth => _paperConfiguration.GetPaperWidthInPixels();
    private int PrintWidth => _paperConfiguration.GetPrintWidthInPixels();
    private int PaperMargins => (PaperWidth - PrintWidth) / 2;

    private PrintMode _printMode;
    private List<IPrintable> _renderLines;
    private ReceiptTextLine? _currentTextLine;
    private int _lineSpacing;
    private int _tabSpacing;

    public bool IsEmpty => (_currentTextLine == null || _currentTextLine.IsEmpty) && _renderLines.Count == 0;

    public Receipt(PaperConfiguration paperConfiguration, PrintMode printMode, int lineSpacing)
    {
        Guid = System.Guid.NewGuid().ToString();
        
        _paperConfiguration = paperConfiguration;

        _printMode = printMode;
        _renderLines = new();
        _currentTextLine = null;
        _lineSpacing = lineSpacing;
        _tabSpacing = paperConfiguration.DefaultTabSpacing;
    }

    public void ChangeFontConfiguration(PrintMode printMode)
    {
        FinalizeTextLine(false);

        _printMode = printMode.Clone();
    }

    public void SetLineSpacing(int value)
    {
        _lineSpacing = value;
    }

    public void SetTabSpacing(int value)
    {
        _tabSpacing = value;
    }

    private ReceiptTextLine CreateNewTextLine() => new(_paperConfiguration, _printMode);
    
    public void PrintText(string text)
    {
        if (_currentTextLine is null)
            _currentTextLine = CreateNewTextLine();

        for (var i = 0; i < text.Length; i++)
        {
            var canContinue = _currentTextLine.TryWriteChar(text[i]);

            if (!canContinue)
            {
                FinalizeTextLine(false);

                _currentTextLine = CreateNewTextLine();
                canContinue = _currentTextLine.TryWriteChar(text[i]);

                if (!canContinue)
                    throw new Exception("Logic error - line must be able to contain > 0 chars");
            }
        }
    }

    public void FinalizeTextLine(bool insertLineSpacing)
    {
        if (_currentTextLine != null)
        {
            if (!_currentTextLine.IsEmpty)
                _renderLines.Add(_currentTextLine);
            _currentTextLine = null;
        }

        if (insertLineSpacing)
        {
            _renderLines.Add(new ReceiptEmptyLine(_lineSpacing));
        }
    }

    public void AdvanceToNewLine() => FinalizeTextLine(true);

    public int GetTotalPrintHeight()
        => _renderLines.Sum(line => line.GetPrintHeight());

    public int GetTotalPaperHeight() =>
        GetTotalPrintHeight() + (PaperMargins * 2);

    public SKData Render(bool drawPartials = true)
    {
        var paperWidth = PaperWidth;
        var paperHeight = GetTotalPaperHeight();
        
        // var bmp = new SKBitmap(paperWidth, paperHeight);
        // SKCanvas canvas = new SKCanvas();
        var info = new SKImageInfo(paperWidth, paperHeight);
        using var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        Console.WriteLine("");
        
        // Draw all rendered lines
        var offsetX = PaperMargins;
        var offsetY = PaperMargins;
    
        foreach (var line in _renderLines)
        {
            line.Render(canvas, offsetX, offsetY);
            offsetY += line.GetPrintHeight();
        }

        var skimage = surface.Snapshot();
        var encoded = skimage.Encode(SKEncodedImageFormat.Png, 100);

        return encoded;
    }
}