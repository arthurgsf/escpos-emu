using System.Drawing;
using Printer.Emulator;
using Printer.Emulator.Enums;
using SkiaSharp;

namespace Printer.Rendering;

public class ReceiptTextLine : IPrintable
{
    private readonly PaperConfiguration.FontConfiguration _font;
    private readonly int _printWidth;
    private readonly int _charWidth;
    private readonly int _charHeight;
    private readonly TextJustification _justification;
    private readonly bool _bold;
    private readonly bool _italic;
    private readonly UnderlineMode _underline;

    private string _text;
    private int _totalWidth;

    public bool IsEmpty => string.IsNullOrEmpty(_text);
    
    public ReceiptTextLine(PaperConfiguration paperConfiguration, PrintMode printMode)
    {
        _font = paperConfiguration.GetFont(printMode.Font);
        _printWidth = paperConfiguration.GetPrintWidthInPixels();
        _charWidth = _font.CharacterWidth * printMode.CharWidthScale;
        _charHeight = _font.CharacterHeight * printMode.CharHeightScale;
        _justification = printMode.Justification;
        _bold = printMode.Emphasize;
        _italic = printMode.Italic;
        _underline = printMode.Underline;

        _text = "";
        _totalWidth = 0;
    }

    public bool TryWriteChar(char c)
    {
        if ((_totalWidth + _charWidth) >= _printWidth)
            return false;

        _text += c;
        _totalWidth += _charWidth;
        return true;
    }

    public int GetPrintHeight()
    {
        return _charHeight;
    }
    
    public void Render(SKCanvas canvas, int offsetX, int offsetY)
    {
        SKTextAlign align = SKTextAlign.Left;

        var slant = _italic ? SKFontStyleSlant.Italic: SKFontStyleSlant.Upright;
        var weight = _bold ? SKFontStyleWeight.Bold : SKFontStyleWeight.Normal;
        var typeface = SKTypeface.FromFamilyName(_font.RenderFont, weight, SKFontStyleWidth.Normal, slant);
        var font = new SKFont(
            typeface,
            _charWidth
        );
        
        var paint = new SKPaint() { Color = SKColors.Black };
        
        
        for (var i = 0; i < _text.Length; i++)
        {
            var c = _text[i];
        
            var rect = new Rectangle(
                x: (offsetX + (_charWidth * i)),
                y: offsetY,
                width: _charWidth,
                height: _charHeight
            );
            
            if (_justification == TextJustification.Center)
            {
                rect.X += (_printWidth / 2) - (_totalWidth / 2);
            }
            else if (_justification == TextJustification.Right)
            {
                rect.X += (_printWidth - _totalWidth);
            }
            
            if (_italic) rect.Width += 10; // Leo@2024.02.19 - Only needed with italic because it continue in the next cell
            canvas.DrawText(c.ToString(), rect.Left, rect.Top + _charWidth, align, font, paint);
            if (_italic) rect.Width -= 10;
            
            if (_underline is UnderlineMode.OnOneDot or UnderlineMode.OnTwoDots)
            {
                var dotHeight = (_underline is UnderlineMode.OnTwoDots ? 2 : 1);
                var pt1 = new SKPoint(rect.Left, rect.Bottom);
                var pt2 = new SKPoint(rect.Right, rect.Bottom);
                using var underlinepaint = new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = dotHeight, // Define a espessura da linha
                    IsAntialias = true, // Para linhas mais suaves
                    Style = SKPaintStyle.Stroke // Garante que desenhe como uma linha
                };
                canvas.DrawLine(pt1, pt2, underlinepaint);
            }
            
        }
    }
}